using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace KI_DataInterface
{
    public class Connection
    {
        public string ServerIp { get; set; }
        public int Port { get; set; }
        private readonly Logger _logger;
        private const int MaxRetries = 3;
        private const int TimeoutMs = 5000;

        public Connection(Logger logger)
        {
            _logger = logger;
        }

        public void UpdateConnection(string ip, int port)
        {
            ServerIp = ip;
            Port = port;
            _logger.Log($"Connection info updated: IP = {ip}, Port = {port}");
        }

        public bool TestConnection()
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.ReceiveTimeout = TimeoutMs;
                    client.SendTimeout = TimeoutMs;

                    _logger.Log($"Connecting to {ServerIp}:{Port}...");
                    client.Connect(ServerIp, Port);

                    if (client.Connected)
                    {
                        _logger.Log("Connection successful!");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Log($"[ERROR] Connection test failed: {ex.Message}");
            }
            return false;
        }

        public ReceiveMessage SendMeasurementData(SendMessage message)
        {
            if (string.IsNullOrEmpty(ServerIp) || Port == 0)
            {
                _logger.Log("[ERROR] Server IP or Port is not configured!");
                return null;
            }

            string dataToSend = message.ToProtocolString();
            int attempt = 0;

            while (attempt < MaxRetries)
            {
                attempt++;
                _logger.Log($"[Attempt {attempt}/{MaxRetries}] Sending data to {ServerIp}:{Port}");

                try
                {
                    using (TcpClient client = new TcpClient())
                    {
                        client.ReceiveTimeout = TimeoutMs;
                        client.SendTimeout = TimeoutMs;

                        // 연결
                        client.Connect(ServerIp, Port);
                        NetworkStream stream = client.GetStream();

                        // 데이터 전송
                        byte[] sendBytes = Encoding.ASCII.GetBytes(dataToSend);
                        stream.Write(sendBytes, 0, sendBytes.Length);
                        _logger.Log($"Data sent ({sendBytes.Length} bytes): {dataToSend.Substring(0, Math.Min(50, dataToSend.Length))}...");

                        // 응답 수신
                        byte[] receiveBuffer = new byte[1024];
                        int bytesRead = stream.Read(receiveBuffer, 0, receiveBuffer.Length);

                        if (bytesRead > 0)
                        {
                            string response = Encoding.ASCII.GetString(receiveBuffer, 0, bytesRead);
                            _logger.Log($"Response received ({bytesRead} bytes): {response}");

                            // 응답 파싱
                            ReceiveMessage receiveMsg = ReceiveMessage.Parse(response);
                            if (receiveMsg != null)
                            {
                                _logger.Log($"Communication successful! Receive_Result: {receiveMsg.ReceiveResult}");
                                return receiveMsg;
                            }
                            else
                            {
                                _logger.Log("[ERROR] Failed to parse response");
                            }
                        }
                        else
                        {
                            _logger.Log("[ERROR] No response received from server");
                        }
                    }
                }
                catch (SocketException ex)
                {
                    _logger.Log($"[ERROR] Socket error (Attempt {attempt}): {ex.Message}");
                }
                catch (Exception ex)
                {
                    _logger.Log($"[ERROR] Communication error (Attempt {attempt}): {ex.Message}");
                }

                // 재전송 대기
                if (attempt < MaxRetries)
                {
                    _logger.Log($"Retrying in 1 second...");
                    Thread.Sleep(1000);
                }
            }

            _logger.Log($"[ERROR] Failed to communicate after {MaxRetries} attempts");
            return null;
        }
    }
}