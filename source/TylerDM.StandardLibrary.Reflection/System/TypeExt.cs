using System.Runtime.CompilerServices;

namespace TylerDM.StandardLibrary.Reflection.System;

public static class TypeExt
{
	private const string _methodNotFoundMessage = "No method matches the supplied constraints.";

	public static bool IsAnonymous(this Type type) =>
		type.GetCustomAttributes(typeof(CompilerGeneratedAttribute), false).Any() &&
		(type.FullName?.Contains("AnonymousType") ?? false);

	public static MethodInfo GetMethodRequired(this Type type, string name) =>
		type.GetMethod(name) ?? throw new Exception(_methodNotFoundMessage);

	public static MethodInfo GetMethodRequired(this Type type, string name, BindingFlags bindingFlags) =>
		type.GetMethod(name, bindingFlags) ?? throw new Exception(_methodNotFoundMessage);

	public static MethodInfo GetMethodRequired(this Type type, string name, params Type[] types) =>
		type.GetMethod(name, types) ?? throw new Exception(_methodNotFoundMessage);

	public static MethodInfo GetMethodRequired(this Type type, string name, BindingFlags bindingFlags, params Type[] types) =>
		type.GetMethod(name, bindingFlags, types) ?? throw new Exception(_methodNotFoundMessage);

	public static bool GetIsNullableEnum(this Type t)
	{
		var underlyingType = Nullable.GetUnderlyingType(t);
		return underlyingType is not null and { IsEnum: true };
	}
	public static bool Implements<TInterface>(this Type type)
				where TInterface : class
	{
		var interfaceType = typeof(TInterface);
		if (interfaceType.IsInterface is false)
			throw new ArgumentException($"{nameof(TInterface)} must be an interface.", nameof(TInterface));

		return interfaceType.IsAssignableFrom(type);
	}
}
