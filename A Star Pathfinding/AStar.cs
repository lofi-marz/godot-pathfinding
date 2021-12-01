using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using static Grid;
public class AStar
{

    public int Width;

    public int Height;

    public List<ASNode> OpenList;

    public List<ASNode> ClosedList;

    public Vector2 Start;
    public Vector2 Goal;
    public TileMap Terrain;

    public AStar(int width, int height, TileMap terrain)
    {
        Width = width;
        Height = height;
        Terrain = terrain;
        OpenList = new List<ASNode>();
        ClosedList = new List<ASNode>();
        Start = (Vector2)terrain.GetUsedCellsById((int)Grid.Types.Start)[0]; //We should never have multiple starts, so we can assume the first one we run into is right
        Goal = (Vector2)terrain.GetUsedCellsById((int)Grid.Types.Goal)[0];
    }

    public List<ASNode> Run()
    {
        GD.Print("Starting A*");
        List<ASNode> path = new List<ASNode>();
        List<ASNode> openList = new List<ASNode> {new ASNode(Start, false)};
        List<ASNode> closedList = new List<ASNode>();
        ASNode current;
        do
        {
            if (openList.Count == 0) return path;
            current = openList[0];
            openList.Remove(current);
            closedList.Add(current);
            //GD.Print(current.Pos);

            for (int dy = -1; dy < 2; dy++)
            {
                int y = (int)current.Pos.y + dy;

                if (y < 0 || y >= Height) continue;

                for (int dx = -1; dx < 2; dx++)
                {
                    if (dy == dx) continue;
                    if (!(dx == 0 || dy == 0)) continue;
                    int x = (int)current.Pos.x + dx;
                    if (x < 0 || x >= Width) continue;
                    
                    
                    
                    Types cell = (Types)Terrain.GetCell(x, y);
                    Vector2 pos = new Vector2(x, y);
                    ASNode neighbour = new ASNode(pos, cell == Types.Wall);
                    if (neighbour.IsWall) continue;
                    if (closedList.Any(node => node.Pos.Equals(neighbour.Pos))) continue;
                    neighbour.parent = current;
                    if (!openList.Any(node => node.Pos.Equals(neighbour.Pos))) openList.Add(neighbour);
                    if (neighbour.Pos == Goal)
                    {
                        ASNode curr = neighbour;
                        while (curr != null)
                        {
                            path.Add(curr);
                            curr = curr.parent;
                        }
                        return path;
                    }
                }
            }
        } while (true);
    }
}
