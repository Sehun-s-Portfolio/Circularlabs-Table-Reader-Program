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

namespace RFIDTest
{
    public partial class FormHabConfig : Form
    {
        Reader reader;
        public FormHabConfig(Reader reader)
        {
            InitializeComponent();
            this.reader = reader;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string err = "";
            #region 1#
            {
                MsgHubAntennaConfig msg = new MsgHubAntennaConfig(0x01);
                if (reader.Send(msg))
                {
                    HabAntennaStatus hs = msg.ReceivedMessage.PortInfo;
                    foreach (AntennaStatus a in hs.Antennas)
                    {
                        checkedListBox1.SetItemChecked(a.AntennaNO - 1, a.IsEnable);
                    }
                    checkBox1.CheckState = changeCkbAllState(checkedListBox1);
                    if (checkBox1.CheckState == CheckState.Unchecked)
                        checkBox3.Checked = false;
                    else
                        checkBox3.Checked = true;
                }
                else
                    err = "1#天线分支器天线使能查询失败," + msg.ErrorInfo.ErrMsg + "\r\n";
                MsgHubAntennaTimeResidentConfig msg1 = new MsgHubAntennaTimeResidentConfig(0x01);
                if (reader.Send(msg1))
                {
                    numericUpDown2.Value = msg1.ReceivedMessage.Time;
                }
                else
                    err += "1#天线分支器天线驻留时间查询失败," + msg1.ErrorInfo.ErrMsg + "\r\n";

            }
            #endregion
            #region 2#
            {
                MsgHubAntennaConfig msg = new MsgHubAntennaConfig(0x02);
                if (reader.Send(msg))
                {
                    HabAntennaStatus hs = msg.ReceivedMessage.PortInfo;
                    foreach (AntennaStatus a in hs.Antennas)
                    {
                        checkedListBox3.SetItemChecked(a.AntennaNO - 1, a.IsEnable);
                    }
                    checkBox4.CheckState = changeCkbAllState(checkedListBox3);
                    if (checkBox4.CheckState == CheckState.Unchecked)
                        checkBox6.Checked = false;
                    else
                        checkBox6.Checked = true;
                }
                else
                    err += "2#天线分支器天线使能查询失败," + msg.ErrorInfo.ErrMsg + "\r\n";
                MsgHubAntennaTimeResidentConfig msg1 = new MsgHubAntennaTimeResidentConfig(0x02);
                if (reader.Send(msg1))
                {
                    numericUpDown1.Value = msg1.ReceivedMessage.Time;
                }
                else
                    err += "2#天线分支器天线驻留时间查询失败," + msg1.ErrorInfo.ErrMsg + "\r\n";

            }
            #endregion
            #region 3#
            {
                MsgHubAntennaConfig msg = new MsgHubAntennaConfig(0x03);
                if (reader.Send(msg))
                {
                    HabAntennaStatus hs = msg.ReceivedMessage.PortInfo;
                    foreach (AntennaStatus a in hs.Antennas)
                    {
                        checkedListBox2.SetItemChecked(a.AntennaNO - 1, a.IsEnable);
                    }
                    checkBox2.CheckState = changeCkbAllState(checkedListBox2);
                    if (checkBox2.CheckState == CheckState.Unchecked)
                        checkBox7.Checked = false;
                    else
                        checkBox7.Checked = true;
                }
                else
                    err += "3#天线分支器天线使能查询失败," + msg.ErrorInfo.ErrMsg + "\r\n";
                MsgHubAntennaTimeResidentConfig msg1 = new MsgHubAntennaTimeResidentConfig(0x03);
                if (reader.Send(msg1))
                {
                    numericUpDown3.Value = msg1.ReceivedMessage.Time;
                }
                else
                    err += "3#天线分支器天线驻留时间查询失败," + msg1.ErrorInfo.ErrMsg + "\r\n";

            }
            #endregion
            #region 4#
            {
                MsgHubAntennaConfig msg = new MsgHubAntennaConfig(0x04);
                if (reader.Send(msg))
                {
                    HabAntennaStatus hs = msg.ReceivedMessage.PortInfo;
                    foreach (AntennaStatus a in hs.Antennas)
                    {
                        checkedListBox4.SetItemChecked(a.AntennaNO - 1, a.IsEnable);
                    }
                    checkBox5.CheckState = changeCkbAllState(checkedListBox4);
                    if (checkBox5.CheckState == CheckState.Unchecked)
                        checkBox8.Checked = false;
                    else
                        checkBox8.Checked = true;
                }
                else
                    err += "4#天线分支器天线使能查询失败," + msg.ErrorInfo.ErrMsg + "\r\n";
                MsgHubAntennaTimeResidentConfig msg1 = new MsgHubAntennaTimeResidentConfig(0x04);
                if (reader.Send(msg1))
                {
                    numericUpDown4.Value = msg1.ReceivedMessage.Time;
                }
                else
                    err += "4#天线分支器天线驻留时间查询失败," + msg1.ErrorInfo.ErrMsg + "\r\n";

            }
            #endregion
            if (err == "")
                err = "天线分支器参数查询成功！";
            MessageBox.Show(err);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string err = "";
            #region 1#
            {
                HabAntennaStatus hs = new HabAntennaStatus();
                hs.HabNO = 0x01;
                if (checkBox3.Checked)
                {
                    hs.Antennas = new AntennaStatus[48];
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        hs.Antennas[i] = new AntennaStatus();
                        hs.Antennas[i].AntennaNO = (byte)(i + 1);
                        hs.Antennas[i].IsEnable = checkedListBox1.GetItemChecked(i);
                    }
                }
                MsgHubAntennaConfig msg = new MsgHubAntennaConfig(hs);
                if (!reader.Send(msg, 3000))
                {
                    err = "1#天线分支器天线使能配置失败," + msg.ErrorInfo.ErrMsg + "\r\n";
                }

                MsgHubAntennaTimeResidentConfig msg1 = new MsgHubAntennaTimeResidentConfig(0x01, (ushort)numericUpDown2.Value);
                if (!reader.Send(msg1, 3000))
                {
                    err += "1#天线分支器天线驻留时间配置失败," + msg1.ErrorInfo.ErrMsg + "\r\n";
                }
            }
            #endregion
            #region 2#
            {
                HabAntennaStatus hs = new HabAntennaStatus();
                hs.HabNO = 0x02;
                if (checkBox6.Checked)
                {
                    hs.Antennas = new AntennaStatus[48];
                    for (int i = 0; i < checkedListBox3.Items.Count; i++)
                    {
                        hs.Antennas[i] = new AntennaStatus();
                        hs.Antennas[i].AntennaNO = (byte)(i + 1);
                        hs.Antennas[i].IsEnable = checkedListBox3.GetItemChecked(i);
                    }
                }
                MsgHubAntennaConfig msg = new MsgHubAntennaConfig(hs);
                if (!reader.Send(msg, 3000))
                {
                    err += "2#天线分支器天线使能配置失败," + msg.ErrorInfo.ErrMsg + "\r\n";
                }

                MsgHubAntennaTimeResidentConfig msg1 = new MsgHubAntennaTimeResidentConfig(0x02, (ushort)numericUpDown1.Value);
                if (!reader.Send(msg1, 3000))
                {
                    err += "2#天线分支器天线驻留时间配置失败," + msg1.ErrorInfo.ErrMsg + "\r\n";
                }
            }
            #endregion
            #region 3#
            {
                HabAntennaStatus hs = new HabAntennaStatus();
                hs.HabNO = 0x03;
                if (checkBox7.Checked)
                {
                    hs.Antennas = new AntennaStatus[48];
                    for (int i = 0; i < checkedListBox2.Items.Count; i++)
                    {
                        hs.Antennas[i] = new AntennaStatus();
                        hs.Antennas[i].AntennaNO = (byte)(i + 1);
                        hs.Antennas[i].IsEnable = checkedListBox2.GetItemChecked(i);
                    }
                }
                MsgHubAntennaConfig msg = new MsgHubAntennaConfig(hs);
                if (!reader.Send(msg, 3000))
                {
                    err += "3#天线分支器天线使能配置失败," + msg.ErrorInfo.ErrMsg + "\r\n";
                }

                MsgHubAntennaTimeResidentConfig msg1 = new MsgHubAntennaTimeResidentConfig(0x03, (ushort)numericUpDown3.Value);
                if (!reader.Send(msg1, 3000))
                {
                    err += "3#天线分支器天线驻留时间配置失败," + msg1.ErrorInfo.ErrMsg + "\r\n";
                }
            }
            #endregion
            #region 4#
            {
                HabAntennaStatus hs = new HabAntennaStatus();
                hs.HabNO = 0x04;
                if (checkBox8.Checked)
                {
                    hs.Antennas = new AntennaStatus[48];
                    for (int i = 0; i < checkedListBox4.Items.Count; i++)
                    {
                        hs.Antennas[i] = new AntennaStatus();
                        hs.Antennas[i].AntennaNO = (byte)(i + 1);
                        hs.Antennas[i].IsEnable = checkedListBox4.GetItemChecked(i);
                    }
                }
                MsgHubAntennaConfig msg = new MsgHubAntennaConfig(hs);
                if (!reader.Send(msg, 3000))
                {
                    err += "4#天线分支器天线使能配置失败," + msg.ErrorInfo.ErrMsg + "\r\n";
                }

                MsgHubAntennaTimeResidentConfig msg1 = new MsgHubAntennaTimeResidentConfig(0x04, (ushort)numericUpDown4.Value);
                if (!reader.Send(msg1, 3000))
                {
                    err += "4#天线分支器天线驻留时间配置失败," + msg1.ErrorInfo.ErrMsg + "\r\n";
                }
            }
            #endregion
            if (err == "")
                err = "天线分支器参数配置成功！";

