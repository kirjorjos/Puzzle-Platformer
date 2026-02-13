using Godot;
using System;

public partial class Player : CharacterBody2D {
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	protected bool isActive;
	protected AnimatedSprite2D animation;
	private Camera2D camera;
	protected Player other;

	protected Player(bool startActive) {
		isActive = startActive;
	}

	public override void _Ready() {
		base._Ready();
		animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animation.Play("Idle");
		camera = GetNode<Camera2D>("Camera2D");
	}

	public void toggleActive() {
		isActive = !isActive;
	}

	public void toggleCamera() {
		GD.Print(3);
		camera.Enabled = !camera.Enabled;
	}

	public override void _PhysicsProcess(double delta) {
		base._PhysicsProcess(delta);
		Vector2 velocity = Velocity;

		velocity = HandlePassiveMovement(velocity, delta);

		if (isActive) velocity = HandleActiveMovement(velocity, delta);

		HandleCameraLogic();
		HandleSwitchPlayer();

		Velocity = velocity;
		MoveAndSlide();
		
	}

	protected Vector2 HandlePassiveMovement(Vector2 velocity, double delta) {
		// Add the gravity.
		if (!IsOnFloor()) {
			velocity += GetGravity() * (float) delta;
			if (velocity.Y > 0) animation.Play("Fall");
		}
		return velocity;
	}

	protected virtual Vector2 HandleActiveMovement(Vector2 velocity, double delta) {
		velocity = HandleAction(velocity, delta);
		velocity = HandleJump(velocity, delta);
		velocity = HandleHorizontalMovement(velocity, delta);
		return velocity;
	}

	protected Vector2 HandleJump(Vector2 velocity, double delta) {
		if (Input.IsActionJustPressed("MoveUp") && IsOnFloor()) {
			velocity.Y = JumpVelocity;
			animation.Play("Jump");
		}
		return velocity;
	}

	protected Vector2 HandleHorizontalMovement(Vector2 velocity, double delta) {
		float horizontalDirection = Input.GetAxis("MoveLeft", "MoveRight");

		// flip the sprite based on last movement direction
		if (horizontalDirection < 0) animation.FlipH = true;
		if (horizontalDirection > 0) animation.FlipH = false;

		if (horizontalDirection == 0) { // neither left or right is held
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			if (velocity.Y == 0) animation.Play("Idle");
		} else {
			velocity.X = horizontalDirection * Speed;
			animation.Play("Run");
		}
		return velocity;
	}

	// Does nothing in base class, overridden in sub classes
	protected virtual Vector2 HandleAction(Vector2 velocity, double delta) {
		return velocity;
	}

	private void HandleSwitchPlayer() {
		if (Input.IsActionJustPressed("SwitchPlayer")) {
			isActive = !isActive;
			if (DataHolder.cameraZoomed) {
				toggleCamera();
			}
		}
	}

	private void HandleCameraLogic() {
		if (Input.IsActionJustPressed("SwapCameraMode")) {
			GD.Print(1);
			if (isActive) {
				GD.Print(2);
				toggleCamera();
				Camera2D camera = GetNode<Camera2D>("../Camera2D");
				camera.Enabled = !camera.Enabled;
				DataHolder.cameraZoomed = !DataHolder.cameraZoomed;
			}
		}
	}
}
