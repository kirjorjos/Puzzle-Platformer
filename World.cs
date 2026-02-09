using Godot;
using System;

public partial class World : Node2D {
	
	private Player[] players;
	private int activePlayer;
	public override void _Ready() {
		base._Ready();
		Player player1 = GetNode<Player>("Player1");
		Player player2 = GetNode<Player>("Player2");
		players = [player1, player2];
		activePlayer = 0;
	}

	public override void _Process(double delta) {
		base._Process(delta);
		if (Input.IsActionJustPressed("SwitchPlayer")) {
			activePlayer = (activePlayer +1) % 2;
			players[0].toggleActive();
			players[1].toggleActive();
		}
	}
}
