namespace TylerDM.StandardLibrary.Reflection.System.Reflection;

public static class PropertyInfoExt
{
	public static bool IsStatic(this PropertyInfo propertyInfo) =>
		(propertyInfo.GetGetMethod() ?? propertyInfo.GetSetMethod())!.IsStatic;

	public static bool IsInstance(this PropertyInfo propertyInfo) =>
		propertyInfo.IsStatic() == false;

	public static TValue GetValue<TValue>(this PropertyInfo propertyInfo, object? obj) =>
		(TValue)propertyInfo.GetValue(obj)!;

	public static TValue StaticGetValue<TValue>(this PropertyInfo propertyInfo) =>
		(TValue)propertyInfo.GetValue(null)!;

	public static void StaticSetValue(this PropertyInfo propertyInfo, object? value) =>
		propertyInfo.SetValue(null, value);
}
