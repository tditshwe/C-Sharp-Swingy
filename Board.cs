using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swingy
{
    public class Board
    {
        private int[,] gameBoard;
	    public int Size { get; set; }
        private Hero hero;
        private List<Character> enemies;

        public Board(int size)
        {
            gameBoard = new int[size, size];
            this.Size = size;
        }

        public int[,] InitBoard()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    gameBoard[i, j] = 0;
                }
            }

            return gameBoard;
        }

        public void PlaceHero(Hero hero, int pos)
        {
            this.hero = hero;
            gameBoard[pos, pos] = 1;
            this.hero.XPosition = pos;
            this.hero.YPosition = pos;
        }

        public void SpreadVilians()
        {
            enemies = new List<Character>();
            Random rnd = new Random();
            int randInt = -1;
            Character enemy;
            string name;

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (gameBoard[i, j] != 1)
                    {
                        randInt = rnd.Next(6);

                        if (randInt == 0)
                        {
                            randInt = rnd.Next(10);

                            switch (randInt)
                            {
                                case 0:
                                    name = "Superbat";
                                    break;
                                case 1:
                                case 4:
                                    name = "Ghost";
                                    break;
                                default:
                                    name = "Demon";
                                    break;
                            }

                            enemy = new Character();
                            enemy.Name = name;
                            enemy.XPosition = i;
                            enemy.YPosition = j;

                            CreateEnemy(enemy);
                            enemies.Add(enemy);
                            gameBoard[i, j] = 2;
                        }
                    }
                }
            }
        }

        private void CreateEnemy(Character enemy)
        {
            int attack, defense, hitPoints;

            switch (enemy.Name)
            {
                case "Demon":
                    attack = 15;
                    defense = 3;
                    hitPoints = 50;
                    break;
                case "Ghost":
                    attack = 25;
                    defense = 5;
                    hitPoints = 100;
                    break;
                default:
                    attack = 40;
                    defense = 10;
                    hitPoints = 150;
                    break;
            }

            enemy.Attack = attack;
            enemy.Defense = defense;
            enemy.HitPoints = hitPoints;
        }

        public void ChangeHeroPos(int x, int y, bool rev)
        {
            if (rev)
                gameBoard[hero.XPosition, hero.YPosition] = 2;
            else
                gameBoard[hero.XPosition, hero.YPosition] = 0;

            gameBoard[x, y] = 1;

            hero.XPrevious = hero.XPosition;
            hero.YPrevious = hero.YPosition;
            hero.XPosition = x;
            hero.YPosition = y;
        }

        public void PrintBoard()
        {
            char icon;

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    switch (gameBoard[j, i])
                    {
                        case 1:
                            icon = 'H';
                            break;
                        case 2:
                            icon = EnemyIcon(j, i);
                            break;
                        case 3:
                            icon = 'X';
                            break;
                        default:
                            icon = '.';
                            break;
                    }

                    Console.Write(icon);
                }

                Console.WriteLine();
            }
        }

        private char EnemyIcon(int x, int y)
        {
            Character enemy = GetEnemy(x, y);
            char eChar;

            switch (enemy.Name)
            {
                case "Ghost":
                    eChar = 'G';
                    break;
                case "Superbat":
                    eChar = 'S';
                    break;
                default:
                    eChar = 'V';
                    break;
            }

            return eChar;
        }

        public Character GetEnemy(int x, int y)
        {
            foreach (Character E in enemies)
            {
                if (E.XPosition == x && E.YPosition == y)
                    return E;
            }

            return null;
        }

        public void MarkBattle(int xPos, int yPos)
        {
            gameBoard[xPos, yPos] = 3;
        }

        public void KillEnemy(Character e)
        {
            enemies.Remove(e);
        }
    }
}
