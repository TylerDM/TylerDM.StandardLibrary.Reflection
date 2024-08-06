namespace TylerDM.StandardLibrary.Reflection;

public static class TypeExt
{
	public static bool Implements<TInterface>(this Type type)
		where TInterface : class
	{
		var interfaceType = typeof(TInterface);
		if (interfaceType.IsInterface is false)
			throw new ArgumentException($"{nameof(TInterface)} must be an interface.", nameof(TInterface));

		return interfaceType.IsAssignableFrom(type);
	}
}
