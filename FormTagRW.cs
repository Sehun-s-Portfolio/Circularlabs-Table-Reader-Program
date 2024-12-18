using NetAPI;
using NetAPI.Core;
using NetAPI.Entities;
using NetAPI.Protocol.VRP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RFIDTest
{
    public partial class FormTagRW : Form
    {
        Reader reader;
        TagParameter selectTagParam;
        volatile bool isTesting = false;
        AutoResetEvent re = null;
        volatile bool IsCouldClose = true;
        public FormTagRW(Reader reader, TagParameter selectTagParam)
        {
            InitializeComponent();
            this.reader = reader;
            this.selectTagParam = selectTagParam;
            if (selectTagParam != null)
                txt_TagID.Text = Util.ConvertbyteArrayToHexstring(selectTagParam.TagData);
            cb_MB.SelectedIndex = 0;
        }

        private void btn_Test_Click(object sender, EventArgs e)
        {
            if (selectTagParam != null)
            {
                if (!Common.IsHexstring(txt_pwd.Text))
                {
                    MessageBox.Show("密码应为8位16进制字符", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                testMB = cb_MB.Items[cb_MB.SelectedIndex].ToString();

                if (num_Ptr.Value + num_Length.Value > num_Length.Maximum)
                {
                    MessageBox.Show("起始地址+长度不能大于256", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                testCount = (Int32)num_TestCount.Value;
                len = (int)num_Length.Value;
                ptr = (int)num_Ptr.Value;

                isWrite = cb_write.Checked;
                isRead = cb_read.Checked;

                if (txt_TagID.Text.Trim() != "" && (isWrite || isRead))
                {
                    btn_Test.Enabled = false;
                    btn_StopTest.Enabled = true;
                    txt_WriteData.Text = "";
                    txt_ReadData.Text = "";
                    lblDoneCount.Text = "";
                    lblSuccessCount.Text = "";
                    lblSuccessPercentage.Text = "";
                    lblAverageTime.Text = "";
                    lblTime.Text = "";
                    cb_read.Enabled = cb_write.Enabled = false;

                    IsCouldClose = false;
                    Thread t = new Thread(new ThreadStart(Testing));
                    t.Start();
                    timeStartTest = DateTime.Now;
                }
                else
                {
                    MessageBox.Show("标签ID不能为空", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
                MessageBox.Show("请选择要测试的标签", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btn_StopTest_Click(object sender, EventArgs e)
        {
            re = new AutoResetEvent(false);
            isTesting = false;
            re.WaitOne(500, false);
            re = null;            
            btn_Test.Enabled = true;
            btn_StopTest.Enabled = false;
            cb_read.Enabled = cb_write.Enabled = true;           
            IsCouldClose = true;
        }


        string testMB;
        Int32 testCount;
        int ptr;
        int len;
        bool isWrite;
        bool isRead;
        byte[] pwd = new byte[4];
        volatile Int32 currentTestCount;
        volatile Int32 sCount;
        DateTime timeStartTest;
        void Testing()
        {
            isTesting = true;
            currentTestCount = 0;
            sCount = 0;
            for (int i = 0; i < testCount; i++)
            {
                bool isSuc = true;
                byte[] wd = getRandomData(len);
                string wErr = "";
                string rErr = "";
                string rwErr = "";

                #region 写数据
                if (isWrite && isTesting)
                {
                    WriteTagParameter wp = new WriteTagParameter();
                    wp.SelectTagParam = selectTagParam;
                    wp.AccessPassword = Util.ConvertHexstringTobyteArray(txt_pwd.Text.Trim());
                    wp.IsLoop = false;
                    wp.WriteDataAry = new TagParameter[1];
                    #region VRP
                    if (testMB == "UserData")
                    {
                        TagParameter tp = new TagParameter();
                        tp.TagData = wd;
                        tp.MemoryBank = MemoryBank.UserMemory;
                        tp.Ptr = (uint)ptr;
                        wp.WriteDataAry[0] = tp;
                    }
                    else if (testMB == "EPC")
                    {
                        TagParameter tp = new TagParameter();
                        tp.TagData = wd;
                        tp.MemoryBank = MemoryBank.EPCMemory;
                        tp.Ptr = (uint)ptr;
                        wp.WriteDataAry[0] = tp;
                    }
                    #endregion
                    MsgTagWrite msg = msg = new MsgTagWrite(wp);
                    if (!reader.Send(msg))
                    {
                        isSuc = false;
                        wErr = "写失败：" + msg.ErrorInfo.ErrMsg;
                    }
                }
                #endregion

                Thread.Sleep(10);
                if (!isTesting)
                    break;

                #region 读数据
                byte[] rd = null;
                if (isRead && isSuc && isTesting)
                {
                    ReadTagParameter rp = new ReadTagParameter();
                    rp.SelectTagParam = selectTagParam;
                    rp.AccessPassword = Util.ConvertHexstringTobyteArray(txt_pwd.Text.Trim());
                    rp.IsLoop = false;
                    rp.IsReturnTID = false;

                    #region VRP                   
                    if (testMB == "UserData")
                    {
                        rp.IsReturnEPC = false;
                        rp.UserPtr = (uint)ptr;
                        rp.UserLen = (byte)len;
                        MsgTagRead msg = msg = new MsgTagRead(rp);
                        if (reader.Send(msg))
                            rd = msg.ReceivedMessage.TagData.User;
                        else
                        {
                            isSuc = false;
                            rErr = "读失败：" + msg.ErrorInfo.ErrMsg;
                        }
                    }
                    else if (testMB == "EPC")
                    {
                        rp.IsReturnEPC = true;
                        MsgTagRead msg = msg = new MsgTagRead(rp);
                        if (reader.Send(msg))
                        {

                            if (ptr > 2)
                            {
                                rd = new byte[len * 2];
                                Array.Copy(msg.ReceivedMessage.TagData.EPC, (ptr - 2) * 2, rd, 0, rd.Length);
                            }
                            else
                                rd = msg.ReceivedMessage.TagData.EPC;
                        }
                        else
                        {
                            isSuc = false;
                            rErr = "读失败：" + msg.ErrorInfo.ErrMsg;
                        }
                    }

                    #endregion
                }
                #endregion

                Thread.Sleep(10);
                if (!isTesting)
                    break;

                #region 写读对比
                if (isRead && isWrite && isSuc)
                {
                    if (wd != null && rd != null && rd.Length == wd.Length)
                    {
                        for (int j = 0; j < wd.Length; j++)
                        {
                            if (wd[j] != rd[j])
                            {
                                isSuc = false;
                                break;
                            }
                        }
                    }
                    else
                        isSuc = false;
                    if (!isSuc)
                    {
                        rwErr = "读写不一致";
                    }
                }
                #endregion

                if (!isTesting)
                    break;

                #region 统计并显示
                if (isTesting)
                {
                    string wdStr = null;
                    string rdStr = null;
                    #region WriteData
                    if (wd != null && isWrite)
                    {
                        wdStr = Util.ConvertbyteArrayToHexWordstring(wd);
                    }
                    #endregion
                    #region ReadData
                    if (rd != null)
                    {
                        rdStr = Util.ConvertbyteArrayToHexWordstring(rd);
                    }
                    #endregion
                    this.currentTestCount = i + 1;
                    if (wErr != "")
                    {
                        wdStr = wErr;
                        if (rdStr == null && rErr == "")
                            rdStr = "写操作错误，读取指令跳过";
                    }
                    if (rErr != "")
                        rdStr = rErr;
                    if (rwErr != "")
                        rdStr += " " + rwErr;
                    display(isSuc, wdStr, rdStr);
                }
                #endregion

                if (!isTesting)
                    break;
            }
            testEnd();// 测试完成
            isTesting = false;
            if (re != null)
                re.Set();
            IsCouldClose = true;
        }

        #region 随机写入数据 
        private byte[] getRandomData(int len)
        {
            byte[] data;
            data = new byte[len * 2];//双字节
            Random random = new Random();
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)random.Next(0, 256);
            }
            return data;
        }
        #endregion

        private void display(bool isSuc, string wdStr, string rdStr)
        {
            this.BeginInvoke(new MethodInvoker(delegate () {
                if (!string.IsNullOrEmpty(wdStr))
                    txt_WriteData.AppendText(currentTestCount.ToString().PadLeft(testCount.ToString().Length, '0') + ": " + wdStr + "\r\n\r\n");
                if (!string.IsNullOrEmpty(rdStr))
                    txt_ReadData.AppendText(currentTestCount.ToString().PadLeft(testCount.ToString().Length, '0') + ": " + rdStr + "\r\n\r\n");
                lblDoneCount.Text = currentTestCount.ToString();// 完成次数
                //成功次数
                if (isSuc)
                {
                    sCount++;
                    lblSuccessCount.Text = sCount.ToString();
                }
                // 成功率
                double sp = ((double)sCount / (double)currentTestCount) * 100;
                lblSuccessPercentage.Text = sp.ToString("00.00") + "%";
            }));           
        }

        private void testEnd()
        {
            this.BeginInvoke(new MethodInvoker(delegate ()
            {
                btn_Test.Enabled = true;
                btn_StopTest.Enabled = false;
                cb_write.Enabled = true;
                cb_read.Enabled = true;
                if (currentTestCount == 0)
                    return;

                // 平均时间，所需时间
                DateTime now = DateTime.Now;
                TimeSpan ts = new TimeSpan();
                ts = now.Subtract(this.timeStartTest);
                int totalTime = Convert.ToInt32(ts.TotalMilliseconds);
                lblTime.Text = totalTime.ToString() + "ms";
                double avg = ts.TotalMilliseconds / (double)currentTestCount;
                lblAverageTime.Text = avg.ToString("0.0") + "ms";
            }));
        }

        private void FormTagRW_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btn_StopTest.Enabled)
            {
                btn_StopTest_Click(null, EventArgs.Empty);
            }
            if(!IsCouldClose)
            {
                if(MessageBox.Show("读写器正在运行，请稍等几秒再关闭窗体！","系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    if(!IsCouldClose)
                        e.Cancel = true;
                }
            }
        }

        private void cb_MB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_MB.Items[cb_MB.SelectedIndex].ToString() == "EPC")
            {
                if (selectTagParam.MemoryBank == MemoryBank.EPCMemory)
                {
                    MessageBox.Show("若要对EPC进行重复读写测试，请读取标签TID！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cb_MB.SelectedIndex = 0;
                    return;
                }
                num_Ptr.Value = 2;
                num_Ptr.Minimum = 2;
            }
            else
            {
                num_Ptr.Minimum = 0;
                num_Ptr.Value = 0;
            }
        }
    }
}
