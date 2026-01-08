using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace KI_DataInterface
{
    public class Db
    {
        private readonly string _sourceConnStr;
        private readonly string _destConnStr;
        private readonly Logger _logger;
        private readonly ReceiveMessage _recvMsg;

        public Db(string dbFolderPath, Logger logger)
        {
            _logger = logger;
            _recvMsg = new ReceiveMessage();
            string sourceDbPath = Path.Combine(dbFolderPath, "Measurement.db");
            string destDbPath = Path.Combine(dbFolderPath, "Measurement_Test.db");

            _sourceConnStr = $"Data Source={sourceDbPath};Version=3;";
            _destConnStr = $"Data Source={destDbPath};Version=3;";

            InitializeDatabase(sourceDbPath, destDbPath);
        }

        private void InitializeDatabase(string sourceDbPath, string destDbPath)
        {
            try
            {
                if (!File.Exists(sourceDbPath))
                {
                    _logger.Log($"[ERROR] Source database not found: {sourceDbPath}");
                    return;
                }

                if (!File.Exists(destDbPath))
                {
                    _logger.Log($"Destination database not found. Creating new one: {destDbPath}");
                    SQLiteConnection.CreateFile(destDbPath);
                }

                using (var sourceConn = new SQLiteConnection(_sourceConnStr))
                using (var destConn = new SQLiteConnection(_destConnStr))
                {
                    sourceConn.Open();
                    destConn.Open();

                    // tMeasurement 테이블 구조를 소스 DB에서 가져와 대상 DB에 생성
                    var schemaQuery = "SELECT sql FROM sqlite_master WHERE name='tMeasurement'";
                    using (var cmd = new SQLiteCommand(schemaQuery, sourceConn))
                    {
                        var schema = cmd.ExecuteScalar()?.ToString();
                        if (!string.IsNullOrEmpty(schema))
                        {
                            var tableExistsQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='tMeasurement'";
                            using (var checkCmd = new SQLiteCommand(tableExistsQuery, destConn))
                            {
                                if (checkCmd.ExecuteScalar() == null)
                                {
                                    using (var createCmd = new SQLiteCommand(schema, destConn))
                                    {
                                        createCmd.ExecuteNonQuery();
                                        _logger.Log("Created 'tMeasurement' table in destination database.");
                                    }
                                }
                            }
                        }
                        else
                        {
                            _logger.Log("[ERROR] 'tMeasurement' table not found in source database.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Log($"[ERROR] Database initialization failed: {ex.Message}");
            }
        }

        public void CopyNewMeasurements()
        {
            string selectQuery = "SELECT * FROM tMeasurement WHERE cNotUsed IS NULL OR cNotUsed = '' OR cNotUsed = 0";
            var recordsToCopy = new List<DataRow>();
            DataTable schemaTable = null;

            try
            {
                using (var sourceConn = new SQLiteConnection(_sourceConnStr))
                {
                    sourceConn.Open();
                    using (var adapter = new SQLiteDataAdapter(selectQuery, sourceConn))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        schemaTable = dataTable.Clone();
                        foreach (DataRow row in dataTable.Rows)
                        {
                            recordsToCopy.Add(row);
                        }
                    }
                }

                if (recordsToCopy.Count == 0) return;

                _logger.Log($"Found {recordsToCopy.Count} new records to copy.");

                using (var destConn = new SQLiteConnection(_destConnStr))
                {
                    destConn.Open();
                    foreach (var row in recordsToCopy)
                    {
                        long recordId = Convert.ToInt64(row["cKey"]);
                        string chassisNo = row["cChassisNo"]?.ToString() ?? "N/A";
                        bool success = false;
                        try
                        {
                            using (var transaction = destConn.BeginTransaction())
                            {
                                var columns = new List<string>();
                                var values = new List<string>();
                                var parameters = new List<SQLiteParameter>();

                                foreach (DataColumn col in schemaTable.Columns)
                                {
                                    columns.Add($"\"{col.ColumnName}\"");
                                    values.Add($"@{col.ColumnName}");
                                    parameters.Add(new SQLiteParameter($"@{col.ColumnName}", row[col.ColumnName]));
                                }

                                string insertQuery = $"INSERT INTO tMeasurement ({string.Join(", ", columns)}) VALUES ({string.Join(", ", values)})";
                                using (var cmd = new SQLiteCommand(insertQuery, destConn))
                                {
                                    cmd.Parameters.AddRange(parameters.ToArray());
                                    cmd.ExecuteNonQuery();
                                }
                                transaction.Commit();
                                success = true;
                                _logger.Log($"Successfully copied record - cKey: {recordId}, cChassisNo: {chassisNo}");
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.Log($"[ERROR] Failed to copy record - cKey: {recordId}, cChassisNo: {chassisNo}. Reason: {ex.Message}");
                            success = false;
                        }

                        UpdateStatusFromServerResponse(recordId, ReceiveMessage.msg.ReceiveResult, chassisNo);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Log($"[ERROR] An error occurred during the copy process: {ex.Message}");
            }
        }

        private void UpdateSourceRecordStatus(long recordId, bool success, string chassisNo)
        {
            string updateQuery = $"UPDATE tMeasurement SET cNotUsed = @Status WHERE cKey = @cKey";
            try
            {
                using (var sourceConn = new SQLiteConnection(_sourceConnStr))
                {
                    sourceConn.Open();
                    using (var cmd = new SQLiteCommand(updateQuery, sourceConn))
                    {
                        cmd.Parameters.AddWithValue("@Status", success ? "True" : "False");
                        cmd.Parameters.AddWithValue("@cKey", recordId);
                        cmd.ExecuteNonQuery();
                    }
                }
                _logger.Log($"Updated status to '{(success ? "True" : "False")}' - cKey: {recordId}, cChassisNo: {chassisNo}");
            }
            catch (Exception ex)
            {
                _logger.Log($"[ERROR] Failed to update status - cKey: {recordId}, cChassisNo: {chassisNo}. Reason: {ex.Message}");
            }
        }

        public void UpdateStatusFromServerResponse(long recordId, string receiveResult, string chassisNo)
        {
            if (receiveResult == null) return;

            string updateQuery = $"UPDATE tMeasurement SET cNotUsed = @Status WHERE cKey = @cKey";

            // Receive_Result가 "P"면 True, "F"면 False
            string status = receiveResult.Trim().ToUpper() == "P" ? "True" : "False";

            try
            {
                using (var sourceConn = new SQLiteConnection(_sourceConnStr))
                {
                    sourceConn.Open();
                    using (var cmd = new SQLiteCommand(updateQuery, sourceConn))
                    {
                        cmd.Parameters.AddWithValue("@Status", status);
                        cmd.Parameters.AddWithValue("@cKey", recordId);
                        cmd.ExecuteNonQuery();
                    }
                }
                _logger.Log($"Server response processed - cKey: {recordId}, cChassisNo: {chassisNo}, Receive_Result: {receiveResult}, Status updated to: {status}");
            }
            catch (Exception ex)
            {
                _logger.Log($"[ERROR] Failed to update status from server response - cKey: {recordId}, cChassisNo: {chassisNo}. Reason: {ex.Message}");
            }
        }
    }
}