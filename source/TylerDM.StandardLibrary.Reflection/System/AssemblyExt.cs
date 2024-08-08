namespace TylerDM.StandardLibrary.Reflection.System;

public static class AssemblyExt
{
	#region fields
	private static readonly ConcurrentDictionary<Assembly, Type[]> _cache = [];
	#endregion

	#region methods
	public static IEnumerable<Type> GetImplementingTypes<T>(Func<Type, bool>? where = null) =>
		Assembly.GetCallingAssembly().GetImplementingTypes(typeof(T), where);

	public static IEnumerable<Type> GetImplementingTypes(Type type, Func<Type, bool>? where = null) =>
		Assembly.GetCallingAssembly().GetImplementingTypes(type, where);

	public static IEnumerable<Type> GetImplementingTypes<T>(this Assembly assembly, Func<Type, bool>? where = null) =>
		assembly.GetImplementingTypes(typeof(T), where);

	public static IEnumerable<Type> GetImplementingTypes(this Assembly assembly, Type type, Func<Type, bool>? where = null) =>
		assembly.GetDeveloperTypes(where).Where(x => x.Implements(type));

	/// <summary>
	/// Returns all the types not created by the compiler.
	/// </summary>
	public static IEnumerable<Type> GetDeveloperTypes(Func<Type, bool>? where = null) =>
		Assembly.GetCallingAssembly().GetDeveloperTypes(where);

	/// <summary>
	/// Returns all the types not created by the compiler.
	/// </summary>
	public static IEnumerable<Type> GetDeveloperTypes(this Assembly assembly, Func<Type, bool>? where = null)
	{
		var value = _cache.GetOrAdd(
			assembly,
			assembly => assembly.GetTypes()
				.Where(x => x.IsAnonymous() is false && x.IsCompilerGenerated() is false)
				.ToArray()
		);
		if (where is null) return value;

		return value.Where(where);
	}
	#endregion
}
