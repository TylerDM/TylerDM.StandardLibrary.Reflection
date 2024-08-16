namespace TylerDM.StandardLibrary.Reflection.Reflection;

public class SymbolIdAttributeExtTests
{
	private const string _methodIdStr = "451F0F72-6E92-4E83-98BC-BCA972D87473";
	private static readonly Guid _methodId = Guid.Parse(_methodIdStr);
	private static readonly int _expectedValue = Random.Shared.Next();

	class TestClass()
	{
		[SymbolId(_methodIdStr)]
		public static int Test() => _expectedValue;
	}

	[Fact]
	public void GetByIdOnType()
	{
		var method = typeof(TestClass).Get<MethodInfo>(_methodId);
		Assert.Equal(_expectedValue, method.Invoke(null, null));
	}

	[Fact]
	public void GetById()
	{
		var method = SymbolId.Get<MethodInfo>(_methodId);
		Assert.Equal(_expectedValue, method.Invoke(null, null));
	}
}
