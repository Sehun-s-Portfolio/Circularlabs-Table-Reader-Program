using System;
using System.Windows.Forms;
using System.Xml;
using NetAPI;

namespace RFIDTest
{
    public partial class FormConn : Form
    {
        DeviceCfg cfg = new DeviceCfg();
        DeviceCfgItem cfgItem = null;

        public FormConn()
        {
            InitializeComponent();
            cfgItem = cfg.FindReaderItem("Device1");
        }

        private void FormConn_Load(object sender, EventArgs e)
        {
            // 加载串口
            string[] comPorts = System.IO.Ports.SerialPort.GetPortNames();
            foreach (string s in comPorts)
            {
                cbPort.Items.Add(s);
            }
            if (cfgItem != null)
            {
                switch (cfgItem.PortType)
                {
                    case "RS232":
                        {
                            rbCom.Checked = true;
                            string[] sAry = cfgItem.ConnStr.Split(',');
                            cbPort.Text = sAry[0];
                            cbBaud.Text = sAry[1];
                            cbRs485.Checked = false;
                        }
                        break;                    
                    case "TcpClient":
                        {
                            rbTcp.Checked = true;
                            string[] sAry = cfgItem.ConnStr.Split(':');
                            txtIp.Text = sAry[0];
                            numPort.Value = int.Parse(sAry[1]);
                        }
                        break;
                }
            }           
        }

        private void btnConn_Click(object sender, EventArgs e)
        {
            saveConfig();
            this.DialogResult = DialogResult.OK;
        }

        private void saveConfig()
        {           
            if (rbCom.Checked)
            {
                cfgItem.PortType = "RS232";
                cfgItem.ConnStr = cbPort.Text + "," + cbBaud.Text;
               
            }
            else
            {
                cfgItem.PortType = "TcpClient";
                cfgItem.ConnStr = txtIp.Text + ":" + numPort.Value;
            }
            cfg.ModifyReaderItem(cfgItem.ReaderName,cfgItem);
        }

        private void btnDisconn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbPortType_CheckedChanged(object sender, EventArgs e)
        {
            gbTcp.Enabled = rbTcp.Checked;
            gbCom.Enabled = rbCom.Checked;
        }

        private void cbRs485_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = cbRs485.Checked;
        }
    }
}
