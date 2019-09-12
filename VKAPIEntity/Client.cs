using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using skxrb1ud.VKAPIEntity.Models;

namespace skxrb1ud.VKAPIEntity
{
    public abstract class ClientPeer : Core
    {
        LongPollServer LongPollServer { get; set; }
        public ClientPeer(string access_tokken) : base(access_tokken) 
        {

        }
        public void StartListen()
        {
            Request request = new Request("messages.getLongPollServer");
            request["lp_version"] = "3";
            LongPollServer = API<LongPollServer>(request);
            LongPollServer.version = 3;
            while (true)
            {
                string json = GET(LongPollServer.GetUrl());
                LongPollResponse longPoll = JsonConvert.DeserializeObject<LongPollResponse>(json);
                LongPollServer.ts = longPoll.ts;
                foreach(List<object> update in longPoll.updates)
                {
                    switch ((long)update[0])
                    {
                        case 4:
                            {
                                LongPollAddMessage(update);
                            }
                            break;
                    }
                }
            }
        }
        void LongPollAddMessage(List<object> upd)
        {
            
            long id = (long)upd[1];
            long flag = (long)upd[2];
            long peerId = (long)upd[3];
            long mktime = (long)upd[4];
            string body = (string)upd[5];
            Newtonsoft.Json.Linq.JToken attach = (Newtonsoft.Json.Linq.JToken)upd[6];
            long from = attach["from"] == null ? (flag&2)==2? IAm.Id : peerId : (long)attach["from"];
            Message msg = new Message(this,id,from,peerId,body,flag,mktime);
            OnMessageReceive(msg);
        }
        public abstract void OnMessageReceive(Message message);
    }
}
