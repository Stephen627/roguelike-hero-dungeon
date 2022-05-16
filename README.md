# Roguelike Hero Dungeon

## Base Gameplay
- Team of 3 heroes
- Turn based combat
- You can defend with a timing based mini-game to avoid the damage
  - Defending will use a lot of action points and end your turn
  - The main purpose of this will be to avoid the main damage dealing attacks no all attacks
- Setup stage where you can place your heroes on the borad without knowing where the enemy will spawn
- The longer it takes for you to complete a stage the harder the game will become
- Items to boost the stats of the heroes
  - An item will apply to an individual hero not all of them
- Earn a random item at the end of a stage and earn money by defeating enemies
- Player gets a choice of what item they are rewarded with next at the end of the stage
  - The player will get the option to choose a shop as part of these
- Heroes will have 5 unique moves
  - General split of moves, although some heroes will be different
    - 3 that are focused around damage
    - 1 for movement
    - 1 for utility
  - Maybe include a range of moves that a hero can have and the player can choose between them
- An entity has a number of actions points per turn (this can vary on the class and other boosts from items, etc.)
  - This may change per entity
  - moving is 1 action per square
  - using an ability can range from 0 to any amount of action points
    - more powerful abilities will cost more action points
- Implement line sight
  - Bushes, rocks, etc. will break line of sight
  - Ranged attacks will not be able to hit without line of sight
  - Entities will not be allowed to pass through obstacles
- Enemies will telegraph their attacks the for next turn, giving a player that is observing the enemies a chance to avoid attacks or prepare for them
  - For example, a melee based enemy might point their sword at the next target and they will charge at them the next turn
  - or as a magic user, they will start producing a small flame before throwing a fireball
- This might make the enemies feel like they are no longer responding to you, so maybe certain actions will break the determined next turn action
  - For example, if the melee attacker has determined they are attacking a certain player character next turn and another attacks them first. The enemy action can change.

## Base Stats

- Strength
  - Hit harder with melee weapons that require force (Axe, Hammer, etc.)
- Dexterity
  - Hit harder with melee weapons that are more skillful (Rapier, Dagger, etc.)
- Intelligence
  - Spell damage
- Faith
  - Healing and other faith based spells
- Action Points
  - The amount of action points per turn

## Hero Ideas
Not all heroes will be unlocked at the beginning but the player will have to do certain actions to unlock them.
Starting heroes are marked with an asterisk

- Rogue*
  - Produce a fog that enemies are unable to look through
- Warrior*
- Mage*
- Knight
- Necromancer
- Potion Man
  - Focus around poison and other debuffs
- Cleric
- Monk
- Blood magic user (can use health to power up the attacks)
  - Maybe have a melee based one of these too
- Druid

## Buffs and Debuffs
### Buffs
- Healing per turn
- Movement speed
- Defence
- Damage
- Invisible
- Taunt

### Debuffs
- Bleed
- Burn
- Frozen
- Poison
- Blind
- Rage
- Slow

## Items
### Common
Slightly improve base stats

### Rare
Significantly effect the gameplay, things like adding more splash damage or chance to mirror damage to a random enemy.

### Legendary
Heavily effect the way the character is played. Very low drop rate and should be run defining

### Items with a downside
Amazing items with a downside that changes the play style of the player.
For example, doubles the damage but halfs the health or removes all healing affects but heals at the end of each stage.

### Curses
Get a permenant debuff, but when the curse is lifted get a powerful buff

## Rest Mechanic
Between stages the player should be given the option to rest. Resting will fully heal the party. As a punishment for healing the game difficulty should be increased, by increasing the turn counter by a set amount. Maybe further into a run this amount of increase, 10% of turns already taken.

## Colours

Blue 1: #00a8ff
Blue 2: #0097e6
Purple 1: #9c88ff
Purple 2: #8c7ae6
Yellow 1: #fbc531
Yellow 2: #e1b12c
Green 1: #4cd137
Green 2: #44bd32
Middle Blue 1: #487eb0
Middle Blue 2: #40739e
Red 1: #e84118
Red 2: #c23616
White 1: #f5f6fa
White 2: #dcdde1
Grey 1: #7f8fa6
Grey 2: #718093
Dark Blue 1: #273c75
Dark Blue 2: #192a56
Black 1: #353b48
Black 2: #2f3640
