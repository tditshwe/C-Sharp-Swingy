using System.Collections.Generic;

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

        //Log to keep track of the battle outcome
        protected List<string> Log;

        public Character()
        {
            Logger logger = Logger.GetInstance();
            //Get existing logger
            Log = logger.GetLog();
        }

        public virtual void AttackOpponent()
        {
            //Calculate opponent hitpoints after attack
            int HpLoss = Attack - Opponent.Defense;
            int HpLeft = Opponent.HitPoints - HpLoss;

            //Opponet hitpoints must never go below 0
            Opponent.HitPoints = HpLeft < 0 ? 0 : HpLeft;

            //Add attack result to the battle outcome
            Log.Add(Name + "(" + HitPoints + ") attacks hero ");
            Log.Add("taking " + HpLoss + " of enemy HP\n");
        }
    }
}
