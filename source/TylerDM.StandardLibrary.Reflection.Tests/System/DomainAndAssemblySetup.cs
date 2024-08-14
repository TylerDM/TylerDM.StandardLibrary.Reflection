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
}
