namespace TylerDM.StandardLibrary.Reflection.Testing;

//Existing tests expect each of the concrete classes to be the only types inheriting from each of these base classes and interfaces.  If you need different behavior, create new classes, preferrably private ones.

abstract class TestingClass { }
class TestingClassImplementation : TestingClass { }
abstract class TestingGenericClass<T> { }
class TestingGenericClassImplementation : TestingGenericClass<object> { }
interface ITestingInterface { }
class TestingInterfaceImplementation : ITestingInterface { }
interface ITestingGenericInterface<T> { }
class TestingGenericInterfaceImplementation : ITestingGenericInterface<object> { }