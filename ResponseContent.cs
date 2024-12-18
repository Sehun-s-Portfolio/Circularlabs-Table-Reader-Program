using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFIDTest
{
    class ResponseContent
    {

        private string statusMessage;
        private string statusCode;
        private string data;

        public string StatusMessage
        {
            get { return statusMessage; }
            set { statusMessage = value; }
        }

        public string StatusCode
        {
            get { return statusCode; }
            set { statusCode = value; }
        }

        public string Data
        {
            get { return data; }
            set { data = value; }
        }
    }
}
