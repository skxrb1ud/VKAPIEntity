using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skxrb1ud.VKAPIEntity
{
    public class LongPollResponse
    {
        public long ts { get; set; }
        public List<List<object>> updates { get; set; }
    }
}
