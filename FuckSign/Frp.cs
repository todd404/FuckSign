using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


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

                IniFunc.writeString("common", "server_addr", "frp.7066.site", iniPath);
                IniFunc.writeString("common", "server_port", "7000", iniPath);

                IniFunc.writeString("proxy", "type", "tcp", iniPath);
                IniFunc.writeString("proxy", "local_ip", "127.0.0.1", iniPath);
                IniFunc.writeString("proxy", "local_port", "1203", iniPath);
                IniFunc.writeString("proxy", "remote_port", "6000", iniPath);
            }
        }

        
    }
}
