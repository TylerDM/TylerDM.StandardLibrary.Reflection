namespace TylerDM.StandardLibrary.Reflection.System;

public class TypeExtTests
{
	interface InterfaceGeneric<T>;
	interface Interface;
	abstract class BaseBase;
	abstract class Base : BaseBase;
	class Implementation : Base, Interface, InterfaceGeneric<object>;
	class ImplementationGeneric<T> : InterfaceGeneric<object>, Interface;

	[Fact]
	public void ImplementsBaseOfBase() =>
		Assert.True(typeof(Implementation).Implements<BaseBase>());

	[Fact]
	public void ImplementsInterface() =>
		Assert.True(typeof(Implementation).Implements(typeof(Interface)));

	[Fact]
	public void NongenericImplementsOpenGenericInterface() =>
		Assert.True(typeof(Implementation).Implements(typeof(InterfaceGeneric<>)));

	[Fact]
	public void OpenGenericImplementsOpenGenericInterface() =>
		Assert.True(typeof(List<>).Implements(typeof(IList<>)));

	[Fact]
	public void ImplementsBaseClass() =>
		Assert.True(typeof(AppDomain).Implements(typeof(MarshalByRefObject)));

	[Fact]
	public void NongenericImplementsClosedGeneric() =>
		Assert.True(typeof(Implementation).Implements(typeof(InterfaceGeneric<object>)));

	[Fact]
	public void OpenGenericImplementsOpenGeneric() =>
		Assert.True(typeof(ImplementationGeneric<>).Implements(typeof(InterfaceGeneric<>)));

	[Fact]
	public void OpenGenericImplementsNongeneric() =>
		Assert.True(typeof(ImplementationGeneric<>).Implements(typeof(Interface)));

	[Fact]
	public void ObjectDoesNotImplementEnum() =>
		Assert.False(typeof(object).Implements(typeof(Enum)));
}
