using Godot;
using System;

public partial class Player : CharacterBody2D {
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	protected bool isActive;
	private AnimatedSprite2D animation;
	private Camera2D camera;
	private World world;

	protected Player(bool startActive) {
		isActive = startActive;
	}

	public override void _Ready() {
		base._Ready();
		animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		animation.Play("Idle");
		camera = GetNode<Camera2D>("Camera2D");
		world = GetNode<World>("..");
	}

	public void toggleActive() {
		isActive = !isActive;
	}

	public void toggleCamera() {
		camera.Enabled = !camera.Enabled;
	}

	public override void _PhysicsProcess(double delta) {
		base._PhysicsProcess(delta);
		Vector2 velocity = Velocity;

		velocity = HandlePassiveMovement(velocity, delta);

		if (isActive) velocity = HandleActiveMovement(velocity, delta);

		MoveAndSlide();
		Velocity = velocity;

		
	}

	public Vector2 HandlePassiveMovement(Vector2 velocity, double delta) {
		// Add the gravity.
		GD.Print(isActive);
		if (!IsOnFloor()) {
			velocity += GetGravity() * (float) delta;
		}
		return velocity;
	}

	private Vector2 HandleActiveMovement(Vector2 velocity, double delta) {
		// Handle Jump.
		if (Input.IsActionJustPressed("MoveUp") && IsOnFloor()) {
			velocity.Y = JumpVelocity;
			animation.Play("Jump");
		}

		// flip the sprite when moving left
		float horizontalDirection = Input.GetAxis("MoveLeft", "MoveRight");
		GetNode<AnimatedSprite2D>("AnimatedSprite2D").FlipH = (horizontalDirection < 0);

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("MoveLeft", "MoveRight", "MoveUp", "MoveDown");
		if (direction != Vector2.Zero) {
			velocity.X = direction.X * Speed;
			animation.Play("Run");
		}
		else {
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			if (velocity.Y == 0) animation.Play("Idle");
		}
		if (velocity.Y > 0) animation.Play("Fall");

		
		return velocity;
	}
}
