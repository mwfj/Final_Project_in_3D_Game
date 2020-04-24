# Update
(03/31/2020) **Decide our game topic and upload the final project proposal**

(04/07/2020) **Division of labor for game development**

(04/09/2020) **Post our first playable game**

In 04/09 updates we add a random maze and a boss room.  But currently no boss in it. We will put adds in the maze and boss in the room in next updates. 

![map_overview](pic/update1.png) At this patch, the way connected to the boss room will open after click on all three 'switches'. There isn't a finish condition yet, because no boss there. 

* Add DeleteEdge(MazeDirection) function to the MazeCell class. Which can delete the wall or passage of the cell for that direction.
* Add abstract GetEdgeType() function to the MazeCellEdge class. Which should return the type of Edge.
* Override GetEdgeType() in class MazeWall and MazePassage.
* Add code to generate boss room. 
* Add conditions to open the door. 
* Now left click mouse will change the player camera angle from buttom to top for the future use, and click it again to resume the default camera angle
* Now scroll the mouse wheel will change the field of view for camera

(04/14/2020) **Add Camera Collision detection, now camera won't across the Wall**

+ modify the code in Player.cs, where I change RayCast to LineCast for detecting the camera collision
+ change the layer of wall from "default" to "Wall"<br>
![add_wall_layer](pic/add_wall_layer.png)
+  Set collision layer in Player.cs to "wall" in Unity Engine<br>
 ![set_collision_layer](pic/set_collision_layer.png)
+ Change scene name to "Main"
+ Add Character Buff Utility

(04/23/2020) **Add Runtime Navmesh Baker and Agent**

+ Now, our game can dynamically bake Nav Mesh.
+ The pic below is the configuration of Nav Mesh, where we use **Maze.prefab** as nav mesh plane

> ![Configure_navmash_agent](pic/Configure_navmash_agent.png)
> ![Configure_navmesh_bake](pic/Configure_navmesh_bake.png)

+ We add tag to Player call "Player"

> ![Player_tag](pic/Player_tag.png)

+ We add mobs/trash in the maze, which creates near to the player. We regard these trash as the Navmesh Agent, which chase player in the limited walkable navmesh distance and the tag "Emery" specific to  Trash/Boss in the future.
+ **Note that:** the trash has three types. When the game begin, our system will randomly choose the trash types, that **means each time you open the game, the numer of each type of trash may vary**.

> ![trash_configuration](pic/trash_configuration.png)

+ The pic below is the prefab  and script update
+ **Note that:** the class of "**EmeryBase"** is the abstract class, where all trashes and bosses must inherit this class. **BakeNavMesh** mainly response for building mesh; The logic of "Nav Agent" you can find in the class of **"Trash"**

> ![2020-4-23_update_prefab](pic/2020-4-23_update_prefab.png)
> ![2020-4-23_update_scripts](pic/2020-4-23_update_scripts.png)