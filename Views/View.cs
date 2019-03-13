using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swingy.Views
{
    public class View
    {
        private List<string> Log;

        public View()
        {
            Logger logger = Logger.GetInstance();
            logger.CreateLog();
            Log = logger.GetLog();
        }

        public void PrintHeroStats(Hero hero)
        {
            WriteLine("Name: " + hero.Name);
            WriteLine("Class: " + hero.Type);
            WriteLine("Level: " + hero.Level);
            WriteLine("Attack: " + hero.Attack);
            WriteLine("Defense: " + hero.Defense);
            WriteLine("Hitpoints: " + hero.HitPoints);
            WriteLine("XP: " + hero.Xp);
        }

        public void PrintHeroOptions()
        {
            WriteLine("1. Create hero");
            WriteLine("2. Select hero");
            Write("Your choice: ");
        }

        public void Write(string str)
        {
            Console.Write(str);
        }

        public void WriteLine(string str)
        {
            Console.WriteLine(str);
        }

        public void UpdateLog()
        {
            foreach (string L in Log)
            {
                Write(L);
            }

            Log.Clear();
        }
    }
}
