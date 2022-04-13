# Roguelike Hero Dungeon

## Base Gameplay
- Team of 3 heroes
- Turn based combat
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
