namespace TylerDM.StandardLibrary.Reflection.Linq.Expressions;

public static class ExpressionExt
{
	#region methods
	public static PropertyInfo ToPropertyInfo<T1, T2>(this Expression<Func<T1, T2>> property)
	{
		if (
			property.Body is UnaryExpression unaryExp &&
			unaryExp.Operand is MemberExpression memberExp
		)
			return (PropertyInfo)memberExp.Member;

		if (property.Body is MemberExpression memberExp2)
			return (PropertyInfo)memberExp2.Member;

		throw new ArgumentException($"The expression does not point to a property.");
	}

	public static TProperty GetValue<TValue, TProperty>(this TValue value, Expression<Func<TValue, TProperty>> expression) =>
				expression.Compile().Invoke(value);

	public static string GetMemberName<TValue, TProperty>(this Expression<Func<TValue, TProperty>> expression)
	{
		if (expression.Body is not MemberExpression memberExpression)
			throw new InvalidOperationException($"{nameof(Expression)} must be a {nameof(MemberExpression)}.");

		return memberExpression.Member.Name;
	}

	public static PropertyInfo GetPropertyInfo<TValue, TProperty>(this Expression<Func<TValue, TProperty>> expression)
	{
		if (expression.Body is not MemberExpression memberExpression)
			throw new InvalidOperationException($"{nameof(Expression)}.{nameof(expression.Body)} is not a {nameof(MemberExpression)}.");

		if (memberExpression.Member is not PropertyInfo propertyInfo)
			throw new InvalidOperationException($"{nameof(Expression)}.{nameof(memberExpression.Member)} is not a {nameof(PropertyInfo)}.");

		//I tried `return propertyInfo` here.  But this didn't work for interfaces.
		//This hack works, but I suspect not for private interface implementations.
		var tValueType = expression.Parameters[0].Type;
		return tValueType.GetProperty(propertyInfo.Name) ?? throw new Exception("Could not find matching property by name");
	}

	public static TAttribute GetAttribute<TValue, TProperty, TAttribute>(this Expression<Func<TValue, TProperty>>
expression)
			where TAttribute : Attribute =>
			expression.GetAttributeOptional<TValue, TProperty, TAttribute>() ??
			throw new Exception("Attribute not found on member.");

	public static TAttribute? GetAttributeOptional<TValue, TProperty, TAttribute>(this Expression<Func<TValue, TProperty>>
expression)
			where TAttribute : Attribute
	{
		if (expression.Body is not MemberExpression memberExpression)
			throw new InvalidOperationException($"{nameof(Expression)} must be a {nameof(MemberExpression)}.");

		return memberExpression.Member.getAttribute<TAttribute>();
	}
	#endregion

	#region private methods
	private static T? getAttribute<T>(this ICustomAttributeProvider provider)
					where T : Attribute
	{
		var attributes = provider.GetCustomAttributes(typeof(T), true);
		return attributes.Length > 0 ? attributes[0] as T : null;
	}
	#endregion
}