using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Hero(): base() { }

        public void Fight(Board board)
        {
            Log.Add("---Battle begins---\n");
            Log.Add("Hero is fighting " + Opponent.Name + " villian on pos: ");
            Log.Add(Opponent.XPosition + "," + Opponent.YPosition + "\n");

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

            if (HitPoints > 0)
            {
                Log.Add("You win. ");
                Log.Add(Opponent.Name + " dies\n");
                board.KillEnemy(Opponent);
                GainXp();
                LevelUp();
                HasWon = true;
            }
            else
            {
                board.MarkBattle(XPosition, YPosition);
                Log.Add("You are a loser.\n");
                HasWon = false;
            }

            Log.Add("---End of battle---\n");
        }

        public void Run(Board board)
        {
            Random rnd = new Random();
            int odds = rnd.Next(2);

            if (odds == 1)
            {
                HasWon = true;
                board.ChangeHeroPos(XPrevious, YPrevious, true);
                Log.Add("You chose to run from battle.\n");
            }
            else
                Fight(board);
        }

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

            Xp += Convert.ToInt32((decimal)Level / 2 + xpGain);
        }

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

        public override void AttackOpponent()
        {
            int bonus;
            Random rnd = new Random();
            int randInt = rnd.Next(4);

            if (randInt == 3)
                bonus = Attack;
            else
                bonus = 0;

            int hpLoss = Attack + bonus - Opponent.Defense;
            int HpLeft = Opponent.HitPoints - hpLoss;
            Opponent.HitPoints = HpLeft < 0 ? 0 : HpLeft;

            Log.Add(Name + "(" + HitPoints + ") attacks enemy ");
            Log.Add("taking " + hpLoss + " of enemy HP\n");

            if (bonus > 0)
                Log.Add("OMG Double damage!!!\n");
        }
    }
}
