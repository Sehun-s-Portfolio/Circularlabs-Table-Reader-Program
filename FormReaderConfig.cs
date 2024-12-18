using NetAPI.Protocol.VRP;
using NetAPI.Entities;
using System;
using System.Threading;
using System.Windows.Forms;
using NetAPI;

namespace RFIDTest
{
    public partial class FormReaderConfig : Form
    {
        Reader reader;
        string baudRate;
        string ip;
        string mask;
        string gate;
        FormMain frm;

        public FormReaderConfig(Reader reader, FormMain frm)
        {
            InitializeComponent();
            this.frm = frm;
            this.reader = reader;
        }
        private void FormReaderConfig_Load(object sender, EventArgs e)
        {           
            btnQuery_Click(null, EventArgs.Empty);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {            
            MsgIpAddressConfig ipQuery = new MsgIpAddressConfig();
            if (reader.Send(ipQuery))
            {
                ip = txtIp.Text = ipQuery.ReceivedMessage.IP;
                mask = txtSubnet.Text = ipQuery.ReceivedMessage.Subnet;
                gate = txtGateway.Text = ipQuery.ReceivedMessage.Gateway;
            }
            else
            {
                groupBox1.Enabled = false;
                MessageBox.Show(ipQuery.ErrorInfo.ErrMsg);
            }

            MsgRs232BaudRateConfig rs232Query = new MsgRs232BaudRateConfig();
            if(reader.Send(rs232Query))
            {
                baudRate = cbBaud.Text = rs232Query.ReceivedMessage.RS232BaudRate.ToString().Substring(1);
            }
            else
            {
                groupBox2.Enabled = false;
                MessageBox.Show(rs232Query.ErrorInfo.ErrMsg);
            }

            //MsgRs485BaudRateConfig rs485Query = new MsgRs485BaudRateConfig();
            //if (reader.Send(rs485Query))
            //{
            //    cbRs485Baudrate.Text = rs485Query.ReceivedMessage.RS485BaudRate.ToString().Substring(1);
            //}
            //else
            //{
            //    groupBox3.Enabled = false;
            //    MessageBox.Show(rs485Query.ErrorInfo.ErrMsg);
            //}

            //MsgRs485AddressConfig rs485AddrQuery = new MsgRs485AddressConfig();
            //if (reader.Send(rs485AddrQuery))
            //{
            //    numRs485Address.Value = rs485AddrQuery.ReceivedMessage.Address;
            //}
            //else
            //{
            //    groupBox3.Enabled = false;
            //    MessageBox.Show(rs485AddrQuery.ErrorInfo.ErrMsg);
            //}
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            string str = "";
            //string errStr = "";
            bool isRst = false;

            if (cbBaud.SelectedIndex != -1)
            {
                if(cbBaud.Text != baudRate)
                {
                    NetAPI.BaudRate b = (NetAPI.BaudRate)Enum.Parse(typeof(NetAPI.BaudRate), "R" + cbBaud.Text);
                    MsgRs232BaudRateConfig msg = new MsgRs232BaudRateConfig(b);
                    if (reader.Send(msg))
                    {
                        str += "RS232串口参数设置成功\r\n";
                        baudRate = cbBaud.Text;
                        if (reader.CommPort is Rs232Port)
                            isRst = true;
                    }                        
                    else
                        str += "RS232串口参数设置失败：" + msg.ErrorInfo.ErrMsg + "\r\n";
                }                
            }
            else
            {
                str += "RS232串口参数设置不正确" ;
            }            

            //if (cbRs485Baudrate.SelectedIndex != -1)
            //{
            //    NetAPI.BaudRate b = (NetAPI.BaudRate)Enum.Parse(typeof(NetAPI.BaudRate), "R" + cbRs485Baudrate.Text);
            //    MsgRs485BaudRateConfig msg = new MsgRs485BaudRateConfig(b);
            //    if (reader.Send(msg))
            //        str += "RS485串口参数设置成功\r\n";
            //    else
            //        str += "RS485串口参数设置失败：" + msg.ErrorInfo.ErrMsg + "\r\n";
            //}
            //else
            //{
            //    str += "RS485串口参数设置不正确\r\n";
            //}

            //{
            //    MsgRs485AddressConfig msg = new MsgRs485AddressConfig((byte)numRs485Address.Value);
            //    if (reader.Send(msg))
            //        str += "RS485串口地址参数设置成功\r\n";
            //    else
            //        str += "RS485串口地址参数设置失败：" + msg.ErrorInfo.ErrMsg + "\r\n";
            //} 

            if (groupBox1.Enabled)
            {
                if (Common.IsIP(txtIp.Text.Trim()) && Common.IsIP(txtSubnet.Text.Trim()) && Common.IsIP(txtGateway.Text.Trim()))
                {
                    if(ip != txtIp.Text.Trim() || mask != txtSubnet.Text.Trim() || gate != txtGateway.Text.Trim())
                    {
                        MsgIpAddressConfig msg = new MsgIpAddressConfig(txtIp.Text.Trim(), txtSubnet.Text.Trim(), txtGateway.Text.Trim());
                        if (reader.Send(msg))
                        {
                            str += "网口参数设置成功\r\n";
                            ip = txtIp.Text.Trim();
                            mask = txtSubnet.Text.Trim();
                            gate = txtGateway.Text.Trim();
                            if (reader.CommPort is TcpClientPort)
                                isRst = true;
                        }
                        else
                            str += "网口参数设置失败：" + msg.ErrorInfo.ErrMsg + "\r\n";
                    }
                }
                else
                {
                    MessageBox.Show("网口参数不合法，请检查！");
                    return;
                }
            }
            if (str == "")
                str = "无配置更改";
            MessageBox.Show(str);
            if (isRst)
            {
                frm.btnConn_Click(null, EventArgs.Empty);
                this.Close();
            }
        }
    }
}
