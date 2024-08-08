namespace TylerDM.StandardLibrary.Reflection.System;

public static class TypeExtTests
{
	[Fact]
	public static void TestImplements()
	{
		if (typeof(object).Implements(typeof(Enum)))
			throw new Exception();
		if (typeof(TestingClassImplementation).Implements(typeof(TestingClass)) is false)
			throw new Exception();
		if (typeof(TestingGenericClassImplementation).Implements(typeof(TestingGenericClass<>)) is false)
			throw new Exception();
		if (typeof(TestingInterfaceImplementation).Implements(typeof(ITestingInterface)) is false)
			throw new Exception();
		if (typeof(TestingGenericInterfaceImplementation).Implements(typeof(ITestingGenericInterface<>)) is false)
			throw new Exception();
	}
}
