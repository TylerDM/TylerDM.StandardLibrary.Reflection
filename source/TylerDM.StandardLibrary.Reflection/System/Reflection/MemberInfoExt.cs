namespace TylerDM.StandardLibrary.Reflection.System.Reflection;

public static class MemberInfoExt
{
	public static bool HasAttribute(this MemberInfo memberInfo, Type attributeType, bool inherit = false) =>
		memberInfo.GetCustomAttribute(attributeType, inherit) is not null;

	public static bool IsStatic(this MemberInfo memberInfo) =>
		memberInfo switch
		{
			PropertyInfo propertyInfo => propertyInfo.IsStatic(),
			MethodInfo methodInfo => methodInfo.IsStatic,
			FieldInfo fieldInfo => fieldInfo.IsStatic,

			_ => throw new ArgumentOutOfRangeException(nameof(memberInfo))
		};

	public static bool IsInstance(this MemberInfo memberInfo) =>
		memberInfo.IsStatic() == false;

	public static IEnumerable<MemberInfo> GetDeveloperMembers() =>
		GetDeveloperMembers(
			BindingFlags.Instance |
			BindingFlags.Static |
			BindingFlags.Public |
			BindingFlags.NonPublic
		);

	public static IEnumerable<MemberInfo> GetDeveloperMembers(BindingFlags bindingFlags)
	{
		foreach (var type in AppDomainExt.GetDeveloperTypes())
		{
			yield return type;
			foreach (var member in type.GetMembers(bindingFlags))
				yield return member;
		}
	}

	public static TAttribute GetAttributeRequired<TAttribute>(this MemberInfo method)
		where TAttribute : Attribute =>
		method.GetCustomAttribute<TAttribute>() ??
		throw new Exception("Specified attribute not found on property.");
}
