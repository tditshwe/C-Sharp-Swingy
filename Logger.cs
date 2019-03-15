using System.Collections.Generic;

namespace Swingy
{
    /*
     * A Singleton class
     */
    public class Logger
    {
        private static Logger Instance = new Logger();
        private List<string> Log;

        //Restricts the instantiation of Logger
        private Logger() { }

        //Logger instance only accessible via this method
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
