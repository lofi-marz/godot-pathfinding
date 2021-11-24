using Godot;
using System;

public class ASNode {

    public int G;

    public int H;

    public Vector2 Pos; 

    public bool IsWall;

    public int F() 
    { 
        return G + H;
    }

    public ASNode parent;
    
    public ASNode()
    {

    }

    public ASNode(Vector2 pos, bool isWall) 
    { 
        this.Pos = pos; this.IsWall = isWall; 
    } 
}