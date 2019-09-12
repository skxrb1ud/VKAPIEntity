using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using skxrb1ud.VKAPIEntity;
using skxrb1ud.VKAPIEntity.Models;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            MyBot myBot = new MyBot(TOKEN.TOKKEN);
            Console.WriteLine($"Bot started! Name: {myBot.IAm.First_name} {myBot.IAm.Last_name}");
            myBot.StartListen();
        }
    }
    class MyBot : ClientPeer
    {
        public MyBot(string access_tokken) : base(access_tokken)
        {

        }

        public override void OnMessageReceive(Message msg)
        {
            if (!msg[Flags.OUTBOX])
            {
                Request r = new Request("messages.markAsRead");
                r["peer_id"] = msg.Peer_id.ToString();
                r["start_message_id"] = msg.Id.ToString();

                API<object>(r,false);
            }
        }
    }
}
