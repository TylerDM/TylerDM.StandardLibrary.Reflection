namespace TylerDM.StandardLibrary.Reflection.System;

public static class AssemblyExt
{
	#region fields
	private static readonly ConcurrentDictionary<Assembly, Type[]> _cache = [];
	#endregion

	#region methods
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
		_cache.GetOrAdd(
			assembly,
			assembly => assembly.GetTypes()
				.Where(x => x.IsAnonymous() is false && x.IsCompilerGenerated() is false)
				.ToArray()
		);
	#endregion
}
