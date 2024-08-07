namespace TylerDM.StandardLibrary.Reflection.System;

[Obsolete]
public static class TypeFinder
{
	private static readonly IReadOnlyCollection<Type> _concreteTypes;

	static TypeFinder()
	{
		_concreteTypes = AppDomain.CurrentDomain.GetAssemblies()
			.SelectMany(x => x.GetTypes())
			.Where(x => x.IsClass && !x.IsAbstract)
			.ToList();
	}

	public static IEnumerable<Type> WithAttribute<TAttribute>(Func<Type, TAttribute, bool> filter)
		where TAttribute : Attribute =>
		from x in _concreteTypes
		let y = x.GetCustomAttribute<TAttribute>()
		where
			y is not null &&
			filter(x, y)
		select x;

	public static IEnumerable<Type> WithAttribute(Type attributeType)
	{
		if (attributeType.IsAssignableTo(typeof(Attribute)) is false)
			throw new ArgumentOutOfRangeException(nameof(attributeType));

		return _concreteTypes.Where(x => x.GetCustomAttributes(attributeType, true).Length > 0);
	}

	public static IEnumerable<Type> Implementing(Type type) =>
		_concreteTypes.Where(x => isAssignableFromSmart(x, type));

	public static bool IsAssignableToGenericType(this Type type, Type genericType)
	{
		var implementsInterface = type.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == genericType);
		if (implementsInterface) return true;

		if (type.IsGenericType && type.GetGenericTypeDefinition() == genericType)
			return true;

		var baseType = type.BaseType;
		if (baseType is null) return false;

		return baseType.IsAssignableToGenericType(genericType);
	}

	private static bool isAssignableFromSmart(Type t1, Type t2)
	{
		while (t1.BaseType is not null)
		{
			if (
				t2.IsGenericType && t1.IsAssignableToGenericType(t2) ||
				t2.IsAssignableFrom(t1)
			)
				return true;

			t1 = t1.BaseType;
		}
		return false;
	}
}
