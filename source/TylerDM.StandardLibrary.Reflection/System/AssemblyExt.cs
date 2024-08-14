namespace TylerDM.StandardLibrary.Reflection.System;

public static class AssemblyExt
{
	#region fields
	private static readonly ConcurrentDictionary<Assembly, Type[]> _developerTypesByAssembly = [];
	#endregion

	#region methods
	public static bool IsDeveloper(this Assembly assembly) =>
		assembly.FullName is not null &&
		assembly.IsMicrosoft() == false &&
		assembly.IsTest() == false;

	public static bool IsTest(this Assembly assembly)
	{
		var fullName = assembly.FullName;
		if (string.IsNullOrWhiteSpace(fullName)) return false;
		return
			fullName.startsWith("testhost") ||
			fullName.startsWith("xunit.") ||
			fullName.startsWith("nunit.");
	}

	public static bool IsMicrosoft(this Assembly assembly)
	{
		var fullName = assembly.FullName;
		if (string.IsNullOrWhiteSpace(fullName)) return false;
		return
			fullName.StartsWith("System.") ||
			fullName.StartsWith("Microsoft.") ||
			fullName.StartsWith("netstandard") ||
			fullName.StartsWith("mscorlib") ||
			fullName.StartsWith("PresentationFramework") ||
			fullName.StartsWith("WindowsBase") ||
			fullName.StartsWith("PresentationCore") ||
			fullName.Contains("PublicKeyToken=b77a5c561934e089");
	}

	public static IEnumerable<Type> GetImplementingTypes<T>() =>
		Assembly.GetCallingAssembly().GetImplementingTypes(typeof(T));

	public static IEnumerable<Type> GetImplementingTypes(Type type) =>
		Assembly.GetCallingAssembly().GetImplementingTypes(type);

	public static IEnumerable<Type> GetImplementingTypes<T>(this Assembly assembly) =>
		assembly.GetImplementingTypes(typeof(T));

	public static IEnumerable<Type> GetImplementingTypes(this Assembly assembly, Type type) =>
		assembly.GetDeveloperTypes().Where(x => x.Implements(type));

	/// <summary>
	/// Returns all the types not created by the compiler.
	/// </summary>
	public static IEnumerable<Type> GetDeveloperTypes() =>
		Assembly.GetCallingAssembly().GetDeveloperTypes();

	/// <summary>
	/// Returns all the types not created by the compiler.
	/// </summary>
	public static IEnumerable<Type> GetDeveloperTypes(this Assembly assembly) =>
		_developerTypesByAssembly.GetOrAdd(
			assembly,
			assembly => assembly.GetTypes()
				.Where(TypeExt.IsDeveloper)
				.ToArray()
		);
	#endregion

	#region private methods
	private static bool startsWith(this string str, string start) =>
		str.StartsWith(start, StringComparison.InvariantCultureIgnoreCase);
	#endregion
}
