using System;
using System.Data;
using System.Windows.Forms;
using NetAPI;
using NetAPI.Protocol.VRP;
using NetAPI.Entities;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Net.WebRequestMethods;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Security.Cryptography;


namespace RFIDTest
{
    public partial class FormMain : Form
    {
        #region 字段
        public Reader reader = null;
        volatile bool isTriggerScan = false;
        object LockRxdTagData = new object();
        DataTable mDt = null;
        DateTime dateTime;
        public long totalCount;
        public volatile UInt32 lastCount;
        public System.Timers.Timer myStatTimer = new System.Timers.Timer(1000);
        Thread tBtnClkCnt;
        volatile bool isEnBtnClkCnt = true;
        volatile bool isClickConn = false;
        volatile bool isClickScan = false;
        volatile bool isClickGetBuff = false;
        volatile bool isScan = false;
        volatile bool isBeep = false;
        [DllImport("kernel32.dll")]
        public static extern bool Beep(int freq, int duration);

        #endregion

        #region 窗体函数
        public FormMain()
        {
            InitializeComponent();
            initTagDataTable();
            myStatTimer.Elapsed += new System.Timers.ElapsedEventHandler(myStatTimer_Elapsed);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            AllocConsole();
            DisbleQuickEditMode();
            tBtnClkCnt = new Thread(new ThreadStart(tBtnClkCntMethod));
            tBtnClkCnt.Start();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [DllImport("Kernel32")]
        public static extern void FreeConsole();

        const int STD_INPUT_HANDLE = -10;
        const uint ENABLE_QUICK_EDIT_MODE = 0x0040;
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr GetStdHandle(int hConsoleHandle);
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint mode);
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint mode);

        public static void DisbleQuickEditMode()
        {
            IntPtr hStdin = GetStdHandle(STD_INPUT_HANDLE);
            uint mode;
            GetConsoleMode(hStdin, out mode);
            mode &= ~ENABLE_QUICK_EDIT_MODE;
            SetConsoleMode(hStdin, mode);

        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                FreeConsole();
                isEnBtnClkCnt = false;
                if (btnConn.Text == "끊기")
                    btnConn_Click(null, EventArgs.Empty);
                Environment.Exit(Environment.ExitCode);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
        #endregion

        #region 菜单、按钮
        private void MI_Conn_Click(object sender, EventArgs e)
        {
            FormConn frm = new FormConn();
            if (frm.ShowDialog() == DialogResult.OK)
                btnConn_Click(null, EventArgs.Empty);
        }

        private void MI_AntennaConfig_Click(object sender, EventArgs e)
        {
            FormAntennaConfig frm = new FormAntennaConfig(reader);
            frm.ShowDialog();
        }

        private void MI_ReaderConfig_Click(object sender, EventArgs e)
        {
            FormReaderConfig frm = new FormReaderConfig(reader, this);
            frm.ShowDialog();
        }

        private void MI_ReaderTest_Click(object sender, EventArgs e)
        {
            FormReaderTest frm = new FormReaderTest(this.reader, this);
            frm.ShowDialog();
        }

        private void MI_AppCpuTest_Click(object sender, EventArgs e)
        {
            FormAppCpuTest frm = new FormAppCpuTest(this.reader);
            frm.ShowDialog();
        }

        private void MI_TagRW_Click(object sender, EventArgs e)
        {
            if (btnConn.Text == "연결")
            {
                MessageBox.Show("리더기를 연결하십시오.");
                return;
            }
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("작업 탭을 선택하십시오");
                return;
            }
            TagParameter selectParam = getSelectTagParameter();
            if (tagID == null || tagID.Length == 0)
            {
                MessageBox.Show("작업 탭을 선택하십시오");
                return;
            }
            FormTagRW frm = new FormTagRW(reader, selectParam);
            frm.ShowDialog();
            tagID = null;
        }

        private void MI_HabConfig_Click(object sender, EventArgs e)
        {
            FormHabConfig frm = new FormHabConfig(reader);
            frm.ShowDialog();
            frm.Dispose();
        }

        private void MI_PortalConfig_Click(object sender, EventArgs e)
        {
            FormPortalConfig frm = new FormPortalConfig(reader);
            frm.ShowDialog();
        }

        private void MI_BookShelf_Click(object sender, EventArgs e)
        {
            FormBookShelf frm = new FormBookShelf(reader);
            frm.ShowDialog();
        }