            MessageBox.Show(err);
        }

        private void selAll(CheckedListBox cklb, bool isChecked)
        {
            for (int i = 0; i < cklb.Items.Count; i++)
            {
                cklb.SetItemChecked(i, isChecked);
            }
        }
        volatile bool isClick = false;
        private void checkBox1_Click(object sender, EventArgs e)
        {
            CheckBox ckb = (CheckBox)sender;
            if (ckb.Name == "checkBox1")
                selAll(checkedListBox1, ckb.Checked);
            if (ckb.Name == "checkBox4")
                selAll(checkedListBox3, ckb.Checked);
            if (ckb.Name == "checkBox2")
                selAll(checkedListBox2, ckb.Checked);
            if (ckb.Name == "checkBox5")
                selAll(checkedListBox4, ckb.Checked);
        }

        private void checkedListBox1_Click(object sender, EventArgs e)
        {
            isClick = true;
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (isClick)
            {
                isClick = false;
                bool isT = false;
                bool isF = false;
                CheckedListBox cklb = (CheckedListBox)sender;
                if (e.NewValue == CheckState.Checked)
                    isT = true;
                else
                    isF = true;
                for (int i = 0; i < cklb.Items.Count; i++)
                {
                    if (i == e.Index)
                        continue;
                    if (cklb.GetItemChecked(i))
                        isT = true;
                    else
                        isF = true;
                    if (isT && isF)
                        break;
                }
                if (isT && !isF)
                {
                    if (cklb.Name == "checkedListBox1")
                        checkBox1.CheckState = CheckState.Checked;
                    if (cklb.Name == "checkedListBox3")
                        checkBox4.CheckState = CheckState.Checked;
                    if (cklb.Name == "checkedListBox2")
                        checkBox2.CheckState = CheckState.Checked;
                    if (cklb.Name == "checkedListBox4")
                        checkBox5.CheckState = CheckState.Checked;
                }
                else if (isF && !isT)
                {
                    if (cklb.Name == "checkedListBox1")
                        checkBox1.CheckState = CheckState.Unchecked;
                    if (cklb.Name == "checkedListBox3")
                        checkBox4.CheckState = CheckState.Unchecked;
                    if (cklb.Name == "checkedListBox2")
                        checkBox2.CheckState = CheckState.Unchecked;
                    if (cklb.Name == "checkedListBox4")
                        checkBox5.CheckState = CheckState.Unchecked;
                }
                else
                {
                    if (cklb.Name == "checkedListBox1")
                        checkBox1.CheckState = CheckState.Indeterminate;
                    if (cklb.Name == "checkedListBox3")
                        checkBox4.CheckState = CheckState.Indeterminate;
                    if (cklb.Name == "checkedListBox2")
                        checkBox2.CheckState = CheckState.Indeterminate;
                    if (cklb.Name == "checkedListBox4")
                        checkBox5.CheckState = CheckState.Indeterminate;
                }
            }
        }

