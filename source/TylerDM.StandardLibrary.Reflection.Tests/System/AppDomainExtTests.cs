namespace TylerDM.StandardLibrary.Reflection.System;

public class AppDomainExtTests : DomainAndAssemblySetup
{
	[Fact]
	public void CountDeveloperAssemblies() =>
		Assert.Equal(4, AppDomainExt.GetDeveloperAssemblies().Count());

	[Fact]
	public void GetImplementingTypes_Class() =>
		assertGets<Service1A, Service1>();

	[Fact]
	public void GetImplementingTypes_GenericClass() =>
		assertGets<GenericImplementation>(typeof(GenericClass<>));

	[Fact]
	public void GetImplementingTypes_Interface()
	{
		var implementers = getImplementers(typeof(IInterface1));
		Assert.Equal(2, implementers.Length);
		Assert.Contains(typeof(Service1), implementers);
		Assert.Contains(typeof(Service1A), implementers);
	}

	[Fact]
	public void GetImplementingTypes_GenericInterface() =>
		assertGets<GenericImplementation>(typeof(IGenericInterface<>));

	protected override IEnumerable<Type> getImplementingTypes(Type interfaceType) =>
		AppDomainExt.GetImplementingTypes(interfaceType);
}
