using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responce
{
    public class clsResponse
    {
        public HttpStatusCode status { get; set; }
        public bool isSuccess { get; set; }
        public string msg { get; set; }
        public object data { get; set; }
    }
}
