using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Autorunner
{
    public class Autorun
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Command { get; set; }
        public bool Minimized { get; set; }
        public AutorunType Type { get; set; }
        public Autorun(string name, string path, string command = "", AutorunType type = AutorunType.Command, bool minimized = false)
        {
            Name = name;
            Path = path;
            Type = type;
            Command = command;
            Minimized = minimized;
        }

        public string GetCreateCommand()
        {
            if (Type == AutorunType.Command)
            {
                return Command;
            }
            else if (Type == AutorunType.Application)
            {
                return Path;
            }
            return "";
        }
        public void Run()
        {
            var process = new Process();
            process.StartInfo.FileName = Path ?? "cmd.exe";
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = Minimized ? ProcessWindowStyle.Minimized : ProcessWindowStyle.Normal;
            process.Start();
        }
    }
}
