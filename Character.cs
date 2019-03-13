using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swingy
{
    public class Character
    {
        public string Name { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int HitPoints { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public Character Opponent { get; set; }

        protected List<string> Log;

        public Character()
        {
            Logger logger = Logger.GetInstance();
            Log = logger.GetLog();
        }

        public virtual void AttackOpponent()
        {
            int HpLoss = Attack - Opponent.Defense;
            int HpLeft = Opponent.HitPoints - HpLoss;
            Opponent.HitPoints = HpLeft < 0 ? 0 : HpLeft;

            Log.Add(Name + "(" + HitPoints + ") attacks hero ");
            Log.Add("taking " + HpLoss + " of enemy HP\n");
        }
    }
}
