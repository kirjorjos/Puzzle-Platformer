using Godot;
using System;

public partial class Main : Node2D
{
	private Player[] players;
	private int activePlayer;
	public void onQuitPressed() {
		GetTree().Quit();
	}

	public override void _Ready() {
		base._Ready();
		Player player1 = GetNode<Player>("Player1");
		Player player2 = GetNode<Player>("Player2");
		players = [player1, player2];
		activePlayer = 0;
	}

	public void onPlayPressed() {
		GetTree().ChangeSceneToFile("res://world.tscn");
	}

	public override void _Process(double delta) {
		base._Process(delta);
		if (Input.IsActionJustPressed("switch_player")) {
			activePlayer = (activePlayer +1) % 2;
			players[0].toggleActive();
			players[1].toggleActive();
		}
	}
}
