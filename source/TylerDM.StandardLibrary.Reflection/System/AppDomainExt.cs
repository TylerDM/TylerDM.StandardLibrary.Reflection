namespace TylerDM.StandardLibrary.Reflection.System;

public static class AppDomainExt
{
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
		appDomain.GetAssemblies().SelectMany(x => x.GetDeveloperTypes(where));
}
