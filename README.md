# C-Sharp-Swingy

## Overview

This project is based on the second project in a series of Java projects, produced at [42](https://www.42.fr/). It is a console based minimalistic text-based RPG game.

### Gameplay

A player can pick from multiple heroes of different types (Wizard, Warrior and Alchemist) with different starting stats among them:

- Wizard: attack = 13, defense = 7, hitPoints = 80
- Warrior: attack = 15, defense = 3, hitPoints = 110
- Alchemist: attack = 25, defense = 2, hitPoints = 90

When the player starts the game he has 2 options:

- Create a hero
- Select a previously created hero.

After choosing a hero the actual game begins. The hero needs to navigate a square map with the size calculated by the formula (level - 1) * 5 + 10 - (level % 2). For example a hero of level 7 will be placed on a 39X39 map. The initial position of the hero is in the center of the map. He wins the game if he
reaches on of the borders of the map. Each turn he can move one position in one of the4 four directions:

- North
- East
- South
- West

When the map is generated, villains of varying power will be spread randomly over the
map:

- Demon: attack = 15, defense = 3, hitPoints = 50, letter on the map `V`
- Ghost: attack = 25, defense = 5, hitPoints = 100, letter on the map `G`
- Superbat: attack = 40, defense = 10, hitPoints = 150, letter on the map `S`

It is always a good idea to target Demons first when the hero is still on low level as they are the weakest. When the hero moves to a position occupied by a villain, the hero has 2 options:

- Fight, which engages him in a battle with the villain
- Run, which gives him a 50% chance of returning to the previous position. If the odds aren’t on his side, he must fight the villain.

The battle between the hero and the villian is simulated and the outcome of the battle is presented to the player. If the hero looses a battle, he dies and looses the mission. If the battle is won, the hero gains experience points, based on the villain power and levels up if he reaches the next level experience. The experience gain is calculated by the formula: Level / 2 + xpGain. The xpGain is based on the villian as follows:

- Demon: 450
- Ghost: 500
- Superbat: 600

Leveling up is based on the following formula level * 1000 + Pow(level - 1, 2) * 450. So the necessary experience to level follows this pattern:

- Level 1 - 1000 XP
- Level 2 - 2450 XP
- Level 3 - 4800 XP
- Level 4 - 8050 XP
- Level 5 - 12200 XP

## Prerequisites

- Visual Studio 2015 or later
- Entity Framework 6
- SQL Server 2014 or later

## Running the Application

- Before launching the application, it is assumed that you already have SQL Server 2014 or later configured and running. `C-Sharp-Swingy` has a feature that automatically initializes the dababase and create the required table for you if it doesn't already exist.

- Open `Swingy.csproj` with Visual Studio to launch the project.

- Change the database connection string in `App.config` to the one that corresponds to your configuration.

- Click the Start button on Visual Studio or press the F5 key to run the project





