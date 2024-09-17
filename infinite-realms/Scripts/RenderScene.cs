using Godot;
using System;

public partial class RenderScene : Node2D
{
	public event Action RenderFinished;
	public event Action StartRender;
	
	public void Render(TileMapLayer map)
	{

	}
	
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		var parent = GetParent();
		Console.WriteLine("Rendering: " + parent.Name);

		var cellSize = map.TileSet.TileSize;
		var mapTileSize = map.GetUsedRect();
		var viewport = DisplayServer.WindowGetSize();
		var mapPixWidth = mapTileSize.Size.X * cellSize.X;
		var mapPixHeight = mapTileSize.Size.Y * cellSize.Y;
		var mapPixOffsetX = mapTileSize.Position.X * cellSize.X;
		var mapPixOffsetY = mapTileSize.Position.Y * cellSize.Y;
		
		Console.WriteLine("cellSize: " + cellSize);
		Console.WriteLine("mapTileSize: " + mapTileSize);
		Console.WriteLine("viewport size: " + viewport);
		Console.WriteLine("map pixel dimensions : (" + mapPixWidth + ", " + mapPixHeight + ")");
		Console.WriteLine("map pixel offset : (" + mapPixOffsetX + ", " + mapPixOffsetY + ")");
		Console.WriteLine("screen size: " + DisplayServer.ScreenGetSize());

		var vpMaxX = viewport.X;
		var vpMaxY = viewport.Y;
		var mapMaxX  = mapPixOffsetX + mapPixWidth;
		var mapMaxY = mapPixOffsetY + mapPixHeight;
		
		var maxX = Math.Max(vpMaxX, mapMaxX);
		var maxY = Math.Max(vpMaxY, mapMaxY);
		
		Console.WriteLine("Final map size: (" + maxX + ", " + maxY + ")");
	

		var img = Image.CreateEmpty((int) maxX, (int) maxY, false, Image.Format.L8);

		var sourceRect = DisplayServer.WindowGetSize();

		// var loop = true;
		var xStep = (int) vpMaxX / 5;
		var yStep = (int) vpMaxY / 5;

		for (int currentY = 0; currentY < -maxY - yStep; currentY -= yStep)
		{
			map.Position = new Vector2(map.Position.X, currentY);
			
			for (int currentX = 0; currentX < maxX; currentX -= xStep)
			{
				map.Position = new Vector2(-currentX, map.Position.Y);
				var chunk = GetViewport().GetTexture().GetImage();
				var destPost = new Vector2I(currentX, Math.Abs(currentY));
				img.BlitRect(chunk, (Rect2I) sourceRect, destPost);
				
			}
		}
		
		var template = "template-" + parent.Name + ".png";
		var dir = DirAccess.Open(".");
		if (dir is not null)
		{
			if(!dir.DirExists("templates"))
				dir.MakeDir("templates");
		}
		
		img.SavePng("./templates/" + template);
		Console.WriteLine("image saved to: " + template);
		RenderFinished?.Invoke();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