        private CheckState changeCkbAllState(CheckedListBox cklb)
        {
            CheckState state = CheckState.Unchecked;
            bool isT = false;
            bool isF = false;
           
            for (int i = 0; i < cklb.Items.Count; i++)
            {               
                if (cklb.GetItemChecked(i))
                    isT = true;
                else
                    isF = true;
                if (isT && isF)
                    break;
            }
            if (isT && !isF)
            {
                state = CheckState.Checked;
            }
            else if (isF && !isT)
            {
                state = CheckState.Unchecked;
            }
            else
                state = CheckState.Indeterminate;

            return state;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = checkBox3.Checked;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            groupBox3.Enabled = checkBox6.Checked;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Enabled = checkBox7.Checked;
        }

        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            groupBox4.Enabled = checkBox8.Checked;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string err = query1();
            if (err == "")
                err = "天线分支器参数查询成功！";
            MessageBox.Show(err);
        }

        string query1()
        {
            string err = "";
            #region 1#
            {
                MsgHubAntennaConfig msg = new MsgHubAntennaConfig(0x01);
                if (reader.Send(msg))
                {
                    HabAntennaStatus hs = msg.ReceivedMessage.PortInfo;
                    foreach (AntennaStatus a in hs.Antennas)
                    {
                        checkedListBox1.SetItemChecked(a.AntennaNO - 1, a.IsEnable);
                    }
                    checkBox1.CheckState = changeCkbAllState(checkedListBox1);
                    if (checkBox1.CheckState == CheckState.Unchecked)
                        checkBox3.Checked = false;
                    else
                        checkBox3.Checked = true;
                }
                else
                    err = "1#天线分支器天线使能查询失败," + msg.ErrorInfo.ErrMsg + "\r\n";
                MsgHubAntennaTimeResidentConfig msg1 = new MsgHubAntennaTimeResidentConfig(0x01);
                if (reader.Send(msg1))
                {
                    ushort time = msg1.ReceivedMessage.Time;
                    if (time < 20)
                    {
                        time = 20;
                        err += "1#天线分支器天线驻留时间低于20，请重新设置！\r\n";
                    }
                    numericUpDown2.Value = time;
                }
                else
                    err += "1#天线分支器天线驻留时间查询失败," + msg1.ErrorInfo.ErrMsg + "\r\n";

            }
            #endregion
            return err;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string err = "";
            #region 1#
            {
                HabAntennaStatus hs = new HabAntennaStatus();
                hs.HabNO = 0x01;
                if (checkBox3.Checked)
                {
                    hs.Antennas = new AntennaStatus[48];
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        hs.Antennas[i] = new AntennaStatus();
                        hs.Antennas[i].AntennaNO = (byte)(i + 1);
                        hs.Antennas[i].IsEnable = checkedListBox1.GetItemChecked(i);
                    }
                }
                MsgHubAntennaConfig msg = new MsgHubAntennaConfig(hs);
                if (!reader.Send(msg, 3000))
                {
                    err = "1#天线分支器天线使能配置失败," + msg.ErrorInfo.ErrMsg + "\r\n";
                }

                MsgHubAntennaTimeResidentConfig msg1 = new MsgHubAntennaTimeResidentConfig(0x01, (ushort)numericUpDown2.Value);
                if (!reader.Send(msg1, 3000))
                {
                    err += "1#天线分支器天线驻留时间配置失败," + msg1.ErrorInfo.ErrMsg + "\r\n";
                }
            }
            #endregion           
            if (err == "")
                err = "天线分支器参数配置成功！";
            MessageBox.Show(err);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string err = query2();
            if (err == "")
                err = "天线分支器参数查询成功！";
            MessageBox.Show(err);
        }

        string query2()
        {
            string err = "";
            #region 2#
            {
                MsgHubAntennaConfig msg = new MsgHubAntennaConfig(0x02);
                if (reader.Send(msg))
                {
                    HabAntennaStatus hs = msg.ReceivedMessage.PortInfo;
                    foreach (AntennaStatus a in hs.Antennas)
                    {
                        checkedListBox3.SetItemChecked(a.AntennaNO - 1, a.IsEnable);
                    }
                    checkBox4.CheckState = changeCkbAllState(checkedListBox3);
                    if (checkBox4.CheckState == CheckState.Unchecked)
                        checkBox6.Checked = false;
                    else
                        checkBox6.Checked = true;
                }
                else
                    err += "2#天线分支器天线使能查询失败," + msg.ErrorInfo.ErrMsg + "\r\n";
                MsgHubAntennaTimeResidentConfig msg1 = new MsgHubAntennaTimeResidentConfig(0x02);
                if (reader.Send(msg1))
                {
                    ushort time = msg1.ReceivedMessage.Time;
                    if (time < 20)
                    {
                        time = 20;
                        err += "2#天线分支器天线驻留时间低于20，请重新设置！\r\n";
                    }
                    numericUpDown1.Value = time;
                }
                else
                    err += "2#天线分支器天线驻留时间查询失败," + msg1.ErrorInfo.ErrMsg + "\r\n";

            }
            #endregion
            return err;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string err = "";
            #region 2#
            {
                HabAntennaStatus hs = new HabAntennaStatus();
                hs.HabNO = 0x02;
                if (checkBox6.Checked)
                {
                    hs.Antennas = new AntennaStatus[48];
                    for (int i = 0; i < checkedListBox3.Items.Count; i++)
                    {
                        hs.Antennas[i] = new AntennaStatus();
                        hs.Antennas[i].AntennaNO = (byte)(i + 1);
                        hs.Antennas[i].IsEnable = checkedListBox3.GetItemChecked(i);
                    }
                }
                MsgHubAntennaConfig msg = new MsgHubAntennaConfig(hs);
                if (!reader.Send(msg, 3000))
                {
                    err += "2#天线分支器天线使能配置失败," + msg.ErrorInfo.ErrMsg + "\r\n";
                }

                MsgHubAntennaTimeResidentConfig msg1 = new MsgHubAntennaTimeResidentConfig(0x02, (ushort)numericUpDown1.Value);
                if (!reader.Send(msg1, 3000))
                {
                    err += "2#天线分支器天线驻留时间配置失败," + msg1.ErrorInfo.ErrMsg + "\r\n";
                }
            }
            #endregion
            if (err == "")
                err = "天线分支器参数配置成功！";
            MessageBox.Show(err);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string err = query3();
            if (err == "")
                err = "天线分支器参数查询成功！";
            MessageBox.Show(err);
        }

        string query3()
        {
            string err = "";
            #region 3#
            {
                MsgHubAntennaConfig msg = new MsgHubAntennaConfig(0x03);
                if (reader.Send(msg))
                {
                    HabAntennaStatus hs = msg.ReceivedMessage.PortInfo;
                    foreach (AntennaStatus a in hs.Antennas)
                    {
                        checkedListBox2.SetItemChecked(a.AntennaNO - 1, a.IsEnable);
                    }
                    checkBox2.CheckState = changeCkbAllState(checkedListBox2);
                    if (checkBox2.CheckState == CheckState.Unchecked)
                        checkBox7.Checked = false;
                    else
                        checkBox7.Checked = true;
                }
                else
                    err += "3#天线分支器天线使能查询失败," + msg.ErrorInfo.ErrMsg + "\r\n";
                MsgHubAntennaTimeResidentConfig msg1 = new MsgHubAntennaTimeResidentConfig(0x03);
                if (reader.Send(msg1))
                {
                    ushort time = msg1.ReceivedMessage.Time;
                    if (time < 20)
                    {
                        time = 20;
                        err += "3#天线分支器天线驻留时间低于20，请重新设置！\r\n";
                    }
                    numericUpDown3.Value = time;
                }
                else
                    err += "3#天线分支器天线驻留时间查询失败," + msg1.ErrorInfo.ErrMsg + "\r\n";

            }
            #endregion
            return err;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string err = "";
            #region 3#
            {
                HabAntennaStatus hs = new HabAntennaStatus();
                hs.HabNO = 0x03;
                if (checkBox7.Checked)
                {
                    hs.Antennas = new AntennaStatus[48];
                    for (int i = 0; i < checkedListBox2.Items.Count; i++)
                    {
                        hs.Antennas[i] = new AntennaStatus();
                        hs.Antennas[i].AntennaNO = (byte)(i + 1);
                        hs.Antennas[i].IsEnable = checkedListBox2.GetItemChecked(i);
                    }
                }
                MsgHubAntennaConfig msg = new MsgHubAntennaConfig(hs);
                if (!reader.Send(msg, 3000))
                {
                    err += "3#天线分支器天线使能配置失败," + msg.ErrorInfo.ErrMsg + "\r\n";
                }

                MsgHubAntennaTimeResidentConfig msg1 = new MsgHubAntennaTimeResidentConfig(0x03, (ushort)numericUpDown3.Value);
                if (!reader.Send(msg1, 3000))
                {
                    err += "3#天线分支器天线驻留时间配置失败," + msg1.ErrorInfo.ErrMsg + "\r\n";
                }
            }
            #endregion
            if (err == "")
                err = "天线分支器参数配置成功！";
            MessageBox.Show(err);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string err = query4();
            if (err == "")
                err = "天线分支器参数查询成功！";
            MessageBox.Show(err);
        }

        string query4()
        {
            string err = "";
            #region 4#
            {
                MsgHubAntennaConfig msg = new MsgHubAntennaConfig(0x04);
                if (reader.Send(msg))
                {
                    HabAntennaStatus hs = msg.ReceivedMessage.PortInfo;
                    foreach (AntennaStatus a in hs.Antennas)
                    {
                        checkedListBox4.SetItemChecked(a.AntennaNO - 1, a.IsEnable);
                    }
                    checkBox5.CheckState = changeCkbAllState(checkedListBox4);
                    if (checkBox5.CheckState == CheckState.Unchecked)
                        checkBox8.Checked = false;
                    else
                        checkBox8.Checked = true;
                }
                else
                    err += "4#天线分支器天线使能查询失败," + msg.ErrorInfo.ErrMsg + "\r\n";
                MsgHubAntennaTimeResidentConfig msg1 = new MsgHubAntennaTimeResidentConfig(0x04);
                if (reader.Send(msg1))
                {
                    ushort time = msg1.ReceivedMessage.Time;
                    if (time < 20)
                    {
                        time = 20;
                        err += "3#天线分支器天线驻留时间低于20，请重新设置！\r\n";
                    }
                    numericUpDown4.Value = time;
                }
                else
                    err += "4#天线分支器天线驻留时间查询失败," + msg1.ErrorInfo.ErrMsg + "\r\n";

            }
            #endregion
            return err;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string err = "";
            #region 4#
            {
                HabAntennaStatus hs = new HabAntennaStatus();
                hs.HabNO = 0x04;
                if (checkBox8.Checked)
                {
                    hs.Antennas = new AntennaStatus[48];
                    for (int i = 0; i < checkedListBox4.Items.Count; i++)
                    {
                        hs.Antennas[i] = new AntennaStatus();
                        hs.Antennas[i].AntennaNO = (byte)(i + 1);
                        hs.Antennas[i].IsEnable = checkedListBox4.GetItemChecked(i);
                    }
                }
                MsgHubAntennaConfig msg = new MsgHubAntennaConfig(hs);
                if (!reader.Send(msg, 3000))
                {
                    err += "4#天线分支器天线使能配置失败," + msg.ErrorInfo.ErrMsg + "\r\n";
                }

                MsgHubAntennaTimeResidentConfig msg1 = new MsgHubAntennaTimeResidentConfig(0x04, (ushort)numericUpDown4.Value);
                if (!reader.Send(msg1, 3000))
                {
                    err += "4#天线分支器天线驻留时间配置失败," + msg1.ErrorInfo.ErrMsg + "\r\n";
                }
            }
            #endregion
            if (err == "")
                err = "天线分支器参数配置成功！";
            MessageBox.Show(err);
        }

        private void FormHabConfig_Load(object sender, EventArgs e)
        {
            string err = "";
            err += query1();
            err += query2();
            err += query3();
            err += query4();
            if (err != "")
                MessageBox.Show(err);
        }
    }
}
