using Godot;
using System;
using System.Collections.Generic;
public class Main : Node2D
{
	private TileMap tileMap;
	private AStar pathfinder;
	private const int GRID_WIDTH = 13;
	private const int GRID_HEIGHT = 8;
	private Vector2 start;
	private Vector2 goal;

	private List<ASNode> path;
	
	// Declare member variables here. Examples: // private int a = 2; // private string b = "text"; // Called when the node enters the scene tree for the first time. 
	public override void _Ready() 
	{
		SetProcess(true);
		tileMap = (TileMap)GetNode("Grid");
		path = new List<ASNode>();
	} 
	//  // Called every frame. 'delta' is the elapsed time since the previous frame. //  
	public override void _Process(float delta) 
	{

    }
	

    public override void _Draw()
    {
	    base._Draw();
	    GD.Print("Drawing");
	    for (int i = 0; i < path.Count-1; i++)
	    {
		    Vector2 currPos = tileMap.MapToWorld(path[i].Pos) + new Vector2(8, 8);
		    Vector2 nextPos = tileMap.MapToWorld(path[i + 1].Pos) + new Vector2(8, 8);
		    GD.Print($"Drawing {currPos} to {nextPos}");
		    DrawLine(currPos, nextPos, Colors.Black, 1f);
	    }
    }


    public void _OnButtonPressed()
    {
	    pathfinder = new AStar(GRID_WIDTH, GRID_HEIGHT, tileMap);
	    path = pathfinder.Run();
	    GD.Print("Done");
	    Update();
    }
}

