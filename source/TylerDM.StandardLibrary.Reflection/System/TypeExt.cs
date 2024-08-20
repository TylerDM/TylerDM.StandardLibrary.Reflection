namespace TylerDM.StandardLibrary.Reflection.System;

public static class TypeExt
{
	#region methods
	public static MemberInfo[] GetAllMembers(this Type type) =>
		type.GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

	public static MemberInfo GetMember(this Type type, Func<MemberInfo, bool> predicate) =>
		type.GetMembers().Single(predicate);

	public static TMemberInfo Get<TMemberInfo>(this Type type, Func<TMemberInfo, bool> predicate)
		where TMemberInfo : MemberInfo =>
		type.GetAllMembers()
			.OfType<TMemberInfo>()
			.Single(predicate);

	public static TMemberInfo GetStatic<TMemberInfo>(this Type type, Func<TMemberInfo, bool> predicate)
		where TMemberInfo : MemberInfo =>
		type.GetAllMembers()
			.OfType<TMemberInfo>()
			.Where(x => x.IsStatic())
			.Single(predicate);

	public static TMemberInfo GetStatic<TMemberInfo>(this Type type, string name, Func<TMemberInfo, bool>? predicate = null)
		where TMemberInfo : MemberInfo
	{
		var query = type.GetAllMembers()
			.OfType<TMemberInfo>()
			.Where(x => x.Name == name && x.IsStatic());
		if (predicate is not null)
			query = query.Where(predicate);
		return query.Single();
	}

	public static TMemberInfo Get<TMemberInfo>(this Type type, string name, Func<TMemberInfo, bool>? predicate = null)
		where TMemberInfo : MemberInfo
	{
		var query = type.GetAllMembers()
				.OfType<TMemberInfo>()
				.Where(x => x.Name == name);
		if (predicate is not null)
			query = query.Where(predicate);
		return query.Single();
	}

	public static bool Implements<TInterface>(this Type type) =>
		Implements(type, typeof(TInterface));

	public static bool Implements(this Type implementationType, Type type) =>
		type switch
		{
			{ ContainsGenericParameters: false, IsInterface: true } => implementsInterface(implementationType, type),
			{ ContainsGenericParameters: true, IsInterface: true } => implementsOpenGenericInterface(implementationType, type),
			{ ContainsGenericParameters: false, IsInterface: false } => inheritsType(implementationType, type),
			{ ContainsGenericParameters: true, IsInterface: false } => inheritsOpenGenericType(implementationType, type)
		};

	public static bool IsDeveloper(this Type type) =>
		type.IsAnonymous() == false &&
		type.IsCompilerGenerated() == false &&
		type.Namespace is not null;

	public static bool IsMicrosoft(this Type type) =>
		type.Namespace?.StartsWith("Microsoft.") ?? false;

	public static bool IsAnonymous(this Type type) =>
		type.IsCompilerGenerated() &&
		type.FullName!.Contains("AnonymousType");

	public static bool IsCompilerGenerated(this Type type) =>
		type.HasAttribute(typeof(CompilerGeneratedAttribute));

	public static bool HasAttribute<TAttribute>(this Type type, bool inherit = false)
		where TAttribute : Attribute =>
		type.HasAttribute(typeof(TAttribute), inherit);

	public static bool HasAttribute(this Type type, Type attributeType, bool inherit = false) =>
		type.GetCustomAttribute(attributeType, inherit) is not null;

	public static bool GetIsNullableEnum(this Type t)
	{
		var underlyingType = Nullable.GetUnderlyingType(t);
		return underlyingType is not null and { IsEnum: true };
	}
	#endregion

	#region private methods
	private static bool inheritsType(this Type implementationType, Type baseType) =>
		implementationType
			.SelectFollow(x => x.BaseType, false)
			.Any(x => x == baseType);

	private static bool inheritsOpenGenericType(this Type implementationType, Type baseType) =>
		implementationType
			.SelectFollow(x => x.BaseType, false)
			.Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == baseType);

	private static bool implementsInterface(this Type implementationType, Type interfaceType) =>
		implementationType.GetInterfaces().Any(x => x == interfaceType);

	private static bool implementsOpenGenericInterface(this Type implementationType, Type genericInterfaceType) =>
		implementationType.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == genericInterfaceType);
	#endregion
}
