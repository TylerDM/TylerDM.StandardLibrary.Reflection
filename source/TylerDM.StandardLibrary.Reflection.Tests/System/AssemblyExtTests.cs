namespace TylerDM.StandardLibrary.Reflection.System;

public static class AssemblyExtTests
{
	[Fact]
	public static void GetImplementingTypes_Class()
	{
		var implementer = AssemblyExt.GetImplementingTypes<TestingClass>().Single();
		if (implementer != typeof(TestingClassImplementation))
			throw new Exception("Incorrect class returned.");
	}

	[Fact]
	public static void GetImplementingTypes_GenericClass()
	{
		var implementer = AssemblyExt.GetImplementingTypes(typeof(TestingGenericClass<>)).Single();
		if (implementer != typeof(TestingGenericClassImplementation))
			throw new Exception("Incorrect class returned.");
	}

	[Fact]
	public static void GetImplementingTypes_Interface()
	{
		var implementer = AssemblyExt.GetImplementingTypes<ITestingInterface>().Single();
		if (implementer != typeof(TestingInterfaceImplementation))
			throw new Exception("Incorrect class returned.");
	}

	[Fact]
	public static void GetImplementingTypes_GenericInterface()
	{
		var implementer = AssemblyExt.GetImplementingTypes(typeof(ITestingGenericInterface<>)).Single();
		if (implementer != typeof(TestingGenericInterfaceImplementation))
			throw new Exception("Incorrect class returned.");
	}
}
