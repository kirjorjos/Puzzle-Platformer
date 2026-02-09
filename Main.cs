using Godot;
using System;

public partial class Main : Node2D
{
	public void onQuitPressed() {
		GetTree().Quit();
	}

	public void onPlayPressed() {
		GetTree().ChangeSceneToFile("res://world.tscn");
	}
}
