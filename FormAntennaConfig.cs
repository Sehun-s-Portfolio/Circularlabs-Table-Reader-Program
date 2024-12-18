using NetAPI.Protocol.VRP;
using NetAPI.Entities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace RFIDTest
{
    public partial class FormAntennaConfig : Form
    {
        Reader reader;
        byte antCount;
        byte minPower;
        byte maxPower;

        public FormAntennaConfig(Reader reader)
        {
            InitializeComponent();
            this.reader = reader;
        }

        private void FormScanConfig_Load(object sender, EventArgs e)
        {
            MsgReaderCapabilityQuery msg = new MsgReaderCapabilityQuery();
            if(reader.Send(msg))
            {
                antCount = msg.ReceivedMessage.AntennaCount;
                minPower = msg.ReceivedMessage.MinPowerValue;
                maxPower = msg.ReceivedMessage.MaxPowerValue;

                if (antCount > 32)
                    antCount = 32;//先做限制，与界面保持一致
                for (int i = 1; i <= 32; i++)
                {                    
                    ComboBox cb = (ComboBox)groupBox2.Controls.Find("cbAnt" + i, true)[0];
                    cb.Items.Clear();
                    for(int j= minPower;j<=maxPower;j++ )
                    {
                        cb.Items.Add(j.ToString());
                    }
                    if (i > antCount)
                    {
                        cb.Visible = false;
                        CheckBox ckb = (CheckBox)groupBox2.Controls.Find("ckbAnt" + i, true)[0];
                        ckb.Visible = false;
                        Label lbl = (Label)groupBox2.Controls.Find("lblAnt" + i, true)[0];
                        lbl.Visible = false;
                    }
                }
            }
            query();
        }

        private void query()
        {
            #region 查天线状态           
            MsgRfidStatusQuery msg = new MsgRfidStatusQuery();
            if (reader.Send(msg))
            {
                foreach (AntennaPowerStatus a in msg.ReceivedMessage.Antennas)
                {
                    if (a.AntennaNO > 32)
                        break;//与界面保持一致
                    CheckBox ckb = (CheckBox)groupBox2.Controls.Find("ckbAnt" + a.AntennaNO.ToString(), true)[0];
                    ckb.Checked = a.IsEnable;
                    ComboBox cb = (ComboBox)groupBox2.Controls.Find("cbAnt" + a.AntennaNO.ToString(), true)[0];
                    cb.Text = a.PowerValue.ToString();
                }
            }                
            #endregion
        }

        private void ckbAnt_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = (CheckBox)sender;
            ComboBox cb = this.Controls.Find(ckb.Name.Replace("k", ""), true)[0] as ComboBox;
            cb.Enabled = ckb.Checked;
            
            if (!isClick)
                ckbAll.CheckState = getCheckState();
        }

       
        private void btnConfig_Click(object sender, EventArgs e)
        {
            string errStr = "";
            AntennaStatus[] ants = new AntennaStatus[antCount];
            byte[] ps = new byte[antCount];
            for (int i = 1; i <= antCount; i++)
            {
                ants[i - 1] = new AntennaStatus();
                ants[i - 1].AntennaNO = (byte)i;
                CheckBox ckb = (CheckBox)groupBox2.Controls.Find("ckbAnt" + i, true)[0];
                ComboBox cb = (ComboBox)groupBox2.Controls.Find("cbAnt" + i, true)[0];
                ants[i - 1].IsEnable = ckb.Checked;
                ps[i - 1] = byte.Parse(cb.Text);
            }


            #region 配置
            MsgAntennaConfig aMsg = new MsgAntennaConfig(ants);
            if (reader.Send(aMsg, 3000))
                errStr += "天线启用设置成功。\r\n";
            else
                errStr += "天线启用设置失败！" + aMsg.ErrorInfo.ErrMsg + "\r\n";
            MsgPowerConfig pMsg = new MsgPowerConfig(ps);
            if (reader.Send(pMsg, 3000))
                errStr += "天线功率设置成功。";
            else
                errStr += "天线功率设置失败！" + pMsg.ErrorInfo.ErrMsg;
            #endregion
            MessageBox.Show(errStr);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {            
            this.Close();
        }

        private void btnAllConfig_Click(object sender, EventArgs e)
        {
            if(cbAll.SelectedIndex == -1)
            {
                MessageBox.Show("설정 전력의 크기를 선택하십시오");
                return;
            }
            if (DialogResult.Cancel == MessageBox.Show("안테나 전력을 대량으로 구성하시겠습니까?", "시스템 힌트", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                return;
            }
            string errStr = "";
            byte[] ps = new byte[antCount];
            for (int i = 1; i <= antCount; i++)
            {               
                ps[i - 1] = byte.Parse(cbAll.Text);
            }

            #region 配置
            MsgPowerConfig pMsg = new MsgPowerConfig(ps);
            if (reader.Send(pMsg, 3000))
                errStr += "天线功率设置成功。";
            else
                errStr += "天线功率设置失败！" + pMsg.ErrorInfo.ErrMsg;
            #endregion
            query();
            MessageBox.Show(errStr);
        }

        volatile bool isClick;
        private void ckbAll_Click(object sender, EventArgs e)
        {
            isClick = true;
            for (int i = 1; i <= antCount; i++)
            {
                Control[] ctrl = groupBox2.Controls.Find("ckbAnt" + i, false);
                if (ctrl != null && ctrl.Length > 0)
                {
                    CheckBox ckb = (CheckBox)ctrl[0];
                    ckb.Checked = ckbAll.Checked;
                }
            }
            isClick = false;
        }

        private void ckbAlarm_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = (CheckBox)sender;
            string i = ckb.Name.Replace("ckbAnt", "");
            Control[] ctrl = groupBox2.Controls.Find("gbAlarm" + i, false);
            if (ctrl != null && ctrl.Length > 0)
            {
                ctrl[0].Enabled = ckb.Checked;
            }
            if (!isClick)
                ckbAll.CheckState = getCheckState();
        }

        private CheckState getCheckState()
        {
            CheckState cs = CheckState.Unchecked;
            bool isChk = false;
            bool isUnChk = false;
            for (int i = 1; i <= antCount; i++)
            {
                Control[] ctrl = groupBox2.Controls.Find("ckbAnt" + i, false);
                if (ctrl != null && ctrl.Length > 0)
                {
                    CheckBox ckb = (CheckBox)ctrl[0];
                    if (ckb.Checked)
                        isChk = true;
                    else
                        isUnChk = true;
                }
            }
            if (isChk && isUnChk)
                cs = CheckState.Indeterminate;
            else if (isChk)
                cs = CheckState.Checked;
            else
                cs = CheckState.Unchecked;
            return cs;
        }
    }
}
