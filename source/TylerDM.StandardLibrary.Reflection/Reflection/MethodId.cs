namespace TylerDM.StandardLibrary.Reflection.Reflection;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class MethodId : Attribute
{
	public string Id { get; }

	public MethodId(string id)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(id, nameof(id));

		Id = id;
	}
}
