namespace TylerDM.StandardLibrary.Reflection;

public static class TypeBuilderExt
{
	#region const
	private static readonly MethodAttributes _accessorMethodAttributes =
		MethodAttributes.Public |
		MethodAttributes.SpecialName |
		MethodAttributes.HideBySig;
	#endregion

	#region methods
	public static PropertyBuilder CreateProperty(this TypeBuilder typeBuilder, string propertyName, Type propertyType)
	{
		if (string.IsNullOrWhiteSpace(propertyName)) throw new ArgumentNullOrWhitespaceException(nameof(propertyName));

		var fieldBuilder = typeBuilder.DefineField("_" + propertyName, propertyType, FieldAttributes.Private);
		var propertyBuilder = typeBuilder.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);

		var getMethod = typeBuilder.DefineMethod("get_" + propertyName, _accessorMethodAttributes, propertyType, Type.EmptyTypes);
		var getMethodIL = getMethod.GetILGenerator();
		getMethodIL.Emit(OpCodes.Ldarg_0);
		getMethodIL.Emit(OpCodes.Ldfld, fieldBuilder);
		getMethodIL.Emit(OpCodes.Ret);

		var setMethod = typeBuilder.DefineMethod("set_" + propertyName, _accessorMethodAttributes, null, [propertyType]);
		var setMethodIL = setMethod.GetILGenerator();
		var modifyProperty = setMethodIL.DefineLabel();
		var exitSet = setMethodIL.DefineLabel();

		setMethodIL.MarkLabel(modifyProperty);
		setMethodIL.Emit(OpCodes.Ldarg_0);
		setMethodIL.Emit(OpCodes.Ldarg_1);
		setMethodIL.Emit(OpCodes.Stfld, fieldBuilder);
		setMethodIL.Emit(OpCodes.Nop);
		setMethodIL.MarkLabel(exitSet);
		setMethodIL.Emit(OpCodes.Ret);

		propertyBuilder.SetGetMethod(getMethod);
		propertyBuilder.SetSetMethod(setMethod);

		return propertyBuilder;
	}

	public static Type CreateTypeRequired(this TypeBuilder typeBuilder) =>
		typeBuilder.CreateType() ?? throw new Exception("Create type failed.");

	public static void AddPassthroughConstructor(this TypeBuilder typeBuilder, Type baseType)
	{
		foreach (var constructor in baseType.GetConstructors())
		{
			var parameters = constructor.GetParameters();
			if (parameters.Length > 0 && parameters.Last().IsDefined(typeof(ParamArrayAttribute), false))
				continue;

			var parameterTypes = parameters.Select(p => p.ParameterType).ToArray();
			var requiredCustomModifiers = parameters.Select(p => p.GetRequiredCustomModifiers()).ToArray();
			var optionalCustomModifiers = parameters.Select(p => p.GetOptionalCustomModifiers()).ToArray();

			var constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, constructor.CallingConvention, parameterTypes, requiredCustomModifiers, optionalCustomModifiers);
			for (var i = 0; i < parameters.Length; ++i)
			{
				var parameter = parameters[i];
				var parameterBuilder = constructorBuilder.DefineParameter(i + 1, parameter.Attributes, parameter.Name);
				if (((int)parameter.Attributes & (int)ParameterAttributes.HasDefault) != 0)
					parameterBuilder.SetConstant(parameter.RawDefaultValue);

				foreach (var attribute in buildCustomAttributes(parameter.GetCustomAttributesData()))
					parameterBuilder.SetCustomAttribute(attribute);
			}

			foreach (var attribute in buildCustomAttributes(constructor.GetCustomAttributesData()))
				constructorBuilder.SetCustomAttribute(attribute);

			var emitter = constructorBuilder.GetILGenerator();
			emitter.Emit(OpCodes.Nop);

			emitter.Emit(OpCodes.Ldarg_0);
			for (var i = 1; i <= parameters.Length; ++i)
				emitter.Emit(OpCodes.Ldarg, i);
			emitter.Emit(OpCodes.Call, constructor);

			emitter.Emit(OpCodes.Ret);
		}
	}
	#endregion

	#region private methods
	private static CustomAttributeBuilder[] buildCustomAttributes(IEnumerable<CustomAttributeData> customAttributes)
	{
		return customAttributes.Select(attribute =>
		{
			var attributeArgs = attribute.ConstructorArguments.Select(a => a.Value).ToArray();
			var namedPropertyInfos = attribute.NamedArguments.Select(a => a.MemberInfo).OfType<PropertyInfo>().ToArray();
			var namedPropertyValues = attribute.NamedArguments.Where(a => a.MemberInfo is PropertyInfo).Select(a => a.TypedValue.Value).ToArray();
			var namedFieldInfos = attribute.NamedArguments.Select(a => a.MemberInfo).OfType<FieldInfo>().ToArray();
			var namedFieldValues = attribute.NamedArguments.Where(a => a.MemberInfo is FieldInfo).Select(a => a.TypedValue.Value).ToArray();
			return new CustomAttributeBuilder(attribute.Constructor, attributeArgs, namedPropertyInfos, namedPropertyValues, namedFieldInfos, namedFieldValues);
		}).ToArray();
	}
	#endregion
}
