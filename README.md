# Skeleton Farming Simulator
Short version of a 2D platformer game I'm creating on Unity.

Gameplay video on [YouTube: Skeleton Farming Simulator](https://youtu.be/AO2dPDledO8) <br/>

Click the link to play! https://simmer.io/@Krykutay/skeleton-farming-simulator  <br/>
<br/>
<img src="https://user-images.githubusercontent.com/44427408/149325365-5b406779-6d0d-4c3b-b4b1-1e4a8d3f3cda.png" width="720" height="405"> <br/>

This is a mini game version of what I've been enjoying to create lately. The actual game is leaning toward a platformer game with puzzles and such whereas this one is basically a "skeleton farming simulator" with a main character, enemies and npcs. 

# Game Overview
<br/>
<img src="https://user-images.githubusercontent.com/44427408/150685719-54b6bc9a-933a-44b6-8378-ef6017769e8a.jpg" width="720" height="405">

## Game Genre
Platformer Game

## Game Summary
It's basically a platformer game where the main character can climb, jump, dash, slide and fights with combat mechanics against.. well, skeletons.
The game has three scenes, first one being the main menu with its play button, settings (Sound, Graphics, Controls), how to play tab and Exit.
The second one is the Traning Groud where an npc guides the player with dialog system, and simply teaches the game in a story driven way, explaining the keys and such.
The last scene is where the actual endless game begins, there are spots where enemies & powerups spawn, and the spawn rate as well as the difficulty gets harder the more time passes. By killing a skeletons, you do collect a stone which then turns into a gem, and with gems player can buy new outfits, new swords and enhances the character's offense and defense. Oh and the game includes quite sophisticated easter eggs.

## Things the game include

+ A main character that can climb, jump, dash, wall slide, attack, parry and talk.
+ Enemies with well detailed AI and abilities, such as dodge, teleport, charge, melee attack or range attack etc.
+ Quite smooth player & enemy animations made with sprites in cartoonish style.
+ Powerup system, such as health, extra damage, damage shield and much more!
+ Many visual effects.
+ Voiced Npcs with dialogue system.
+ A story to go along with.
+ Merchants that sell new Outfits, Swords and stimulates that enhance the character's attack damage and total health.
+ Responsive UI system working on all resolutions.
+ A rich detialed Settings menu -> Sounds (master, effects, ambiance, voice), Graphics (resolution, quality, vsync, fullscreen), Controls (changable key binds).
+ Super nice sound effects and ambiance music!
+ Some alright easter eggs.
+ Save system.
+ Small details such as leaving dust particles when jumping/landing, screen shaking slightly when landing & hitting enemies, moving grass when player touches.
+ An endless, infinite and moving background.

# Technical Details

- Both the Player and Enemy behaviors are scripted with Fininite State Machine system. This results in much clearer and flexable code, more performant since both the player and enemy are in a no more than one single state at a time, and that state has it's own only update method, thus far less checks. Additionally, With its object oriented back ground, creating new enemies and new enemy behaviors are much easier this way.

- Both the Player's and Enemies' datas are kept in scriptable objects.

- The game is run via managers, most of which are simple singletons.<br/><br/>
GameManager -> Adjusts the difficulty over time, spawns enemies and powerups and responsible for the time when player is dead and respawned back. Also manages Pause & Play throughout the game as well as the dialogs.<br/><br/>
GameSceneManager -> This manager adjusts the scenes accordingly, player can move between the scenes including the main menu scene.<br/><br/>
SoundManager -> This manager simply adjusts all the sounds in the game. Instead of adding audio for a lot of objects, a simple manager handles it all.<br/><br/>
ObjectPoolingManager -> Instead of destroying and re instantiating gameobjects, Objects pooling manager simply has a Generic Script and basically disables and re-enables objects (object pooling) without destroying them in order to avoid memory allocations which causes performance issues.<br/><br/>
PowerupManager -> Basically whenever the player picks up a powerup, this manager is responsible for the outcome.<br/><br/>
ScoreManager -> This manager is responsible for the stones and gems that are collected as well as keeping the highscore.<br/><br/>
InputManager -> Since Unity's new input system is used, this manager handles the C# based event system for the inputs.<br/><br/>
GameAssets -> A manager that keeps references to the outfit, sword, sword effect sprites.<br/><br/>

- Playerpref Save system for all settings, scores, player inventory and current outfit/sword.

- Many performance and memory adjustments such as keeping the sprites in power of 2, using sprite atlas, efficient animations and coding.

## Player Finite State Machine diagrams in detail
![SkeletonFarmSimulatorPlayerStates](https://user-images.githubusercontent.com/44427408/150682231-b6cf1a75-69e5-441d-99c4-d3e08d633d9b.png) <br/>

The diagram above explains the translations from a state to another. When observered in detail, there are two types of player states, Super States and Sub States. Super states are Grounded state, Ability state and Touching Wall states. <br/>
Grounded state includes Move state, Idle state, Crouch Move state, Crouch Idle state and Landing state. <br/>
Touching Wall state includes Wall Grab state, Wall Climb state and Wall Slide state. <br/>
Ability state includes Primary Attack state, Dash state, Jump state, Wall Jump state and finally Dodge Roll state. <br/>
Finally, there are also two states that doesn't have a superstates. These are, In Air State, Ledge Climb, Knockback state and Defense(parry) state. <br/>

These states, of course, also bring up animations as the complicity. Animation state behaviours are shows in the following picture. Note that player can actively be on only a single state at a time. <br/>

![PlayerAnims](https://user-images.githubusercontent.com/44427408/150682589-362494d3-07f0-47de-8670-ae97ec970d27.jpg) <br/>

UML Diagram of Player States:
![PlayerStateUML](https://user-images.githubusercontent.com/44427408/150683315-dd2cc360-bd39-42dd-bc09-abb695c4a629.png) <br/>

## Enemy Finite State Machines

This is what makes the Enemy AI act like they got a semblance of brain! <br/>
With enemies, there is sub/super state system. There are certain states as Idle, Move, PlayerDetected, Stun, Charge, RangeAttack, MeleeAttack, Dead, Respawn and such, and enemies get to have as many of these states. I've used inheritance here such that each enemy has their own, let's say, melee attack behaviours but also do have certain shared melee attack behaviors, thus EnemyXMeleeAttackState inherits from MeleeAttackState. <br/>

This way, some enemies may or may not have, let's say stun state, and therefore they won't be able to stunned by the player. Additionally, each state has it's own scriptable object and the characteristics / stats of each enemy is saved in them. Finally, Since all enemies do share similar behaviors, they all inherit from the class Entity. <br/>

To give an example, Enemy4 (Archer), uses the following scriptable objects as its data behaviors.
<br/>
<img src="https://user-images.githubusercontent.com/44427408/150683899-04f9cb26-f0ee-4b6d-a49d-40d4a76e9cb8.png" width="371" height="260"> <br/>

## Checks for both Player and Enemy
Each enemy and player object has its own Ground Check, WallCheck, LedgeCheck, MeleeAttackPositionCheck. Some enemies also have checks as RangeAttack, LedgeBehind etc. <br/>
With these checks, in each physics update (Fixed Update), the game checks if player is let's say grounded or not if its ground touching is concerned in its current state. <br/>
Additionally, on this demo, Each enemy checks its distance between the player in each physics update and determines whether it is in agro range or not. However, this behaviour will get costy and costy the bigger the game gets. Thus, as a solution, instead player will constantly crate an overlapseCircleAll in every .1 seconds and let the enemies know the player has approached with an observer pattern.

## Combat System
In this game, combat system is handled through interfaces. When the player weapon's hit area hits the enemy hitbox, the engine checks for IDamagable interface, and if it exists, then the enemy takes hit. A simillar pattern for the vice versa. <br/>
<img src="https://user-images.githubusercontent.com/44427408/150685590-a78a4fe2-0feb-4a9a-89c7-737b11a38a50.jpg" width="720" height="405"> <br/>

## UI Elements
### Settings
Audio Settings offer the player a chance to alter Master, Effects, Music and Voice Volumes.<br/>
<img src="https://user-images.githubusercontent.com/44427408/150685194-e9c31e10-1101-4a5e-969d-50c70fca5eb9.jpg" width="390" height="250"> <br/>
Graphics Settings offer the player a chance to change Quality, Resolution, FullScreen(or not), V-Sync(or not). <br/>
<img src="https://user-images.githubusercontent.com/44427408/150685219-25eb842c-d655-4a41-b552-a0a2bc8c60aa.jpg" width="390" height="250"> <br/>
Controls Settings offer the player a chance to change any key binding. <br/>
<img src="https://user-images.githubusercontent.com/44427408/150685265-bacd5063-d7dd-4ee9-bdb6-77c46d453dd4.jpg" width="390" height="250"> <br/>

### Shops
Shops do include new outfits, new swords and stimulates that enchance the character's total health and damage. These are bought with gems which are collected from skeletons. Not that each item that is bought from the shop is added to player inventory and stays there forever thanks to the save system! <br/>
<img src="https://user-images.githubusercontent.com/44427408/150685303-c24b9167-8994-4f2e-a390-8067f3d6f3e9.jpg" width="750" height="465"><br/>

### Dialogue System
Guess what! Npcs do talk with voice and tell the game's story as well as helping player how to play. <br/>
<img src="https://user-images.githubusercontent.com/44427408/150685319-4134b4be-667f-45b9-aed6-67b3c99fae6a.jpg" width="720" height="405"> <br/> 

### Powerup Visual Effects & Duration Countdown
Each powerup pops up it's own visual effect. Health has its own particle-system animations, Offensive powerup vibrates an orange aura and the character glooms orange while Defensive powerup vibrates a blue auro and the character glooms blue. <br/>
These icons on top left indicates that the corresponding powerup is active and also tell its remaning duration. <br/>
<img src="https://user-images.githubusercontent.com/44427408/150685532-85cbc5b0-a237-4465-93d7-4e0aa00090bb.jpg" width="720" height="405"> <br/>

### Gameover
When the player dies, or tries to leave the scene, the gameover panel pops-up and turns the collected stones into gems as well as showing the kill/collect counts and highscore. <br/>
<img src="https://user-images.githubusercontent.com/44427408/150685561-fb974309-7cc5-4c6c-be78-cd7bd0ebf4eb.jpg" width="720" height="405"> <br/> 

## Quite sophisticated easter eggs
Loading Screen <br/>
<img src="https://user-images.githubusercontent.com/44427408/150685643-d2fb9171-c012-4b0c-9367-e83bb828445a.jpg" width="720" height="405"> <br/> 
A hidden floor where the player finds secret powerups <br/>
<img src="https://user-images.githubusercontent.com/44427408/150685665-350198c3-c1b8-478e-9e07-fabcf166f412.jpg" width="720" height="405"> <br/> 

As I make progress in the original game, I'll make sure to put its updates here on its own github page! Thanks for reading.

![unity logo](https://user-images.githubusercontent.com/44427408/141788735-9ec1183a-0e02-4acb-b385-a65fc893b201.png)
