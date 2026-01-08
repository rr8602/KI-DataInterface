using System;
using System.IO;
using System.Windows.Forms;

namespace KI_DataInterface
{
    public partial class Frm_Main : Form
    {
        private Logger _logger;
        private IniFile _iniFile;
        private Db _db;
        private Connection _connection;

        private const string IniSection = "Settings";
        private readonly string _iniFilePath = Path.Combine(Application.StartupPath, "config.ini");
        private readonly string _dbFolderPath = Path.Combine(Application.StartupPath, "DB");
        private readonly string _logFolderPath = Path.Combine(Application.StartupPath, "logs");

        public Frm_Main()
        {
            InitializeComponent();
        }

        private void Frm_Main_Load(object sender, EventArgs e)
        {
            // 1. Logger 초기화
            _logger = new Logger(_logFolderPath);
            _logger.SetLogBox(rtbLog);
            _logger.Log("Application starting...");

            // 2. IniFile 초기화 및 설정 로드
            _iniFile = new IniFile(_iniFilePath);
            LoadSettings();
            _logger.Log("Settings loaded.");

            // 3. Connection 클래스 초기화
            _connection = new Connection(_logger);
            UpdateConnectionInfo();

            // 4. Db 클래스 초기화
            _db = new Db(_dbFolderPath, _logger);
            _logger.Log("Database module initialized.");

            // 5. 타이머 시작
            timerDbCopy.Start();
            _logger.Log("Automatic DB copy timer started (1-second interval).");
        }

        private void LoadSettings()
        {
            txtCompany.Text = _iniFile.Read(IniSection, "Company");
            txtPlant.Text = _iniFile.Read(IniSection, "Plant");
            txtLine.Text = _iniFile.Read(IniSection, "Line");
            txtShop.Text = _iniFile.Read(IniSection, "Shop");
            txtMachineCode.Text = _iniFile.Read(IniSection, "MachineCode");
            txtNumber.Text = _iniFile.Read(IniSection, "Number");
            txtIp.Text = _iniFile.Read(IniSection, "ServerIP");
            txtPort.Text = _iniFile.Read(IniSection, "Port");
        }

        private void SaveSettings()
        {
            _iniFile.Write(IniSection, "Company", txtCompany.Text);
            _iniFile.Write(IniSection, "Plant", txtPlant.Text);
            _iniFile.Write(IniSection, "Line", txtLine.Text);
            _iniFile.Write(IniSection, "Shop", txtShop.Text);
            _iniFile.Write(IniSection, "MachineCode", txtMachineCode.Text);
            _iniFile.Write(IniSection, "Number", txtNumber.Text);
            _iniFile.Write(IniSection, "ServerIP", txtIp.Text);
            _iniFile.Write(IniSection, "Port", txtPort.Text);
            _logger.Log("Settings saved to config.ini.");
        }

        private void UpdateConnectionInfo()
        {
            int.TryParse(txtPort.Text, out int port);
            _connection.UpdateConnection(txtIp.Text, port);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveSettings();
            UpdateConnectionInfo(); // 서버 연결
            MessageBox.Show("설정이 저장되었습니다.", "저장 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnManualCopy_Click(object sender, EventArgs e)
        {
            _logger.Log("Manual copy process triggered by user.");
            _db.CopyNewMeasurements();
        }

        private void btnViewData_Click(object sender, EventArgs e)
        {
            string dbPath = Path.Combine(_dbFolderPath, "Measurement_Test.db");
            var viewerForm = new Frm_DbViewer(dbPath, _logger);
            viewerForm.ShowDialog();
        }

        private void timerDbCopy_Tick(object sender, EventArgs e)
        {
            timerDbCopy.Stop();

            try
            {
                _db.CopyNewMeasurements();
            }
            catch (Exception ex)
            {
                _logger.Log($"[ERROR] Timer tick error: {ex.Message}");
            }
            finally
            {
                timerDbCopy.Start();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            timerDbCopy.Stop();
            _logger.Log("Application closing...");
            base.OnFormClosing(e);
        }
    }
}