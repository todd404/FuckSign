using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IniParser;
using IniParser.Model;

namespace FuckSign
{
    class Frp
    {
        private const string frpPath = @".\frp\frpc.exe";
        private const string iniPath = @".\frpc.ini";

        static Frp()
        {
            if (!File.Exists(iniPath))
            {
                File.Create(iniPath).Close();
                
                var parser = new FileIniDataParser();
                IniData data = new IniData();

                data.Sections.AddSection("common");
                data["common"].AddKey("server_addr", "www.7066.site");
                data["common"].AddKey("server_port", "7000");

                data.Sections.AddSection("proxy");
                data["proxy"].AddKey("type", "tcp");
                data["proxy"].AddKey("local_ip", "127.0.0.1");
                data["proxy"].AddKey("local_port", "1203");

                parser.WriteFile(iniPath, data);
            }
        }

        public void setRemotePort(int port)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(iniPath);

            data["proxy"]["remote_port"] = port.ToString();
        }
    }
}
