using System;
using System.Collections.Generic;
using System.Linq;
using Swingy.Views;
using Swingy.DataAcces;
using Swingy.Models;

namespace Swingy.Controllers
{
    public class SwingController
    {
        private readonly View View;
        private readonly GamePlay Game;
        private Board Board;
        private Hero Hero;
        private readonly SwingContext Context;

        public SwingController()
        {
            View = new View();
            Game = new GamePlay(View);
            Context = new SwingContext();
        }

        /*
         * Initialise hero stats based on his type and persists to database
         */
        public void CreateHero()
        {
            int attack, defense, hitPoints;

            //Create stats depending on type
            switch (Hero.Type)
            {
              case "Wizard":
                attack = 13;
                defense = 7;
                hitPoints = 80;
              break;
              case "Alchemist":
                attack = 15;
                defense = 3;
                hitPoints = 110;
              break;
              default:
                attack = 25;
                defense = 2;
                hitPoints = 90;
              break;
            }

            //Assign the stats to hero
            Hero.Attack = attack;
            Hero.Defense = defense;
            Hero.HitPoints = hitPoints;
            Hero.MaxHp = hitPoints;

            //Copy Hero details to HeroEntity
            HeroEntity entity = new HeroEntity
            {
                Name = Hero.Name,
                Type = Hero.Type,
                Level = Hero.Level,
                Xp = Hero.Xp,
                Attack = Hero.Attack,
                Defense = Hero.Defense,
                HitPonts = Hero.HitPoints
            };

            //Save HeroEntity to database
            Context.Heroes.Add(entity);
            Context.SaveChanges();

            //Assign the Id of the HeroEntity that has just been added to the Hero it was copied from
            Hero.Id = entity.Id;
        }

        public void UpdateHero()
        {
            //Find hero with the same Id on the database
            HeroEntity Entity = Context.Heroes.Find(Hero.Id);

            //Update the hero on the daabase with new hero stats
            Entity.Level = Hero.Level;
            Entity.Xp = Hero.Xp;
            Entity.Attack = Hero.Attack;
            Entity.Defense = Hero.Defense;
            Entity.HitPonts = Hero.MaxHp;

            //Save changes to database
            Context.SaveChanges();
        }

        /*
         * Select hero from hero list
         */
        public void SelectHero(List<HeroEntity> heroes)
        {
            int i;

            //If the list is not empty
            if (heroes.Count() > 0)
            {
                //Display hero list
                for (i = 0; i < heroes.Count(); i++)
                {
                    View.WriteLine((i + 1) + ". " + heroes[i].Name);
                }

                View.Write("Your choice: ");
                Console.ReadLine();
                //Hero on the chosen index
                HeroEntity Entity = heroes[Game.TakeInt(i) - 1];

                //Init copy of hero entity
                Hero = new Hero
                {
                    Id = Entity.Id,
                    Name = Entity.Name,
                    Type = Entity.Type,
                    Level = Entity.Level,
                    Xp = Entity.Xp,
                    Attack = Entity.Attack,
                    Defense = Entity.Defense,
                    HitPoints = Entity.HitPonts,
                    MaxHp = Entity.HitPonts
                };

                View.WriteLine("Hero selected.");
            }
            else
            {
                //There is no list to select from so create one
                View.WriteLine("The hero list is empty.");
                View.WriteLine("Creating hero ...");
                Hero newHero = Game.CreateHero();
                Hero = newHero;
                View.WriteLine("Saving hero ...");
                CreateHero();
            }

            //Set the chosen hero as for the current mission
            Game.SetHero(Hero);
        }

        /*
         * Initialise hero by either creating a new one or selecting from the existing list
         */ 
        public void InitHero()
        {
            View.PrintHeroOptions();
            int Option = Game.TakeInt(2);

            //Have chosen to create hero
            if (Option == 1)
            {       
                View.WriteLine("Creating hero ...");
                Hero newHero = Game.CreateHero();
                Hero = newHero;
                View.WriteLine("Saving hero ...");
                CreateHero();

            }
            //Have chosen to select from list
            else if (Option == 2)
            {
                View.WriteLine("Retrieving hero list ...");
                //Retrieve heroes from database
                List<HeroEntity> heroes = Context.Heroes.ToList();
                SelectHero(heroes);
            }
        }

        /*
         *Initialise and display the game map with the hero and villians
         */
        public void InitBoard()
        {
            //Determine the map size based on hero level
            int size = (Hero.Level - 1) * 5 + 10 - (Hero.Level % 2);
            Board = new Board(size);

            //Place hero on the center of the map
            Board.PlaceHero(Hero, Board.Size / 2);

            Board.SpreadVilians();
            View.PrintHeroStats(Hero);
            Board.PrintBoard();
        }

        /*
         * Navigate the map
         */
        public void NavigateHero()
        {
            Console.ReadLine();
            string dir = Game.Direction();

            //For as long as hero hasn't reached the border or died or key 'q' is pressed
            while (dir != "q" && Hero.HitPoints > 0 && !HeroAtBorder())
            {
                switch (dir)
                {
                    case "w":
                        Board.ChangeHeroPos(Hero.XPosition, Hero.YPosition - 1, false);
                    break;
                    case "d":
                        Board.ChangeHeroPos(Hero.XPosition + 1, Hero.YPosition, false);
                    break;
                    case "s":
                        Board.ChangeHeroPos(Hero.XPosition, Hero.YPosition + 1, false);
                    break;
                    case "a":
                        Board.ChangeHeroPos(Hero.XPosition - 1, Hero.YPosition, false);
                    break;
                }

                //Check for enemy on current hero position
                Character enemy = Board.GetEnemy(Hero.XPosition, Hero.YPosition);

                Game.EnemyEncounter(Board, enemy);
                View.UpdateLog();
                View.PrintHeroStats(Hero);
                View.WriteLine("---------------");
                Board.PrintBoard();
                View.WriteLine("---------------");

                //If hero hasn't been killed
                if (Hero.HitPoints > 0 )
                {
                    if (HeroAtBorder())
                        View.WriteLine("You have reached the border, you win.");
                    else
                        dir = Game.Direction();
                }
                else
                    View.WriteLine("Our hero has been killed.");                         
            }

            //If 'q' is pressed
            if (dir == "q")
                View.WriteLine("Exiting game ...");

            //Update hero on the database
            UpdateHero();
        }

        /*
         * Checks if hero has reached the border
         */
        private bool HeroAtBorder()
        {
            if (Hero.XPosition == Board.Size - 1 || Hero.XPosition == 0 ||
                Hero.YPosition == Board.Size - 1 || Hero.YPosition == 0)
            {
                return true;
            }

            return false;
        }
    }
}
