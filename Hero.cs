using System;

namespace Swingy
{
    public class Hero: Character
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int Level { get; set; }
        public int Xp { get; set; }
        public int YPrevious { get; set; }
        public int XPrevious { get; set; }
        public bool HasWon { get; set; }
        public int MaxHp { get; set; }

        //Inherit the base constructor
        public Hero(): base() { }

        /*
         * Emulate fight between hero and opponent
         */ 
        public void Fight(Board board)
        {
            Log.Add("---Battle begins---\n");
            Log.Add("Hero is fighting " + Opponent.Name + " villian on pos: ");
            Log.Add(Opponent.XPosition + "," + Opponent.YPosition + "\n");

            //While none of the fighters has died
            while (HitPoints > 0 && Opponent.HitPoints > 0)
            {
                AttackOpponent();
                Log.Add("Enemy hp: " + Opponent.HitPoints + "\n");

                if (Opponent.HitPoints > 0)
                {
                    Opponent.AttackOpponent();
                    Log.Add("Hero hp: " + HitPoints + "\n");
                }
                else
                    break;
            }

            //if the hero is still standing
            if (HitPoints > 0)
            {
                Log.Add("You win. ");
                Log.Add(Opponent.Name + " dies\n");

                //Remove villian from enemy list
                board.KillEnemy(Opponent);

                GainXp();
                LevelUp();
                HasWon = true;
            }
            else
            {
                //Mark the position where the hero died with X
                board.MarkBattle(XPosition, YPosition);
                Log.Add("You are a loser.\n");
                HasWon = false;
            }

            Log.Add("---End of battle---\n");
        }

        /*
         * 50% odds of running if you don't want to fight the villian
         */ 
        public void Run(Board board)
        {
            Random rnd = new Random();
            //Random number to represent 50% odds
            int odds = rnd.Next(2);

            //You can return to the previous position
            if (odds == 1)
            {
                HasWon = true;
                board.ChangeHeroPos(XPrevious, YPrevious, true);
                Log.Add("You chose to run from battle.\n");
            }
            else
                Fight(board);
        }

        /*
         * Gain experience
         */
        private void GainXp()
        {
            int xpGain;

            switch (Opponent.Name)
            {
                case "Ghost":
                    xpGain = 500;
                    break;
                case "Demon":
                    xpGain = 450;
                    break;
                default:
                    xpGain = 600;
                    break;
            }

            //Just a fancy formula no magic
            Xp += Convert.ToInt32((decimal)Level / 2 + xpGain);
        }

        /*
         * Level up following the necessaary experience pattern and advance hero stats
         */
        private void LevelUp()
        {
            int lev = Level + 1;

            if (Xp >= (lev * 1000 + Math.Pow(lev - 1, 2) * 450))
            {
                Level = lev;
                HitPoints += Level * 20;
                MaxHp += Level * 20;
                Attack += 4 * Level;
                Defense += 1;
                Log.Add("You have leveled up\n");
            }
        }

        /*
         * 
         */
        public override void AttackOpponent()
        {
            int bonus;
            Random rnd = new Random();
            //A random integer to decide on when the hero gets to inflict double damage
            int randInt = rnd.Next(4);

            //A bonus attack occurs when the random integer = 3
            if (randInt == 3)
                bonus = Attack;
            else
                bonus = 0;

            //Calculate opponent hitpoints after attack
            int hpLoss = Attack + bonus - Opponent.Defense;
            int HpLeft = Opponent.HitPoints - hpLoss;

            //Opponet hitpoints must never go below 0
            Opponent.HitPoints = HpLeft < 0 ? 0 : HpLeft;

            //Add attack result to the battle outcome
            Log.Add(Name + "(" + HitPoints + ") attacks enemy ");
            Log.Add("taking " + hpLoss + " of enemy HP\n");

            if (bonus > 0)
                Log.Add("OMG Double damage!!!\n");
        }
    }
}
