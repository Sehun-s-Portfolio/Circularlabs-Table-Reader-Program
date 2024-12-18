namespace RFIDTest
{
    partial class FormTagAccess
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTagAccess));
            this.label1 = new System.Windows.Forms.Label();
            this.txtTagId = new System.Windows.Forms.TextBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.gbWriteEPC = new System.Windows.Forms.GroupBox();
            this.txtWriteEpcData = new System.Windows.Forms.TextBox();
            this.btnWrite = new System.Windows.Forms.Button();
            this.gbReadUserData = new System.Windows.Forms.GroupBox();
            this.numReadUserLen = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.numReadUserPtr = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.txtReaduser = new System.Windows.Forms.TextBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.gbWriteUserData = new System.Windows.Forms.GroupBox();
            this.txtWriteUser = new System.Windows.Forms.TextBox();
            this.numWriteUserPtr = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.gbLockTag = new System.Windows.Forms.GroupBox();
            this.cbLockType = new System.Windows.Forms.ComboBox();
            this.cbLockMemory = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnLock = new System.Windows.Forms.Button();
            this.gbChangePwd = new System.Windows.Forms.GroupBox();
            this.txtKillPwd_Change = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtAccessPwd = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.gbKillTag = new System.Windows.Forms.GroupBox();
            this.txtKillPwd = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.btnKillTag = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtReadEpc = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtReadTid = new System.Windows.Forms.TextBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtReadKPwd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReadAPwd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ckbReadEpc = new System.Windows.Forms.CheckBox();
            this.ckbReadTid = new System.Windows.Forms.CheckBox();
            this.ckbReadUser = new System.Windows.Forms.CheckBox();
            this.ckbReadResv = new System.Windows.Forms.CheckBox();
            this.ckbWriteEpc = new System.Windows.Forms.CheckBox();
            this.ckbWriteUser = new System.Windows.Forms.CheckBox();
            this.ckbWriteResv = new System.Windows.Forms.CheckBox();
            this.gbWriteEPC.SuspendLayout();
            this.gbReadUserData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numReadUserLen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReadUserPtr)).BeginInit();
            this.gbWriteUserData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWriteUserPtr)).BeginInit();
            this.gbLockTag.SuspendLayout();
            this.gbChangePwd.SuspendLayout();
            this.gbKillTag.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "操作标签：";
            // 
            // txtTagId
            // 
            this.txtTagId.Location = new System.Drawing.Point(83, 12);
            this.txtTagId.Name = "txtTagId";
            this.txtTagId.ReadOnly = true;
            this.txtTagId.Size = new System.Drawing.Size(716, 21);
            this.txtTagId.TabIndex = 1;
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(881, 12);
            this.txtPwd.MaxLength = 8;
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(91, 21);
            this.txtPwd.TabIndex = 3;
            this.txtPwd.Text = "00000000";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(806, 16);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 1;
            this.label9.Text = "访问密码：";
            // 
            // gbWriteEPC
            // 
            this.gbWriteEPC.Controls.Add(this.txtWriteEpcData);
            this.gbWriteEPC.Location = new System.Drawing.Point(7, 21);
            this.gbWriteEPC.Margin = new System.Windows.Forms.Padding(4);
            this.gbWriteEPC.Name = "gbWriteEPC";
            this.gbWriteEPC.Padding = new System.Windows.Forms.Padding(4);
            this.gbWriteEPC.Size = new System.Drawing.Size(178, 129);
            this.gbWriteEPC.TabIndex = 0;
            this.gbWriteEPC.TabStop = false;
            this.gbWriteEPC.Text = "写EPC：";
            // 
            // txtWriteEpcData
            // 
            this.txtWriteEpcData.Location = new System.Drawing.Point(7, 25);
            this.txtWriteEpcData.Multiline = true;
            this.txtWriteEpcData.Name = "txtWriteEpcData";
            this.txtWriteEpcData.Size = new System.Drawing.Size(164, 95);
            this.txtWriteEpcData.TabIndex = 0;
            this.txtWriteEpcData.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.txtWriteEpcData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            this.txtWriteEpcData.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(562, 157);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(75, 23);
            this.btnWrite.TabIndex = 1;
            this.btnWrite.Text = "写入";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // gbReadUserData
            // 
            this.gbReadUserData.Controls.Add(this.numReadUserLen);
            this.gbReadUserData.Controls.Add(this.label14);
            this.gbReadUserData.Controls.Add(this.numReadUserPtr);
            this.gbReadUserData.Controls.Add(this.label13);
            this.gbReadUserData.Controls.Add(this.txtReaduser);
            this.gbReadUserData.Location = new System.Drawing.Point(437, 21);
            this.gbReadUserData.Margin = new System.Windows.Forms.Padding(4);
            this.gbReadUserData.Name = "gbReadUserData";
            this.gbReadUserData.Padding = new System.Windows.Forms.Padding(4);
            this.gbReadUserData.Size = new System.Drawing.Size(335, 129);
            this.gbReadUserData.TabIndex = 0;
            this.gbReadUserData.TabStop = false;
            this.gbReadUserData.Text = "读用户区：";
            // 
            // numReadUserLen
            // 
            this.numReadUserLen.Location = new System.Drawing.Point(180, 98);
            this.numReadUserLen.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.numReadUserLen.Name = "numReadUserLen";
            this.numReadUserLen.Size = new System.Drawing.Size(46, 21);
            this.numReadUserLen.TabIndex = 3;
            this.numReadUserLen.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(122, 103);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 2;
            this.label14.Text = "长度：";
            // 
            // numReadUserPtr
            // 
            this.numReadUserPtr.Location = new System.Drawing.Point(64, 98);
            this.numReadUserPtr.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numReadUserPtr.Name = "numReadUserPtr";
            this.numReadUserPtr.Size = new System.Drawing.Size(46, 21);
            this.numReadUserPtr.TabIndex = 3;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 103);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 2;
            this.label13.Text = "起始地址：";
            // 
            // txtReaduser
            // 
            this.txtReaduser.Location = new System.Drawing.Point(7, 25);
            this.txtReaduser.Multiline = true;
            this.txtReaduser.Name = "txtReaduser";
            this.txtReaduser.Size = new System.Drawing.Size(322, 68);
            this.txtReaduser.TabIndex = 0;
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(883, 157);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 1;
            this.btnRead.Text = "读取";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // gbWriteUserData
            // 
            this.gbWriteUserData.Controls.Add(this.txtWriteUser);
            this.gbWriteUserData.Controls.Add(this.numWriteUserPtr);
            this.gbWriteUserData.Controls.Add(this.label15);
            this.gbWriteUserData.Location = new System.Drawing.Point(193, 21);
            this.gbWriteUserData.Margin = new System.Windows.Forms.Padding(4);
            this.gbWriteUserData.Name = "gbWriteUserData";
            this.gbWriteUserData.Padding = new System.Windows.Forms.Padding(4);
            this.gbWriteUserData.Size = new System.Drawing.Size(335, 129);
            this.gbWriteUserData.TabIndex = 0;
            this.gbWriteUserData.TabStop = false;
            this.gbWriteUserData.Text = "写用户区：";
            // 
            // txtWriteUser
            // 
            this.txtWriteUser.Location = new System.Drawing.Point(7, 25);
            this.txtWriteUser.Multiline = true;
            this.txtWriteUser.Name = "txtWriteUser";
            this.txtWriteUser.Size = new System.Drawing.Size(322, 68);
            this.txtWriteUser.TabIndex = 0;
            this.txtWriteUser.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.txtWriteUser.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            this.txtWriteUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // numWriteUserPtr
            // 
            this.numWriteUserPtr.Location = new System.Drawing.Point(66, 99);
            this.numWriteUserPtr.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.numWriteUserPtr.Name = "numWriteUserPtr";
            this.numWriteUserPtr.Size = new System.Drawing.Size(46, 21);
            this.numWriteUserPtr.TabIndex = 3;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 104);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 2;
            this.label15.Text = "起始地址：";
            // 
            // gbLockTag
            // 
            this.gbLockTag.Controls.Add(this.cbLockType);
            this.gbLockTag.Controls.Add(this.cbLockMemory);
            this.gbLockTag.Controls.Add(this.label11);
            this.gbLockTag.Controls.Add(this.label10);
            this.gbLockTag.Controls.Add(this.btnLock);
            this.gbLockTag.Location = new System.Drawing.Point(672, 236);
            this.gbLockTag.Margin = new System.Windows.Forms.Padding(4);
            this.gbLockTag.Name = "gbLockTag";
            this.gbLockTag.Padding = new System.Windows.Forms.Padding(4);
            this.gbLockTag.Size = new System.Drawing.Size(178, 187);
            this.gbLockTag.TabIndex = 0;
            this.gbLockTag.TabStop = false;
            this.gbLockTag.Text = "锁标签：";
            // 
            // cbLockType
            // 
            this.cbLockType.AutoCompleteCustomSource.AddRange(new string[] {
            "解锁",
            "锁定",
            "永久解锁",
            "永久锁定"});
            this.cbLockType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLockType.FormattingEnabled = true;
            this.cbLockType.Items.AddRange(new object[] {
            "锁定",
            "解锁"});
            this.cbLockType.Location = new System.Drawing.Point(52, 60);
            this.cbLockType.Margin = new System.Windows.Forms.Padding(4);
            this.cbLockType.Name = "cbLockType";
            this.cbLockType.Size = new System.Drawing.Size(118, 20);
            this.cbLockType.TabIndex = 11;
            // 
            // cbLockMemory
            // 
            this.cbLockMemory.AutoCompleteCustomSource.AddRange(new string[] {
            "灭活密码区",
            "访问密码区",
            "EPC区",
            "TID区",
            "用户数据区"});
            this.cbLockMemory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLockMemory.FormattingEnabled = true;
            this.cbLockMemory.Items.AddRange(new object[] {
            "所有数据区",
            "TID区",
            "EPC区",
            "用户数据区",
            "访问密码区",
            "销毁密码区"});
            this.cbLockMemory.Location = new System.Drawing.Point(52, 26);
            this.cbLockMemory.Margin = new System.Windows.Forms.Padding(4);
            this.cbLockMemory.Name = "cbLockMemory";
            this.cbLockMemory.Size = new System.Drawing.Size(118, 20);
            this.cbLockMemory.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(5, 63);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 4;
            this.label11.Text = "操作：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(5, 29);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 4;
            this.label10.Text = "区域：";
            // 
            // btnLock
            // 
            this.btnLock.Location = new System.Drawing.Point(95, 156);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(75, 23);
            this.btnLock.TabIndex = 1;
            this.btnLock.Text = "确定";
            this.btnLock.UseVisualStyleBackColor = true;
            this.btnLock.Click += new System.EventHandler(this.btnLock_Click);
            // 
            // gbChangePwd
            // 
            this.gbChangePwd.Controls.Add(this.txtKillPwd_Change);
            this.gbChangePwd.Controls.Add(this.label12);
            this.gbChangePwd.Controls.Add(this.txtAccessPwd);
            this.gbChangePwd.Controls.Add(this.label16);
            this.gbChangePwd.Location = new System.Drawing.Point(536, 21);
            this.gbChangePwd.Margin = new System.Windows.Forms.Padding(4);
            this.gbChangePwd.Name = "gbChangePwd";
            this.gbChangePwd.Padding = new System.Windows.Forms.Padding(4);
            this.gbChangePwd.Size = new System.Drawing.Size(110, 129);
            this.gbChangePwd.TabIndex = 0;
            this.gbChangePwd.TabStop = false;
            this.gbChangePwd.Text = "修改密码：";
            // 
            // txtKillPwd_Change
            // 
            this.txtKillPwd_Change.Location = new System.Drawing.Point(10, 88);
            this.txtKillPwd_Change.MaxLength = 8;
            this.txtKillPwd_Change.Name = "txtKillPwd_Change";
            this.txtKillPwd_Change.PasswordChar = '*';
            this.txtKillPwd_Change.Size = new System.Drawing.Size(91, 21);
            this.txtKillPwd_Change.TabIndex = 5;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(8, 73);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 4;
            this.label12.Text = "销毁密码：";
            // 
            // txtAccessPwd
            // 
            this.txtAccessPwd.Location = new System.Drawing.Point(10, 44);
            this.txtAccessPwd.MaxLength = 8;
            this.txtAccessPwd.Name = "txtAccessPwd";
            this.txtAccessPwd.PasswordChar = '*';
            this.txtAccessPwd.Size = new System.Drawing.Size(91, 21);
            this.txtAccessPwd.TabIndex = 5;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(8, 29);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 4;
            this.label16.Text = "访问密码：";
            // 
            // gbKillTag
            // 
            this.gbKillTag.Controls.Add(this.txtKillPwd);
            this.gbKillTag.Controls.Add(this.label17);
            this.gbKillTag.Controls.Add(this.btnKillTag);
            this.gbKillTag.Location = new System.Drawing.Point(858, 236);
            this.gbKillTag.Margin = new System.Windows.Forms.Padding(4);
            this.gbKillTag.Name = "gbKillTag";
            this.gbKillTag.Padding = new System.Windows.Forms.Padding(4);
            this.gbKillTag.Size = new System.Drawing.Size(120, 187);
            this.gbKillTag.TabIndex = 0;
            this.gbKillTag.TabStop = false;
            this.gbKillTag.Text = "销毁标签：";
            // 
            // txtKillPwd
            // 
            this.txtKillPwd.Location = new System.Drawing.Point(11, 56);
            this.txtKillPwd.MaxLength = 8;
            this.txtKillPwd.Name = "txtKillPwd";
            this.txtKillPwd.PasswordChar = '*';
            this.txtKillPwd.Size = new System.Drawing.Size(103, 21);
            this.txtKillPwd.TabIndex = 5;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 28);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 12);
            this.label17.TabIndex = 4;
            this.label17.Text = "销毁密码：";
            // 
            // btnKillTag
            // 
            this.btnKillTag.Location = new System.Drawing.Point(37, 156);
            this.btnKillTag.Name = "btnKillTag";
            this.btnKillTag.Size = new System.Drawing.Size(75, 23);
            this.btnKillTag.TabIndex = 1;
            this.btnKillTag.Text = "销毁";
            this.btnKillTag.UseVisualStyleBackColor = true;
            this.btnKillTag.Click += new System.EventHandler(this.btnKillTag_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.groupBox5);
            this.groupBox1.Controls.Add(this.gbReadUserData);
            this.groupBox1.Controls.Add(this.btnRead);
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Location = new System.Drawing.Point(12, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(966, 190);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "读操作";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtReadEpc);
            this.groupBox6.Location = new System.Drawing.Point(13, 21);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox6.Size = new System.Drawing.Size(203, 129);
            this.groupBox6.TabIndex = 0;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "读EPC区：";
            // 
            // txtReadEpc
            // 
            this.txtReadEpc.Location = new System.Drawing.Point(7, 25);
            this.txtReadEpc.Multiline = true;
            this.txtReadEpc.Name = "txtReadEpc";
            this.txtReadEpc.Size = new System.Drawing.Size(189, 90);
            this.txtReadEpc.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtReadTid);
            this.groupBox5.Location = new System.Drawing.Point(224, 21);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(203, 129);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "读TID区：";
            // 
            // txtReadTid
            // 
            this.txtReadTid.Location = new System.Drawing.Point(7, 25);
            this.txtReadTid.Multiline = true;
            this.txtReadTid.Name = "txtReadTid";
            this.txtReadTid.Size = new System.Drawing.Size(189, 90);
            this.txtReadTid.TabIndex = 0;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtReadKPwd);
            this.groupBox7.Controls.Add(this.label2);
            this.groupBox7.Controls.Add(this.txtReadAPwd);
            this.groupBox7.Controls.Add(this.label3);
            this.groupBox7.Location = new System.Drawing.Point(780, 21);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox7.Size = new System.Drawing.Size(178, 129);
            this.groupBox7.TabIndex = 0;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "读保留区：";
            // 
            // txtReadKPwd
            // 
            this.txtReadKPwd.Location = new System.Drawing.Point(80, 56);
            this.txtReadKPwd.MaxLength = 8;
            this.txtReadKPwd.Name = "txtReadKPwd";
            this.txtReadKPwd.Size = new System.Drawing.Size(91, 21);
            this.txtReadKPwd.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 60);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "销毁密码：";
            // 
            // txtReadAPwd
            // 
            this.txtReadAPwd.Location = new System.Drawing.Point(80, 25);
            this.txtReadAPwd.MaxLength = 8;
            this.txtReadAPwd.Name = "txtReadAPwd";
            this.txtReadAPwd.Size = new System.Drawing.Size(91, 21);
            this.txtReadAPwd.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 29);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "访问密码：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gbWriteEPC);
            this.groupBox2.Controls.Add(this.gbWriteUserData);
            this.groupBox2.Controls.Add(this.btnWrite);
            this.groupBox2.Controls.Add(this.gbChangePwd);
            this.groupBox2.Location = new System.Drawing.Point(12, 235);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(653, 188);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "写操作";
            // 
            // ckbReadEpc
            // 
            this.ckbReadEpc.AutoSize = true;
            this.ckbReadEpc.Checked = true;
            this.ckbReadEpc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbReadEpc.Location = new System.Drawing.Point(83, 59);
            this.ckbReadEpc.Name = "ckbReadEpc";
            this.ckbReadEpc.Size = new System.Drawing.Size(15, 14);
            this.ckbReadEpc.TabIndex = 5;
            this.ckbReadEpc.UseVisualStyleBackColor = true;
            // 
            // ckbReadTid
            // 
            this.ckbReadTid.AutoSize = true;
            this.ckbReadTid.Checked = true;
            this.ckbReadTid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbReadTid.Location = new System.Drawing.Point(297, 59);
            this.ckbReadTid.Name = "ckbReadTid";
            this.ckbReadTid.Size = new System.Drawing.Size(15, 14);
            this.ckbReadTid.TabIndex = 5;
            this.ckbReadTid.UseVisualStyleBackColor = true;
            // 
            // ckbReadUser
            // 
            this.ckbReadUser.AutoSize = true;
            this.ckbReadUser.Location = new System.Drawing.Point(517, 59);
            this.ckbReadUser.Name = "ckbReadUser";
            this.ckbReadUser.Size = new System.Drawing.Size(15, 14);
            this.ckbReadUser.TabIndex = 5;
            this.ckbReadUser.UseVisualStyleBackColor = true;
            // 
            // ckbReadResv
            // 
            this.ckbReadResv.AutoSize = true;
            this.ckbReadResv.Location = new System.Drawing.Point(858, 59);
            this.ckbReadResv.Name = "ckbReadResv";
            this.ckbReadResv.Size = new System.Drawing.Size(15, 14);
            this.ckbReadResv.TabIndex = 5;
            this.ckbReadResv.UseVisualStyleBackColor = true;
            // 
            // ckbWriteEpc
            // 
            this.ckbWriteEpc.AutoSize = true;
            this.ckbWriteEpc.Checked = true;
            this.ckbWriteEpc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbWriteEpc.Location = new System.Drawing.Point(67, 256);
            this.ckbWriteEpc.Name = "ckbWriteEpc";
            this.ckbWriteEpc.Size = new System.Drawing.Size(15, 14);
            this.ckbWriteEpc.TabIndex = 5;
            this.ckbWriteEpc.UseVisualStyleBackColor = true;
            // 
            // ckbWriteUser
            // 
            this.ckbWriteUser.AutoSize = true;
            this.ckbWriteUser.Location = new System.Drawing.Point(271, 256);
            this.ckbWriteUser.Name = "ckbWriteUser";
            this.ckbWriteUser.Size = new System.Drawing.Size(15, 14);
            this.ckbWriteUser.TabIndex = 5;
            this.ckbWriteUser.UseVisualStyleBackColor = true;
            // 
            // ckbWriteResv
            // 
            this.ckbWriteResv.AutoSize = true;
            this.ckbWriteResv.Location = new System.Drawing.Point(614, 256);
            this.ckbWriteResv.Name = "ckbWriteResv";
            this.ckbWriteResv.Size = new System.Drawing.Size(15, 14);
            this.ckbWriteResv.TabIndex = 5;
            this.ckbWriteResv.UseVisualStyleBackColor = true;
            // 
            // FormTagAccess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 436);
            this.Controls.Add(this.ckbWriteResv);
            this.Controls.Add(this.ckbWriteUser);
            this.Controls.Add(this.ckbWriteEpc);
            this.Controls.Add(this.ckbReadResv);
            this.Controls.Add(this.ckbReadUser);
            this.Controls.Add(this.ckbReadTid);
            this.Controls.Add(this.ckbReadEpc);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.gbKillTag);
            this.Controls.Add(this.gbLockTag);
            this.Controls.Add(this.txtTagId);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTagAccess";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "标签操作";
            this.Load += new System.EventHandler(this.FormTagAccess_Load);
            this.gbWriteEPC.ResumeLayout(false);
            this.gbWriteEPC.PerformLayout();
            this.gbReadUserData.ResumeLayout(false);
            this.gbReadUserData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numReadUserLen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numReadUserPtr)).EndInit();
            this.gbWriteUserData.ResumeLayout(false);
            this.gbWriteUserData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWriteUserPtr)).EndInit();
            this.gbLockTag.ResumeLayout(false);
            this.gbLockTag.PerformLayout();
            this.gbChangePwd.ResumeLayout(false);
            this.gbChangePwd.PerformLayout();
            this.gbKillTag.ResumeLayout(false);
            this.gbKillTag.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTagId;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox gbWriteEPC;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.TextBox txtWriteEpcData;
        private System.Windows.Forms.GroupBox gbReadUserData;
        private System.Windows.Forms.NumericUpDown numReadUserLen;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown numReadUserPtr;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.TextBox txtReaduser;
        private System.Windows.Forms.GroupBox gbWriteUserData;
        private System.Windows.Forms.TextBox txtWriteUser;
        private System.Windows.Forms.NumericUpDown numWriteUserPtr;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox gbLockTag;
        private System.Windows.Forms.ComboBox cbLockType;
        private System.Windows.Forms.ComboBox cbLockMemory;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnLock;
        private System.Windows.Forms.GroupBox gbChangePwd;
        private System.Windows.Forms.TextBox txtKillPwd_Change;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtAccessPwd;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox gbKillTag;
        private System.Windows.Forms.TextBox txtKillPwd;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button btnKillTag;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtReadEpc;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtReadTid;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txtReadKPwd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtReadAPwd;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox ckbReadEpc;
        private System.Windows.Forms.CheckBox ckbReadTid;
        private System.Windows.Forms.CheckBox ckbReadUser;
        private System.Windows.Forms.CheckBox ckbReadResv;
        private System.Windows.Forms.CheckBox ckbWriteEpc;
        private System.Windows.Forms.CheckBox ckbWriteUser;
        private System.Windows.Forms.CheckBox ckbWriteResv;
    }
}