using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skxrb1ud.VKAPIEntity.Models
{
    public class Users
    {
        public List<User> response;
    }
    public class User : Model
    {
        static Dictionary<long, User> All = new Dictionary<long, User>();
        public int Id { get; set; }
        public string First_name { get; set; }
        public string Last_name { get; set; }
        public bool Is_closed { get; set; }
        public bool Can_access_closed { get; set; }
        public City City { get; set; }
        public string Photo_50 { get; set; }
        public string Status { get; set; }
        public LastSeen Last_seen { get; set; }
        public int Verified { get; set; }

        ClientPeer _client;
        public static User GetUser(ClientPeer client,long id)
        {
            if (All.ContainsKey(id))
            {
                return All[id];
            }
            Request r = new Request("users.get");
            r["user_ids"] = id.ToString();
            r["fields"] = "photo_50,city,verified,status,last_seen";
            Users users = client.API<Users>(r);
            User user = users.response[0];
            user._client = client;
            All.Add(user.Id, user);
            return user;
        }
    }
}
