namespace TylerDM.StandardLibrary.Reflection.System.Reflection;

public static class SymbolIdAttributeExt
{
	public static Guid? GetSymbolId(this MemberInfo memberInfo) =>
		memberInfo.GetCustomAttribute<SymbolIdAttribute>()?.Id;

	public static MemberInfo GetMember(this Type type, Guid id) =>
		type.GetMembers()
			.FirstOrDefault(x => x.GetSymbolId() == id) ??
			throw new Exception($"No member with specified ID found.");

	public static TMember Get<TMember>(this Type type, Guid id)
		where TMember : MemberInfo =>
		type.GetMembers()
			.OfType<TMember>()
			.FirstOrDefault(x => x.GetSymbolId() == id) ??
			throw new Exception($"No {typeof(TMember).Name} with specified ID found.");
}
