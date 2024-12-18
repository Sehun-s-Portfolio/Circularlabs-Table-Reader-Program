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
using System.Text.RegularExpressions;
using NetAPI;

namespace RFIDTest
{
    public partial class FormAppCpuTest : Form
    {
        Reader reader;
        bool isFormLoad = true;

        public FormAppCpuTest(Reader reader)
        {
            InitializeComponent();
            this.reader = reader;
        }

        private void FormAppCpuTest_Load(object sender, EventArgs e)
        {
            button1_Click(sender, e);
            button3_Click(sender, e);
            button6_Click(sender, e);
            button4_Click(sender, e);
            button8_Click(sender, e);
            button10_Click(sender, e);
            button12_Click(sender, e);
            isFormLoad = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MsgTcpModeConfig msg = new MsgTcpModeConfig();
            if (reader.Send(msg))
            {
                switch(msg.ReceivedMessage.Mode)
                {
                    case ReaderTcpMode.Server:
                        radioButton1.Checked = true;
                        break;
                    case ReaderTcpMode.Client:
                        radioButton2.Checked = true;
                        break;
                }
                numericUpDown1.Value = msg.ReceivedMessage.ServerPortNO;
            }
            else
            {
                if (!isFormLoad)
                    MessageBox.Show(msg.ErrorInfo.ErrMsg);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked || radioButton2.Checked)
            {
                ReaderTcpMode m = (radioButton2.Checked) ? ReaderTcpMode.Client : ReaderTcpMode.Server;
                MsgTcpModeConfig msg = new MsgTcpModeConfig(m, (ushort)numericUpDown1.Value);
                if (reader.Send(msg))
                {                    
                    MessageBox.Show("동작 성공");
                }
                else
                    MessageBox.Show(msg.ErrorInfo.ErrMsg);
            }
            else
                MessageBox.Show("설정 선택");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MsgMacConfig msg = new MsgMacConfig();
            if (reader.Send(msg))
            {
                string macStr = "";
                byte[] mac = msg.ReceivedMessage.MAC;
                foreach(byte b in mac)
                {
                    macStr += b.ToString("X2") + ":";
                }
                textBox1.Text = macStr.Trim(new char[] { ':' });
            }
            else
            {
                if (!isFormLoad)
                    MessageBox.Show(msg.ErrorInfo.ErrMsg);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {            
            string macReg = @"^([0-9a-fA-F]{2})(([\s:-]{0,1}[0-9a-fA-F]{2}){5})$";
            string mac = textBox1.Text.Trim();
            if (mac != "" && Regex.IsMatch(mac, macReg))
            {
                Regex r = new Regex(@"[\s:-]");
                mac = r.Replace(mac, "");
                if(MessageBox.Show("确定改写MAC地址？","系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MsgMacConfig msg = new MsgMacConfig(Util.ConvertHexstringTobyteArray(mac));
                    if (reader.Send(msg))
                    {
                        MessageBox.Show("操作成功");
                    }
                    else
                        MessageBox.Show(msg.ErrorInfo.ErrMsg);
                }                
            }
            else
                MessageBox.Show("请正确设置MAC地址");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MsgWifiMacConfig msg = new MsgWifiMacConfig();
            if (reader.Send(msg))
            {
                string macStr = "";
                byte[] mac = msg.ReceivedMessage.MAC;
                foreach (byte b in mac)
                {
                    macStr += b.ToString("X2") + ":";
                }
                textBox2.Text = macStr.Trim(new char[] { ':' });
            }
            else
            {
                if (!isFormLoad)
                    MessageBox.Show(msg.ErrorInfo.ErrMsg);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            string macReg = @"^([0-9a-fA-F]{2})(([\s:-]{0,1}[0-9a-fA-F]{2}){5})$";
            string mac = textBox2.Text.Trim();
            if (mac != "" && Regex.IsMatch(mac, macReg))
            {
                Regex r = new Regex(@"[\s:-]");
                mac = r.Replace(mac, "");
                if (MessageBox.Show("确定改写MAC地址？", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MsgWifiMacConfig msg = new MsgWifiMacConfig(Util.ConvertHexstringTobyteArray(mac));
                    if (reader.Send(msg))
                    {
                        MessageBox.Show("操作成功");
                    }
                    else
                        MessageBox.Show(msg.ErrorInfo.ErrMsg);
                }
            }
            else
                MessageBox.Show("请正确设置MAC地址");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MsgWifiModeConfig msg = new MsgWifiModeConfig();
            if (reader.Send(msg))
            {
                switch (msg.ReceivedMessage.Mode)
                {
                    case ReaderTcpMode.Server:
                        radioButton4.Checked = true;
                        break;
                    case ReaderTcpMode.Client:
                        radioButton3.Checked = true;
                        break;
                }
                numericUpDown2.Value = msg.ReceivedMessage.ServerPortNO;
            }
            else
            {
                if (!isFormLoad)
                    MessageBox.Show(msg.ErrorInfo.ErrMsg);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (radioButton4.Checked || radioButton3.Checked)
            {
                ReaderTcpMode m = (radioButton3.Checked) ? ReaderTcpMode.Client : ReaderTcpMode.Server;
                MsgWifiModeConfig msg = new MsgWifiModeConfig(m, (ushort)numericUpDown2.Value);
                if (reader.Send(msg))
                {
                    MessageBox.Show("操作成功");
                }
                else
                    MessageBox.Show(msg.ErrorInfo.ErrMsg);
            }
            else
                MessageBox.Show("请选择设置");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MsgWifiIpAddressConfig ipQuery = new MsgWifiIpAddressConfig();
            if (reader.Send(ipQuery))
            {
                txtIp.Text = ipQuery.ReceivedMessage.IP;
                txtSubnet.Text = ipQuery.ReceivedMessage.Subnet;
                txtGateway.Text = ipQuery.ReceivedMessage.Gateway;
            }
            else
            {
                groupBox5.Enabled = false;
                if (!isFormLoad)
                    MessageBox.Show(ipQuery.ErrorInfo.ErrMsg);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string str = "";
            if (Common.IsIP(txtIp.Text.Trim()) && Common.IsIP(txtSubnet.Text.Trim()) && Common.IsIP(txtGateway.Text.Trim()))
            {
                MsgWifiIpAddressConfig msg = new MsgWifiIpAddressConfig(txtIp.Text.Trim(), txtSubnet.Text.Trim(), txtGateway.Text.Trim());
                if (reader.Send(msg))
                    str = "网口参数设置成功\r\n";
                else
                    str = "网口参数设置失败：" + msg.ErrorInfo.ErrMsg + "\r\n";
                MessageBox.Show(str);
            }
            else
            {
                MessageBox.Show("网口参数不合法，请检查！");
                return;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MsgWifiHotspotConfig msg = new MsgWifiHotspotConfig();
            if (reader.Send(msg))
            {
                textBox3.Text = msg.ReceivedMessage.SSID;
                textBox5.Text = msg.ReceivedMessage.Password;
            }
            else
            {
                if (!isFormLoad)
                    MessageBox.Show(msg.ErrorInfo.ErrMsg);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string str = "";
            if (textBox3.Text.Trim() != "" && textBox5.Text.Trim() != "")
            {
                MsgWifiHotspotConfig msg = new MsgWifiHotspotConfig(textBox3.Text.Trim(), textBox5.Text.Trim());
                if (reader.Send(msg))
                    str = "热点参数设置成功\r\n";
                else
                    str = "热点参数设置失败：" + msg.ErrorInfo.ErrMsg + "\r\n";
                MessageBox.Show(str);
            }
            else
            {
                MessageBox.Show("热点参数不合法，请检查！");
                return;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            MsgWifiStatusConfig msg = new MsgWifiStatusConfig();
            if (reader.Send(msg))
            {
                comboBox2.SelectedIndex = msg.ReceivedMessage.Status;
            }
            else
            {
                if (!isFormLoad)
                    MessageBox.Show(msg.ErrorInfo.ErrMsg);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string str = "";
            if (comboBox1.SelectedIndex != -1)
            {
                MsgWifiStatusConfig msg = new MsgWifiStatusConfig((byte)comboBox1.SelectedIndex);
                if (reader.Send(msg))
                    str = "设置成功\r\n";
                else
                    str = "设置失败：" + msg.ErrorInfo.ErrMsg + "\r\n";
                MessageBox.Show(str);
            }
            else
            {
                MessageBox.Show("请选择设置参数！");
                return;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            MsgMasterAndSlaveModeConfig msg = new MsgMasterAndSlaveModeConfig();
            if (reader.Send(msg))
            {
                switch (msg.ReceivedMessage.WorkMode)
                {
                    case ReaderWorkMode.Master:
                        radioButton6.Checked = true;
                        break;
                    case ReaderWorkMode.Slave:
                        radioButton5.Checked = true;
                        break;
                }
            }
            else
                MessageBox.Show(msg.ErrorInfo.ErrMsg);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (radioButton6.Checked || radioButton5.Checked)
            {
                ReaderWorkMode m = (radioButton6.Checked) ? ReaderWorkMode.Master : ReaderWorkMode.Slave;
                MsgMasterAndSlaveModeConfig msg = new MsgMasterAndSlaveModeConfig(m);
                if (reader.Send(msg))
                {
                    MessageBox.Show("操作成功");
                }
                else
                    MessageBox.Show(msg.ErrorInfo.ErrMsg);
            }
            else
                MessageBox.Show("请选择设置");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            MsgSlaveModeCacheClean msg = new MsgSlaveModeCacheClean();
            if (reader.Send(msg))
            {
                MessageBox.Show("缓存清空完成");
            }
            else
                MessageBox.Show(msg.ErrorInfo.ErrMsg);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            MsgSlaveModeCacheCountQuery msg = new MsgSlaveModeCacheCountQuery();
            if (reader.Send(msg))
            {
                textBox4.Text = msg.ReceivedMessage.NumberOfCaches.ToString();
            }
            else
                MessageBox.Show(msg.ErrorInfo.ErrMsg);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            MsgSlaveModeCacheQuery msg = new MsgSlaveModeCacheQuery((int)numericUpDown3.Value);
            if (reader.Send(msg))
            {
                MessageBox.Show("数据上传开始，请在主窗体数据列表中查看数据");
            }
            else
                MessageBox.Show(msg.ErrorInfo.ErrMsg);
        }
    }
}
