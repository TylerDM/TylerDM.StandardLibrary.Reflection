namespace TylerDM.StandardLibrary.Reflection.System.Reflection;

[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
public class SymbolIdAttribute(string id) : Attribute
{
	public Guid Id { get; } = Guid.Parse(id);
}
