using System;
using Swingy.Views;


namespace Swingy
{
    public class GamePlay
    {
        private Hero Hero;
        private View View;

        public GamePlay(View view)
        {
            View = view;
        }

        /*
         * Create Hero with name and type
         */
        public Hero CreateHero()
        {
            string choice;
            Console.ReadLine();
            View.Write("Enter hero name: ");
            Hero = new Hero();
            Hero.Name = Console.ReadLine();
            View.WriteLine("Choose hero type");
            View.WriteLine("1. Wizard");
            View.WriteLine("2. Warrior");
            View.WriteLine("3. Alchemist");
            View.Write("Your choice: ");

            //Assign hero type based on player input
            switch (TakeInt(3))
            {
                case 1:
                    choice = "Wizard";
                    break;
                case 2:
                    choice = "Warrior";
                    break;
                default:
                    choice = "Alchemist";
                    break;
            }

            Hero.Type = choice;

            return Hero;
        }

        public void SetHero(Hero hero)
        {
            Hero = hero;
        }

        /*
         * Pick a direction for hero to navigate
         */
        public string Direction()
        {
            View.WriteLine("Choose a direction");
            View.WriteLine("w: North");
            View.WriteLine("d: East");
            View.WriteLine("s: South");
            View.WriteLine("a: West");
            View.Write("Your direction: ");

            //Return player input
            return TakeDir();
        }

        /*
         * Move on a position occupied by a villian and choose whether to fight or run
         */
        public void EnemyEncounter(Board board, Character e)
        {
            if (e != null)
            {
                View.WriteLine("Enemy encountered, fight or run?");
                View.WriteLine("1. Fight");
                View.WriteLine("2. Run");
                View.Write("Your response: ");
                Hero.Opponent = e;
                e.Opponent = Hero;

                switch (TakeInt(2))
                {
                    case 1:
                        Hero.Fight(board);
                        break;
                    case 2:
                        Hero.Run(board);
                        break;
                }

                Console.ReadLine();
            }
        }

        /*
         * Only accept an integer that is greater than or equal to 1 and less than cap from player input
         */
        public int TakeInt(int cap)
        {
            int choice = Console.Read() - 48;

            if (choice < 1 || choice > cap)
            {
                Console.ReadLine();
                View.Write("Invalid choice, try again: ");
                //Prompt player for another input
                return TakeInt(cap);
            }
            else
                return choice;
        }
        /*
         * Prompt player for direction and only return valid input
         */
        private string TakeDir()
        {
            string dir = Console.ReadLine();

            switch (dir)
            {
                case "w":
                case "d":
                case "s":
                case "a":
                case "q":
                    return dir;
                default:
                    View.Write("Invalid direction, choose again: ");
                    //Prompt player for another direction input
                    return TakeDir();
            }
        }
    }
}
