namespace RFIDTest
{
    partial class FormTagRW
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTagRW));
            this.txt_TagID = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txt_WriteData = new System.Windows.Forms.TextBox();
            this.cb_write = new System.Windows.Forms.CheckBox();
            this.g_data = new System.Windows.Forms.GroupBox();
            this.txt_ReadData = new System.Windows.Forms.TextBox();
            this.cb_read = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblSuccessCount = new System.Windows.Forms.Label();
            this.lblAverageTime = new System.Windows.Forms.Label();
            this.lblSuccessPercentage = new System.Windows.Forms.Label();
            this.lblDoneCount = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.num_TestCount = new System.Windows.Forms.NumericUpDown();
            this.num_Length = new System.Windows.Forms.NumericUpDown();
            this.num_Ptr = new System.Windows.Forms.NumericUpDown();
            this.cb_MB = new System.Windows.Forms.ComboBox();
            this.txt_pwd = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_StopTest = new System.Windows.Forms.Button();
            this.btn_Test = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.g_data.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_TestCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Length)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Ptr)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_TagID
            // 
            this.txt_TagID.BackColor = System.Drawing.Color.White;
            this.txt_TagID.Location = new System.Drawing.Point(89, 16);
            this.txt_TagID.Name = "txt_TagID";
            this.txt_TagID.ReadOnly = true;
            this.txt_TagID.Size = new System.Drawing.Size(309, 21);
            this.txt_TagID.TabIndex = 53;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txt_WriteData);
            this.groupBox3.Location = new System.Drawing.Point(210, 46);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(322, 151);
            this.groupBox3.TabIndex = 49;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "写入数据：";
            // 
            // txt_WriteData
            // 
            this.txt_WriteData.BackColor = System.Drawing.Color.White;
            this.txt_WriteData.Location = new System.Drawing.Point(7, 20);
            this.txt_WriteData.MaxLength = 243;
            this.txt_WriteData.Multiline = true;
            this.txt_WriteData.Name = "txt_WriteData";
            this.txt_WriteData.ReadOnly = true;
            this.txt_WriteData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_WriteData.Size = new System.Drawing.Size(309, 125);
            this.txt_WriteData.TabIndex = 0;
            this.txt_WriteData.TabStop = false;
            // 
            // cb_write
            // 
            this.cb_write.AutoSize = true;
            this.cb_write.Checked = true;
            this.cb_write.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_write.Location = new System.Drawing.Point(281, 46);
            this.cb_write.Name = "cb_write";
            this.cb_write.Size = new System.Drawing.Size(15, 14);
            this.cb_write.TabIndex = 52;
            this.cb_write.UseVisualStyleBackColor = true;
            // 
            // g_data
            // 
            this.g_data.Controls.Add(this.txt_ReadData);
            this.g_data.Controls.Add(this.cb_read);
            this.g_data.Location = new System.Drawing.Point(210, 203);
            this.g_data.Name = "g_data";
            this.g_data.Size = new System.Drawing.Size(322, 151);
            this.g_data.TabIndex = 50;
            this.g_data.TabStop = false;
            this.g_data.Text = "读出数据：";
            // 
            // txt_ReadData
            // 
            this.txt_ReadData.BackColor = System.Drawing.Color.White;
            this.txt_ReadData.Location = new System.Drawing.Point(7, 20);
            this.txt_ReadData.MaxLength = 243;
            this.txt_ReadData.Multiline = true;
            this.txt_ReadData.Name = "txt_ReadData";
            this.txt_ReadData.ReadOnly = true;
            this.txt_ReadData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_ReadData.Size = new System.Drawing.Size(309, 125);
            this.txt_ReadData.TabIndex = 0;
            this.txt_ReadData.TabStop = false;
            // 
            // cb_read
            // 
            this.cb_read.AutoSize = true;
            this.cb_read.Checked = true;
            this.cb_read.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_read.Location = new System.Drawing.Point(71, 0);
            this.cb_read.Name = "cb_read";
            this.cb_read.Size = new System.Drawing.Size(15, 14);
            this.cb_read.TabIndex = 51;
            this.cb_read.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTime);
            this.groupBox2.Controls.Add(this.lblSuccessCount);
            this.groupBox2.Controls.Add(this.lblAverageTime);
            this.groupBox2.Controls.Add(this.lblSuccessPercentage);
            this.groupBox2.Controls.Add(this.lblDoneCount);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 203);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(192, 151);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "测试结果：";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(120, 98);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(11, 12);
            this.lblTime.TabIndex = 1;
            this.lblTime.Text = "0";
            // 
            // lblSuccessCount
            // 
            this.lblSuccessCount.AutoSize = true;
            this.lblSuccessCount.Location = new System.Drawing.Point(120, 48);
            this.lblSuccessCount.Name = "lblSuccessCount";
            this.lblSuccessCount.Size = new System.Drawing.Size(11, 12);
            this.lblSuccessCount.TabIndex = 1;
            this.lblSuccessCount.Text = "0";
            // 
            // lblAverageTime
            // 
            this.lblAverageTime.AutoSize = true;
            this.lblAverageTime.Location = new System.Drawing.Point(120, 123);
            this.lblAverageTime.Name = "lblAverageTime";
            this.lblAverageTime.Size = new System.Drawing.Size(11, 12);
            this.lblAverageTime.TabIndex = 1;
            this.lblAverageTime.Text = "0";
            // 
            // lblSuccessPercentage
            // 
            this.lblSuccessPercentage.AutoSize = true;
            this.lblSuccessPercentage.Location = new System.Drawing.Point(120, 73);
            this.lblSuccessPercentage.Name = "lblSuccessPercentage";
            this.lblSuccessPercentage.Size = new System.Drawing.Size(11, 12);
            this.lblSuccessPercentage.TabIndex = 1;
            this.lblSuccessPercentage.Text = "0";
            // 
            // lblDoneCount
            // 
            this.lblDoneCount.AutoSize = true;
            this.lblDoneCount.Location = new System.Drawing.Point(120, 23);
            this.lblDoneCount.Name = "lblDoneCount";
            this.lblDoneCount.Size = new System.Drawing.Size(11, 12);
            this.lblDoneCount.TabIndex = 1;
            this.lblDoneCount.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 123);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 0;
            this.label13.Text = "平均时间：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 98);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "所用时间：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "成功比率：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "成功次数：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "完成次数：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.num_TestCount);
            this.groupBox1.Controls.Add(this.num_Length);
            this.groupBox1.Controls.Add(this.num_Ptr);
            this.groupBox1.Controls.Add(this.cb_MB);
            this.groupBox1.Controls.Add(this.txt_pwd);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(12, 46);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(192, 151);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "标签设置(单位：字)";
            // 
            // num_TestCount
            // 
            this.num_TestCount.Location = new System.Drawing.Point(110, 123);
            this.num_TestCount.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.num_TestCount.Name = "num_TestCount";
            this.num_TestCount.Size = new System.Drawing.Size(72, 21);
            this.num_TestCount.TabIndex = 41;
            this.num_TestCount.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // num_Length
            // 
            this.num_Length.Location = new System.Drawing.Point(110, 97);
            this.num_Length.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.num_Length.Name = "num_Length";
            this.num_Length.Size = new System.Drawing.Size(72, 21);
            this.num_Length.TabIndex = 41;
            this.num_Length.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // num_Ptr
            // 
            this.num_Ptr.Location = new System.Drawing.Point(110, 71);
            this.num_Ptr.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
            this.num_Ptr.Name = "num_Ptr";
            this.num_Ptr.Size = new System.Drawing.Size(72, 21);
            this.num_Ptr.TabIndex = 41;
            // 
            // cb_MB
            // 
            this.cb_MB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_MB.FormattingEnabled = true;
            this.cb_MB.Items.AddRange(new object[] {
            "UserData",
            "EPC"});
            this.cb_MB.Location = new System.Drawing.Point(110, 18);
            this.cb_MB.Name = "cb_MB";
            this.cb_MB.Size = new System.Drawing.Size(72, 20);
            this.cb_MB.TabIndex = 40;
            this.cb_MB.SelectedIndexChanged += new System.EventHandler(this.cb_MB_SelectedIndexChanged);
            // 
            // txt_pwd
            // 
            this.txt_pwd.Location = new System.Drawing.Point(110, 44);
            this.txt_pwd.MaxLength = 8;
            this.txt_pwd.Name = "txt_pwd";
            this.txt_pwd.PasswordChar = '*';
            this.txt_pwd.Size = new System.Drawing.Size(72, 21);
            this.txt_pwd.TabIndex = 1;
            this.txt_pwd.Text = "00000000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 39;
            this.label5.Text = "访问密码：";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 33;
            this.label1.Text = "测试区域：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(6, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 35;
            this.label3.Text = "测试次数：";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(6, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 35;
            this.label2.Text = "数据长度：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(6, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 37;
            this.label6.Text = "起始地址：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btn_StopTest
            // 
            this.btn_StopTest.Enabled = false;
            this.btn_StopTest.Location = new System.Drawing.Point(471, 15);
            this.btn_StopTest.Name = "btn_StopTest";
            this.btn_StopTest.Size = new System.Drawing.Size(61, 23);
            this.btn_StopTest.TabIndex = 2;
            this.btn_StopTest.Text = "停止";
            this.btn_StopTest.UseVisualStyleBackColor = true;
            this.btn_StopTest.Click += new System.EventHandler(this.btn_StopTest_Click);
            // 
            // btn_Test
            // 
            this.btn_Test.Location = new System.Drawing.Point(404, 15);
            this.btn_Test.Name = "btn_Test";
            this.btn_Test.Size = new System.Drawing.Size(61, 23);
            this.btn_Test.TabIndex = 2;
            this.btn_Test.Text = "开始";
            this.btn_Test.UseVisualStyleBackColor = true;
            this.btn_Test.Click += new System.EventHandler(this.btn_Test_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "操作标签：";
            // 
            // FormTagRW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 362);
            this.Controls.Add(this.txt_TagID);
            this.Controls.Add(this.cb_write);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.g_data);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_Test);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_StopTest);
            this.Controls.Add(this.label8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormTagRW";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "重复读写测试";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTagRW_FormClosing);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.g_data.ResumeLayout(false);
            this.g_data.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_TestCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Length)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_Ptr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txt_TagID;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txt_WriteData;
        private System.Windows.Forms.CheckBox cb_write;
        private System.Windows.Forms.GroupBox g_data;
        private System.Windows.Forms.TextBox txt_ReadData;
        private System.Windows.Forms.CheckBox cb_read;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblSuccessCount;
        private System.Windows.Forms.Label lblAverageTime;
        private System.Windows.Forms.Label lblSuccessPercentage;
        private System.Windows.Forms.Label lblDoneCount;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cb_MB;
        private System.Windows.Forms.TextBox txt_pwd;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_StopTest;
        private System.Windows.Forms.Button btn_Test;
        private System.Windows.Forms.NumericUpDown num_TestCount;
        private System.Windows.Forms.NumericUpDown num_Length;
        private System.Windows.Forms.NumericUpDown num_Ptr;
        private System.Windows.Forms.Label label8;
    }
}