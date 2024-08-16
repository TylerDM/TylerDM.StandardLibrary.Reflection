namespace TylerDM.StandardLibrary.Reflection.System.Reflection;

public static class MethodInfoExt
{
	public static async Task<object?> InvokeAsync(this MethodInfo method, object instance, params object[] args)
	{
		var task = method.Invoke(instance, args);
		await (Task)task!;
		return task.GetType()!.GetProperty(nameof(Task<object>.Result))!.GetValue(task, null);
	}

	public static async Task<TValue?> InvokeAsync<TValue>(this MethodInfo method, object instance, params object[] args)
	{
		var task = method.Invoke(instance, args);
		await (Task)task!;
		return (TValue?)task.GetType()!.GetProperty(nameof(Task<object>.Result))!.GetValue(task, null);
	}
}
