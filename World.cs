using Godot;
using System;

public partial class World : Node2D {
	
	private Player[] players;
	private int activePlayer;
	private bool cameraZoomed;
	public override void _Ready() {
		base._Ready();
		Player player1 = GetNode<Player>("Player1");
		Player player2 = GetNode<Player>("Player2");
		players = [player1, player2];
		activePlayer = 0;
		cameraZoomed = true;
	}

	public override void _PhysicsProcess(double delta) {
		base._PhysicsProcess(delta);
		if (Input.IsActionJustPressed("SwitchPlayer")) {
			activePlayer = (activePlayer +1) % 2;
			players[0].toggleActive();
			if (cameraZoomed) players[0].toggleCamera();
			players[1].toggleActive();
			if (cameraZoomed) players[1].toggleCamera();
		}

		if (Input.IsActionJustPressed("SwapCameraMode")) {
			players[activePlayer].toggleCamera();
			cameraZoomed = !cameraZoomed;
		}
	}

	public bool isCameraZoomed() {
		return cameraZoomed;
	}
}
