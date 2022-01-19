# Skeleton Farming Simulator
Short version of a 2D platformer game I'm creating on Unity.

Gameplay video on [YouTube: Skeleton Farming Simulator](https://youtu.be/vG0dXZinKso)

Click the link to play! https://simmer.io/@Krykutay/skeleton-farming-simulator

![Sfsmainss](https://user-images.githubusercontent.com/44427408/149325365-5b406779-6d0d-4c3b-b4b1-1e4a8d3f3cda.png)


This is a mini game version of what I've been enjoying to create lately. The actual game is leaning toward a platformer game with puzzles and such whereas this one is basically a "skeleton farming simulator" with a main character, enemies and npcs. 

# Game Overview

photo

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
ObjectPoolingManager -> Instead of destroying and re instantiating gameobjects, Objects pooling manager simply has a Generic Scripts and basically disables and re-enables objects (object pooling) without destroying them in order to avoid memory adjustments which causes performance issues.<br/><br/>
PowerupManager -> Basically whenever the player picks up a powerup, this manager is responsible for the outcome.<br/><br/>
ScoreManager -> This manager is responsible for the stones and gems that are collected as well as keeping the highscore.<br/><br/>
InputManager -> Since Unity's new input system is used, this manager handles the C# based event system for the inputs.<br/><br/>
GameAssets -> A manager that keeps references to the outfit, sword, sword effect sprites.<br/><br/>

- Playerpref Save system for all settings, scores, player inventory and current outfit/sword.

- Many performance and memory adjustments such as keeping the sprites in power of 2, using sprite atlas, efficient animations and coding.

Includes quite sophisticated easter eggs.
![ssSkeleton](https://user-images.githubusercontent.com/44427408/149325407-3a37f129-3dfd-423a-acea-0fa060235db9.png)

![unity logo](https://user-images.githubusercontent.com/44427408/141788735-9ec1183a-0e02-4acb-b385-a65fc893b201.png)
