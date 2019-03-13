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

It is always a good idea to targets Demons when the hero is weak as they are the weakest.



