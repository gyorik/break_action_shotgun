using Sandbox;

public sealed class 
	Bullet : Component, Component.ICollisionListener
{
	[Property] public float ShootDamage { get; set; } = 9.0f;
	public void OnCollisionStart( Collision other )
	{
		other.Other.Body.ApplyImpulseAt(other.Contact.Point,other.Contact.Speed * 20.0f);
		var damage = new DamageInfo( ShootDamage, GameObject, GameObject);
		damage.Position = other.Contact.Point;

		foreach ( var damageable in other.Other.GameObject.Components.GetAll<IDamageable>() )
		{
			damageable.OnDamage( damage );
		}
		foreach ( var damageable in other.Self.GameObject.Components.GetAll<IDamageable>() )
		{
			damageable.OnDamage( damage );
		}
	}

	public void OnCollisionStop( CollisionStop other )
	{
	}

	public void OnCollisionUpdate( Collision other )
	{
	}

	protected override void OnStart()
	{
		var physicsBodyPhysicsGroup = Components.Get<Rigidbody>().RigidbodyFlags;
		Log.Info( physicsBodyPhysicsGroup );
	}

	protected override void OnUpdate()
	{

	}
}
