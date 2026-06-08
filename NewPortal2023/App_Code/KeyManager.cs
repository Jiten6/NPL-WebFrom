using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;

namespace NewPortal2023.ESS
{
    internal class KeyManager
    {
        private static Dictionary<string, string> _secrets;
        private static readonly object _lock = new object();

        public static string Get(string key)
        {
            if (_secrets == null)
            {
                lock (_lock)
                {
                    if (_secrets == null)
                    {
                        string path = HttpContext.Current.Server.MapPath("~/Keys/Key.json");
                        string json = File.ReadAllText(path);
                        _secrets = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(json);
                    }
                }
            }

            return _secrets.ContainsKey(key) ? _secrets[key] : null;
        }
    }
}