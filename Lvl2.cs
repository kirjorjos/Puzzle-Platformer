using Godot;
using System;

public partial class Lvl2 : Node2D {
	private Camera2D camera;
	
	
	private Player[] players;
	private int activePlayer;
	private bool cameraZoomed;
	public override void _Ready() {
		base._Ready();
		Player player1 = GetNode<Player>("Player1");
		Player player2 = GetNode<Player>("Player2");
		camera = GetNode<Camera2D>("Camera2d");
		camera.Enabled = !camera.Enabled;
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
