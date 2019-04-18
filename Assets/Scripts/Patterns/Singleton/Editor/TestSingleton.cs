using NUnit.Framework;
using Patterns;

namespace Test
{
    public class TestableSingleton : Singleton<TestableSingleton>
    {
        public string SomeProperty { get; set; }
    }

    public class TestSingleton
    {
        [Test]
        public void SingletonTest()
        {
            var someString = "testProperty";
            TestableSingleton.Instance.SomeProperty = someString;

            //create two sparing instances
            var singleton1 = new TestableSingleton();
            var singleton2 = new TestableSingleton();
            var singleton3 = new TestableSingleton();

            //set property of 1
            var someString1 = "testProperty1";
            singleton1.SomeProperty = someString1;

            //assert if the instances point to the same adress
            Assert.False(ReferenceEquals(TestableSingleton.Instance, singleton1));

            //assert if the instances are equal
            Assert.AreNotEqual(TestableSingleton.Instance, singleton1);

            //assert if the property on the singleton is still set
            Assert.False(TestableSingleton.Instance.SomeProperty == singleton1.SomeProperty);

            //inject singleton instance
            TestableSingleton.Instance.InjectInstance(singleton1);

            //assert if the instances point to the same adress
            Assert.True(ReferenceEquals(TestableSingleton.Instance, singleton1));

            //assert if the instances are equal
            Assert.AreEqual(TestableSingleton.Instance, singleton1);

            //assert if the property on the singleton is still set
            Assert.True(TestableSingleton.Instance.SomeProperty == someString1);
        }
    }
}