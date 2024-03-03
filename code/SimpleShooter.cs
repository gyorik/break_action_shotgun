using Sandbox;

public sealed class SimpleShooter : Component
{
	[Property] public float ShootDamage { get; set; } = 9.0f;
	[Property] public float ShootRate { get; set; } = 0.4f;
	[Property] public float ShootVelocity { get; set; } = 1000.0f;
	[Property] public float BulletWeight { get; set; } = 0.0f;
	[Property] public float BulletHealth { get; set; } = 0.0f;
	[Property] public float BulletSize { get; set; } = 1.0f;
	[Property] public float BulletGravityScale { get; set; } = 1.0f;
	
	[Property] public int BulletCount { get; set; } = 1;
	
	[Property] public float Spread { get; set; } = 0.1f;
	[Property] public GameObject BulletPrefab { get; set; }

	[Property] public GameObject Camera { get; set; }
	protected override void OnUpdate()
	{
		if ( IsProxy )
			return;

		Transform aimTransform = Camera.Transform.World;
		if ( Input.Down( "attack1" ) )
		{
			Shoot();
			return;
		}

	}

	SoundEvent shootSound;

	TimeSince timeSinceShoot;

	public void Shoot()
	{
		if ( timeSinceShoot < ShootRate )
			return;

		timeSinceShoot = 0;

		Sound.Play( shootSound, Transform.Position );

		if(BulletPrefab is not null){
			for ( int i = 0; i < BulletCount; i++ )
			{
				var bullet = BulletPrefab.Clone( Camera.Transform.Position+Camera.Transform.Rotation.Forward*10.0f, Camera.Transform.Rotation);
				var script = bullet.Components.Get<Bullet>();
				script.ShootDamage = ShootDamage;
				bullet.Components.Get<Gib>().Health = BulletHealth;
				bullet.Transform.LocalScale = new Vector3(BulletSize,BulletSize,BulletSize);
				var physics = bullet.Components.Get<Rigidbody>( FindMode.EnabledInSelfAndDescendants );
				if ( physics is not null )
				{
					physics.PhysicsBody.GravityScale = BulletGravityScale;
					var randomRotation = Rotation.Random;;
					physics.Velocity = (Camera.Transform.Rotation.Forward+randomRotation.Forward*Spread) * ShootVelocity;
				}
				
			}
		}
	}
}
