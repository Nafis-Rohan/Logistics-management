using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace project
{
    public class databaseConnection
    {
        public static string database()
        {

            string s = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\5th sem\\SQLDATABASE (1).mdf\";Integrated Security=True;Connect Timeout=30";

            return s;

        }
    }
}

