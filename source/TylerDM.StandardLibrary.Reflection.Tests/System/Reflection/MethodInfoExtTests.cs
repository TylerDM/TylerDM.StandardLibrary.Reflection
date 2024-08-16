namespace TylerDM.StandardLibrary.Reflection.System.Reflection;

public class MethodInfoExtTests
{
	private const string _symbolIdStr = "{075DB25A-1627-422B-ACA1-63D0AA3850B0}";
	private readonly Guid _symbolId = Guid.Parse(_symbolIdStr);
	private static readonly int _expectedValue = Random.Shared.Next();
	private readonly TestingClass _instance = new();

	class TestingClass()
	{
		[SymbolId(_symbolIdStr)]
		public Task<int> GetValueAsync() =>
				Task.FromResult(_expectedValue);
	}

	[Fact]
	public async Task InvokeAsync()
	{
		var method = SymbolId.Get<MethodInfo>(_symbolId);
		var result = await method.InvokeAsync(_instance);
		Assert.Equal(_expectedValue, result);
	}

	[Fact]
	public async Task InvokeAsyncT()
	{
		var method = SymbolId.Get<MethodInfo>(_symbolId);
		var result = await method.InvokeAsync<int>(_instance);
		Assert.Equal(_expectedValue, result);
	}
}
