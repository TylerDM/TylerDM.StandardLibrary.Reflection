namespace TylerDM.StandardLibrary.Reflection.System;

public static class AppDomainExtTests
{
	[Fact]
	public static void GetImplementingTypes_Class()
	{
		var implementer = AppDomainExt.GetImplementingTypes<TestingClass>().Single();
		if (implementer != typeof(TestingClassImplementation))
			throw new Exception("Incorrect class returned.");
	}

	[Fact]
	public static void GetImplementingTypes_GenericClass()
	{
		var implementer = AppDomainExt.GetImplementingTypes(typeof(TestingGenericClass<>)).Single();
		if (implementer != typeof(TestingGenericClassImplementation))
			throw new Exception("Incorrect class returned.");
	}

	[Fact]
	public static void GetImplementingTypes_Interface()
	{
		var implementer = AppDomainExt.GetImplementingTypes<ITestingInterface>().Single();
		if (implementer != typeof(TestingInterfaceImplementation))
			throw new Exception("Incorrect class returned.");
	}

	[Fact]
	public static void GetImplementingTypes_GenericInterface()
	{
		var implementer = AppDomainExt.GetImplementingTypes(typeof(ITestingGenericInterface<>)).Single();
		if (implementer != typeof(TestingGenericInterfaceImplementation))
			throw new Exception("Incorrect class returned.");
	}
}
