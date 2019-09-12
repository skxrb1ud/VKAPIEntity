using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skxrb1ud.VKAPIEntity.Models
{
    public class LongPollServer : Model
    {
        public string key { get; set; }
        public string server { get; set; }
        public long ts { get; set; }
        public byte version { get; set; }
        public string GetUrl()
        {
            return $"https://{server}?act=a_check&key={key}&wait=25&mode=2&ts={ts}&version={version}";
        }
    }
}
