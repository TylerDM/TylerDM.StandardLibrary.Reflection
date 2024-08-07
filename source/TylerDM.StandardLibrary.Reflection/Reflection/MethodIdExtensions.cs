namespace TylerDM.StandardLibrary.Reflection.Reflection;

public static class MethodIdExtensions
{
	public static MethodInfo GetMethodById(this Type type, string id) =>
		type.GetMethods().First(x => x.Name == id);
}
