using Godot;
using System;

public partial class Player1 : Player {
	private bool charging;
	private float MAX_SPEED = 400.0f;
	private float PEELOUT_BOOSTER = 3.0f;
	private Vector2 storedVelocity;
	public Player1() : base(true) {
		charging = false;
		storedVelocity = new Vector2();
	}

	protected override Vector2 HandleAction(Vector2 velocity, double delta) {
		if (Input.IsActionJustPressed("Ability")) {
			charging = true;
			storedVelocity.X = animation.FlipH ? -1.1f : 1.1f;
			// animation.Play("PeeloutCharge");
		} else if (Input.IsActionPressed("Ability") && IsOnFloor() && Mathf.Abs(velocity.X) < MAX_SPEED) {
			storedVelocity.X *= PEELOUT_BOOSTER;
		} else { // player let go of button
			charging = false;
			animation.Play("Idle");
			velocity.X = storedVelocity.X;
		}
		return velocity;
	}

	override protected Vector2 HandleActiveMovement(Vector2 velocity, double delta) {
		velocity = HandleAction(velocity, delta);
		if (!charging) velocity = HandleJump(velocity, delta);
		if (!charging) velocity = HandleHorizontalMovement(velocity, delta);
		return velocity;
	}
}
