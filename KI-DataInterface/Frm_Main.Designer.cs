namespace KI_DataInterface
{
    partial class Frm_Main
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grpSettings = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.lblIp = new System.Windows.Forms.Label();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.lblNumber = new System.Windows.Forms.Label();
            this.txtMachineCode = new System.Windows.Forms.TextBox();
            this.lblMachineCode = new System.Windows.Forms.Label();
            this.txtShop = new System.Windows.Forms.TextBox();
            this.lblShop = new System.Windows.Forms.Label();
            this.txtLine = new System.Windows.Forms.TextBox();
            this.lblLine = new System.Windows.Forms.Label();
            this.txtPlant = new System.Windows.Forms.TextBox();
            this.lblPlant = new System.Windows.Forms.Label();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this.lblCompany = new System.Windows.Forms.Label();
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.btnManualCopy = new System.Windows.Forms.Button();
            this.btnViewData = new System.Windows.Forms.Button();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.btnSendData = new System.Windows.Forms.Button();
            this.timerDbCopy = new System.Windows.Forms.Timer(this.components);
            this.lbl_tLocSrDb = new System.Windows.Forms.Label();
            this.lbl_LocSrDb = new System.Windows.Forms.Label();
            this.grpSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSettings
            // 
            this.grpSettings.Controls.Add(this.lbl_LocSrDb);
            this.grpSettings.Controls.Add(this.lbl_tLocSrDb);
            this.grpSettings.Controls.Add(this.btnSave);
            this.grpSettings.Controls.Add(this.txtPort);
            this.grpSettings.Controls.Add(this.lblPort);
            this.grpSettings.Controls.Add(this.txtIp);
            this.grpSettings.Controls.Add(this.lblIp);
            this.grpSettings.Controls.Add(this.txtNumber);
            this.grpSettings.Controls.Add(this.lblNumber);
            this.grpSettings.Controls.Add(this.txtMachineCode);
            this.grpSettings.Controls.Add(this.lblMachineCode);
            this.grpSettings.Controls.Add(this.txtShop);
            this.grpSettings.Controls.Add(this.lblShop);
            this.grpSettings.Controls.Add(this.txtLine);
            this.grpSettings.Controls.Add(this.lblLine);
            this.grpSettings.Controls.Add(this.txtPlant);
            this.grpSettings.Controls.Add(this.lblPlant);
            this.grpSettings.Controls.Add(this.txtCompany);
            this.grpSettings.Controls.Add(this.lblCompany);
            this.grpSettings.Location = new System.Drawing.Point(12, 12);
            this.grpSettings.Name = "grpSettings";
            this.grpSettings.Size = new System.Drawing.Size(776, 237);
            this.grpSettings.TabIndex = 0;
            this.grpSettings.TabStop = false;
            this.grpSettings.Text = "Settings";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(580, 150);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(190, 40);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "서버 연결 및 설정 저장";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(334, 26);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(150, 25);
            this.txtPort.TabIndex = 15;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(281, 29);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(47, 15);
            this.lblPort.TabIndex = 14;
            this.lblPort.Text = "PORT";
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(76, 26);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(150, 25);
            this.txtIp.TabIndex = 13;
            // 
            // lblIp
            // 
            this.lblIp.AutoSize = true;
            this.lblIp.Location = new System.Drawing.Point(6, 31);
            this.lblIp.Name = "lblIp";
            this.lblIp.Size = new System.Drawing.Size(67, 15);
            this.lblIp.TabIndex = 12;
            this.lblIp.Text = "Server IP";
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(615, 65);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(150, 25);
            this.txtNumber.TabIndex = 11;
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Location = new System.Drawing.Point(545, 70);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(55, 15);
            this.lblNumber.TabIndex = 10;
            this.lblNumber.Text = "Number";
            // 
            // txtMachineCode
            // 
            this.txtMachineCode.Location = new System.Drawing.Point(616, 108);
            this.txtMachineCode.Name = "txtMachineCode";
            this.txtMachineCode.Size = new System.Drawing.Size(150, 25);
            this.txtMachineCode.TabIndex = 9;
            // 
            // lblMachineCode
            // 
            this.lblMachineCode.AutoSize = true;
            this.lblMachineCode.Location = new System.Drawing.Point(510, 113);
            this.lblMachineCode.Name = "lblMachineCode";
            this.lblMachineCode.Size = new System.Drawing.Size(103, 15);
            this.lblMachineCode.TabIndex = 8;
            this.lblMachineCode.Text = "Machine_code";
            // 
            // txtShop
            // 
            this.txtShop.Location = new System.Drawing.Point(335, 109);
            this.txtShop.Name = "txtShop";
            this.txtShop.Size = new System.Drawing.Size(150, 25);
            this.txtShop.TabIndex = 7;
            // 
            // lblShop
            // 
            this.lblShop.AutoSize = true;
            this.lblShop.Location = new System.Drawing.Point(290, 112);
            this.lblShop.Name = "lblShop";
            this.lblShop.Size = new System.Drawing.Size(42, 15);
            this.lblShop.TabIndex = 6;
            this.lblShop.Text = "Shop";
            // 
            // txtLine
            // 
            this.txtLine.Location = new System.Drawing.Point(335, 69);
            this.txtLine.Name = "txtLine";
            this.txtLine.Size = new System.Drawing.Size(150, 25);
            this.txtLine.TabIndex = 5;
            // 
            // lblLine
            // 
            this.lblLine.AutoSize = true;
            this.lblLine.Location = new System.Drawing.Point(295, 72);
            this.lblLine.Name = "lblLine";
            this.lblLine.Size = new System.Drawing.Size(34, 15);
            this.lblLine.TabIndex = 4;
            this.lblLine.Text = "Line";
            // 
            // txtPlant
            // 
            this.txtPlant.Location = new System.Drawing.Point(76, 107);
            this.txtPlant.Name = "txtPlant";
            this.txtPlant.Size = new System.Drawing.Size(150, 25);
            this.txtPlant.TabIndex = 3;
            // 
            // lblPlant
            // 
            this.lblPlant.AutoSize = true;
            this.lblPlant.Location = new System.Drawing.Point(6, 112);
            this.lblPlant.Name = "lblPlant";
            this.lblPlant.Size = new System.Drawing.Size(40, 15);
            this.lblPlant.TabIndex = 2;
            this.lblPlant.Text = "Plant";
            // 
            // txtCompany
            // 
            this.txtCompany.Location = new System.Drawing.Point(76, 67);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(150, 25);
            this.txtCompany.TabIndex = 1;
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Location = new System.Drawing.Point(6, 72);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(69, 15);
            this.lblCompany.TabIndex = 0;
            this.lblCompany.Text = "Company";
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(12, 310);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(776, 300);
            this.rtbLog.TabIndex = 1;
            this.rtbLog.Text = "";
            // 
            // btnManualCopy
            // 
            this.btnManualCopy.Location = new System.Drawing.Point(12, 255);
            this.btnManualCopy.Name = "btnManualCopy";
            this.btnManualCopy.Size = new System.Drawing.Size(150, 40);
            this.btnManualCopy.TabIndex = 2;
            this.btnManualCopy.Text = "수동 복사 실행";
            this.btnManualCopy.UseVisualStyleBackColor = true;
            this.btnManualCopy.Click += new System.EventHandler(this.btnManualCopy_Click);
            // 
            // btnViewData
            // 
            this.btnViewData.Location = new System.Drawing.Point(180, 255);
            this.btnViewData.Name = "btnViewData";
            this.btnViewData.Size = new System.Drawing.Size(150, 40);
            this.btnViewData.TabIndex = 3;
            this.btnViewData.Text = "데이터 보기";
            this.btnViewData.UseVisualStyleBackColor = true;
            this.btnViewData.Click += new System.EventHandler(this.btnViewData_Click);
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(348, 255);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(150, 40);
            this.btnTestConnection.TabIndex = 4;
            this.btnTestConnection.Text = "연결 테스트";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // btnSendData
            // 
            this.btnSendData.Location = new System.Drawing.Point(516, 255);
            this.btnSendData.Name = "btnSendData";
            this.btnSendData.Size = new System.Drawing.Size(150, 40);
            this.btnSendData.TabIndex = 5;
            this.btnSendData.Text = "데이터 전송 테스트";
            this.btnSendData.UseVisualStyleBackColor = true;
            this.btnSendData.Click += new System.EventHandler(this.btnSendData_Click);
            // 
            // timerDbCopy
            // 
            this.timerDbCopy.Enabled = true;
            this.timerDbCopy.Interval = 1000;
            this.timerDbCopy.Tick += new System.EventHandler(this.timerDbCopy_Tick);
            // 
            // lbl_tLocSrDb
            // 
            this.lbl_tLocSrDb.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_tLocSrDb.ForeColor = System.Drawing.Color.DarkCyan;
            this.lbl_tLocSrDb.Location = new System.Drawing.Point(6, 202);
            this.lbl_tLocSrDb.Name = "lbl_tLocSrDb";
            this.lbl_tLocSrDb.Size = new System.Drawing.Size(109, 21);
            this.lbl_tLocSrDb.TabIndex = 17;
            this.lbl_tLocSrDb.Text = "원본 DB 위치 : ";
            // 
            // lbl_LocSrDb
            // 
            this.lbl_LocSrDb.AutoSize = true;
            this.lbl_LocSrDb.Location = new System.Drawing.Point(121, 202);
            this.lbl_LocSrDb.Name = "lbl_LocSrDb";
            this.lbl_LocSrDb.Size = new System.Drawing.Size(0, 15);
            this.lbl_LocSrDb.TabIndex = 18;
            // 
            // Frm_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 622);
            this.Controls.Add(this.btnSendData);
            this.Controls.Add(this.btnTestConnection);
            this.Controls.Add(this.btnViewData);
            this.Controls.Add(this.btnManualCopy);
            this.Controls.Add(this.rtbLog);
            this.Controls.Add(this.grpSettings);
            this.Name = "Frm_Main";
            this.Text = "KI-DataInterface";
            this.Load += new System.EventHandler(this.Frm_Main_Load);
            this.grpSettings.ResumeLayout(false);
            this.grpSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSettings;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Label lblIp;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.TextBox txtMachineCode;
        private System.Windows.Forms.Label lblMachineCode;
        private System.Windows.Forms.TextBox txtShop;
        private System.Windows.Forms.Label lblShop;
        private System.Windows.Forms.TextBox txtLine;
        private System.Windows.Forms.Label lblLine;
        private System.Windows.Forms.TextBox txtPlant;
        private System.Windows.Forms.Label lblPlant;
        private System.Windows.Forms.TextBox txtCompany;
        private System.Windows.Forms.Label lblCompany;
        private System.Windows.Forms.RichTextBox rtbLog;
        private System.Windows.Forms.Button btnManualCopy;
        private System.Windows.Forms.Button btnViewData;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Button btnSendData;
        private System.Windows.Forms.Timer timerDbCopy;
        private System.Windows.Forms.Label lbl_LocSrDb;
        private System.Windows.Forms.Label lbl_tLocSrDb;
    }
}