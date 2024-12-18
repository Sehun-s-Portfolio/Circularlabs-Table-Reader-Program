using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetAPI.Protocol.VRP;
using NetAPI.Entities;
using NetAPI.Core;
using System.IO;
using System.Threading;

namespace RFIDTest
{
    public partial class FormReaderTest : Form
    {
        FormMain frm;
        Reader reader;
        bool isFormLoad = true;

        public FormReaderTest(Reader reader,FormMain frm)
        {
            InitializeComponent();
            this.frm = frm;
            this.reader = reader;
            reader.OnFirmwareOnlineUpgradeReceived += Reader_OnFirmwareOnlineUpgradeReceived;
            reader.OnAppFirmwareOnlineUpgradeReceived += Reader_OnAppFirmwareOnlineUpgradeReceived;
        }

        private void FormReaderTest_Load(object sender, EventArgs e)
        {
            button5_Click(sender, e);           
            button10_Click(sender, e);
            comboBox1.SelectedIndex = 0;
            button8_Click(sender, e);
            button15_Click(sender, e);
            button18_Click(sender, e);
            button20_Click(sender, e);
            button28_Click(sender, e);
            button12_Click(sender, e);
            button24_Click(sender, e);
            button26_Click(sender, e);
            button17_Click(sender, e);
            button31_Click(sender, e);
            isFormLoad = false;
        }

