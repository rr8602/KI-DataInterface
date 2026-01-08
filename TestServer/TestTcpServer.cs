using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TestServer
{
    class TestTcpServer
    {
        private static TcpListener listener;
        private static bool isRunning = false;

        static void Main(string[] args)
        {
            Console.WriteLine("=== TCP Test Server ===");
            Console.WriteLine("Press Ctrl+C to stop server");
            Console.WriteLine();

            // 기본 포트: 8300
            int port = 8300;
            if (args.Length > 0)
            {
                int.TryParse(args[0], out port);
            }

            StartServer(port);
        }

        static void StartServer(int port)
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                isRunning = true;

                Console.WriteLine($"Server started on port {port}");
                Console.WriteLine("Waiting for connections...");
                Console.WriteLine();

                while (isRunning)
                {
                    if (listener.Pending())
                    {
                        TcpClient client = listener.AcceptTcpClient();
                        Thread clientThread = new Thread(new ParameterizedThreadStart(HandleClient));
                        clientThread.Start(client);
                    }
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Server error: {ex.Message}");
            }
            finally
            {
                listener?.Stop();
                Console.WriteLine("Server stopped.");
            }
        }

        static void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream stream = null;

            try
            {
                string clientEndpoint = client.Client.RemoteEndPoint.ToString();
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Client connected: {clientEndpoint}");

                stream = client.GetStream();
                byte[] buffer = new byte[4096];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);

                if (bytesRead > 0)
                {
                    string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Received ({bytesRead} bytes):");
                    Console.WriteLine($"  Data: {receivedData}");
                    Console.WriteLine();

                    // 데이터 파싱 (간단하게)
                    string response = GenerateResponse(receivedData);

                    // 응답 전송
                    byte[] responseBytes = Encoding.ASCII.GetBytes(response);
                    stream.Write(responseBytes, 0, responseBytes.Length);

                    Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Response sent ({responseBytes.Length} bytes):");
                    Console.WriteLine($"  Data: {response}");
                    Console.WriteLine(new string('-', 60));
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Client handling error: {ex.Message}");
            }
            finally
            {
                stream?.Close();
                client?.Close();
            }
        }

        static string GenerateResponse(string receivedData)
        {
            // 수신 데이터에서 헤더 정보 추출
            if (string.IsNullOrEmpty(receivedData) || receivedData.Length < 45)
            {
                return "ERROR;";
            }

            int pos = 0;
            string company = Extract(receivedData, ref pos, 2);
            string plant = Extract(receivedData, ref pos, 4);
            string line = Extract(receivedData, ref pos, 1);
            string shop = Extract(receivedData, ref pos, 2);
            string machineCode = Extract(receivedData, ref pos, 4);
            string number = Extract(receivedData, ref pos, 3);
            string seq = Extract(receivedData, ref pos, 4);
            string vehicle = Extract(receivedData, ref pos, 4);
            string bodyNo = Extract(receivedData, ref pos, 6);
            string checkDay = Extract(receivedData, ref pos, 8);
            string checkTime = Extract(receivedData, ref pos, 6);
            string cycleTime = Extract(receivedData, ref pos, 6);
            string totalResult = Extract(receivedData, ref pos, 2).Trim();

            // Receive_Result 결정 (Total_result가 "OK"면 "P", "NG"면 "F")
            string receiveResult = (totalResult.ToUpper() == "OK") ? "P" : "F";

            // 응답 메시지 생성
            StringBuilder response = new StringBuilder();
            response.Append(PadRight(company, 2));
            response.Append(PadRight(plant, 4));
            response.Append(PadRight(line, 1));
            response.Append(PadRight(shop, 2));
            response.Append(PadRight(machineCode, 4));
            response.Append(PadRight(number, 3));
            response.Append(PadRight(seq, 4));
            response.Append(PadRight(vehicle, 4));
            response.Append(PadRight(bodyNo, 6));
            response.Append(PadRight(checkDay, 8));
            response.Append(PadRight(checkTime, 6));
            response.Append(receiveResult);
            response.Append(";");

            return response.ToString();
        }

        static string Extract(string data, ref int position, int length)
        {
            if (position + length > data.Length)
                return new string(' ', length);

            string result = data.Substring(position, length);
            position += length;
            return result;
        }

        static string PadRight(string value, int length)
        {
            if (string.IsNullOrEmpty(value))
                return new string(' ', length);

            return value.Length >= length
                ? value.Substring(0, length)
                : value.PadRight(length);
        }
    }
}
