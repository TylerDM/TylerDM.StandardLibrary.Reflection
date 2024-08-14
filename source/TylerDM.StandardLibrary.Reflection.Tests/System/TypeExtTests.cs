namespace TylerDM.StandardLibrary.Reflection.System;

public class TypeExtTests
{
	private interface ITestingLevel1;
	private class TestingLevel1() : ITestingLevel1;
	private class TestingLevel2<T>() : TestingLevel1;
	private class TestingLevel3() : TestingLevel2<object>;
	private class List2<T> : List<T>;
	private class Nongeneric : List2<object>;
	private class OpenGeneric<T> : Nongeneric;

	[Fact]
	public void ImplementsBaseOfBase() =>
		Assert.True(typeof(TestingLevel3).Implements<TestingLevel1>());

	[Fact]
	public void ImplementsInterface() =>
		Assert.True(typeof(TestingLevel1).Implements(typeof(ITestingLevel1)));

	[Fact]
	public void OpenGenericImplementsOpenGenericInterface() =>
		Assert.True(typeof(List<>).Implements(typeof(IList<>)));

	[Fact]
	public void ImplementsBaseClass() =>
		Assert.True(typeof(AppDomain).Implements(typeof(MarshalByRefObject)));

	[Fact]
	public void NongenericImplementsClosedGeneric() =>
		Assert.True(typeof(Nongeneric).Implements(typeof(List2<object>)));

	[Fact]
	public void OpenGenericImplementsOpenGeneric() =>
		Assert.True(typeof(List2<>).Implements(typeof(List<>)));

	[Fact]
	public void OpenGenericImplementsNongeneric() =>
		Assert.True(typeof(OpenGeneric<>).Implements(typeof(Nongeneric)));

	[Fact]
	public void ObjectDoesNotImplementEnum() =>
		Assert.False(typeof(object).Implements(typeof(Enum)));
}
