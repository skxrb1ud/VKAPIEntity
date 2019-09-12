using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using skxrb1ud.VKAPIEntity.Models;

namespace skxrb1ud.VKAPIEntity
{
    public class Core
    {
        public User IAm;
        Random rnd;
        private string access_token;
        private string v;
        public Core(string access_token, string v = "5.101")
        {
            this.access_token = access_token;
            this.v = v;
            this.rnd = new Random();
            IAm = API<Users>(new Request("users.get"), true).response[0];
        }
        public string GET(string url)
        {
            WebRequest wr = WebRequest.Create(url);
            Stream str = wr.GetResponse().GetResponseStream();
            StreamReader sr = new StreamReader(str);
            return sr.ReadToEnd();
        }
        public T API<T> (Request r,bool needResponse = true)
        {
            string json = GET(r.GetUrl() + $"access_token={access_token}&v={v}");
            if(needResponse)
                return JsonConvert.DeserializeObject<T>(json);
            return (T)(object)null;
        }
        public void MessageSend(SendMessage message)
        {
            Request r = new Request("messages.send");
            r["peer_id"] = message.PeerId.ToString();
            r["message"] = message.Body;
            r["random_id"] = rnd.Next().ToString();
            r["forward_messages"] = message.Fwds_messages;
            API<object>(r, false);
        }
    }
}
