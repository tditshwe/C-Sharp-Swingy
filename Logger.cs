using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swingy
{
    public class Logger
    {
        private static Logger Instance = new Logger();
        private List<string> Log;

        private Logger() { }

        public static Logger GetInstance()
        {
            return Instance;
        }

        public List<string> GetLog()
        {
            return Log;
        }

        public void CreateLog()
        {
            Log = new List<string>();
        }
    }
}
