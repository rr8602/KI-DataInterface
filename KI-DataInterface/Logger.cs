using System;
using System.IO;
using System.Windows.Forms;

namespace KI_DataInterface
{
    public class Logger
    {
        private RichTextBox _logBox;
        private readonly string _logFilePath;

        public Logger(string logDirectory)
        {
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }
            _logFilePath = Path.Combine(logDirectory, $"log_{DateTime.Now:yyyyMMdd}.txt");
        }

        public void SetLogBox(RichTextBox logBox)
        {
            _logBox = logBox;
        }

        public void Log(string message)
        {
            string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";

            // UI에 로그 출력
            if (_logBox != null && _logBox.IsHandleCreated)
            {
                _logBox.Invoke((MethodInvoker)delegate {
                    _logBox.AppendText(logMessage + Environment.NewLine);
                    _logBox.ScrollToCaret();
                });
            }

            // 파일에 로그 기록
            try
            {
                File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);
            }
            catch (Exception)
            {
                // 파일 쓰기 오류는 무시
            }
        }
    }
}