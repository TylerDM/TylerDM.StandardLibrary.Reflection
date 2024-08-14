using static TylerDM.StandardLibrary.Reflection.System.AssemblyExt;

namespace TylerDM.StandardLibrary.Reflection.System;

public class AssemblyExtTests : DomainAndAssemblySetup
{
	[Fact]
	public void GetImplementingTypes_Class() =>
		assertGets<Service1A, Service1>();

	[Fact]
	public void GetImplementingTypes_GenericClass() =>
		assertGets<GenericImplementation>(typeof(GenericClass<>));

	[Fact]
	public void GetImplementingTypes_Interface() =>
		assertGets<Service1>(typeof(IInterface1));

	[Fact]
	public void GetImplementingTypes_GenericInterface() =>
		assertGets<GenericImplementation>(typeof(IGenericInterface<>));

	private void assertGets<TExpected, TInterfaceType>() =>
		assertGets<TExpected>(typeof(TInterfaceType));

	private void assertGets<TExpected>(Type interfaceType) =>
		assertGets(typeof(TExpected), interfaceType);

	private void assertGets(Type expectedType, Type interfaceType) =>
		Assert.Equal(expectedType, getImplementation(interfaceType));

	private Type getImplementation<T>() =>
		GetImplementingTypes<T>().Single();

	private Type getImplementation(Type type) =>
		GetImplementingTypes(type).Single();
}