        public void btnConn_Click(object sender, EventArgs e)
        {
            if (isClickConn)
                return;
            isClickConn = true;
            //if (btnConn.Text == "连接")
            if (btnConn.Text == "연결")
            {
                bool isSuc = false;
                reader = new Reader("Device1");
                ConnectResponse rep = reader.Connect();
                if (rep.IsSucessed)
                {
                    isSuc = true;
                    comboBox1.SelectedIndex = 0;
                    // TODO: 添加事件
                    reader.OnInventoryReceived += OnInventoryReceived;
                    reader.OnMsgAlarmInfo_AccessControl += Reader_OnMsgAlarmInfo_AccessControl;
                    reader.OnBrokenNetwork += Reader_OnBrokenNetwork;

                    ckbAntenna.Checked = reader.IsEnableAntenna;
                    ckbRSSI.Checked = reader.IsEnableRSSI;

                    setText(lblMsg, "연결 성공！");
                    //btnConn.Text = "断开";
                    btnConn.Text = "중단";
                }
                else
                    setText(lblMsg, "연결 실패，" + rep.ErrorInfo.ErrMsg);

                if (isSuc)
                    changeState(demoState.Connected);
            }
            else
            {
                reader.Disconnect();
                // 删除事件
                reader.OnInventoryReceived -= OnInventoryReceived;
                reader.OnMsgAlarmInfo_AccessControl -= Reader_OnMsgAlarmInfo_AccessControl;
                reader.OnBrokenNetwork -= Reader_OnBrokenNetwork;
                changeState(demoState.Disconnected);
                string str = "연결 끊기";
                setText(lblMsg, str);
                reader = null;
                //btnConn.Text = "连接";
                btnConn.Text = "연결";
            }
        }

