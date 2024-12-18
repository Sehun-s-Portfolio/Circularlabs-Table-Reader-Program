using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetAPI.Entities;
using NetAPI;
using NetAPI.Protocol.VRP;

namespace RFIDTest
{
    public partial class FormTagAccess : Form
    {
        Reader reader;
        byte[] tagID;
        TagParameter selectParam;
        FormMain frmMain;

        public FormTagAccess(Reader reader, byte[] tagID, TagParameter selectParam, FormMain frmMain)
        {
            InitializeComponent();
            this.reader = reader;
            this.tagID = tagID;
            this.selectParam = selectParam;
            this.frmMain = frmMain;
        }

        private void FormTagAccess_Load(object sender, EventArgs e)
        {
            this.txtTagId.Text = Util.ConvertbyteArrayToHexstring(tagID);
        }

        #region 标签访问
        private void btnWrite_Click(object sender, EventArgs e)
        {
            int c = (ckbWriteEpc.Checked ? 1 : 0) + (ckbWriteUser.Checked ? 1 : 0) + (ckbWriteResv.Checked ? 1 : 0);
            if(c==0)
            {
                MessageBox.Show("请勾选写入区域！");
                return;
            }
            if (ckbWriteEpc.Checked)
            {
                if (txtWriteEpcData.Text.Trim() == "")
                {
                    MessageBox.Show("请输入16进制标签写入数据。");
                    return;
                }
            }
            if (ckbWriteUser.Checked)
            {
                if (txtWriteUser.Text.Trim() == "")
                {
                    MessageBox.Show("请输入16进制标签写入数据。");
                    return;
                }
            }
            if (ckbWriteResv.Checked)
            {
                if (txtAccessPwd.Text.Trim() == "" && txtKillPwd_Change.Text.Trim() == "")
                {
                    MessageBox.Show("请输入16进制修改密码。");
                    return;
                }
                if (txtAccessPwd.Text.Trim().Length > 0 && txtAccessPwd.Text.Trim().Length != 8)
                {
                    MessageBox.Show("请输入8位16进制标签访问密码。");
                    return;
                }
                if (txtKillPwd_Change.Text.Trim().Length > 0 && txtKillPwd_Change.Text.Trim().Length != 8)
                {
                    MessageBox.Show("请输入8位16进制标签销毁密码。");
                    return;
                }
            }
            
            WriteTagParameter wp = new WriteTagParameter();
            wp.SelectTagParam = selectParam;            
            wp.AccessPassword = Util.ConvertHexstringTobyteArray(txtPwd.Text.Trim());
            wp.WriteDataAry = new TagParameter[c];
            int p = 0;
            if (ckbWriteEpc.Checked)
            {
                TagParameter tp = new TagParameter();
                tp.MemoryBank = MemoryBank.EPCMemory;
                tp.Ptr = 0x02;
                tp.TagData = Util.ConvertHexstringTobyteArray(txtWriteEpcData.Text.Trim());
                wp.WriteDataAry[p] = tp;
                p++;
            }
            if (ckbWriteUser.Checked)
            {
                TagParameter tp = new TagParameter();
                tp.MemoryBank = MemoryBank.UserMemory;
                tp.Ptr = (UInt32)numWriteUserPtr.Value;
                tp.TagData = Util.ConvertHexstringTobyteArray(txtWriteUser.Text.Trim());
                wp.WriteDataAry[p] = tp;
                p++;
            }
            if (ckbWriteResv.Checked)
            {
                TagParameter tp = new TagParameter();
                tp.MemoryBank = MemoryBank.ReservedMemory;
                if (txtKillPwd_Change.Text.Trim() == "")                    
                    tp.Ptr = 0x02;
                List<byte> td = new List<byte>();                
                if (txtKillPwd_Change.Text.Trim() != "")
                {
                    td.AddRange( Util.ConvertHexstringTobyteArray(txtKillPwd_Change.Text.Trim()));//销毁密码
                }
                if (txtAccessPwd.Text.Trim() != "")
                {
                    td.AddRange(Util.ConvertHexstringTobyteArray(txtAccessPwd.Text.Trim()));//访问密码
                }
                tp.TagData = td.ToArray();
                wp.WriteDataAry[p] = tp;
            }
            MsgTagWrite msg = new MsgTagWrite(wp);
            if (reader.Send(msg))
            {
                if (ckbWriteEpc.Checked || ckbWriteUser.Checked)
                {
                    string epc = "";
                    string user = "";
                    if (ckbWriteEpc.Checked)
                    {
                        epc = Util.ConvertbyteArrayToHexWordstring(Util.ConvertHexstringTobyteArray(txtWriteEpcData.Text));
                        txtTagId.Text = epc.Trim().Replace(" ", "");
                        selectParam.TagData = Util.ConvertHexstringTobyteArray(epc);
                    }
                    if (ckbWriteUser.Checked)
                    {
                        user = Util.ConvertbyteArrayToHexWordstring(Util.ConvertHexstringTobyteArray(txtWriteUser.Text));
                    }
                    if (epc != "" || user != "")
                        frmMain.SetEpcAndUserData(epc, user);
                }
                MessageBox.Show("写入成功");
            }
            else
                MessageBox.Show("写入失败：" + msg.ErrorInfo.ErrMsg);
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            if(!(ckbReadEpc.Checked || ckbReadUser.Checked || ckbReadTid.Checked || ckbReadResv.Checked))
            {
                MessageBox.Show("请勾选要读取的区域");
                return;
            }
            #region 读标签
            ReadTagParameter rp = new ReadTagParameter();
            rp.SelectTagParam = selectParam;
            rp.AccessPassword = Util.ConvertHexstringTobyteArray(txtPwd.Text.Trim());
            rp.IsReturnEPC = ckbReadEpc.Checked;
            rp.IsReturnTID = ckbReadTid.Checked;
            if (ckbReadUser.Checked)
            {
                rp.UserPtr = (uint)numReadUserPtr.Value;
                rp.UserLen = (byte)numReadUserLen.Value;
            }
            rp.IsReturnReserved = ckbReadResv.Checked;

            MsgTagRead msg = new MsgTagRead(rp);
            if (reader.Send(msg))
            {
                txtReadEpc.Text = Util.ConvertbyteArrayToHexWordstring(msg.ReceivedMessage.TagData.EPC);
                txtReadTid.Text = Util.ConvertbyteArrayToHexWordstring(msg.ReceivedMessage.TagData.TID);
                txtReaduser.Text = Util.ConvertbyteArrayToHexWordstring(msg.ReceivedMessage.TagData.User);
                if (ckbReadResv.Checked)
                {
                    byte[] resv = msg.ReceivedMessage.TagData.Reserved;
                    txtReadAPwd.Text = (resv.Length >= 4) ? Util.ConvertbyteArrayToHexstring(resv).Substring(0, 8) : "";
                    txtReadKPwd.Text = (resv.Length >= 8) ? Util.ConvertbyteArrayToHexstring(resv).Substring(8) : "";
                }
            }
            else
            {
                MessageBox.Show("读取失败：" + msg.ErrorInfo.ErrMsg);
            }
            #endregion
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            if (cbLockMemory.SelectedIndex == -1 || cbLockType.SelectedIndex == -1)
            {
                MessageBox.Show("请选择锁定条件。");
                return;
            }
            
            LockTagParameter lp = new LockTagParameter();
            lp.SelectTagParam = selectParam;
            lp.LockBank = (LockBank)cbLockMemory.SelectedIndex;
            lp.LockType = (LockType)cbLockType.SelectedIndex;            
            lp.AccessPassword = Util.ConvertHexstringTobyteArray(txtPwd.Text.Trim());
            MsgTagLock msg = new MsgTagLock(lp);
            if (reader.Send(msg))
                MessageBox.Show("操作成功");
            else
                MessageBox.Show("操作失败：" + msg.ErrorInfo.ErrMsg);
        }

        private void btnKillTag_Click(object sender, EventArgs e)
        {
            if (txtKillPwd.Text.Trim().Length != 8)
            {
                MessageBox.Show("请输入8位16进制标签销毁密码。");
                return;
            }
            
            KillTagParameter kp = new KillTagParameter();
            kp.KillPassword = Util.ConvertHexstringTobyteArray(txtKillPwd.Text.Trim());
            kp.SelectTagParam = selectParam;
            MsgTagKill msg = new MsgTagKill(kp);
            if (reader.Send(msg))
                MessageBox.Show("销毁成功");
            else
                MessageBox.Show("销毁失败：" + msg.ErrorInfo.ErrMsg);
        }
        #endregion

        #region 输入框
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

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            int txtSelPtr = txt.SelectionStart;
            string str = txt.Text;
            int bCount = 0;
            for (int i = 0; i < txtSelPtr; i++)
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
        #endregion
    }
}
