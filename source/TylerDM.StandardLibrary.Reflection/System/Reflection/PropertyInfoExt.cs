namespace TylerDM.StandardLibrary.Reflection.System.Reflection;

public static class PropertyInfoExt
{
	public static TValue GetValue<TValue>(this PropertyInfo propertyInfo, object? obj) =>
			(TValue)propertyInfo.GetValue(obj)!;
}
