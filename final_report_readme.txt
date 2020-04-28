Team Name: Refulgence
Team Member: Wufangjie Ma, Zhongyue Ren

Character Control
=================

1. A/D for Horizontal Move

2. W/S for Vertical Move

3. Mouse Left Button for Attack(click/Hold)

4. Hold mouse right button to change direction

5. Space to jump

6. Escape to quit the Game


Game Mechanics
======================
The player first need to find three switches in the randomly generated maze.
When all the switches turned on, the boss room will opened. Then the game target will change to find the entry of boss room.
When the player enter the boss room, the boss will generate in the boss room. Now the game target is to kill the boss.
When the boss die, game over.



Animation Control
=================

1. Blend Tree for different direction running(Scripts/WGS Jump)

2. Different attack stage different animation(Scripts/WGS Jump)

   Use trigger condition to change the phase of attack animation. Using
   a cooldown to make sure the animation will not be interrupted. Also
   use onStateEnter and onStateExit to change the parameter isAttacking,
   which is related to the damage count.

3. Jump animation(Scripts/WGS Jump)

   Its difficult to synchronize the movement of the character with the
   animation. The change from jump up animation to jump down should test
   a lot for the best performance.

4. Dying animation

   Use any state -> dying. The transition condition is the bool Alive,
   which will be updated by the Character Stats scripts. Use onStateExit
   () event to destroy the dead object.

5. For Trash/Boss, we use animation transition to control different animation(idle, walk, getHit and attack; locate at Scripts/TrashAnimationController)
   Both trash and boss we use StateMachineBehavior Script(dyingBehavior) to achieve the dying effect


Boss Generate Animation
-----------------------
Boss generate generation is little different with trash. The reason is that when the boss generated, it will play an introduce animation first, where the boss is undefeatable  at that time. What's more, when boss playing the introduce animation, it will not chase main character even it has detect the exist of main character. After playing the introduce animation, it will start to chase main character just like other navmesh agent. I use AnimatorStateInfo to achieve this funciton



Health Bar
==========

Add world space canvas to the enemy prefab, and change the fill account
by the health point of the enemy.

If health equals 0, play the dying animation and destroy the object
after the animation.

And the boss health bar will use the camera space, and is located at the
top left of the screen. It will be activated when the boss is generated.

If we just set the canvas as a sub object of the enemy, the health bar
will rotate with the enemy. We want it always face to our camera, so we
add a script to dynamically detect the rotation and make sure the health
bar is perfectly displayed.



Random Map Generation
=====================

Using a BFS based searching method. Each generation step randomly choose
one direction to add a new moveable cell or a wall to block the passage.

Trace the number of wall of one cell, make sure each cell should not be
isolated.



Attack
======

Detection
---------

We use collision detection on the weapon model to check the attack. If
the weapon hits the enemy, it will have an collision event.

Add collision trigger on the weapon. Using onTriggerStay to check the
attack the collision for the enemy with a delay have the best
performance. Because sometimes enter and leave will not occur at the
same time as the animation event change the attacking stats. So trigger
the attack count and then delay the next attack until current animation
is finish will be the best.



Hit Box
-------

We should add additional box collider to check the attack. Because of
the NaveMesh, some times the Weapon may not really attack on the enemy
model, so a little bigger hit box should be added. This will improve the
use experience.



Dynamic Navmesh Generation
======================
Because all the object in our game is dynamically generated, traditional NavMesh creation is not fit for our game. Thus, I use dynamic way to create Navmesh. Specifically,  I use the NavMesh Component provided by Unity to achieve this.(link: https://github.com/Unity-Technologies/NavMeshComponents). In our game, the navmesh baker just like other Prefab. When the game open, the game map(maze) randomly generated and the NavMesh will according to the current map to bake the mesh

For NavMesh Agent, we have two types of navmesh agent in our game: one is Trash(walk around in the maze) and Boss(born in Boss Room when all the switch is open and main character enter the boss room). All of them will regard the main character as the target and chase it. When agent close to the main character, they will attack main character automatically. When agent and main character move further, agent start to chase main character again

Note that, there totally have three different trash type in the trash pool(trash_type_list). When the game starts, the system will randomly select three of these three types of monsters as the trash in the game, where in different game, the trash are different.

Boss Creation
----------------------------
The boss creation is little complicated, when the main character turn all the switch on(3 in this game), boss room will open, and the location of door will automatically create a box collider to detect whether the main character enter the boss room or not. When the main character first enter the boss room, it will trigger this collider and boss will generate in the boss room.





Sound
=========================
There has two background sound in the game, when the main character in the maze, the game will play relatively soothing background music. However, when the main character entered the boss room, the concert became relatively intense in order to give the player a sense of tension.

Character sound effect
----------------------------
Because the time limit, I only added sound effects for the main character and boss. When player run, attack and jump, it will have sound effect. When boss roar(introduce animation) and attack, it will have sound effect. We will add sound effect for all character in the future.

Source
=======================
[MainCharacter] https://haonstore.wixsite.com/main 
[FREE Stylized PBR Textures Pack] https://assetstore.unity.com/packages/2d/textures-materials/free-stylized-pbr-textures-pack-111778
[Trash] https://assetstore.unity.com/packages/3d/characters/humanoids/mini-legion-rock-golem-pbr-hp-polyart-94707
[Boss] https://assetstore.unity.com/packages/3d/characters/humanoids/golem-3620
[Maze music]: https://opengameart.org/content/soliloquy
[Boss Room Music]: https://opengameart.org/content/the-dark-amulet-dark-mage-theme  
[Victory Music]: https://opengameart.org/content/lively-meadow-victory-fanfare-and-song
[Monster Roar]: http://soundbible.com/1963-Giant-Monster.html
[Boss Punch]: http://soundbible.com/992-Right-Cross.html
[Main character sound]: https://assetstore.unity.com/packages/3d/characters/unity-chan-model-18705