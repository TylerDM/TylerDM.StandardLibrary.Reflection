namespace TylerDM.StandardLibrary.Reflection.System;

public static class AppDomainExt
{
	#region fields
	private static ConcurrentDictionary<AppDomain, Assembly[]> _assemblies = [];
	#endregion

	#region methods
	public static IEnumerable<Type> GetImplementingTypes<T>(Func<Type, bool>? where = null) =>
	AppDomain.CurrentDomain.GetImplementingTypes(typeof(T), where);

	public static IEnumerable<Type> GetImplementingTypes(Type type, Func<Type, bool>? where = null) =>
		AppDomain.CurrentDomain.GetImplementingTypes(type, where);

	public static IEnumerable<Type> GetImplementingTypes<T>(this AppDomain appDomain, Func<Type, bool>? where = null) =>
		appDomain.GetImplementingTypes(typeof(T), where);

	public static IEnumerable<Type> GetImplementingTypes(this AppDomain appDomain, Type type, Func<Type, bool>? where = null) =>
		appDomain.GetDeveloperTypes(where).Where(x => x.Implements(type));

	/// <summary>
	/// Returns all the types not created by the compiler.
	/// </summary>
	public static IEnumerable<Type> GetDeveloperTypes(Func<Type, bool>? where = null) =>
		AppDomain.CurrentDomain.GetDeveloperTypes(where);

	/// <summary>
	/// Returns all the types not created by the compiler.
	/// </summary>
	public static IEnumerable<Type> GetDeveloperTypes(this AppDomain appDomain, Func<Type, bool>? where = null) =>
		appDomain.getAssemblies().SelectMany(x => x.GetTypes());

	public static bool IsMicrosoft(this Assembly assembly)
	{
		if (string.IsNullOrWhiteSpace(assembly.FullName)) return false;
		return
			assembly.FullName.StartsWith("System.") ||
			assembly.FullName.StartsWith("Microsoft.") ||
			assembly.FullName.StartsWith("netstandard") ||
			assembly.FullName.StartsWith("mscorlib") ||
			assembly.FullName.StartsWith("PresentationFramework") ||
			assembly.FullName.StartsWith("WindowsBase") ||
			assembly.FullName.StartsWith("PresentationCore") ||
			assembly.FullName.Contains("PublicKeyToken=b77a5c561934e089");
	}

	public static void ClearAssemblyCache() =>
		_assemblies = [];
	#endregion

	#region private methods
	private static Assembly[] getAssemblies(this AppDomain appDomain) =>
		_assemblies.GetOrAdd(appDomain, getAssembliesUncached);

	private static Assembly[] getAssembliesUncached(AppDomain appDomain) =>
		appDomain.GetAssemblies()
			.Where(x =>
				string.IsNullOrWhiteSpace(x.FullName) == false &&
				x.IsMicrosoft() == false
			)
			.ToArray();
	#endregion
}
