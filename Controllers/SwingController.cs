using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void CreateHero()
        {
            int attack, defense, hitPoints;

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

            Hero.Attack = attack;
            Hero.Defense = defense;
            Hero.HitPoints = hitPoints;
            Hero.MaxHp = hitPoints;

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

            Context.Heroes.Add(entity);
            Context.SaveChanges();

            Hero.Id = entity.Id;
        }

        public void UpdateHero()
        {
            HeroEntity Entity = Context.Heroes.Find(Hero.Id);

            Entity.Level = Hero.Level;
            Entity.Xp = Hero.Xp;
            Entity.Attack = Hero.Attack;
            Entity.Defense = Hero.Defense;
            Entity.HitPonts = Hero.MaxHp;

            Context.SaveChanges();
        }

        public void SelectHero(List<HeroEntity> heroes)
        {
            int i;

            if (heroes.Count() > 0)
            {
                for (i = 0; i < heroes.Count(); i++)
                {
                    View.WriteLine((i + 1) + ". " + heroes[i].Name);
                }

                View.Write("Your choice: ");
                Console.ReadLine();
                HeroEntity Entity = heroes[Game.TakeInt(i) - 1];

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
                View.WriteLine("The hero list is empty.");
                View.WriteLine("Creating hero ...");
                Hero newHero = Game.CreateHero();
                Hero = newHero;
                View.WriteLine("Saving hero ...");
                CreateHero();
            }

            Game.SetHero(Hero);
        }

        public void InitHero()
        {
            View.PrintHeroOptions();
            int Option = Game.TakeInt(2);

            if (Option == 1)
            {       
                View.WriteLine("Creating hero ...");
                Hero newHero = Game.CreateHero();
                Hero = newHero;
                View.WriteLine("Saving hero ...");
                CreateHero();

            }
            else if (Option == 2)
            {
                View.WriteLine("Retrieving hero list ...");
                List<HeroEntity> heroes = Context.Heroes.ToList();
                SelectHero(heroes);
            }
        }

        public void InitBoard()
        {
            int size = (Hero.Level - 1) * 5 + 10 - (Hero.Level % 2);
            Board = new Board(size);

            Board.PlaceHero(Hero, Board.Size / 2);
            Hero.MaxHp = Hero.HitPoints;
            Board.SpreadVilians();
            View.PrintHeroStats(Hero);
            Board.PrintBoard();
        }

        public void NavigateHero()
        {
            Console.ReadLine();
            string dir = Game.Direction();

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

                Character enemy = Board.GetEnemy(Hero.XPosition, Hero.YPosition);

                Game.EnemyEncounter(Board, enemy);
                View.UpdateLog();
                View.PrintHeroStats(Hero);
                View.WriteLine("---------------");
                Board.PrintBoard();
                View.WriteLine("---------------");

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

            if (dir == "q")
                View.WriteLine("Exiting game ...");

            UpdateHero();
        }

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
