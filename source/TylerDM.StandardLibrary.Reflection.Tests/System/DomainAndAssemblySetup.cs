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

	protected Type[] getImplementers(Type type) =>
		getImplementingTypes(type).ToArray();

	protected void assertGets<TExpected, TInterfaceType>() =>
		assertGets<TExpected>(typeof(TInterfaceType));

	protected void assertGets<TExpected>(Type interfaceType) =>
		assertGets(typeof(TExpected), interfaceType);

	protected void assertGets(Type expectedType, Type interfaceType) =>
		Assert.Equal(expectedType, getImplementation(interfaceType));

	protected Type getImplementation(Type type) =>
		getImplementingTypes(type).Single();
}
