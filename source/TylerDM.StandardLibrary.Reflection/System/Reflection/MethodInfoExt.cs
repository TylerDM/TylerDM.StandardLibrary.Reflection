namespace TylerDM.StandardLibrary.Reflection.System.Reflection;

public static class MethodInfoExt
{
	public static async Task<object?> ObjectiveInvokeAsync(this MethodInfo method, object instance, params object[] args)
	{
		var task = method.Invoke(instance, args);
		await (Task)task!;
		return task.GetType()!.GetProperty("Result")!.GetValue(task, null);
	}
}
