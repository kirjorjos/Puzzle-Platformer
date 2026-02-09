using Godot;
using System;

public partial class Player : CharacterBody2D {
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	protected bool isActive;
	private AnimatedSprite2D animation;
	private Camera2D camera;

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
		camera.Enabled = !camera.Enabled;
		isActive = !isActive;
	}

	public override void _PhysicsProcess(double delta) {
		base._PhysicsProcess(delta);
		Vector2 velocity = Velocity;

		HandlePassiveMovement(velocity, delta);

		if (this.isActive) HandleActiveMovement(velocity, delta);
	}

	public void HandlePassiveMovement(Vector2 velocity, double delta) {
		// Add the gravity.
		if (!IsOnFloor()) {
			velocity += GetGravity() * (float) delta;
		}
	}

	private void HandleActiveMovement(Vector2 velocity, double delta) {
		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor()) {
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero) {
			velocity.X = direction.X * Speed;
			animation.Play("Run");
		}
		else {
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
