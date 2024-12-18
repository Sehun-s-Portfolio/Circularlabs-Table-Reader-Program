using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetAPI;
using System.Runtime.InteropServices;

namespace RFIDTest
{
    public class Common
    {        
        public static object LockRxdTagData = new object();

        public static bool IsIP(string ip)
        {
            //判断是否为IP
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// 判断字符串是否满足16进制字符串，可包含空格和“-”
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsHexstring(string str)
        {
            string str2 = str.Replace(" ", string.Empty).Replace("-", string.Empty);
            if (str2 == string.Empty) return false;
            foreach (char c in str2)
            {
                if (!(c >= '0' && c <= '9') && !(c >= 'a' && c <= 'f') && !(c >= 'A' && c <= 'F'))//发现不合法字符
                {
                    return false;
                }
            }
            return true;
        }
    }
}
