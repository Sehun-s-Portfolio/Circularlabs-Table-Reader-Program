using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDTest
{
    public class RfidScanData
    {
        private string epc;
        private string tid;
        private string userData;
        private string antenna;
        private int readCount;
        private string timeStamp;

        public RfidScanData(string epc, string tid, string userData, string antenna, int readCount, string timeStamp )
        {
            this.epc = epc;
            this.tid = tid;
            this.userData = userData;
            this.antenna = antenna;
            this.readCount = readCount;
            this.timeStamp = timeStamp;
        }

        public RfidScanData()
        {

        }

        public string Epc
        {
            get { return epc; }
            set { epc = value; }
        }

        public string Tid
        {
            get { return tid; }
            set { tid = value; }
        }

        public string UserData
        {
            get { return userData; }
            set { userData = value; }
        }
        
        public string Antenna
        {
            get { return antenna; }
            set { antenna = value; }
        }

        public int ReadCount
        {
            get { return readCount; }
            set { readCount = value; }
        }

        public string TimeStamp
        {
            get { return timeStamp; }
            set { timeStamp = value; }
        }
    }
}
