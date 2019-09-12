using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skxrb1ud.VKAPIEntity
{
    public class Request
    {
        string method;
        Dictionary<string, string> args = new Dictionary<string, string>();
        public Request(string method)
        {
            this.method = method;
        }
        public string this [string arg]
        {
            get
            {
                if (args.ContainsKey(arg))
                    return args[arg];
                return null;
            }
            set
            {
                args.Add(arg, value);
            }
        }
        public virtual string GetUrl()
        {
            string[] keys = args.Keys.ToArray();
            string output = $"https://api.vk.com/method/{method}?";
            foreach(string key in keys)
            {
                output += $"{key}={args[key]}&";
            }
            return output;
        }
    }
}
