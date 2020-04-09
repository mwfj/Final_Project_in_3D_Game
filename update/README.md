# Update
(03/31/2020) Decide our game topic and upload the final project proposal

(04/07/2020) Division of labor for game development

(04/09/2020) Post our first playable game

In 04/09 updates we add a random maze and a boss room.  But currently no boss in it. We will put adds in the maze and boss in the room in next updates. 

![](/pic/update1.png) At this patch, the way connected to the boss room will open after click on all three 'switches'. There isn't a finish condition yet, because no boss there. 

* Add DeleteEdge(MazeDirection) function to the MazeCell class. Which can delete the wall or passage of the cell for that direction.
* Add abstract GetEdgeType() function to the MazeCellEdge class. Which should return the type of Edge.
* Override GetEdgeType() in class MazeWall and MazePassage.
* Add code to generate boss room. 
* Add conditions to open the door. 