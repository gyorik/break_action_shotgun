namespace Gunfight;

public partial class CrouchMechanic : BasePlayerControllerMechanic
{
	public override bool ShouldBecomeActive()
	{
		return Input.Down( "Duck" ) && !HasAnyTag( "sprint", "slide" );
	}

	public override IEnumerable<string> GetTags()
	{
		yield return "crouch";
	}

	public override float? GetEyeHeight()
	{
		return -32.0f;
	}

	public override float? GetSpeed()
	{
		return 65.0f;
	}
}
