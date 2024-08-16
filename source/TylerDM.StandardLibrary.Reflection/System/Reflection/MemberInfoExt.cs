namespace TylerDM.StandardLibrary.Reflection.System.Reflection;

public static class MemberInfoExt
{
	public static IEnumerable<MemberInfo> GetDeveloperMembers() =>
		GetDeveloperMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

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
