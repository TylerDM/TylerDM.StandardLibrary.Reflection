namespace TylerDM.StandardLibrary.Reflection.System.Reflection;

public class PropertyInfoExtTests
{
	private static readonly int _expectedValue = Random.Shared.Next();

	interface StaticInterface
	{
		static abstract int StaticProperty { get; }
	}

	class Implementation : StaticInterface
	{
		public static int StaticProperty => _expectedValue;
		public int InstanceProperty { set { } }
		public int WeirdStaticProperty { set { } }
	}

	[Fact]
	public void IsStatic()
	{
		var type = typeof(Implementation);

		var staticPropertyInfo = type.Get<PropertyInfo>(nameof(Implementation.StaticProperty));
		Assert.True(staticPropertyInfo.IsStatic());

		var instancePropertyInfo = type.Get<PropertyInfo>(nameof(Implementation.InstanceProperty));
		Assert.False(instancePropertyInfo.IsStatic());
	}

	[Fact]
	public void StaticGetValue()
	{
		var value = typeof(Implementation)
			.Get<PropertyInfo>(nameof(StaticInterface.StaticProperty), x => x.IsStatic())
			.StaticGetValue<int>();
		Assert.Equal(_expectedValue, value);
	}
}
