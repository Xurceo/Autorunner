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
        public AutorunType Type { get; set; }
        public Autorun(string name, string path, string command = "", AutorunType type = AutorunType.Command)
        {
            Name = name;
            Path = path;
            Type = type;
            Command = command;
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
            process.StartInfo.FileName = Path;
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
        }
    }
}
