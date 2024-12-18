namespace RFIDTest
{
    partial class FormConn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConn));
            this.rbCom = new System.Windows.Forms.RadioButton();
            this.rbTcp = new System.Windows.Forms.RadioButton();
            this.gbCom = new System.Windows.Forms.GroupBox();
            this.cbBaud = new System.Windows.Forms.ComboBox();
            this.cbPort = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.cbRs485 = new System.Windows.Forms.CheckBox();
            this.gbTcp = new System.Windows.Forms.GroupBox();
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDisconn = new System.Windows.Forms.Button();
            this.btnConn = new System.Windows.Forms.Button();
            this.gbCom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.gbTcp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            this.SuspendLayout();
            // 
            // rbCom
            // 
            this.rbCom.AutoSize = true;
            this.rbCom.Location = new System.Drawing.Point(22, 102);
            this.rbCom.Name = "rbCom";
            this.rbCom.Size = new System.Drawing.Size(14, 13);
            this.rbCom.TabIndex = 11;
            this.rbCom.UseVisualStyleBackColor = true;
            this.rbCom.CheckedChanged += new System.EventHandler(this.rbPortType_CheckedChanged);
            // 
            // rbTcp
            // 
            this.rbTcp.AutoSize = true;
            this.rbTcp.Checked = true;
            this.rbTcp.Location = new System.Drawing.Point(22, 10);
            this.rbTcp.Name = "rbTcp";
            this.rbTcp.Size = new System.Drawing.Size(14, 13);
            this.rbTcp.TabIndex = 12;
            this.rbTcp.TabStop = true;
            this.rbTcp.UseVisualStyleBackColor = true;
            this.rbTcp.CheckedChanged += new System.EventHandler(this.rbPortType_CheckedChanged);
            // 
            // gbCom
            // 
            this.gbCom.Controls.Add(this.cbBaud);
            this.gbCom.Controls.Add(this.cbPort);
            this.gbCom.Controls.Add(this.label3);
            this.gbCom.Controls.Add(this.label4);
            this.gbCom.Enabled = false;
            this.gbCom.Location = new System.Drawing.Point(13, 103);
            this.gbCom.Name = "gbCom";
            this.gbCom.Size = new System.Drawing.Size(183, 81);
            this.gbCom.TabIndex = 9;
            this.gbCom.TabStop = false;
            this.gbCom.Text = "   COM：";
            // 
            // cbBaud
            // 
            this.cbBaud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBaud.FormattingEnabled = true;
            this.cbBaud.Items.AddRange(new object[] {
            "9600",
            "19200",
            "115200",
            "230400",
            "460800"});
            this.cbBaud.Location = new System.Drawing.Point(69, 53);
            this.cbBaud.Name = "cbBaud";
            this.cbBaud.Size = new System.Drawing.Size(102, 20);
            this.cbBaud.TabIndex = 1;
            // 
            // cbPort
            // 
            this.cbPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPort.FormattingEnabled = true;
            this.cbPort.Location = new System.Drawing.Point(69, 24);
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(102, 20);
            this.cbPort.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 0;
            //this.label3.Text = "波特率：";
            this.label3.Text = "전송률：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            //this.label4.Text = "串口号：";
            this.label4.Text = "구분：";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Enabled = false;
            this.numericUpDown1.Location = new System.Drawing.Point(82, 263);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(102, 21);
            this.numericUpDown1.TabIndex = 3;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbRs485
            // 
            this.cbRs485.AutoSize = true;
            this.cbRs485.Location = new System.Drawing.Point(22, 265);
            this.cbRs485.Name = "cbRs485";
            this.cbRs485.Size = new System.Drawing.Size(54, 16);
            this.cbRs485.TabIndex = 2;
            this.cbRs485.Text = "RS485";
            this.cbRs485.UseVisualStyleBackColor = true;
            this.cbRs485.CheckedChanged += new System.EventHandler(this.cbRs485_CheckedChanged);
            // 
            // gbTcp
            // 
            this.gbTcp.Controls.Add(this.numPort);
            this.gbTcp.Controls.Add(this.txtIp);
            this.gbTcp.Controls.Add(this.label1);
            this.gbTcp.Controls.Add(this.label2);
            this.gbTcp.Location = new System.Drawing.Point(13, 12);
            this.gbTcp.Name = "gbTcp";
            this.gbTcp.Size = new System.Drawing.Size(183, 85);
            this.gbTcp.TabIndex = 10;
            this.gbTcp.TabStop = false;
            this.gbTcp.Text = "   TCP：";
            // 
            // numPort
            // 
            this.numPort.Location = new System.Drawing.Point(69, 52);
            this.numPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numPort.Name = "numPort";
            this.numPort.Size = new System.Drawing.Size(102, 21);
            this.numPort.TabIndex = 2;
            this.numPort.Value = new decimal(new int[] {
            9090,
            0,
            0,
            0});
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(69, 21);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(102, 21);
            this.txtIp.TabIndex = 1;
            this.txtIp.Text = "192.168.1.100";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Port：";
            // 
            // btnDisconn
            // 
            this.btnDisconn.Location = new System.Drawing.Point(109, 190);
            this.btnDisconn.Name = "btnDisconn";
            this.btnDisconn.Size = new System.Drawing.Size(75, 23);
            this.btnDisconn.TabIndex = 13;
            //this.btnDisconn.Text = "关闭";
            this.btnDisconn.Text = "닫기";
            this.btnDisconn.UseVisualStyleBackColor = true;
            this.btnDisconn.Click += new System.EventHandler(this.btnDisconn_Click);
            // 
            // btnConn
            // 
            this.btnConn.Location = new System.Drawing.Point(28, 190);
            this.btnConn.Name = "btnConn";
            this.btnConn.Size = new System.Drawing.Size(75, 23);
            this.btnConn.TabIndex = 14;
            //this.btnConn.Text = "连接";
            this.btnConn.Text = "연결";
            this.btnConn.UseVisualStyleBackColor = true;
            this.btnConn.Click += new System.EventHandler(this.btnConn_Click);
            // 
            // FormConn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(208, 223);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.cbRs485);
            this.Controls.Add(this.rbCom);
            this.Controls.Add(this.rbTcp);
            this.Controls.Add(this.gbCom);
            this.Controls.Add(this.gbTcp);
            this.Controls.Add(this.btnDisconn);
            this.Controls.Add(this.btnConn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConn";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            //this.Text = "连接方式";
            this.Text = "연결 방식";
            this.Load += new System.EventHandler(this.FormConn_Load);
            this.gbCom.ResumeLayout(false);
            this.gbCom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.gbTcp.ResumeLayout(false);
            this.gbTcp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbCom;
        private System.Windows.Forms.RadioButton rbTcp;
        private System.Windows.Forms.GroupBox gbCom;
        private System.Windows.Forms.ComboBox cbBaud;
        private System.Windows.Forms.ComboBox cbPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gbTcp;
        private System.Windows.Forms.NumericUpDown numPort;
        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDisconn;
        private System.Windows.Forms.Button btnConn;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.CheckBox cbRs485;
    }
}