namespace TylerDM.StandardLibrary.Reflection.System;

public static class TypeExt
{
	#region const
	private const string _methodNotFoundMessage = "No method matches the supplied constraints.";
	#endregion

	#region methods
	public static bool Implements<TInterface>(this Type type) =>
		Implements(type, typeof(TInterface));

	public static bool Implements(this Type implementationType, Type type) =>
		type switch
		{
			{ IsGenericType: false, IsInterface: true } => implementsInterface(implementationType, type),
			{ IsGenericType: true, IsInterface: true } => implementsGenericInterface(implementationType, type),
			{ IsGenericType: false, IsInterface: false } => inheritsType(implementationType, type),
			{ IsGenericType: true, IsInterface: false } => inheritsGenericType(implementationType, type)
		};

	public static bool IsAnonymous(this Type type) =>
		type.HasAttribute(typeof(CompilerGeneratedAttribute)) &&
		type.FullName!.Contains("AnonymousType");

	public static bool IsCompilerGenerated(this Type type) =>
		type.HasAttribute(typeof(CompilerGeneratedAttribute));

	public static bool HasAttribute(this Type type, Type attributeType, bool inherit = false) =>
		type.GetCustomAttributes(attributeType, inherit).Any();

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
	#endregion

	#region private methods
	private static bool inheritsType(this Type implementationType, Type baseType) =>
		implementationType.SelectFollow(x => x.BaseType)
			.Skip(1)//Don't match the class itself
			.Any(x => x == baseType);

	private static bool inheritsGenericType(this Type implementationType, Type baseType) =>
		implementationType.SelectFollow(x => x.BaseType)
			.Skip(1)//Don't match the class itself
			.Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == baseType);

	private static bool implementsInterface(this Type implementationType, Type interfaceType) =>
		implementationType.GetInterfaces().Any(x => x == interfaceType);

	private static bool implementsGenericInterface(this Type implementationType, Type genericInterfaceType) =>
		implementationType.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == genericInterfaceType);
	#endregion
}
