namespace TylerDM.StandardLibrary.Reflection.System.Reflection;

public static class MemberInfoExt
{
	public static IEnumerable<MemberInfo> GetDeveloperMembers()
	{
		foreach (var type in AppDomainExt.GetDeveloperTypes())
		{
			yield return type;
			foreach (var member in type.GetMembers())
				yield return member;
		}
	}

	public static TAttribute GetAttributeRequired<TAttribute>(this MemberInfo method)
		where TAttribute : Attribute =>
		method.GetCustomAttribute<TAttribute>() ??
		throw new Exception("Specified attribute not found on property.");
}
