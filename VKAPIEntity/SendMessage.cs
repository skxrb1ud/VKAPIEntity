using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using skxrb1ud.VKAPIEntity.Models;

namespace skxrb1ud.VKAPIEntity
{
    public class SendMessage
    {
        public string Body { get { return body; } set { body = value; } }
        public long PeerId { get { return peerId; } }
        public string Fwds_messages { get { return String.Join(",", fwds); } }

        string body;
        long peerId;
        List<long> fwds = new List<long>();
        public SendMessage(Message msg)
        {
            peerId = msg.Peer_id;
        }
        public SendMessage(Chat chat)
        {
            peerId = chat.Id + 2000000000;
        }
        public SendMessage(User user)
        {
            peerId = user.Id;
        }

        public static SendMessage operator +(SendMessage c1, Message c2)
        {
            c1.fwds.Add(c2.Id);
            return c1;
        }
    }
}
