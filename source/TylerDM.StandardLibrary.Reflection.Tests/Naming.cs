namespace TylerDM.StandardLibrary.Reflection;

public static class Naming
{
	[Fact]
	public static void EnsureCorrectNamespacePrefix()
	{
		var classesWithIncorrectNamespace = (
			from x in typeof(Startup).Assembly.GetDeveloperTypes()
			where x.IsAnonymous() == false
			let y = x.Namespace ?? ""
			where y.StartsWith("TylerDM.StandardLibrary.Reflection") == false
			select x
		).ToArray();
		if (classesWithIncorrectNamespace.Any())
			throw new Exception("Classes found with incorrect namespace.");
	}
}
