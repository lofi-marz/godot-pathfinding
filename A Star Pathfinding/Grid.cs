using Godot;
using System;
using System.Collections.Generic;
public class Grid : TileMap 
{
	
	public enum Types
	{
		Grass,
		Wall,
		Start,
		Goal
	}
	
	const int GRID_WIDTH = 13;
	const int GRID_HEIGHT = 8;
	ASNode[,] nodes;
	ASNode start = new ASNode();
	ASNode goal = new ASNode();

	private List<ASNode> path;
	
	// Declare member variables here. Examples: // private int a = 2; // private string b = "text"; // Called when the node enters the scene tree for the first time. 
	public override void _Ready() 
	{
		SetProcess(true);
		nodes = new ASNode[GRID_HEIGHT, GRID_WIDTH];
		path = new List<ASNode>();
		var cells = this.GetUsedCells();
		foreach (Vector2 cell in cells) {
			GD.Print(cell);
			var cellType = this.GetCell((int)cell.x, (int)cell.y);
			GD.Print(cellType);
			var node = new ASNode(new Vector2(cell.x, cell.y), cellType == (int)Types.Start);
			nodes[(int)cell.y, (int)cell.x] = node;
			if (cellType == (int)Types.Start)
			{
				start = node;
				
			}

			if (cellType == (int)Types.Goal)
			{
				goal = node;
			}
		}
		
		GD.Print("Finished");
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
		    Vector2 currPos = (MapToWorld(path[i].Pos) );
		    Vector2 nextPos = MapToWorld(path[i + 1].Pos);
		    GD.Print($"Drawing {currPos} to {nextPos}");
		    DrawLine(currPos, nextPos, Colors.Black, 10f);
	    }
    }

  
}

