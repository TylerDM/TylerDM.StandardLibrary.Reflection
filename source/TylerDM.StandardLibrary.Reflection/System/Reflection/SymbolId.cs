namespace TylerDM.StandardLibrary.Reflection.System.Reflection;

public static class SymbolId
{
	private static readonly ConcurrentDictionary<Guid, MemberInfo> _cache = new();

	public static MemberInfo GetMember(string id) =>
		GetMember(Guid.Parse(id));

	public static MemberInfo GetMember(Guid id) =>
		_cache.GetOrAdd(id, id =>
			MemberInfoExt.GetDeveloperMembers()
				.FirstOrDefault(x => x.GetSymbolId() == id) ??
				throw new Exception($"No member with specified ID found.")
		);

	public static TMember Get<TMember>(string id)
		where TMember : MemberInfo =>
		Get<TMember>(Guid.Parse(id));

	public static TMember Get<TMember>(Guid id)
		where TMember : MemberInfo =>
		(TMember)GetMember(id);
}
