using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace KI_DataInterface
{
    public partial class Frm_DbViewer : Form
    {
        private readonly string _dbPath;
        private readonly Logger _logger;

        public Frm_DbViewer(string dbPath, Logger logger)
        {
            InitializeComponent();
            _dbPath = dbPath;
            _logger = logger;
        }

        private void Frm_DbViewer_Load(object sender, EventArgs e)
        {
            // DateTimePicker 초기화
            dtpStartDate.Format = DateTimePickerFormat.Short;
            dtpEndDate.Format = DateTimePickerFormat.Short;
            dtpEndDate.Value = DateTime.Now;
            dtpStartDate.Value = DateTime.Now.AddDays(-7); // 기본: 최근 7일

            LoadData();
        }

        private void LoadData(string dateFilter = null)
        {
            try
            {
                if (!File.Exists(_dbPath))
                {
                    MessageBox.Show($"데이터베이스 파일을 찾을 수 없습니다: {_dbPath}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 깨진 텍스트 처리를 위해 FailIfMissing=False 추가
                string connStr = $"Data Source={_dbPath};Version=3;FailIfMissing=False;";
                using (var conn = new SQLiteConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT * FROM tMeasurement";

                    if (!string.IsNullOrEmpty(dateFilter))
                    {
                        query += $" WHERE {dateFilter}";
                    }

                    query += " ORDER BY cKey ASC";

                    using (var adapter = new SQLiteDataAdapter(query, conn))
                    {
                        var dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dgvData.DataSource = dataTable;

                        lblRecordCount.Text = $"총 레코드 수: {dataTable.Rows.Count}";
                        _logger?.Log($"Loaded {dataTable.Rows.Count} records from Measurement_Test.db");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"데이터 로드 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger?.Log($"[ERROR] Failed to load data in DbViewer: {ex.Message}");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string startDate = dtpStartDate.Value.ToString("yyyy_MM_dd");
                string endDate = dtpEndDate.Value.ToString("yyyy_MM_dd");

                string dateFilter = $"cDate >= '{startDate}' AND cDate <= '{endDate}'";

                LoadData(dateFilter);
                _logger?.Log($"Search by date range: {startDate} ~ {endDate}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"검색 중 오류 발생: {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger?.Log($"[ERROR] Search failed: {ex.Message}");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadData(); // 전체 데이터 다시 로드
            _logger?.Log("Data refreshed - showing all records");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
