using static TylerDM.StandardLibrary.Reflection.System.AppDomainExt;

namespace TylerDM.StandardLibrary.Reflection.System;

public class AppDomainExtTests : DomainAndAssemblySetup
{
	[Fact]
	public static void Inspect()
	{
		var developerAssemblies = GetDeveloperAssemblies().ToArray();
		var developerTypes = GetDeveloperTypes().ToArray();
	}

	[Fact]
	public static void GetImplementingTypes_Class() =>
		assertGets<Service1A, Service1>();

	[Fact]
	public static void GetImplementingTypes_GenericClass() =>
		assertGets<GenericImplementation>(typeof(GenericClass<>));

	[Fact]
	public static void GetImplementingTypes_Interface() =>
		assertGets<Service1>(typeof(IInterface1));

	[Fact]
	public static void GetImplementingTypes_GenericInterface() =>
		assertGets<GenericImplementation>(typeof(IGenericInterface<>));

	private static void assertGets<TExpected, TInterfaceType>() =>
		assertGets<TExpected>(typeof(TInterfaceType));

	private static void assertGets<TExpected>(Type interfaceType) =>
		assertGets(typeof(TExpected), interfaceType);

	private static void assertGets(Type expectedType, Type interfaceType) =>
		Assert.Equal(expectedType, getImplementation(interfaceType));

	private static Type getImplementation<T>() =>
		GetImplementingTypes<T>().Single();

	private static Type getImplementation(Type type) =>
		GetImplementingTypes(type).Single();
}
