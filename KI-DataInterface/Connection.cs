namespace KI_DataInterface
{
    public class Connection
    {
        public string ServerIp { get; set; }
        public int Port { get; set; }
        private readonly Logger _logger;

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

        // 추후 서버 연결/통신 관련 코드를 여기에 구현할 수 있습니다.
        // public bool Connect() { ... }
        // public bool SendData(string data) { ... }
    }
}