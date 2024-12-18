using NetAPI.Entities;
using NetAPI.Protocol.VRP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetAPI;

namespace RFIDTest
{
    public partial class FormPortalConfig : Form
    {
        Reader reader;
        bool isFormLoad = true;

        public FormPortalConfig(Reader reader)
        {
            InitializeComponent();
            this.reader = reader;
        }

        private void FormPortalConfig_Load(object sender, EventArgs e)
        {
            button5_Click(null, EventArgs.Empty);
            comboBox1.SelectedIndex = 0;
            button8_Click(null, EventArgs.Empty);
            button3_Click(null, EventArgs.Empty);
            button11_Click(null, EventArgs.Empty);
            ckbAll.Checked = true;
            ckbAll_Click(null, EventArgs.Empty);
            button1_Click(null, EventArgs.Empty);
            isFormLoad = false;
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
                MessageBox.Show(msg.ErrorInfo.ErrMsg);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MsgWorkModeConfig_AccessControl msg = new MsgWorkModeConfig_AccessControl();
            if (reader.Send(msg))
            {
                radioButton1.Checked = (msg.ReceivedMessage.WorkMode == WorkMode_AccessControl.OffLine);
                radioButton2.Checked = (msg.ReceivedMessage.WorkMode == WorkMode_AccessControl.OnLine);
            }
            else
            {
                if (!isFormLoad)
                    MessageBox.Show(msg.ErrorInfo.ErrMsg);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(!radioButton1.Checked && !radioButton2.Checked)
            {
                MessageBox.Show("请选择设置参数");
                return;
            }
            WorkMode_AccessControl ctrl = WorkMode_AccessControl.OffLine;
            if (radioButton2.Checked)
                ctrl = WorkMode_AccessControl.OnLine;
            MsgWorkModeConfig_AccessControl msg = new MsgWorkModeConfig_AccessControl(ctrl);
            if (reader.Send(msg))
                MessageBox.Show("配置成功！");
            else
                MessageBox.Show("配置失败：" + msg.ErrorInfo.ErrMsg);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            MsgGpioConfig_AccessControl msg = new MsgGpioConfig_AccessControl();
            if (reader.Send(msg))
            {
                comboBox2.SelectedIndex = msg.ReceivedMessage.LegalTag.IsEnable ? 0 : 1;
                comboBox5.SelectedIndex = msg.ReceivedMessage.LegalTag.GpioPort - 1;
                comboBox7.SelectedIndex = (int)msg.ReceivedMessage.LegalTag.Level;
                comboBox3.SelectedIndex = msg.ReceivedMessage.IllegalTag.IsEnable ? 0 : 1;
                comboBox6.SelectedIndex = msg.ReceivedMessage.IllegalTag.GpioPort - 1;
                comboBox8.SelectedIndex = (int)msg.ReceivedMessage.IllegalTag.Level;
            }
            else
            {
                if (!isFormLoad)
                    MessageBox.Show(msg.ErrorInfo.ErrMsg);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if(comboBox2.SelectedIndex == -1 || comboBox3.SelectedIndex == -1)
            {
                MessageBox.Show("请选择设置参数");
                return;
            }
            GpioConfig_AccessControl infoState = new GpioConfig_AccessControl();
            infoState.IsEnable = (comboBox2.SelectedIndex == 0);
            infoState.GpioPort = (byte)(comboBox5.SelectedIndex + 1);
            infoState.Level = (GpioLevel)comboBox7.SelectedIndex;
            GpioConfig_AccessControl alarmState = new GpioConfig_AccessControl();
            alarmState.IsEnable = (comboBox3.SelectedIndex == 0);
            alarmState.GpioPort = (byte)(comboBox6.SelectedIndex + 1);
            alarmState.Level = (GpioLevel)comboBox8.SelectedIndex;
            MsgGpioConfig_AccessControl msg = new MsgGpioConfig_AccessControl(infoState, alarmState);
            if (reader.Send(msg))
                MessageBox.Show("配置成功！");
            else
                MessageBox.Show("配置失败：" + msg.ErrorInfo.ErrMsg);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string err = "";
            if (ckbAlarm1.Checked)
            {
                MsgEpcAlarmConfig_AccessControl msg = new MsgEpcAlarmConfig_AccessControl(0x01);
                if (reader.Send(msg))
                {
                    ckbAlarmEnable1.Checked = msg.ReceivedMessage.EpcAlarmConfig.IsEnable;
                    textBox1.Text = Util.ConvertbyteArrayToHexWordstring(msg.ReceivedMessage.EpcAlarmConfig.Epc);
                    textBox2.Text = Util.ConvertbyteArrayToHexWordstring(msg.ReceivedMessage.EpcAlarmConfig.Mask);
                }
                else
                    err += "1#：" + msg.ErrorInfo.ErrMsg;
            }
            if (ckbAlarm2.Checked)
            {
                MsgEpcAlarmConfig_AccessControl msg = new MsgEpcAlarmConfig_AccessControl(0x02);
                if (reader.Send(msg))
                {
                    ckbAlarmEnable2.Checked = msg.ReceivedMessage.EpcAlarmConfig.IsEnable;
                    textBox9.Text = Util.ConvertbyteArrayToHexWordstring(msg.ReceivedMessage.EpcAlarmConfig.Epc);
                    textBox10.Text = Util.ConvertbyteArrayToHexWordstring(msg.ReceivedMessage.EpcAlarmConfig.Mask);
                }
                else
                    err += "\r\n2#：" + msg.ErrorInfo.ErrMsg;
            }
            if (ckbAlarm3.Checked)
            {
                MsgEpcAlarmConfig_AccessControl msg = new MsgEpcAlarmConfig_AccessControl(0x03);
                if (reader.Send(msg))
                {
                    ckbAlarmEnable3.Checked = msg.ReceivedMessage.EpcAlarmConfig.IsEnable;
                    textBox11.Text = Util.ConvertbyteArrayToHexWordstring(msg.ReceivedMessage.EpcAlarmConfig.Epc);
                    textBox12.Text = Util.ConvertbyteArrayToHexWordstring(msg.ReceivedMessage.EpcAlarmConfig.Mask);
                }
                else
                    err += "\r\n3#：" + msg.ErrorInfo.ErrMsg;
            }
            if (ckbAlarm4.Checked)
            {
                MsgEpcAlarmConfig_AccessControl msg = new MsgEpcAlarmConfig_AccessControl(0x04);
                if (reader.Send(msg))
                {
                    ckbAlarmEnable4.Checked = msg.ReceivedMessage.EpcAlarmConfig.IsEnable;
                    textBox13.Text = Util.ConvertbyteArrayToHexWordstring(msg.ReceivedMessage.EpcAlarmConfig.Epc);
                    textBox14.Text = Util.ConvertbyteArrayToHexWordstring(msg.ReceivedMessage.EpcAlarmConfig.Mask);
                }
                else
                    err += "\r\n4#：" + msg.ErrorInfo.ErrMsg;
            }
            if (ckbAlarm5.Checked)
            {
                MsgEpcAlarmConfig_AccessControl msg = new MsgEpcAlarmConfig_AccessControl(0x05);
                if (reader.Send(msg))
                {
                    ckbAlarmEnable5.Checked = msg.ReceivedMessage.EpcAlarmConfig.IsEnable;
                    textBox15.Text = Util.ConvertbyteArrayToHexWordstring(msg.ReceivedMessage.EpcAlarmConfig.Epc);
                    textBox16.Text = Util.ConvertbyteArrayToHexWordstring(msg.ReceivedMessage.EpcAlarmConfig.Mask);
                }
                else
                    err += "\r\n5#：" + msg.ErrorInfo.ErrMsg;
            }
            if (ckbAlarm6.Checked)
            {
                MsgEpcAlarmConfig_AccessControl msg = new MsgEpcAlarmConfig_AccessControl(0x06);
                if (reader.Send(msg))
                {
                    ckbAlarmEnable6.Checked = msg.ReceivedMessage.EpcAlarmConfig.IsEnable;
                    textBox17.Text = Util.ConvertbyteArrayToHexWordstring(msg.ReceivedMessage.EpcAlarmConfig.Epc);
                    textBox18.Text = Util.ConvertbyteArrayToHexWordstring(msg.ReceivedMessage.EpcAlarmConfig.Mask);
                }
                else
                    err += "\r\n6#：" + msg.ErrorInfo.ErrMsg;
            }
            if (ckbAlarm7.Checked)
            {
                MsgEpcAlarmConfig_AccessControl msg = new MsgEpcAlarmConfig_AccessControl(0x07);
                if (reader.Send(msg))
                {
                    ckbAlarmEnable7.Checked = msg.ReceivedMessage.EpcAlarmConfig.IsEnable;
                    textBox19.Text = Util.ConvertbyteArrayToHexWordstring(msg.ReceivedMessage.EpcAlarmConfig.Epc);
                    textBox20.Text = Util.ConvertbyteArrayToHexWordstring(msg.ReceivedMessage.EpcAlarmConfig.Mask);
                }
                else
                    err += "\r\n7#：" + msg.ErrorInfo.ErrMsg;
            }
            if(ckbAlarm8.Checked)
            {
                MsgEpcAlarmConfig_AccessControl msg = new MsgEpcAlarmConfig_AccessControl(0x08);
                if (reader.Send(msg))
                {
                    ckbAlarmEnable8.Checked = msg.ReceivedMessage.EpcAlarmConfig.IsEnable;
                    textBox21.Text = Util.ConvertbyteArrayToHexWordstring(msg.ReceivedMessage.EpcAlarmConfig.Epc);
                    textBox22.Text = Util.ConvertbyteArrayToHexWordstring(msg.ReceivedMessage.EpcAlarmConfig.Mask);
                }
                else
                    err += "\r\n8#：" + msg.ErrorInfo.ErrMsg;
            }
            if (err != "")
                err = "查询完成！\r\n" + err;
            else
                err = "查询完成！";
            if (!isFormLoad)
                MessageBox.Show(err);            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string err = "";
            bool isH = false;
            if (ckbAlarm1.Checked)
            {
                isH = true;
                if (!checkText(textBox1.Text.Trim()) || !checkText(textBox2.Text.Trim()))
                {
                    MessageBox.Show("1#:请输入32个16进制字符");
                    return;
                }
            }
            if (ckbAlarm2.Checked)
            {
                isH = true;
                if (!checkText(textBox9.Text.Trim()) || !checkText(textBox10.Text.Trim()))
                {
                    MessageBox.Show("2#:请输入32个16进制字符");
                    return;
                }
            }
            if (ckbAlarm3.Checked)
            {
                isH = true;
                if (!checkText(textBox11.Text.Trim()) || !checkText(textBox12.Text.Trim()))
                {
                    MessageBox.Show("3#:请输入32个16进制字符");
                    return;
                }
            }
            if (ckbAlarm4.Checked)
            {
                isH = true;
                if (!checkText(textBox13.Text.Trim()) || !checkText(textBox14.Text.Trim()))
                {
                    MessageBox.Show("4#:请输入32个16进制字符");
                    return;
                }
            }
            if (ckbAlarm5.Checked)
            {
                isH = true;
                if (!checkText(textBox15.Text.Trim()) || !checkText(textBox16.Text.Trim()))
                {
                    MessageBox.Show("5#:请输入32个16进制字符");
                    return;
                }
            }
            if (ckbAlarm6.Checked)
            {
                isH = true;
                if (!checkText(textBox17.Text.Trim()) || !checkText(textBox18.Text.Trim()))
                {
                    MessageBox.Show("6#:请输入32个16进制字符");
                    return;
                }
            }
            if (ckbAlarm7.Checked)
            {
                isH = true;
                if (!checkText(textBox19.Text.Trim()) || !checkText(textBox20.Text.Trim()))
                {
                    MessageBox.Show("7#:请输入32个16进制字符");
                    return;
                }
            }
            if (ckbAlarm8.Checked)
            {
                isH = true;
                if (!checkText(textBox21.Text.Trim()) || !checkText(textBox22.Text.Trim()))
                {
                    MessageBox.Show("8#:请输入32个16进制字符");
                    return;
                }
            }
            if (!isH)
            {
                MessageBox.Show("无任何配置项");
                return;
            }
            if (ckbAlarm1.Checked)
            {
                EpcAlarmConfig_AccessControl param = new EpcAlarmConfig_AccessControl();
                param.NO = 0x01;
                param.IsEnable = ckbAlarmEnable1.Checked;
                param.Epc = Util.ConvertHexstringTobyteArray(textBox1.Text.Trim().Replace(" ", "").Replace("-", ""));
                param.Mask = Util.ConvertHexstringTobyteArray(textBox2.Text.Trim().Replace(" ", "").Replace("-", ""));
                MsgEpcAlarmConfig_AccessControl msg = new MsgEpcAlarmConfig_AccessControl(param);
                if (!reader.Send(msg))               
                    err += "1#：" + msg.ErrorInfo.ErrMsg;
            }
            if (ckbAlarm2.Checked)
            {
                EpcAlarmConfig_AccessControl param = new EpcAlarmConfig_AccessControl();
                param.NO = 0x02;
                param.IsEnable = ckbAlarmEnable2.Checked;
                param.Epc = Util.ConvertHexstringTobyteArray(textBox9.Text.Trim().Replace(" ", "").Replace("-", ""));
                param.Mask = Util.ConvertHexstringTobyteArray(textBox10.Text.Trim().Replace(" ", "").Replace("-", ""));
                MsgEpcAlarmConfig_AccessControl msg = new MsgEpcAlarmConfig_AccessControl(param);
                if (!reader.Send(msg))
                    err += "\r\n2#：" + msg.ErrorInfo.ErrMsg;
            }
            if (ckbAlarm3.Checked)
            {
                EpcAlarmConfig_AccessControl param = new EpcAlarmConfig_AccessControl();
                param.NO = 0x03;
                param.IsEnable = ckbAlarmEnable3.Checked;
                param.Epc = Util.ConvertHexstringTobyteArray(textBox11.Text.Trim().Replace(" ", "").Replace("-", ""));
                param.Mask = Util.ConvertHexstringTobyteArray(textBox12.Text.Trim().Replace(" ", "").Replace("-", ""));
                MsgEpcAlarmConfig_AccessControl msg = new MsgEpcAlarmConfig_AccessControl(param);
                if (!reader.Send(msg))
                    err += "\r\n3#：" + msg.ErrorInfo.ErrMsg;
            }
            if (ckbAlarm4.Checked)
            {
                EpcAlarmConfig_AccessControl param = new EpcAlarmConfig_AccessControl();
                param.NO = 0x04;
                param.IsEnable = ckbAlarmEnable4.Checked;
                param.Epc = Util.ConvertHexstringTobyteArray(textBox13.Text.Trim().Replace(" ", "").Replace("-", ""));
                param.Mask = Util.ConvertHexstringTobyteArray(textBox14.Text.Trim().Replace(" ", "").Replace("-", ""));
                MsgEpcAlarmConfig_AccessControl msg = new MsgEpcAlarmConfig_AccessControl(param);
                if (!reader.Send(msg))
                    err += "\r\n4#：" + msg.ErrorInfo.ErrMsg;
            }
            if (ckbAlarm5.Checked)
            {
                EpcAlarmConfig_AccessControl param = new EpcAlarmConfig_AccessControl();
                param.NO = 0x05;
                param.IsEnable = ckbAlarmEnable5.Checked;
                param.Epc = Util.ConvertHexstringTobyteArray(textBox15.Text.Trim().Replace(" ", "").Replace("-", ""));
                param.Mask = Util.ConvertHexstringTobyteArray(textBox16.Text.Trim().Replace(" ", "").Replace("-", ""));
                MsgEpcAlarmConfig_AccessControl msg = new MsgEpcAlarmConfig_AccessControl(param);
                if (!reader.Send(msg))
                    err += "\r\n5#：" + msg.ErrorInfo.ErrMsg;
            }
            if (ckbAlarm6.Checked)
            {
                EpcAlarmConfig_AccessControl param = new EpcAlarmConfig_AccessControl();
                param.NO = 0x06;
                param.IsEnable = ckbAlarmEnable6.Checked;
                param.Epc = Util.ConvertHexstringTobyteArray(textBox17.Text.Trim().Replace(" ", "").Replace("-", ""));
                param.Mask = Util.ConvertHexstringTobyteArray(textBox18.Text.Trim().Replace(" ", "").Replace("-", ""));
                MsgEpcAlarmConfig_AccessControl msg = new MsgEpcAlarmConfig_AccessControl(param);
                if (!reader.Send(msg))
                    err += "\r\n6#：" + msg.ErrorInfo.ErrMsg;
            }
            if (ckbAlarm7.Checked)
            {
                EpcAlarmConfig_AccessControl param = new EpcAlarmConfig_AccessControl();
                param.NO = 0x07;
                param.IsEnable = ckbAlarmEnable7.Checked;
                param.Epc = Util.ConvertHexstringTobyteArray(textBox19.Text.Trim().Replace(" ", "").Replace("-", ""));
                param.Mask = Util.ConvertHexstringTobyteArray(textBox20.Text.Trim().Replace(" ", "").Replace("-", ""));
                MsgEpcAlarmConfig_AccessControl msg = new MsgEpcAlarmConfig_AccessControl(param);
                if (!reader.Send(msg))
                    err += "\r\n7#：" + msg.ErrorInfo.ErrMsg;
            }
            if (ckbAlarm8.Checked)
            {
                EpcAlarmConfig_AccessControl param = new EpcAlarmConfig_AccessControl();
                param.NO = 0x08;
                param.IsEnable = ckbAlarmEnable8.Checked;
                param.Epc = Util.ConvertHexstringTobyteArray(textBox21.Text.Trim().Replace(" ", "").Replace("-", ""));
                param.Mask = Util.ConvertHexstringTobyteArray(textBox22.Text.Trim().Replace(" ", "").Replace("-", ""));
                MsgEpcAlarmConfig_AccessControl msg = new MsgEpcAlarmConfig_AccessControl(param);
                if (!reader.Send(msg))
                    err += "\r\n8#：" + msg.ErrorInfo.ErrMsg;
            }
            if (err != "")
                err = "配置完成！\r\n" + err;
            else
                err = "配置完成！";
            MessageBox.Show(err);
        }

        private bool checkText(string str)
        {
            if (!Common.IsHexstring(str))
                return false;
            str = str.Replace(" ", "").Replace("-", "");
            if (str.Length != 32)
                return false;
            return true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            MsgResetToFactoryDefault_AccessControl msg = new MsgResetToFactoryDefault_AccessControl();
            if (reader.Send(msg))
            {
                MessageBox.Show("操作成功");
            }
            else
                MessageBox.Show(msg.ErrorInfo.ErrMsg);
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
                byte[] msgbody = msg.ReceivedMessage.TriggerMsgBody;
                if (param != null)
                {
                    comboBox9.SelectedIndex = (int)param.TriggerCondition;                    
                    numericUpDown1.Value = param.DelayTime;
                    if (param.TriggerMsg is MsgTagInventory)                       
                    {
                        checkBox1.Checked = true;
                        checkBox2.Checked = false;
                        checkBox3.Checked = false;
                    }
                    else if (param.TriggerMsg is MsgTagRead)
                    {   
                        if(msgbody != null)
                        {
                            checkBox1.Checked = (msgbody[5] != 0x00);
                            checkBox2.Checked = (msgbody[7] != 0x00);                            
                            int ptr = 0;
                            int p = 8;
                            do {//EVB
                                p++;
                                ptr = ptr << 7;
                                ptr += (msgbody[p] & 0x7f);                                
                            } while ((msgbody[p] & 0x80) != 0);                            
                            numUserPtr.Value = ptr;
                            p++;
                            numUserLen.Value = msgbody[p];
                            checkBox3.Checked = (msgbody[p] != 0x00);
                        }                                 
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
            if (comboBox9.SelectedIndex == -1)
            {
                MessageBox.Show("请完善触发参数");
                return;
            }
            if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked)
            {
                MessageBox.Show("请完善指令参数");
                return;
            }
            GpiTriggerParameter tp = new GpiTriggerParameter();
            tp.PortNO = (byte)(comboBox1.SelectedIndex + 1);
            tp.TriggerCondition = (GpiTriggerCondition)comboBox9.SelectedIndex;
            tp.StopCondition = GpiStopCondition.Delayed;
            tp.DelayTime = (ushort)(numericUpDown1.Value);//秒
            
            if (checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked)//EPC
                tp.TriggerMsg = new MsgTagInventory();
            else if (!checkBox1.Checked && checkBox2.Checked && !checkBox3.Checked)//TID
                tp.TriggerMsg = new MsgTagRead();
            else//EPC + TID + User
            {
                ReadTagParameter rp = new ReadTagParameter();
                rp.IsLoop = true;
                rp.ReadCount = 0;
                rp.TotalReadTime = 0;
                rp.IsReturnEPC = checkBox1.Checked;
                rp.IsReturnTID = checkBox2.Checked;
                if (checkBox3.Checked)
                {
                    rp.UserPtr = (uint)numUserPtr.Value;
                    rp.UserLen = (byte)numUserLen.Value;
                }
                else
                {
                    rp.UserPtr = 0;
                    rp.UserLen = 0;
                }

                if (checkBox2.Checked)
                    tp.TriggerMsg = new MsgTagRead(rp);
            }
            MsgGpiTriggerConfig msg = new MsgGpiTriggerConfig(tp);
            if (reader.Send(msg))
            {
                MessageBox.Show("操作成功");
            }
            else
                MessageBox.Show(msg.ErrorInfo.ErrMsg);
        }

        private void ckbAlarm_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox ckb = (CheckBox)sender;
            string i = ckb.Name.Replace("ckbAlarm", "");
            Control[] ctrl = groupBox1.Controls.Find("gbAlarm" + i, false);
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
            for (int i = 1; i <= 8; i++)
            {
                Control[] ctrl = groupBox1.Controls.Find("ckbAlarm" + i, false);
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

        volatile bool isClick;
        private void ckbAll_Click(object sender, EventArgs e)
        {
            isClick = true;
            for (int i = 1; i <= 8; i++)
            {
                Control[] ctrl = groupBox1.Controls.Find("ckbAlarm" + i, false);
                if (ctrl != null && ctrl.Length > 0)
                {
                    CheckBox ckb = (CheckBox)ctrl[0];
                    ckb.Checked = ckbAll.Checked;
                }
            }
            isClick = false;
        }

        private void ckbAlarmEnable1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = textBox2.Enabled = ckbAlarmEnable1.Checked;
        }

        private void ckbAlarmEnable2_CheckedChanged(object sender, EventArgs e)
        {
            textBox9.Enabled = textBox10.Enabled = ckbAlarmEnable2.Checked;
        }

        private void ckbAlarmEnable3_CheckedChanged(object sender, EventArgs e)
        {
            textBox11.Enabled = textBox12.Enabled = ckbAlarmEnable3.Checked;
        }

        private void ckbAlarmEnable4_CheckedChanged(object sender, EventArgs e)
        {
            textBox13.Enabled = textBox14.Enabled = ckbAlarmEnable4.Checked;
        }

        private void ckbAlarmEnable5_CheckedChanged(object sender, EventArgs e)
        {
            textBox15.Enabled = textBox16.Enabled = ckbAlarmEnable5.Checked;
        }

        private void ckbAlarmEnable6_CheckedChanged(object sender, EventArgs e)
        {
            textBox17.Enabled = textBox18.Enabled = ckbAlarmEnable6.Checked;
        }

        private void ckbAlarmEnable7_CheckedChanged(object sender, EventArgs e)
        {
            textBox19.Enabled = textBox20.Enabled = ckbAlarmEnable7.Checked;
        }

        private void ckbAlarmEnable8_CheckedChanged(object sender, EventArgs e)
        {
            textBox21.Enabled = textBox22.Enabled = ckbAlarmEnable8.Checked;
        }
        
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            int txtSelPtr = txt.SelectionStart;
            string str = txt.Text;
            int bCount = 0;
            for(int i=0;i< txtSelPtr; i++)
            {
                if (str[i] == ' ')
                    bCount++;
            }
            int a = bCount - (txtSelPtr - bCount) / 4;//多出的空格
            string txtStr = getTxt(str.Trim());
            if (str != txtStr)
            {
                txt.Text = txtStr;
                int b = txtSelPtr - a;
                if (b > 40)
                    b = 40;
                txt.SelectionStart = b;
            }               
        }
        
        private string getTxt(string str)
        {
            str = str.Trim().Replace(" ", "");
            string txtStr = "";
            int p = 0;
            foreach (char c in str)
            {
                p++;
                txtStr += c;
                if (p % 4 == 0)
                    txtStr += " ";                
            }
            if (txtStr.Length > 40)
                txtStr = txtStr.Substring(0, 40);
            return txtStr;
        }

        private bool nonNumberEntered = false;
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

            }
            else
            {
                // Initialize the flag to false.
                nonNumberEntered = false;

                // Determine whether the keystroke is a number from the top of the keyboard.
                if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
                {
                    // Determine whether the keystroke is a number from the keypad.
                    if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                    {
                        if (e.KeyCode < Keys.A || e.KeyCode > Keys.F)
                        {
                            // Determine whether the keystroke is a backspace.
                            if (e.KeyCode != Keys.Back)
                            {
                                // A non-numerical keystroke was pressed.
                                // Set the flag to true and evaluate in KeyPress event.
                                nonNumberEntered = true;
                            }
                        }
                    }
                }
            }
        }
        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check for the flag being set in the KeyDown event.
            if (nonNumberEntered == true)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }
    }
}
