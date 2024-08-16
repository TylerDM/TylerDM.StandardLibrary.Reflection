namespace TylerDM.StandardLibrary.Reflection.System.Reflection;

public static class MethodInfoExt
{
	public static async Task<object?> GetValueAsync(this MethodInfo method, object instance, params object[] args)
	{
		var task = method.Invoke(instance, args);
		await (Task)task!;
		return task.GetType()!.GetProperty(nameof(Task<object>.Result))!.GetValue(task, null);
	}

	public static async Task<TValue?> GetValueAsync<TValue>(this MethodInfo method, object instance, params object[] args)
	{
		var task = method.Invoke(instance, args);
		await (Task)task!;
		return (TValue?)task.GetType()!.GetProperty(nameof(Task<object>.Result))!.GetValue(task, null);
	}
}
