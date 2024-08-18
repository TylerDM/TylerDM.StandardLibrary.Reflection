namespace TylerDM.StandardLibrary.Reflection.System.Reflection;

public static class StaticInterfaceExt
{
	public static TValue GetStaticInterfaceProperty<TType, TStaticInterface, TValue>(string name) =>
		typeof(TValue).GetStaticInterfaceProperty<TStaticInterface, TValue>(name);

	public static TValue GetStaticInterfaceProperty<TStaticInterface, TValue>(this Type type, string name)
	{
		if (type.Implements<TStaticInterface>() == false)
			throw new Exception("Specified type does not implement specific static interface.");

		return type
			.Get<PropertyInfo>(name, x => x.IsStatic())
			.StaticGetValue<TValue>();
	}

	public static void SetStaticInterfaceProperty<TType, TStaticInterface, TValue>(string name, TValue value) =>
		typeof(TValue).SetStaticInterfaceProperty<TStaticInterface, TValue>(name, value);

	public static void SetStaticInterfaceProperty<TStaticInterface, TValue>(this Type type, string name, TValue value)
	{
		if (type.Implements<TStaticInterface>() == false)
			throw new Exception("Specified type does not implement specific static interface.");

		type
			.Get<PropertyInfo>(name, x => x.IsStatic())
			.StaticSetValue(value);
	}
}
