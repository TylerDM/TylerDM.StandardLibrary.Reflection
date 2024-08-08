namespace TylerDM.StandardLibrary.Reflection.System;

public static class IEnumerableTypeExt
{
	public static IEnumerable<(Type Type, TAttribute Attribute)> WithAttribute<TAttribute>(this IEnumerable<Type> enumerable)
		where TAttribute : Attribute =>
		from type in enumerable
		let attribute = type.GetCustomAttribute<TAttribute>()
		where attribute is not null
		select (type, attribute);
}
