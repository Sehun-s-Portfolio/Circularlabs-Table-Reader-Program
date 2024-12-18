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
using System.Resources;
using System.Reflection;
using RFIDTest.Properties;

namespace RFIDTest
{
    public partial class FormBookShelf : Form
    {
        Reader reader;
        Image led0 = (Image)Resources.ResourceManager.GetObject("led0");
        Image led1 = (Image)Resources.ResourceManager.GetObject("led1");
        bool isFormLoad = true;

        public FormBookShelf(Reader reader)
        {
            InitializeComponent();
            this.reader = reader;
            this.reader.OnCtrlUpdata_YC001 += Reader_OnCtrlUpdata_YC001;
        }

        private void Reader_OnCtrlUpdata_YC001(byte ctrlBoardNO, IrTrigger[] irStates, SenssorTrigger[] senssorStates)
        {
            this.BeginInvoke(new MethodInvoker(delegate () {
                string str = "";
                numericUpDown1.Value = ctrlBoardNO;
                str = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]板号：" + ctrlBoardNO + ";  ";

                str += "人体感应：";
                foreach (SenssorTrigger st in senssorStates)
                {
                    Control[] cs = groupBox1.Controls.Find("picM" + st.SenssorNO, false);
                    if (cs != null && cs.Length > 0)
                    {
                        PictureBox pic = (PictureBox)cs[0];
                        if (st.IsTrigger)
                        {
                            str += st.SenssorNO + "   ";
                            pic.BackgroundImage = led1;
                        }
                        else
                            pic.BackgroundImage = led0;
                    }

                }
                str += ";";
                str += "红外对射：";
                foreach (IrTrigger ir in irStates)
                {
                    Control[] cs = groupBox1.Controls.Find("picIr" + ir.IrNO, false);
                    if (cs != null && cs.Length > 0)
                    {
                        PictureBox pic = (PictureBox)cs[0];
                        if (ir.IsTrigger)
                        {
                            pic.BackgroundImage = led1;
                            str += ir.IrNO + "   ";
                        }
                        else
                            pic.BackgroundImage = led0;
                    }

                }
                str += "\r\n";
                textBox1.AppendText(str);
            }));
        }

