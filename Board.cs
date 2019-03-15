using System;
using System.Collections.Generic;

namespace Swingy
{
    public class Board
    {
        private int[,] GameBoard;
	    public int Size { get; set; }
        private Hero Hero;
        private List<Character> Enemies;

        public Board(int size)
        {
            GameBoard = new int[size, size];
            this.Size = size;
        }

        /*
         * Initialise the 2D map
         */
        public int[,] InitBoard()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    GameBoard[i, j] = 0;
                }
            }

            return GameBoard;
        }

        /*
         * Place hero on the specified position on the map
         */
        public void PlaceHero(Hero hero, int pos)
        {
            this.Hero = hero;

            //Mark the hero position
            GameBoard[pos, pos] = 1;
            this.Hero.XPosition = pos;
            this.Hero.YPosition = pos;
        }

        /*
         * Randomly spread villians over the map
         */
        public void SpreadVilians()
        {
            Enemies = new List<Character>();
            Random rnd = new Random();
            int randInt = -1;
            Character enemy;
            string name;

            //Iterate through the map
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    //If hero hasn,t been placed on the current position
                    if (GameBoard[i, j] != 1)
                    {
                        //Random integer that decides based on a 1/6 chance whether to place a villian on a current position
                        randInt = rnd.Next(6);

                        //Only place villian when randInt is 0
                        if (randInt == 0)
                        {
                            //Generate another random integer to help decide on which villian to place
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

                            //Initialize villian for the current position
                            enemy = new Character();
                            enemy.Name = name;
                            enemy.XPosition = i;
                            enemy.YPosition = j;

                            //Create villian, add it to the list of villians and mark it's position on the map
                            CreateEnemy(enemy);
                            Enemies.Add(enemy);
                            GameBoard[i, j] = 2;
                        }
                    }
                }
            }
        }

        /*
         * Create the specified villian and initialise its stats
        */ 
        private void CreateEnemy(Character enemy)
        {
            int attack, defense, hitPoints;

            //Assign stats to the stats based on the name
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

            //Initialise the villian with the assigned stats
            enemy.Attack = attack;
            enemy.Defense = defense;
            enemy.HitPoints = hitPoints;
        }

        /*
         * Change the hero position to the specified coordinates and mark the map based on whether it's a new position or the previous one
         */ 
        public void ChangeHeroPos(int x, int y, bool rev)
        {
            //If it's the previous position, place back the villian else mark it empty
            if (rev)
                GameBoard[Hero.XPosition, Hero.YPosition] = 2;
            else
                GameBoard[Hero.XPosition, Hero.YPosition] = 0;

            //Mark the new hero position
            GameBoard[x, y] = 1;

            //Save the hero's current position as the previous
            Hero.XPrevious = Hero.XPosition;
            Hero.YPrevious = Hero.YPosition;

            Hero.XPosition = x;
            Hero.YPosition = y;
        }

        /*
         * Print the map with all the villians and the hero
         */
        public void PrintBoard()
        {
            char icon;

            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    switch (GameBoard[j, i])
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

        /*
         * Return the character that represents the villian based on the name
         */ 
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

        /*
         * Return the villian on the specified position
         */ 
        public Character GetEnemy(int x, int y)
        {
            foreach (Character E in Enemies)
            {
                if (E.XPosition == x && E.YPosition == y)
                    return E;
            }

            //No villian encountered on the specified position
            return null;
        }

        /*
         * Mark the specified position with X
         */
        public void MarkBattle(int xPos, int yPos)
        {
            //3 is for character X
            GameBoard[xPos, yPos] = 3;
        }

        /*
         * Remove the defeated villian from the enemy list
         */
        public void KillEnemy(Character e)
        {
            Enemies.Remove(e);
        }
    }
}
