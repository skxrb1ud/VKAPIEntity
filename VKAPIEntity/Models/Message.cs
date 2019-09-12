using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skxrb1ud.VKAPIEntity.Models
{
    public enum Flags
    {
        UNREAD = 1,
        OUTBOX = 2,
        REPLIED = 4,
        IMPORTANT = 8,
        CHAT = 16,
        FRIENDS = 32,
        SPAM = 64,
        DELЕTЕD = 128,
        FIXED = 256,
        MEDIA = 512,
        HIDDEN = 65536,
        DELETE_FOR_ALL = 131072,
        NOT_DELIVERED = 262144
    }
    public class Message
    {
        static Dictionary<long, Message> All = new Dictionary<long, Message>();
        public long Id { get { return id; } }
        public DateTime Date { get { return date; } }
        public User From { get { return User.GetUser(_client,fromId); } }
        public long Peer_id { get { return peerId; } }
        public string Text { get { return body; } }
        public List<Message> Fwd_messages { get { return null; } }
        public bool Important { get; set; }
        public int Random_id { get; set; }
        public List<object> Attachments { get; set; }
        public bool Is_hidden { get; set; }

        public bool this [Flags flag]
        {
            get
            {
                return (flags & (int)flag) == (int)flag;
            }
        }


        long id;
        long fromId; 
        long peerId;
        string body;
        long flags;
        DateTime date;
        ClientPeer _client;
        public Message(ClientPeer client, long id,long fromId,long peerId,string body,long flags,long mktime)
        {
            this.id = id;
            this.fromId = fromId;
            this.peerId = peerId;
            this.body = body;
            this.flags = flags;
            this.date = new DateTime(1970,1,1,0,0,0) + TimeSpan.FromSeconds(mktime);
            _client = client;
            All.Add(id, this);
        }
    }
}
