namespace TylerDM.StandardLibrary;

public static class Naming
{
	[Fact]
	public static void EnsureCorrectNamespacePrefix()
	{
		var classesWithIncorrectNamespace = (
			from x in typeof(Reflection.Startup).Assembly.GetDeveloperTypes()
			where x.IsAnonymous() == false
			let y = x.Namespace ?? ""
			where y.StartsWith("TylerDM.StandardLibrary.Reflection") == false
			select x
		).ToArray();
		Assert.Empty(classesWithIncorrectNamespace);
	}
}