        private void Reader_OnFirmwareOnlineUpgradeReceived(ushort total, ushort rate)
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                progressBar1.Maximum = total;
                progressBar1.Value = rate;
                if (total == 0xff && rate == 0xff)
                    MessageBox.Show("升级成功完成");
            }));
        }

        private void setProgressBar(ProgressBar bar, ushort total, ushort rate)
        {
            bar.Maximum = total;
            bar.Value = rate;
        }

        private void Reader_OnAppFirmwareOnlineUpgradeReceived(ushort total, ushort rate)
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                progressBar2.Maximum = total;
                progressBar2.Value = rate;
                if (total == 0xff && rate == 0xff)
                    MessageBox.Show("升级成功完成");
            }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "BIN文件|*.bin";
            d.FilterIndex = 1;
            if (d.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = d.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filePath = textBox1.Text.Trim();
            int file_len = 0;
            byte[] binchar = null;

            if (filePath != "")
            {
                using (FileStream Myfile = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader binreader = new BinaryReader(Myfile);
                    file_len = (int)Myfile.Length;//获取bin文件长度
                    binchar = new byte[file_len];
                    binreader.Read(binchar, 0, file_len);
                    binreader.Close();
                }

                MsgFirmwareUpgradePreparation msgP = new MsgFirmwareUpgradePreparation();
                for (int j = 0; j < 5; j++)
                {
                    if (reader.Send(msgP, 2000))
                    {
                        break;
                    }
                    Thread.Sleep(2000);
                }
                #region 准备完成，继续
                {
                    int p = 0;
                    int i = 1;
                    int fLen = 100;
                    int tCount = file_len / fLen;
                    if (file_len % fLen > 0)
                        tCount++;
                    byte[] totalCount = new byte[4];
                    totalCount[0] = (byte)(tCount >> 24);
                    totalCount[1] = (byte)(tCount >> 16);
                    totalCount[2] = (byte)(tCount >> 8);
                    totalCount[3] = (byte)tCount;
                    for (int j = 0; j < 5; j++)
                    {
                        MsgFirmwareOnlineUpgrade msg0 = new MsgFirmwareOnlineUpgrade(0x00, totalCount);
                        if (reader.Send(msg0, 2000))
                        {
                            break;
                        }
                        Thread.Sleep(2000);
                    }
                    #region 发送总数据包完成，继续
                    {
                        progressBar1.Value = 0;
                        progressBar1.Maximum = tCount;
                        #region 发送数据帧
                        while (file_len - p > fLen)
                        {
                            byte[] wd = new byte[fLen];
                            Array.Copy(binchar, p, wd, 0, fLen);
                            p += fLen;

                            bool isSuc = false;
                            for (int j = 0; j < 5; j++)
                            {
                                MsgFirmwareOnlineUpgrade msg = new MsgFirmwareOnlineUpgrade((uint)i, wd);
                                if (reader.Send(msg, 2000))
                                {
                                    isSuc = true;
                                    i++;
                                    progressBar1.Value++;
                                    break;
                                }
                                Thread.Sleep(2000);
                            }
                            if (!isSuc)
                            {
                                MessageBox.Show("升级失败");
                                return;
                            }
                        }
                        if (p < file_len - 1)
                        {
                            byte[] wd = new byte[file_len - p];
                            Array.Copy(binchar, p, wd, 0, wd.Length);
                            bool isSuc = false;
                            for (int j = 0; j < 5; j++)
                            {
                                MsgFirmwareOnlineUpgrade msg = new MsgFirmwareOnlineUpgrade((uint)i, wd);
                                if (reader.Send(msg, 2000))
                                {
                                    isSuc = true;
                                    progressBar1.Value++;
                                    break;
                                }
                                Thread.Sleep(2000);
                            }
                            if (!isSuc)
                            {
                                MessageBox.Show("升级失败");
                                return;
                            }
                        }
                        #endregion
                        #region 确认帧
                        {
                            bool isSuc = false;
                            for (int j = 0; j < 5; j++)
                            {
                                MsgFirmwareOnlineUpgrade msg = new MsgFirmwareOnlineUpgrade(0xffffffff, new byte[4]);
                                if (reader.Send(msg, 2000))
                                {
                                    isSuc = true;
                                    progressBar1.Value = 0;
                                    MessageBox.Show("升级成功完成，请重启读写器");
                                    break;
                                }
                                Thread.Sleep(2000);
                            }
                            if (!isSuc)
                            {
                                MessageBox.Show("升级失败");
                                return;
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
                #endregion
            }
            else
                MessageBox.Show("请先浏览文件");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog d = new OpenFileDialog();
            d.Filter = "BIN文件|*.bin";
            d.FilterIndex = 1;
            if (d.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = d.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string filePath = textBox2.Text.Trim();
            int file_len = 0;
            byte[] binchar = null;

            if (filePath != "")
            {
                using (FileStream Myfile = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    BinaryReader binreader = new BinaryReader(Myfile);
                    file_len = (int)Myfile.Length;//获取bin文件长度
                    binchar = new byte[file_len];
                    binreader.Read(binchar, 0, file_len);
                    binreader.Close();
                }

                bool isClose = true;
                for (int j = 0; j < 5; j++)
                {
                    MsgAppFirmwareUpgradePreparation msgP = new MsgAppFirmwareUpgradePreparation();
                    if (reader.Send(msgP, 2000))
                    {
                        if (reader.CommPort.Port == NetAPI.PortType.TcpClient)
                        {
                            isClose = false;
                            reader.Disconnect();
                            //this.BeginInvoke(new MethodInvoker(delegate () { 
                            //    frm.btnConn_Click(null, EventArgs.Empty);
                            //    this.reader = null;
                            //})); 
                        }
                        break;
                    }
                    Thread.Sleep(2000);
                }

                for (int i = 0; i < 3; i++)
                {
                    Thread.Sleep(3000);
                    if (reader == null || !reader.IsConnected)
                    {
                        //frm.btnConn_Click(null, EventArgs.Empty);
                        //this.reader = frm.reader;
                        //if(frm.reader!=null &&frm.reader.IsConnected)                       
                        //{
                        //    isClose = true;
                        //    break;
                        //}

                        ConnectResponse res = reader.Connect();
                        if (res.IsSucessed)
                        {
                            isClose = true;
                            break;
                        }
                    }
                }
                if (!isClose)
                {
                    MessageBox.Show("升级失败，请藏尸用串口方式升级！");
                    return;
                }
                #region 准备完成，继续
                {
                    int p = 0;
                    int i = 1;
                    int fLen = 100;
                    int tCount = file_len / fLen;
                    if (file_len % fLen > 0)
                        tCount++;
                    byte[] totalCount = new byte[4];
                    totalCount[0] = (byte)(tCount >> 24);
                    totalCount[1] = (byte)(tCount >> 16);
                    totalCount[2] = (byte)(tCount >> 8);
                    totalCount[3] = (byte)tCount;
                    for (int j = 0; j < 5; j++)
                    {
                        MsgAppFirmwareOnlineUpgrade msg0 = new MsgAppFirmwareOnlineUpgrade(0x00, totalCount);
                        if (reader.Send(msg0, 2000))
                        {
                            break;
                        }
                        Thread.Sleep(2000);
                    }
                    #region 发送总数据包完成，继续
                    {
                        progressBar2.Value = 0;
                        progressBar2.Maximum = tCount;
                        #region 发送数据帧
                        while (file_len - p > fLen)
                        {
                            byte[] wd = new byte[fLen];
                            Array.Copy(binchar, p, wd, 0, fLen);
                            p += fLen;

                            bool isSuc = false;
                            for (int j = 0; j < 5; j++)
                            {
                                MsgAppFirmwareOnlineUpgrade msg = new MsgAppFirmwareOnlineUpgrade((uint)i, wd);
                                if (reader.Send(msg, 2000))
                                {
                                    isSuc = true;
                                    i++;
                                    progressBar2.Value++;
                                    break;
                                }
                                Thread.Sleep(2000);
                            }
                            if (!isSuc)
                            {
                                MessageBox.Show("升级失败");
                                return;
                            }
                        }
                        if (p < file_len - 1)
                        {
                            byte[] wd = new byte[file_len - p];
                            Array.Copy(binchar, p, wd, 0, wd.Length);
                            bool isSuc = false;
                            for (int j = 0; j < 5; j++)
                            {
                                MsgAppFirmwareOnlineUpgrade msg = new MsgAppFirmwareOnlineUpgrade((uint)i, wd);
                                if (reader.Send(msg, 2000))
                                {
                                    isSuc = true;
                                    progressBar2.Value++;
                                    break;
                                }
                                Thread.Sleep(2000);
                            }
                            if (!isSuc)
                            {
                                MessageBox.Show("升级失败");
                                return;
                            }
                        }
                        #endregion
                        #region 确认帧
                        {
                            bool isSuc = false;
                            for (int j = 0; j < 5; j++)
                            {
                                MsgAppFirmwareOnlineUpgrade msg = new MsgAppFirmwareOnlineUpgrade(0xffffffff, new byte[4]);
                                if (reader.Send(msg, 2000))
                                {
                                    isSuc = true;
                                    progressBar2.Value = 0;
                                    MessageBox.Show("升级成功完成，请重启读写器");
                                    break;
                                }
                                Thread.Sleep(2000);
                            }
                            if (!isSuc)
                            {
                                MessageBox.Show("升级失败");
                                return;
                            }
                            else
                            {
                                frm.btnConn_Click(null, EventArgs.Empty);
                                this.Close();
                            }

                        }
                        #endregion
                    }
                    #endregion
                }
                #endregion
            }
            else
                MessageBox.Show("请先浏览文件");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MsgReaderVersionQuery msg = new MsgReaderVersionQuery();
            if (reader.Send(msg))
            {

                textBox3.Text = msg.ReceivedMessage.ModelNumber;
                textBox4.Text = msg.ReceivedMessage.SoftwareVersion;
                textBox5.Text = msg.ReceivedMessage.HardwareVersion;
            }
            else
            {
                if (!isFormLoad)
                    MessageBox.Show(msg.ErrorInfo.ErrMsg);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MsgCpuVersionQuery msg = new MsgCpuVersionQuery();
            if (reader.Send(msg))
            {
                textBox8.Text = msg.ReceivedMessage.ModelNumber;
                textBox7.Text = msg.ReceivedMessage.SoftwareVersion;
                textBox6.Text = msg.ReceivedMessage.HardwareVersion;
            }
            else
            {
                if (!isFormLoad)
                    MessageBox.Show(msg.ErrorInfo.ErrMsg);
            }
        }


        private void setCb(ComboBox cb, GpioDefinition gpio)
        {
            switch (gpio)
            {
                case GpioDefinition.Disabled:
                    cb.SelectedIndex = 2;
                    break;
                case GpioDefinition.GPI:
                    cb.SelectedIndex = 0;
                    break;
                case GpioDefinition.GPO:
                    cb.SelectedIndex = 1;
                    break;
            }
        }        

        private GpioDefinition getCb(ComboBox cb)
        {
            GpioDefinition g = GpioDefinition.Disabled;
            switch (cb.SelectedIndex)
            {
                case 2:
                    g = GpioDefinition.Disabled;
                    break;
                case 0:
                    g = GpioDefinition.GPI;
                    break;
                case 1:
                    g = GpioDefinition.GPO;
                    break;
              
            }
            return g;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择查询端口");
                return;
            }
            MsgGpiTriggerConfig msg = new MsgGpiTriggerConfig((byte)(comboBox1.SelectedIndex + 1));
            if (reader.Send(msg))
            {
                GpiTriggerParameter param = msg.ReceivedMessage.TriggerParameter;
                if (param != null)
                {
                    comboBox2.SelectedIndex = (int)param.TriggerCondition;
                    comboBox3.SelectedIndex = (int)param.StopCondition;
                    numericUpDown1.Value = param.DelayTime;
                    if (param.TriggerMsg == null)
                        comboBox4.SelectedIndex = 0;
                    else
                    {
                        string msgType = param.TriggerMsg.GetType().ToString();
                        comboBox4.Text = msgType.Substring(msgType.LastIndexOf('.') + 1);
                    }
                }
                else
                {
                    if (!isFormLoad)
                        MessageBox.Show("参数未设置");
                }
            }
            else
            {
                if (!isFormLoad)
                    MessageBox.Show(msg.ErrorInfo.ErrMsg);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("请选择查询端口");
                return;
            }
            if (comboBox2.SelectedIndex == -1 || comboBox3.SelectedIndex == -1 || comboBox4.SelectedIndex == -1)
            {
                MessageBox.Show("请完善触发参数");
                return;
            }
            GpiTriggerParameter tp = new GpiTriggerParameter();
            tp.PortNO = (byte)(comboBox1.SelectedIndex + 1);
            tp.TriggerCondition = (GpiTriggerCondition)comboBox2.SelectedIndex;
            tp.StopCondition = (GpiStopCondition)comboBox3.SelectedIndex;
            tp.DelayTime = (ushort)numericUpDown1.Value;
            if (comboBox4.SelectedIndex > 0)
            {
                switch (comboBox4.Text)
                {
                    case "MsgTagInventory":
                        tp.TriggerMsg = new MsgTagInventory();
                        break;
                    case "MsgTagRead":
                        tp.TriggerMsg = new MsgTagRead();
                        break;
                    case "MsgPowerOff":
                        tp.TriggerMsg = new MsgPowerOff();
                        break;
                }
            }
            MsgGpiTriggerConfig msg = new MsgGpiTriggerConfig(tp);
            if (reader.Send(msg))
            {
                MessageBox.Show("操作成功");
            }
            else
                MessageBox.Show(msg.ErrorInfo.ErrMsg);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                MsgGpiQuery msg1 = new MsgGpiQuery(0x01);
                if (reader.Send(msg1))
                {
                    comboBox10.SelectedIndex = (int)msg1.ReceivedMessage.GpiLevel.Level;
                }
                else
                {
                    if (!isFormLoad)
                        MessageBox.Show(msg1.ErrorInfo.ErrMsg);
                }
            }
            if (checkBox4.Checked)
            {
                MsgGpiQuery msg2 = new MsgGpiQuery(0x02);
                if (reader.Send(msg2))
                {
                    comboBox9.SelectedIndex = (int)msg2.ReceivedMessage.GpiLevel.Level;
                }
                else
                {
                    if (!isFormLoad)
                        MessageBox.Show(msg2.ErrorInfo.ErrMsg);
                }
            }
            if (checkBox6.Checked)
            {
                MsgGpiQuery msg3 = new MsgGpiQuery(0x03);
                if (reader.Send(msg3))
                {
                    comboBox11.SelectedIndex = (int)msg3.ReceivedMessage.GpiLevel.Level;
                }
                else
                {
                    if (!isFormLoad)
                        MessageBox.Show(msg3.ErrorInfo.ErrMsg);
                }
            }
            if (checkBox7.Checked)
            {
                MsgGpiQuery msg4 = new MsgGpiQuery(0x04);
                if (reader.Send(msg4))
                {
                    comboBox12.SelectedIndex = (int)msg4.ReceivedMessage.GpiLevel.Level;
                }
                else
                {
                    if (!isFormLoad)
                        MessageBox.Show(msg4.ErrorInfo.ErrMsg);
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string str = "";
            if (checkBox1.Checked)
            {
                if (comboBox5.SelectedIndex == -1)
                {
                    str = "请选择GPO1输出电平";
                    MessageBox.Show(str);
                    return;
                }
                GpioLevelParameter p = new GpioLevelParameter();
                p.PortNO = 0x01;
                p.Level = (GpioLevel)comboBox5.SelectedIndex;
                MsgGpoConfig msg = new MsgGpoConfig(p);
                if (reader.Send(msg))
                {
                    str = "GPO1设置成功\r\n";
                }
                else
                    str = msg.ErrorInfo.ErrMsg + "\r\n";
            }
            if (checkBox2.Checked)
            {
                if (comboBox6.SelectedIndex == -1)
                {
                    str = "请选择GPO2输出电平";
                    MessageBox.Show(str);
                    return;
                }
                GpioLevelParameter p = new GpioLevelParameter();
                p.PortNO = 0x02;
                p.Level = (GpioLevel)comboBox6.SelectedIndex;
                MsgGpoConfig msg = new MsgGpoConfig(p);
                if (reader.Send(msg))
                {
                    str += "GPO2设置成功";
                }
                else
                    str += msg.ErrorInfo.ErrMsg;
            }
            if (checkBox8.Checked)
            {
                if (comboBox13.SelectedIndex == -1)
                {
                    str = "请选择GPO3输出电平";
                    MessageBox.Show(str);
                    return;
                }
                GpioLevelParameter p = new GpioLevelParameter();
                p.PortNO = 0x03;
                p.Level = (GpioLevel)comboBox13.SelectedIndex;
                MsgGpoConfig msg = new MsgGpoConfig(p);
                if (reader.Send(msg))
                {
                    str = "GPO3设置成功\r\n";
                }
                else
                    str = msg.ErrorInfo.ErrMsg + "\r\n";
            }
            if (checkBox9.Checked)
            {
                if (comboBox14.SelectedIndex == -1)
                {
                    str = "请选择GPO4输出电平";
                    MessageBox.Show(str);
                    return;
                }
                GpioLevelParameter p = new GpioLevelParameter();
                p.PortNO = 0x04;
                p.Level = (GpioLevel)comboBox14.SelectedIndex;
                MsgGpoConfig msg = new MsgGpoConfig(p);
                if (reader.Send(msg))
                {
                    str += "GPO4设置成功";
                }
                else
                    str += msg.ErrorInfo.ErrMsg;
            }
            if (str == "")
                str = "请勾选输出端口进行设置";
            MessageBox.Show(str);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            MsgFilteringTimeConfig msg = new MsgFilteringTimeConfig();
            if (reader.Send(msg))
            {
                numericUpDown2.Value = msg.ReceivedMessage.Time;
            }
            else
            {
                if (!isFormLoad)
                    MessageBox.Show(msg.ErrorInfo.ErrMsg);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            MsgFilteringTimeConfig msg = new MsgFilteringTimeConfig((ushort)numericUpDown2.Value);
            if (reader.Send(msg))
            {
                MessageBox.Show("操作成功");
            }
            else
                MessageBox.Show(msg.ErrorInfo.ErrMsg);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            MsgIntervalTimeConfig msg = new MsgIntervalTimeConfig();
            if (reader.Send(msg))
            {
                numericUpDown3.Value = msg.ReceivedMessage.ReadTime;
                numericUpDown4.Value = msg.ReceivedMessage.StopTime;
            }
            else
            {
                if (!isFormLoad)
                    MessageBox.Show(msg.ErrorInfo.ErrMsg);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            MsgIntervalTimeConfig msg = new MsgIntervalTimeConfig((ushort)numericUpDown3.Value, (ushort)numericUpDown4.Value);
            if (reader.Send(msg))
            {
                MessageBox.Show("操作成功");
            }
            else
                MessageBox.Show(msg.ErrorInfo.ErrMsg);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            MsgRssiThresholdConfig msg = new MsgRssiThresholdConfig();
            if (reader.Send(msg))
            {
                numericUpDown5.Value = msg.ReceivedMessage.RSSI;
            }
            else
            {
                if (!isFormLoad)
                    MessageBox.Show(msg.ErrorInfo.ErrMsg);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            MsgRssiThresholdConfig msg = new MsgRssiThresholdConfig((byte)numericUpDown5.Value);
            if (reader.Send(msg))
            {
                MessageBox.Show("操作成功");
            }
            else
                MessageBox.Show(msg.ErrorInfo.ErrMsg);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            // 频段
            MsgUhfBandConfig msg = new MsgUhfBandConfig();
            if (reader.Send(msg))
            {
                comboBox7.SelectedIndex = (int)msg.ReceivedMessage.UhfBand;
            }
            else
            {
                if (!isFormLoad)
                    MessageBox.Show(msg.ErrorInfo.ErrMsg);
            }

            // 频率
            MsgFrequencyConfig msg1 = new MsgFrequencyConfig();
            if (reader.Send(msg1))
            {
                checkBox3.Checked = msg1.ReceivedMessage.FreqInfo.IsAutoSet;
                byte[] ft = msg1.ReceivedMessage.FreqInfo.FreqTable;
                listBox1.ClearSelected();
                foreach (byte f in ft)
                {
                    listBox1.SetSelected(f, true);
                }
            }
            else
            {
                if (!isFormLoad)
                    MessageBox.Show(msg1.ErrorInfo.ErrMsg);
            }

        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (comboBox7.SelectedIndex == -1)
            {
                MessageBox.Show("请选择频段");
                return;
            }
            if (!checkBox3.Checked && listBox1.SelectedItems.Count == 0)
            {
                MessageBox.Show("请设置频率");
                return;
            }
            string str = "";
            MsgUhfBandConfig msg = new MsgUhfBandConfig((FrequencyArea)comboBox7.SelectedIndex);
            if (reader.Send(msg))
            {
                str = "频段设置成功\r\n";
            }
            else
                str = "频段设置失败：" + msg.ErrorInfo.ErrMsg + "\r\n";

            FrequencyTable ft = new FrequencyTable();
            ft.IsAutoSet = checkBox3.Checked;
            List<byte> lst = new List<byte>();
            if (checkBox3.Checked)
            {
                lst.Add(0x00);//自动的话，频点表无效，但要有；所以这里放一个0进去；不然报错
            }
            else
            {
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    if (listBox1.GetSelected(i))
                        lst.Add((byte)i);
                }
            }
            ft.FreqTable = lst.ToArray();
            MsgFrequencyConfig msg1 = new MsgFrequencyConfig(ft);
            if (reader.Send(msg1))
            {
                str += "频率设置成功";
            }
            else
                str += "频率设置失败：" + msg1.ErrorInfo.ErrMsg;
            MessageBox.Show(str);
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            UhfBandTable lst = new UhfBandTable((UhfBandTable.Name)comboBox7.SelectedIndex);
            foreach (string f in lst.List)
            {
                listBox1.Items.Add(f);
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            MsgAirProtocolConfig msg = new MsgAirProtocolConfig();
            if (reader.Send(msg))
            {
                comboBox8.SelectedIndex = (int)msg.ReceivedMessage.Protocol;
            }
            else
            {
                if (!isFormLoad)
                    MessageBox.Show(msg.ErrorInfo.ErrMsg);
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (comboBox8.SelectedIndex == -1)
            {
                MessageBox.Show("请选择设置项");
                return;
            }
            MsgAirProtocolConfig msg = new MsgAirProtocolConfig((AirProtocol)comboBox8.SelectedIndex);
            if (reader.Send(msg))
            {
                MessageBox.Show("操作成功");
            }
            else
                MessageBox.Show(msg.ErrorInfo.ErrMsg);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            MsgResetToFactoryDefault msg = new MsgResetToFactoryDefault();
            if (reader.Send(msg))
            {
                MessageBox.Show("操作成功");
            }
            else
                MessageBox.Show(msg.ErrorInfo.ErrMsg);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            MsgIdleTimeConfig msg = new MsgIdleTimeConfig();
            if (reader.Send(msg))
            {
                numericUpDown6.Value = msg.ReceivedMessage.Time;
            }
            else
            {
                if (!isFormLoad)
                    MessageBox.Show(msg.ErrorInfo.ErrMsg);
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            MsgIdleTimeConfig msg = new MsgIdleTimeConfig((ushort)numericUpDown6.Value);
            if (reader.Send(msg))
            {
                MessageBox.Show("操作成功");
            }
            else
                MessageBox.Show(msg.ErrorInfo.ErrMsg);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            MsgUtcConfig msg = new MsgUtcConfig();
            if (reader.Send(msg))
            {
                textBox13.Text = msg.ReceivedMessage.UTC.ToString("HH:mm:ss.fff");
            }
            else
            {
                if (!isFormLoad)
                    MessageBox.Show(msg.ErrorInfo.ErrMsg);
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            MsgUtcConfig msg = new MsgUtcConfig(DateTime.UtcNow);
            if (reader.Send(msg))
            {
                textBox13.Text = DateTime.UtcNow.ToString("HH:mm:ss.fff");
                MessageBox.Show("操作成功");
            }
            else
                MessageBox.Show(msg.ErrorInfo.ErrMsg);
        }

        private void FormReaderTest_FormClosing(object sender, FormClosingEventArgs e)
        {
            reader.OnFirmwareOnlineUpgradeReceived -= Reader_OnFirmwareOnlineUpgradeReceived;
            reader.OnAppFirmwareOnlineUpgradeReceived -= Reader_OnAppFirmwareOnlineUpgradeReceived;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            listBox1.Visible = !checkBox3.Checked;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            string err = "";
            MsgQValueConfig msg = new MsgQValueConfig();
            if (reader.Send(msg))
            {
                numericUpDown7.Value = (int)msg.ReceivedMessage.Q;
            }
            else
                err += msg.ErrorInfo.ErrMsg + "\r\n";               

            MsgSessionConfig msg1 = new MsgSessionConfig();
            if (reader.Send(msg1))
            {
                comboBox15.SelectedIndex = (int)msg1.ReceivedMessage.SessionParam.Session;
                comboBox16.SelectedIndex = (int)msg1.ReceivedMessage.SessionParam.Flag;
            }
            else
                err += msg1.ErrorInfo.ErrMsg;
            if(err != "")
            {
                if (!isFormLoad)
                    MessageBox.Show(msg.ErrorInfo.ErrMsg);
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            string err = "";
            MsgQValueConfig msg = new MsgQValueConfig((byte)numericUpDown7.Value);
            if (reader.Send(msg))
            {
                err = "Q值设置成功。\r\n";
            }
            else
                err += "Q值设置失败" + msg.ErrorInfo.ErrMsg + "\r\n";

            SessionInfo si = new SessionInfo();
            si.Session = (Session)comboBox15.SelectedIndex;
            si.Flag = (Flag)comboBox16.SelectedIndex;
            MsgSessionConfig msg1 = new MsgSessionConfig(si);
            if (reader.Send(msg1))
            {
                err += "Session设置成功。\r\n";
            }
            else
                err += "Session设置失败" + msg1.ErrorInfo.ErrMsg + "\r\n";
            MessageBox.Show(err);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            string err = "";
            MsgKeepAliveConfig msg = new MsgKeepAliveConfig();
            if (reader.Send(msg))
            {
                checkBox10.Checked = msg.ReceivedMessage.IsEnable;
                numericUpDown8.Value = (int)msg.ReceivedMessage.Interval;
            }
            else
                err += msg.ErrorInfo.ErrMsg + "\r\n";

            if (err != "")
            {
                if (!isFormLoad)
                    MessageBox.Show(msg.ErrorInfo.ErrMsg);
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            string err = "";
            MsgKeepAliveConfig msg = new MsgKeepAliveConfig(checkBox10.Checked,(ushort)numericUpDown8.Value);
            if (reader.Send(msg,2000))
            {
                err = "心跳指令设置成功。\r\n";
            }
            else
                err += "心跳指令设置失败" + msg.ErrorInfo.ErrMsg + "\r\n";
            
            MessageBox.Show(err);
        }

    }
}
