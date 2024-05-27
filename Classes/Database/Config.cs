using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace TaskManager_Graf.Classes.Database
{
    public class Config
    {
        public static readonly string connection = "server=192.168.0.111;" +
            "uid=root;" +
            "pwd=root;" +
            "database=TaskManager;";

        public static readonly MySqlServerVersion version = new MySqlServerVersion(new Version(8, 0, 11));
    }
}
