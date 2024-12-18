using System;
using NetAPI;
using NetAPI.Entities;
using NetAPI.Protocol.VRP;

namespace RFIDTest
{
    public class ReaderConnTest
    {
         void Main()
        {
            ReaderConnTest test = new ReaderConnTest();
            test.Conn();
            test.ScanEPC();
            test.Stop();
            test.ScanTID();
            test.Stop();
            test.DisConn();
        }

        Reader reader;
        public ReaderConnTest()
        {
            Reader.OnApiException += Reader_OnApiException;
        }

        private void Reader_OnApiException(string senderName, ErrInfo e)
        {
            
        }

        private void Reader_OnBrokenNetwork(string senderName, ErrInfo e)
        {
            
        }

        private void Reader_OnInventoryReceived(string readerName, RxdTagData tagData)
        {
            byte[] epc = tagData.EPC;
            byte[] tid = tagData.TID;
        }

        public void Conn()
        {
            //reader = new Reader("Reader1");//通过配置文件指定
            //reader = new Reader("Reader1", new Rs232Port("COM1,115200"));//串口连接
            reader = new Reader("Reader1", new TcpClientPort("192.168.1.100:9090"));//网口连接
            //reader = new Reader("Reader1", new TcpClientPort("116.125.141.139:8090"));//网口连接
            ConnectResponse resp = reader.Connect();
            if (resp.IsSucessed)
            {
                Console.WriteLine("TCP 연결 성공");
                reader.OnInventoryReceived += Reader_OnInventoryReceived;//加载盘存监听事件
                reader.OnBrokenNetwork += Reader_OnBrokenNetwork;//加载网络断开事件，仅支持网口
            }
        }

        public void DisConn()
        {
            if(reader != null && reader.IsConnected)
            {
                //卸载事件
                reader.OnInventoryReceived -= Reader_OnInventoryReceived;
                reader.OnBrokenNetwork -= Reader_OnBrokenNetwork;
                //断开连接
                reader.Disconnect();
            }
            reader = null;
        }

        public void ScanEPC()
        {
            MsgTagInventory msg = new MsgTagInventory();
            if(reader.Send(msg))
            {
                //成功
            }
            else
            {
                //失败
            }
        }

        public void ScanTID()
        {
            MsgTagRead msg = new MsgTagRead();//循环扫描TID快捷指令
            if (reader.Send(msg))
            {
                //成功
            }
            else
            {
                //失败
            }
        }

        public void Stop()
        {
            reader.Send(new MsgPowerOff());
        }

        private void WriteTag()
        {
            byte[] targetEPC = new byte[] { 0x12, 0x34, 0x56, 0x78 };//标签EPC，根据实际情况而定
            TagParameter selectParam = new TagParameter();
            selectParam.MemoryBank = MemoryBank.EPCMemory;
            //选择标签参数，起始地址单位为bit，EPC数据区前两个字为PC和CRC，所以起始地址从0x20起
            //如果选择标签参数，匹配的是TID区，那起始地址从0x00起
            selectParam.Ptr = 0x20;
            selectParam.TagData = targetEPC;
            WriteTagParameter wp = new WriteTagParameter();
            wp.SelectTagParam = selectParam;
            wp.AccessPassword = new byte[4];//密码
            wp.WriteDataAry = new TagParameter[2];//可同时写几个区，
            int p = 0;
            // 写标签EPC区
            {
                TagParameter tp = new TagParameter();
                tp.MemoryBank = MemoryBank.EPCMemory;
                //写标签参数，起始地址单位为字，EPC数据区前两个字为PC和CRC，所以起始地址从0x02起
                tp.Ptr = 0x02;
                tp.TagData = new byte[] { 0x11, 0x22, 0x33, 0x44 };
                wp.WriteDataAry[p] = tp;
                p++;
            }
            // 写标签User区
            {
                TagParameter tp = new TagParameter();
                tp.MemoryBank = MemoryBank.UserMemory;
                tp.Ptr = 0x00;
                tp.TagData = new byte[] { 0xaa, 0xbb, 0xcc, 0xdd };
                wp.WriteDataAry[p] = tp;                
            }            
            MsgTagWrite msg = new MsgTagWrite(wp);
            if (reader.Send(msg))
            {
                string str = "写入成功";
            }
            else
            {
                string str = "写入失败：" + msg.ErrorInfo.ErrMsg;
            }
        }

        ~ReaderConnTest()
        {
            Reader.OnApiException -= Reader_OnApiException;
        }
    }
}
