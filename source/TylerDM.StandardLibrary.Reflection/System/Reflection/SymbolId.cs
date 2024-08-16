namespace TylerDM.StandardLibrary.Reflection.System.Reflection;

public static class SymbolId
{
	public static MemberInfo GetMember(Guid id) =>
		MemberInfoExt.GetDeveloperMembers()
			.FirstOrDefault(x => x.GetSymbolId() == id) ??
			throw new Exception($"No member with specified ID found.");

	public static TMember Get<TMember>(Guid id)
		where TMember : MemberInfo =>
		MemberInfoExt.GetDeveloperMembers()
			.OfType<TMember>()
			.FirstOrDefault(x => x.GetSymbolId() == id) ??
			throw new Exception($"No {typeof(TMember).Name} with specified ID found.");
}