        private void Reader_OnBrokenNetwork(string readerName, ErrInfo errInfo)
        {
            //if(btnConn.Text == "断开")
            if (btnConn.Text == "중단")
            {
                this.BeginInvoke((MethodInvoker)delegate () { btnConn_Click(null, EventArgs.Empty); });
            }
            MessageBox.Show(readerName + ":" + errInfo.ErrMsg);
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            if (isClickScan)
                return;
            isClickScan = true;
            if (btnScan.Text == "시작")
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    btnScan.Text = "중지";
                });
                scan();
            }
            else
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    btnScan.Text = "시작";
                });
                stop();
                isScan = false;
            }
        }

        private void scan()
        {
            isStop = false;
            bool isSuc = false;
            cleanDisplay();
            initTagDataTable();// 重新绑定数据，目的是取消排序功能
            this.dateTime = DateTime.Now;

            reader.Send(new MsgPowerOff());//先停止
            ReadTagParameter rp = new ReadTagParameter();
            rp.IsLoop = true;
            rp.ReadCount = (ushort)numericUpDown1.Value;
            rp.TotalReadTime = (ushort)numericUpDown2.Value;
            rp.IsReturnEPC = ckbEPC.Checked;
            rp.IsReturnTID = ckbTID.Checked;
            if (ckbUser.Checked)
            {
                rp.UserPtr = (uint)numUserPtr.Value;
                rp.UserLen = (byte)numUserLen.Value;
            }
            else
            {
                rp.UserPtr = 0;
                rp.UserLen = 0;
            }

            string err = "";
            if (ckbEPC.Checked && !ckbTID.Checked && !ckbUser.Checked)
            {
                MsgTagInventory msg = new MsgTagInventory(rp);
                if (reader.Send(msg))
                    isSuc = true;
                else
                    err = msg.ErrorInfo.ErrMsg;
            }
            else
            {
                MsgTagRead msg = new MsgTagRead(rp);
                if (reader.Send(msg))
                    isSuc = true;
                else
                    err = msg.ErrorInfo.ErrMsg;
            }

            if (isSuc)
            {
                if (rp.TotalReadTime > 0)
                {
                    scanTime = rp.TotalReadTime;
                    new Thread(new ThreadStart(autoStop)).Start();
                }
                if (ckbBeep.Checked)
                {
                    isScan = true;
                    new Thread(new ThreadStart(beepMethod)).Start();
                }
                dataGridView1.Enabled = false;
                changeState(demoState.Scaning);
                myStatTimer.Start();
                setText(lblMsg, "태그 검색 중...");
            }
            else
            {
                setText(lblMsg, "태그 명령 보내기 실패," + err);
            }
        }

        private int scanTime = 0;
        private volatile bool isStop = false;

        private void autoStop()
        {
            int maxcount = scanTime + 10;
            for (int i = 0; i < maxcount; i++)
            {
                Thread.Sleep(10);
                if (isStop)
                {
                    return;
                }
            }
            btnScan_Click(null, EventArgs.Empty);
        }

        private void scanEpc()
        {
            bool isSuc = false;
            cleanDisplay();
            initTagDataTable();// 重新绑定数据，目的是取消排序功能
            this.dateTime = DateTime.Now;

            reader.Send(new MsgPowerOff());//先停止
            MsgTagInventory msg = new MsgTagInventory();
            if (reader.Send(msg))
                isSuc = true;

            if (isSuc)
            {
                dataGridView1.Enabled = false;
                myStatTimer.Start();
                changeState(demoState.Scaning);
                setText(lblMsg, "태그 검색 중...");
            }
            else
            {
                setText(lblMsg, "태그 명령 보내기 실패," + msg.ErrorInfo.ErrMsg);
            }
        }

        private void scanTid()
        {
            bool isSuc = false;
            cleanDisplay();
            initTagDataTable();// 重新绑定数据，目的是取消排序功能
            this.dateTime = DateTime.Now;

            reader.Send(new MsgPowerOff());//先停止
            MsgTagRead msg = new MsgTagRead();//默认构造函数提供循环读TID快捷功能
            if (reader.Send(msg))
                isSuc = true;

            if (isSuc)
            {
                dataGridView1.Enabled = false;
                myStatTimer.Start();
                changeState(demoState.Scaning);
                setText(lblMsg, "태그 검색 중...");
            }
            else
            {
                setText(lblMsg, "태그 명령 보내기 실패," + msg.ErrorInfo.ErrMsg);
            }
        }

        private void stop()
        {
            string errStr = "";
            this.BeginInvoke((MethodInvoker)delegate
            {
                bool isSuc = false;
                MsgPowerOff msg = new MsgPowerOff();
                if (!isTriggerScan)
                {
                    if (reader.Send(msg))
                        isSuc = true;
                }
                else
                    isSuc = true;
                if (isSuc)
                {
                    isStop = true;
                    dataGridView1.Enabled = true;
                    changeState(demoState.Stop);
                    setText(lblMsg, "스캔 중지！");
                }
                else
                {
                    setText(lblMsg, "스캔 중지 실패：" + errStr);
                }
                myStatTimer.Stop();// 停止计时  
                stat();
            });
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            cleanDisplay();
            setText(lblAvgSpeed, "000");
            setText(lblScanTime, "00:00:00");
            setText(lblTotalCount, "000");
            setText(lblTotal, "000");
            totalCount = 0;
            lastCount = 0;
            dateTime = DateTime.Now;
        }

        void myStatTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            stat();
        }

        private void stat()
        {
            TimeSpan ts = new TimeSpan();
            ts = DateTime.Now.Subtract(this.dateTime);
            string rtime = string.Format("{0}:{1}:{2}",
                (ts.Days * 24 + ts.Hours).ToString().PadLeft(2, '0'),
                ts.Minutes.ToString().PadLeft(2, '0'),
                ts.Seconds.ToString().PadLeft(2, '0'));
            setText(this.lblScanTime, rtime);

            string aSpeed = "0";
            if (totalCount > 0)
            {
                setText(this.lblTotalCount, totalCount.ToString().PadLeft(3, '0'));
                aSpeed = string.Format("{0:D}", (totalCount * 1000 / (int)ts.TotalMilliseconds));
                setText(this.lblAvgSpeed, aSpeed.PadLeft(3, '0'));
            }

            string tCount = mDt.Rows.Count.ToString();
            setText(lblTotal, tCount.PadLeft(3, '0'));
            lastCount = (uint)totalCount;
        }

        private void setText(Label ctrl, string txt)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                ctrl.Text = txt;
            });
        }

        private void ckbTagFieldConfig_CheckedChanged(object sender, EventArgs e)
        {
            // 设置读6C标签数据包含字段            
            Msg6CTagFieldConfig msg = new Msg6CTagFieldConfig(ckbAntenna.Checked, ckbRSSI.Checked);
            if (reader.Send(msg))
            {

            }
            else
                MessageBox.Show(msg.ErrorInfo.ErrMsg);
        }
        #endregion

        #region 接收事件
        private async void OnInventoryReceived(string readerName, RxdTagData tagData)
        {
            string epc = Util.ConvertbyteArrayToHexWordstring(tagData.EPC);

            if (epc.Contains("CCA2 3101"))
            {
                if (tagData != null)
                {
                    // 필터링 코드 : CCA2 3101
                    display(readerName, tagData);

                    string antenna = (tagData.Antenna == 0x00) ? "" : tagData.Antenna.ToString();
                    string hub = (tagData.Hub == 0x00) ? "" : tagData.Hub.ToString();
                    string port = (tagData.Port == 0x00) ? "" : tagData.Port.ToString();

                    string tid = Util.ConvertbyteArrayToHexWordstring(tagData.TID);
                    string user = Util.ConvertbyteArrayToHexWordstring(tagData.User);
                    string rssi = (tagData.RSSI == 0x00) ? "" : tagData.GetRSSI.ToString("0.0");
                    string time = DateTime.Now.ToString("HH:mm:ss.fff");
                    int readCount = 1;

                    foreach (DataRow dr in mDt.Rows)
                    {
                        if (dr["EPC"].ToString() == epc
                                && dr["TID"].ToString() == tid
                                && dr["UserData"].ToString() == user)
                        {
                            readCount = int.Parse(dr["ReadCount"].ToString()) + 1;
                        }
                    }

                    RfidScanData rfidScanData = new RfidScanData(epc, tid, user, antenna, readCount, time);

                    string beforeData =
                        "서버에 보내기 전 데이터 체크" + "\n" +
                        "\n" +
                        "EPC : " + rfidScanData.Epc + "\n" +
                        "TID : " + rfidScanData.Tid + "\n" +
                        "USER DATA : " + rfidScanData.UserData + "\n" +
                        "ANTENNA : " + rfidScanData.Antenna + "\n" +
                        "READ COUNT : " + rfidScanData.ReadCount + "\n" +
                        "TIME STAMP : " + rfidScanData.TimeStamp + "\n";

                    Console.WriteLine(beforeData);

                    HttpClient client = new HttpClient();
                    client.BaseAddress = new Uri("http://localhost:8090/rfid/data/solo/upload");

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(
                            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                        );

                    // [이후 토큰 값 필요 시 사용]
                    //String tokenValue = "{JWT 토큰 값}";
                    //client.DefaultRequestHeaders.Add("Authorization", "Bearer" + tokenValue);

                    // 데이터 업로드 서버 요청
                    HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress, rfidScanData);

                    // 정상 업로드 및 반환 후 객체에 매핑
                    var responseData = await response.Content.ReadFromJsonAsync<ResponseContent>();
                    // 객체에 매핑된 반환 데이터를 json 형식으로 컨버트
                    //var convertJsonData = JsonConvert.SerializeObject(responseData);

                    // 반환 데이터 중 Data 필드의 데이터들만 따로 JsonDocument로 파싱
                    JsonDocument json = JsonDocument.Parse(responseData.Data);
                    // 파싱된 Data 필드 데이터에 접근하여 변수로 고정
                    var rootJson = json.RootElement;

                    // Data 필드에 존재하는 child 필드들의 값들을 호출
                    string responseEpc = rootJson.GetProperty("epc").ToString();
                    string responseTid = rootJson.GetProperty("tid").ToString();
                    string responseUserData = rootJson.GetProperty("userData").ToString();
                    string responseAntenna = rootJson.GetProperty("antenna").ToString();
                    int responsereadCount = rootJson.GetProperty("readCount").GetInt32();
                    string responseTimeStamp = rootJson.GetProperty("timeStamp").ToString();

                    string afterData =
                        "서버 업로드 후 데이터 체크" + "\n" +
                        "\n" +
                        "EPC : " + responseEpc + "\n" +
                        "TID : " + responseTid + "\n" +
                        "USER DATA : " + responseUserData + "\n" +
                        "ANTENNA : " + responseAntenna + "\n" +
                        "READ COUNT : " + responsereadCount + "\n" +
                        "TIME STAMP : " + responseTimeStamp + "\n";

                    Console.WriteLine(afterData);
                }
            }
            else
            {
                Console.WriteLine("공식 제품이 아닙니다.");
            }

        }

        private void Reader_OnMsgAlarmInfo_AccessControl(string readerName, RxdTagData tagData)
        {
            if (tagData != null)
                display(readerName, tagData);
        }


        // 스캔 시 노출 단 함수
        private void display(string readerName, RxdTagData tagData)
        {
            isBeep = true;

            dataGridView1.BeginInvoke(
                (MethodInvoker)delegate
                {
                    bool isAdd = true;
                    string antenna = (tagData.Antenna == 0x00) ? "" : tagData.Antenna.ToString();
                    string hub = (tagData.Hub == 0x00) ? "" : tagData.Hub.ToString();
                    string port = (tagData.Port == 0x00) ? "" : tagData.Port.ToString();
                    string epc = Util.ConvertbyteArrayToHexWordstring(tagData.EPC);
                    string tid = Util.ConvertbyteArrayToHexWordstring(tagData.TID);
                    string user = Util.ConvertbyteArrayToHexWordstring(tagData.User);
                    string rssi = (tagData.RSSI == 0x00) ? "" : tagData.GetRSSI.ToString("0.0");
                    string time = DateTime.Now.ToString("HH:mm:ss.fff");

                    lock (LockRxdTagData)
                    {
                        this.totalCount++;//计算读取速度
                        if (mDt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in mDt.Rows)
                            {
                                if (dr["EPC"].ToString() == epc
                                        && dr["TID"].ToString() == tid
                                        && dr["UserData"].ToString() == user)
                                    isAdd = false;

                                if (!isAdd)
                                {
                                    dr["ReadCount"] = int.Parse(dr["ReadCount"].ToString()) + 1;
                                    dr["Antenna"] = antenna;
                                    dr["Hub"] = hub;
                                    dr["Port"] = port;
                                    dr["RSSI"] = rssi;
                                    dr["TimeStamp"] = time;

                                    //readCount = int.Parse(dr["ReadCount"].ToString());
                                    break;
                                }
                            }
                        }

                        if (isAdd)
                        {
                            DataRow mydr = mDt.NewRow();
                            mydr[0] = epc;
                            mydr[1] = tid;
                            mydr[2] = user;
                            mydr[3] = "1";
                            mydr[4] = antenna;
                            mydr[5] = hub;
                            mydr[6] = port;
                            mydr[7] = rssi;
                            mydr[8] = time;
                            mDt.Rows.Add(mydr);
                        }

                    }

                    Console.WriteLine(readerName + " : " + epc);
                }

            );

        }



        private void beepMethod()
        {
            while (isScan)
            {
                if (isBeep)
                {
                    isBeep = false;
                    new Thread(new ThreadStart(delegate () { Beep(600, 50); })).Start();
                    Thread.Sleep(5);
                }
                else
                {
                    Thread.Sleep(10);
                }

            }
        }
        #endregion

        #region 软件状态改变
        private delegate void changStateHandle(demoState state);
        private void changeState(demoState state)
        {
            if (this.InvokeRequired)
            {
                changStateHandle h = new changStateHandle(changeStateMethod);
                this.BeginInvoke(h, state);
            }
            else
                changeStateMethod(state);
        }

        private void changeStateMethod(demoState state)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                switch (state)
                {
                    case demoState.Connected:
                    case demoState.Stop:
                        btnScan.Enabled = true;
                        pScan.Enabled = true;
                        pGetBuff.Enabled = true;
                        pGetBuffCount.Enabled = true;
                        pMode.Enabled = true;
                        MI_Conn.Enabled = false;
                        MI_AntennaConfig.Enabled = true;
                        MI_ReaderConfig.Enabled = true;
                        MI_ReaderTest.Enabled = true;
                        MI_AppCpuTest.Enabled = true;
                        MI_TagRW.Enabled = true;
                        MI_HabConfig.Enabled = true;
                        MI_PortalConfig.Enabled = true;
                        MI_BookShelf.Enabled = true;
                        break;
                    case demoState.Disconnected:
                        btnScan.Enabled = false;
                        pScan.Enabled = false;
                        pGetBuff.Enabled = false;
                        pGetBuffCount.Enabled = false;
                        pMode.Enabled = false;
                        MI_Conn.Enabled = true;
                        MI_AntennaConfig.Enabled = false;
                        MI_ReaderConfig.Enabled = false;
                        MI_ReaderTest.Enabled = false;
                        MI_AppCpuTest.Enabled = false;
                        MI_TagRW.Enabled = false;
                        MI_HabConfig.Enabled = false;
                        MI_PortalConfig.Enabled = false;
                        MI_BookShelf.Enabled = false;
                        break;
                    case demoState.Scaning:
                        btnScan.Enabled = true;
                        pScan.Enabled = false;
                        pScan.Enabled = false;
                        pGetBuff.Enabled = false;
                        pGetBuffCount.Enabled = false;
                        pMode.Enabled = false;
                        MI_Conn.Enabled = false;
                        MI_AntennaConfig.Enabled = false;
                        MI_ReaderConfig.Enabled = false;
                        MI_ReaderTest.Enabled = false;
                        MI_AppCpuTest.Enabled = false;
                        MI_TagRW.Enabled = false;
                        MI_HabConfig.Enabled = false;
                        MI_PortalConfig.Enabled = false;
                        MI_BookShelf.Enabled = false;
                        break;
                }
            });
        }

        private enum demoState
        {
            Connected,
            Disconnected,
            Scaning,
            Stop
        }
        #endregion

        #region 清空数据
        private void cleanDisplay()
        {
            dataGridView1.BeginInvoke((MethodInvoker)delegate
            {
                lock (LockRxdTagData)
                {
                    mDt.Rows.Clear();
                    totalCount = 0;
                    this.dateTime = DateTime.Now;
                }
            });
        }
        #endregion

        #region 绑定数据表
        private void initTagDataTable()
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new MethodInvoker(initTagDataTableMethod));
            else
                initTagDataTableMethod();
        }

        private void initTagDataTableMethod()
        {
            mDt = new DataTable();
            mDt.Columns.Add("EPC");
            mDt.Columns.Add("TID");
            mDt.Columns.Add("UserData");
            mDt.Columns.Add("ReadCount");
            mDt.Columns.Add("Antenna");
            mDt.Columns.Add("Hub");
            mDt.Columns.Add("Port");
            mDt.Columns.Add("RSSI");
            mDt.Columns.Add("TimeStamp");
            this.dataGridView1.DataSource = mDt;
            dataGridView1.Columns[0].Width = 245;
            dataGridView1.Columns[1].Width = 190;
            dataGridView1.Columns[2].Width = 260;
            dataGridView1.Columns[3].Width = 70;
            dataGridView1.Columns[4].Width = 55;
            dataGridView1.Columns[5].Width = 55;
            dataGridView1.Columns[6].Width = 55;
            dataGridView1.Columns[7].Width = 45;
            dataGridView1.Columns[8].Width = 90;
            dataGridView1.Columns[0].Visible = ckbEPC.Checked;
            dataGridView1.Columns[1].Visible = ckbTID.Checked;
            dataGridView1.Columns[2].Visible = ckbUser.Checked;
        }
        #endregion

        #region 标签操作
        byte[] tagID = null;
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (btnConn.Text == "연결")
            {
                MessageBox.Show("리더기를 연결하십시오.");
                return;
            }
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("작업 탭을 선택하십시오");
                return;
            }
            TagParameter selectParam = getSelectTagParameter();
            if (tagID == null || tagID.Length == 0)
            {
                MessageBox.Show("작업 탭을 선택하십시오");
                return;
            }
            FormTagAccess frm = new FormTagAccess(reader, tagID, selectParam, this);
            frm.ShowDialog();
            tagID = null;
        }

        private TagParameter getSelectTagParameter()
        {
            TagParameter selParam = new TagParameter();

            if (dataGridView1.CurrentRow.Cells["TID"].Value != null && dataGridView1.CurrentRow.Cells["TID"].Value.ToString() != "")
            {
                tagID = Util.ConvertHexstringTobyteArray(dataGridView1.CurrentRow.Cells["TID"].Value.ToString());
                selParam.MemoryBank = MemoryBank.TIDMemory;
                selParam.TagData = tagID;
            }
            else
            {
                tagID = Util.ConvertHexstringTobyteArray(dataGridView1.CurrentRow.Cells["EPC"].Value.ToString());
                selParam.MemoryBank = MemoryBank.EPCMemory;
                selParam.TagData = tagID;
                //selParam.Ptr = 0x20;//2019.6.26.修改，所有匹配指令，匹配EPC都是从0开始
            }
            return selParam;
        }

        public void SetEpcAndUserData(string epc, string user)
        {
            if (dataGridView1.CurrentRow != null)
            {
                if (!string.IsNullOrEmpty(epc))
                    dataGridView1.CurrentRow.Cells["EPC"].Value = epc;
                if (!string.IsNullOrEmpty(user) && dataGridView1.Columns[2].Visible)
                    dataGridView1.CurrentRow.Cells["UserData"].Value = user;
            }
        }
        #endregion

        #region 门禁

        private void btnQueryMode_Click(object sender, EventArgs e)
        {
            MsgWorkModeConfig_AccessControl msg = new MsgWorkModeConfig_AccessControl();
            if (reader.Send(msg))
            {
                radioButton1.Checked = (msg.ReceivedMessage.WorkMode == WorkMode_AccessControl.OffLine);
                radioButton2.Checked = (msg.ReceivedMessage.WorkMode == WorkMode_AccessControl.OnLine);
            }
            else
                MessageBox.Show(msg.ErrorInfo.ErrMsg);
        }

        private void btnConfigMode_Click(object sender, EventArgs e)
        {
            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                MessageBox.Show("설정 파라미터를 선택하십시오");
                return;
            }
            WorkMode_AccessControl ctrl = WorkMode_AccessControl.OffLine;
            if (radioButton2.Checked)
                ctrl = WorkMode_AccessControl.OnLine;
            MsgWorkModeConfig_AccessControl msg = new MsgWorkModeConfig_AccessControl(ctrl);
            if (reader.Send(msg))
                MessageBox.Show("설정 성공！");
            else
                MessageBox.Show("설정 실패：" + msg.ErrorInfo.ErrMsg);
        }

        private void btnQueryBuff_Click(object sender, EventArgs e)
        {
            MsgAlarmDataCacheNumQuery_AccessControl msg = new MsgAlarmDataCacheNumQuery_AccessControl();
            if (reader.Send(msg))
            {
                textBox1.Text = msg.ReceivedMessage.DataCacheNum.ToString();
            }
            else
                MessageBox.Show(msg.ErrorInfo.ErrMsg);
        }

        private void btnGetBuff_Click(object sender, EventArgs e)
        {
            if (isClickGetBuff)
                return;
            isClickGetBuff = true;
            cleanDisplay();
            initTagDataTable();// 重新绑定数据，目的是取消排序功能
            MsgAlarmInfo_AccessControl msg = new MsgAlarmInfo_AccessControl((ushort)numericUpDown3.Value);
            if (reader.Send(msg))
            {
                new Thread(new ThreadStart(buffStat)).Start();
            }
            else
                MessageBox.Show(msg.ErrorInfo.ErrMsg);
        }

        private void buffStat()
        {
            Thread.Sleep(3000);
            setText(lblTotalCount, totalCount.ToString().PadLeft(3, '0'));
            setText(lblTotal, mDt.Rows.Count.ToString().PadLeft(3, '0'));
        }

        private void btnCleanBuff_Click(object sender, EventArgs e)
        {
            MsgAlarmDataCacheClean_AccessControl msg = new MsgAlarmDataCacheClean_AccessControl();
            if (reader.Send(msg))
            {
                MessageBox.Show("비우기가 완료되었으며, 모듈에 있는 캐시 데이터가 비었습니다.");
            }
            else
                MessageBox.Show(msg.ErrorInfo.ErrMsg);
        }

        // 데이터 업로드 버튼 클릭 시 실제로 RestAPI 호출 동작 수행
        private async void btnTransferData_Click(object sender, EventArgs e)
        {
            MsgAlarmDataCacheClean_AccessControl msg = new MsgAlarmDataCacheClean_AccessControl();
            if (reader.Send(msg))
            {
                Console.WriteLine("데이터 전송 버튼 동작 확인");

                List<RfidScanData> requestRfidDataList = new List<RfidScanData>();

                // 다중 요청 데이터
                foreach (DataRow row in mDt.Rows)
                {
                    string epc = row["EPC"].ToString();
                    string tid = row["TID"].ToString();
                    string userData = row["UserData"].ToString();
                    string antenna = row["Antenna"].ToString();
                    int readCount = int.Parse(row["ReadCount"].ToString());
                    string timeStamp = row["TimeStamp"].ToString();

                    requestRfidDataList.Add(new RfidScanData() { Epc = epc, Tid = tid, UserData = userData, Antenna = antenna, ReadCount = readCount, TimeStamp = timeStamp });
                }

                // 단일 요청 데이터
                //var uploadData = new RfidScanData() { Epc = "EPC", Tid = "TID", UserData = "USER DATA", Antenna  = "ANTENNA", ReadCount  = 2, TimeStamp  = "2024-06-13 14:23:00"};

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://116.125.141.139:8090/rfid/data/multi/upload");
                //client.BaseAddress = new Uri("http://localhost:8090/rfid/data/multi/upload");

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                    );

                // [이후 토큰 값 필요 시 사용]
                //String tokenValue = "{JWT 토큰 값}";
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer" + tokenValue);

                // 데이터 업로드 서버 요청
                HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress, requestRfidDataList);

                // 정상 업로드 및 반환 후 객체에 매핑
                var responseData = await response.Content.ReadFromJsonAsync<ResponseContent>();
                // 객체에 매핑된 반환 데이터를 json 형식으로 컨버트
                //var convertJsonData = JsonConvert.SerializeObject(responseData);

                // 반환 데이터 중 Data 필드의 데이터들만 따로 JsonDocument로 파싱
                JsonDocument json = JsonDocument.Parse(responseData.Data);
                // 파싱된 Data 필드 데이터에 접근하여 변수로 고정
                var rootJson = json.RootElement;

                string checkFinalResponse = "";

                foreach (var element in rootJson.EnumerateArray())
                {
                    // Data 필드에 존재하는 child 필드들의 값들을 호출
                    string responseEpc = element.GetProperty("epc").ToString();
                    string responseTid = element.GetProperty("tid").ToString();
                    string responseUserData = element.GetProperty("userData").ToString();
                    string responseAntenna = element.GetProperty("antenna").ToString();
                    int responsereadCount = element.GetProperty("readCount").GetInt32();
                    string responseTimeStamp = element.GetProperty("timeStamp").ToString();

                    string totalData =
                        "\n" +
                        "EPC : " + responseEpc + "\n" +
                        "TID : " + responseTid + "\n" +
                        "USER DATA : " + responseUserData + "\n" +
                        "ANTENNA : " + responseAntenna + "\n" +
                        "READ COUNT : " + responsereadCount + "\n" +
                        "TIME STAMP : " + responseTimeStamp + "\n";

                    checkFinalResponse += totalData;
                }


                MessageBox.Show(
                    checkFinalResponse
                 );
            }
            else
                MessageBox.Show(msg.ErrorInfo.ErrMsg);

        }

        #endregion

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte q = 4;
            SessionInfo si = new SessionInfo();
            if (comboBox1.SelectedIndex == 0)
            {
                q = 4;
                si.Flag = Flag.Flag_A;
                si.Session = Session.S1;
            }
            else
            {
                q = 0;
                si.Flag = Flag.Flag_A_B;
                si.Session = Session.S0;
            }
            {
                MsgQValueConfig msg = new MsgQValueConfig(q);
                reader.Send(msg);
            }
            {
                MsgSessionConfig msg = new MsgSessionConfig(si);
                reader.Send(msg);
            }
        }

        private void ckbEPC_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Columns["EPC"].Visible = ckbEPC.Checked;
        }

        private void ckbTID_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Columns["TID"].Visible = ckbTID.Checked;
        }

        private void ckbUser_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Columns["UserData"].Visible = ckbUser.Checked;
        }

        private void tBtnClkCntMethod()
        {
            int delayTime0 = 0;
            int delayTime1 = 0;
            int delayTime2 = 0;
            while (isEnBtnClkCnt)
            {
                Thread.Sleep(20);
                if (isClickConn)
                {
                    delayTime0++;
                    if (delayTime0 == 100)
                    {
                        delayTime0 = 0;
                        isClickConn = false;
                    }
                }
                if (isClickScan)
                {
                    delayTime1++;
                    if (delayTime1 == 25)
                    {
                        delayTime1 = 0;
                        isClickScan = false;
                    }
                }
                if (isClickGetBuff)
                {
                    delayTime2++;
                    if (delayTime2 == 25)
                    {
                        delayTime2 = 0;
                        isClickGetBuff = false;
                    }
                }
            }
        }
        #region dataGridView1
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
               e.RowBounds.Location.Y,
               this.dataGridView1.RowHeadersWidth - 4,
               e.RowBounds.Height);

            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(),
                this.dataGridView1.RowHeadersDefaultCellStyle.Font,
                rectangle,
                this.dataGridView1.RowHeadersDefaultCellStyle.ForeColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        #endregion

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
