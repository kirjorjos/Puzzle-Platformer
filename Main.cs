using Godot;
using System;
using System.ComponentModel;

public partial class Main : Node2D
{
	public void onQuitPressed() {
		GetTree().Quit();
	}

	public void onPlayPressed() {
		GetTree().ChangeSceneToFile("res://world.tscn");
	}

	public void onLvl1Pressed() {
		GetTree().ChangeSceneToFile("res://test_lvl.tscn");
	}

	public void onLvl2Pressed() {
		GetTree().ChangeSceneToFile("res://lvl_2.tscn");
	}

}
