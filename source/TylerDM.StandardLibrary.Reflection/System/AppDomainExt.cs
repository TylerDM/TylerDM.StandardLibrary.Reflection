namespace TylerDM.StandardLibrary.Reflection.System;

public static class AppDomainExt
{
	#region fields
	private static ConcurrentDictionary<AppDomain, Assembly[]> _developerAssemblies = [];
	#endregion

	#region methods
	public static IEnumerable<Type> GetImplementingTypes<T>() =>
		AppDomain.CurrentDomain.GetImplementingTypes(typeof(T));

	public static IEnumerable<Type> GetImplementingTypes(Type type) =>
		AppDomain.CurrentDomain.GetImplementingTypes(type);

	public static IEnumerable<Type> GetImplementingTypes<T>(this AppDomain appDomain) =>
		appDomain.GetImplementingTypes(typeof(T));

	public static IEnumerable<Type> GetImplementingTypes(this AppDomain appDomain, Type type) =>
		appDomain.GetDeveloperTypes().Where(x => x.Implements(type));

	public static IEnumerable<Assembly> GetDeveloperAssemblies() =>
		AppDomain.CurrentDomain.GetDeveloperAssemblies();

	public static IEnumerable<Assembly> GetDeveloperAssemblies(this AppDomain appDomain) =>
		_developerAssemblies.GetOrAdd(appDomain, getDeveloperAssembliesUncached);

	/// <summary>
	/// Returns all the types not created by the compiler.
	/// </summary>
	public static IEnumerable<Type> GetDeveloperTypes() =>
		AppDomain.CurrentDomain.GetDeveloperTypes();

	/// <summary>
	/// Returns all the types not created by the compiler.
	/// </summary>
	public static IEnumerable<Type> GetDeveloperTypes(this AppDomain appDomain) =>
		appDomain.GetDeveloperAssemblies().SelectMany(x => x.GetTypes());

	public static void ClearAssemblyCache() =>
		_developerAssemblies.Clear();
	#endregion

	#region private methods
	private static Assembly[] getDeveloperAssembliesUncached(AppDomain appDomain) =>
		appDomain.GetDeveloperAssemblies()
			.Where(AssemblyExt.IsDeveloper)
			.ToArray();
	#endregion
}
