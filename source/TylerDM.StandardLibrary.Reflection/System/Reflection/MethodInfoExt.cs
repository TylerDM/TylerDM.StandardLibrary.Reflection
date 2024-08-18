namespace TylerDM.StandardLibrary.Reflection.System.Reflection;

public static class MethodInfoExt
{
	public static void StaticInvoke(this MethodInfo method, params object?[]? parameters) =>
		method.Invoke(null, parameters);

	public static TValue Invoke<TValue>(this MethodInfo method, object instance, params object?[]? parameters) =>
		(TValue)method.Invoke(instance, parameters)!;

	public static TValue StaticInvoke<TValue>(this MethodInfo method, params object?[]? parameters) =>
		(TValue)method.Invoke(null, parameters)!;

	public static async Task<object?> InvokeAsync(this MethodInfo method, object instance, params object?[] args)
	{
		var task = method.Invoke(instance, args);
		await (Task)task!;
		return task.GetType()!.GetProperty(nameof(Task<object>.Result))!.GetValue(task, null);
	}

	public static async Task<object?> StaticInvokeAsync(this MethodInfo method, params object?[] args)
	{
		var task = method.Invoke(null, args);
		await (Task)task!;
		return task.GetType()!.GetProperty(nameof(Task<object>.Result))!.GetValue(task, null);
	}

	public static async Task<TValue?> StaticInvokeAsync<TValue>(this MethodInfo method, params object?[] args)
	{
		var task = method.Invoke(null, args);
		await (Task)task!;
		return (TValue?)task.GetType()!.GetProperty(nameof(Task<object>.Result))!.GetValue(task, null);
	}

	public static async Task<TValue?> InvokeAsync<TValue>(this MethodInfo method, object instance, params object?[] args)
	{
		var task = method.Invoke(instance, args);
		await (Task)task!;
		return (TValue?)task.GetType()!.GetProperty(nameof(Task<object>.Result))!.GetValue(task, null);
	}
}
