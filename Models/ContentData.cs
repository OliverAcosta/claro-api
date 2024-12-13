using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ContentData<TData>
    {

        public HttpStatusCode StatusCode { get; set; }

        public TData Data { get; set; }
    }
}
