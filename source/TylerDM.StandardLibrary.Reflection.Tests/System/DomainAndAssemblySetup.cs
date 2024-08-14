namespace TylerDM.StandardLibrary.Reflection.System;

public abstract class DomainAndAssemblySetup
{
	protected abstract class BaseService { }
	protected interface IInterface1 { }
	protected interface IGenericInterface<T> { }
	protected class Service1 : BaseService, IInterface1 { }
	protected class Service2 : BaseService { }
	protected class Service1A : Service1 { }
	protected abstract class GenericClass<T> { }
	protected class GenericImplementation : GenericClass<object>, IGenericInterface<object> { }

	protected abstract IEnumerable<Type> getImplementingTypes(Type interfaceType);

	protected Type[] getAllImplementations(Type type) =>
		getImplementingTypes(type).ToArray();

	protected void assertSingleImplementationMatches<TExpected, TInterfaceType>() =>
		assertSingleImplementationMatches<TExpected>(typeof(TInterfaceType));

	protected void assertSingleImplementationMatches<TExpected>(Type interfaceType) =>
		assertSingleImplementationMatches(typeof(TExpected), interfaceType);

	protected void assertSingleImplementationMatches(Type expectedType, Type interfaceType) =>
		Assert.Equal(expectedType, getSingleImplementation(interfaceType));

	protected Type getSingleImplementation(Type type) =>
		getImplementingTypes(type).Single();
}