        private void FormBookShelf_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.reader.OnCtrlUpdata_YC001 -= Reader_OnCtrlUpdata_YC001;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            numericUpDown1.Value = 0;
            foreach(Control cs in groupBox1.Controls)
            {
                if(cs is PictureBox)
                {
                    (cs as PictureBox).BackgroundImage = led0;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str = "";
            for (int i = 1; i <= 2; i++)
            {
                MsgSingleLightStateConfig_YC001 msg = new MsgSingleLightStateConfig_YC001((byte)numericUpDown3.Value, (byte)i);
                if (reader.Send(msg))
                {
                    ComboBox ckb = (ComboBox)groupBox2.Controls.Find("ckb" + i, false)[0];
                    ckb.SelectedIndex = (int)(msg.ReceivedMessage.LightState);
                }
                else
                    str += "照明灯" + i + "  查询失败\r\n";
            }

            //MsgLightSwitchConfig_YC001 msg = new MsgLightSwitchConfig_YC001((byte)numericUpDown3.Value);
            //if (reader.Send(msg))
            //{
            //    ckb1.SelectedIndex = (int)msg.ReceivedMessage.LightSwitchState.Light1;
            //    ckb2.SelectedIndex = (int)msg.ReceivedMessage.LightSwitchState.Light2;
            //}
            //else
            //    str = msg.ErrorInfo.ErrMsg;


            if (str != "")
            {
                if (!isFormLoad)
                    MessageBox.Show(str);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string str = "";
            for (int i = 1; i <= 2; i++)
            {
                ComboBox ckb = (ComboBox)groupBox2.Controls.Find("ckb" + i, false)[0];
                LightState ls = (LightState)(ckb.SelectedIndex);

                MsgSingleLightStateConfig_YC001 msg = new MsgSingleLightStateConfig_YC001((byte)numericUpDown3.Value, (byte)i, ls);
                if (!reader.Send(msg, 2000))
                {
                    str += "照明灯" + i + "  设置失败\r\n";
                }
            }
            //LightSwitch ls = new LightSwitch();
            //ls.Light1 = (LightState)ckb1.SelectedIndex;
            //ls.Light2 = (LightState)ckb2.SelectedIndex;
            //MsgLightSwitchConfig_YC001 msg = new MsgLightSwitchConfig_YC001((byte)numericUpDown3.Value, ls);
            //if (!reader.Send(msg, 2000))
            //{
            //    str = msg.ErrorInfo.ErrMsg;
            //}
            if (str == "")
                str = "设置成功";
            MessageBox.Show(str);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string str = "";
            
            for (int i = 1; i <= 12; i++)
            {
                MsgSingleLayerLightStateConfig_YC001 msg = new MsgSingleLayerLightStateConfig_YC001((byte)numericUpDown2.Value, (byte)i);
                if (reader.Send(msg))
                {
                    ComboBox cb = (ComboBox)groupBox3.Controls.Find("cb" + i, false)[0];
                    cb.SelectedIndex = (int)msg.ReceivedMessage.LayerLightState;
                }
                else
                    str += "层架灯" + i + "  查询失败\r\n";
            }

            //MsgShelfLightStateConfig_YC001 msg = new MsgShelfLightStateConfig_YC001((byte)numericUpDown2.Value);
            //if (reader.Send(msg))
            //{
            //    foreach (ShelfLight sl in msg.ReceivedMessage.LightState)
            //    {
            //        ComboBox cb = (ComboBox)groupBox3.Controls.Find("cb" + sl.ShelfNO, false)[0];
            //        cb.SelectedIndex = (int)sl.State;
            //    }
            //}
            //else
            //    str = msg.ErrorInfo.ErrMsg;


            if (str != "")
            {
                if (!isFormLoad)
                    MessageBox.Show(str);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string str = "";
            for (int i = 1; i <= 12; i++)
            {
                ComboBox cb = (ComboBox)groupBox3.Controls.Find("cb" + i, false)[0];
                if (cb.SelectedIndex == -1)
                    continue;
                LayerLightState ls = (LayerLightState)cb.SelectedIndex;

                MsgSingleLayerLightStateConfig_YC001 msg = new MsgSingleLayerLightStateConfig_YC001((byte)numericUpDown2.Value, (byte)i, ls);
                if (!reader.Send(msg, 2000))
                {
                    str += "层架灯" + i + "  设置失败\r\n";
                }
            }

            //ShelfLight[] sl = new ShelfLight[12];
            //for (int i = 1; i <= 12; i++)
            //{
            //    ComboBox cb = (ComboBox)groupBox3.Controls.Find("cb" + i, false)[0];
            //    if (cb.SelectedIndex == -1)
            //        continue;
            //    sl[i - 1] = new ShelfLight();
            //    sl[i - 1].ShelfNO = (byte)i;
            //    sl[i - 1].State = (LightState)cb.SelectedIndex;
            //}
            //MsgShelfLightStateConfig_YC001 msg = new MsgShelfLightStateConfig_YC001((byte)numericUpDown2.Value, sl);
            //if (!reader.Send(msg, 2000))
            //{
            //    str = msg.ErrorInfo.ErrMsg;
            //}
            if (str == "")
                str = "设置完成";
            MessageBox.Show(str);
        }

        private void button31_Click(object sender, EventArgs e)
        {
            string err = "";
            MsgKeepAliveConfig_YC001 msg = new MsgKeepAliveConfig_YC001((byte)numericUpDown4.Value);
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
            MsgKeepAliveConfig_YC001 msg = new MsgKeepAliveConfig_YC001((byte)numericUpDown4.Value, checkBox10.Checked, (ushort)numericUpDown8.Value);
            if (reader.Send(msg, 2000))
            {
                err = "心跳指令设置成功。\r\n";
            }
            else
                err += "心跳指令设置失败" + msg.ErrorInfo.ErrMsg + "\r\n";

            MessageBox.Show(err);
        }

        private void FormBookShelf_Load(object sender, EventArgs e)
        {
            button2_Click(sender, e);
            button4_Click(sender, e);
            button31_Click(sender, e);
            
            isFormLoad = false;
        }
    }
}
