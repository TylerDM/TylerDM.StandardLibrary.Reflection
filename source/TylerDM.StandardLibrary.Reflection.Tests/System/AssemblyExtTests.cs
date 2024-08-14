namespace TylerDM.StandardLibrary.Reflection.System;

public class AssemblyExtTests : DomainAndAssemblySetup
{
	[Fact]
	public void GetImplementingTypes_Class() =>
		assertSingleImplementationMatches<Service1A, Service1>();

	[Fact]
	public void GetImplementingTypes_GenericClass() =>
		assertSingleImplementationMatches<GenericImplementation>(typeof(GenericClass<>));

	[Fact]
	public void GetImplementingTypes_Interface()
	{
		var implementers = getAllImplementations(typeof(IInterface1));
		Assert.Equal(2, implementers.Length);
		Assert.Contains(typeof(Service1), implementers);
		Assert.Contains(typeof(Service1A), implementers);
	}

	[Fact]
	public void GetImplementingTypes_GenericInterface() =>
		assertSingleImplementationMatches<GenericImplementation>(typeof(IGenericInterface<>));

	protected override IEnumerable<Type> getImplementingTypes(Type interfaceType) =>
		AssemblyExt.GetImplementingTypes(interfaceType);
}
