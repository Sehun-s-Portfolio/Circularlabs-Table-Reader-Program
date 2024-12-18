using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RFIDTest
{
    public class ScanParameter
    {
        private string tidParam;
        private string userDataParam;
        private string selectParam;
        private string antenna = "01";
        private string isLoop = "true";
        private string q = "2";

        /// <summary>
        /// 天线号，16进制字符，U8
        /// </summary>
        public string Antenna
        {
            get
            {
                return antenna;
            }

            set
            {
                antenna = value;
            }
        }

        /// <summary>
        /// TID参数，TID最大长度
        /// </summary>
        public string TidParam
        {
            get
            {
                return tidParam;
            }

            set
            {
                tidParam = value;
            }
        }

        /// <summary>
        /// 是否循环扫描
        /// </summary>
        public string IsLoop
        {
            get
            {
                return isLoop;
            }

            set
            {
                isLoop = value;
            }
        }

        /// <summary>
        /// Q值
        /// </summary>
        public string Q
        {
            get
            {
                return q;
            }

            set
            {
                q = value;
            }
        }

        /// <summary>
        /// 用户数据参数：“起始地址，长度”
        /// </summary>
        public string UserDataParam
        {
            get
            {
                return userDataParam;
            }

            set
            {
                userDataParam = value;
            }
        }

        /// <summary>
        /// 选择指令参数
        /// </summary>
        public string SelectParam
        {
            get
            {
                return selectParam;
            }

            set
            {
                selectParam = value;
            }
        }
    }
}
