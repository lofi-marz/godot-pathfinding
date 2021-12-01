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

    

    private void AddSorted(ASNode node)
    {
        for (int i = 0; i < OpenList.Count; i++)
        {
            if (node.F() <= OpenList[i].F())
            {
                OpenList.Insert(i, node);
                return;
            }
        }
        OpenList.Add(node);
    }

    private bool PosInList(List<ASNode> list, ASNode node)
    {
        return list.Any(n => n.Pos.Equals(node.Pos));
    }
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
        OpenList = new List<ASNode> {new ASNode(Start, false) {G = 0}};
        ClosedList = new List<ASNode>();
        ASNode current;
        do
        {
            if (OpenList.Count == 0) return path;
            current = OpenList[0];
            OpenList.Remove(current);
            ClosedList.Add(current);
            //GD.Print(current.Pos);

            for (int dy = -1; dy < 2; dy++)
            {
                int y = (int)current.Pos.y + dy;

                if (y < 0 || y >= Height) continue;

                for (int dx = -1; dx < 2; dx++)
                {
                    if (dy == dx) continue;
                    if (!(dx == 0 || dy == 0)) continue; //Comment out to enable diagonals
                    int x = (int)current.Pos.x + dx;
                    if (x < 0 || x >= Width) continue;
                    
                    
                    
                    Types cell = (Types)Terrain.GetCell(x, y);
                    Vector2 pos = new Vector2(x, y);
                    ASNode neighbour = new ASNode(pos, cell == Types.Wall);
                    neighbour.G = current.G + 1;
                    neighbour.H = (int)Goal.DistanceTo(pos);
                    neighbour.parent = current;
                    if (neighbour.IsWall) continue;
                    if (PosInList(ClosedList, neighbour)) continue;
                    
                    if (PosInList(OpenList, neighbour) && OpenList.Any(node => node.G < neighbour.G)) continue;
                    AddSorted(neighbour);
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
