using System.Collections.Generic;
using NUnit.Framework;
using Patterns;

namespace Test
{
    //poolable empty object used in the tests
    public class PoolableObjectTest : IPoolableObject
    {
        public void Restart()
        {
        }
    }

    //pool used in the tests
    public class PoolTest : GenericPooler<PoolableObjectTest>
    {
        public const int Size = 20;

        public PoolTest() : base(Size)
        {
        }
    }

    public class GenericPoolerTest
    {
        [Test]
        public void PoolInitialization()
        {
            //create new pool
            var pool = new PoolTest();

            //assert the start size of the pool after initialization
            Assert.True(pool.StartSize == PoolTest.Size);

            //assert the free objects list after initialization
            Assert.True(pool.SizeFreeObjects == PoolTest.Size);

            //assert the busy objects list after initialization
            Assert.True(pool.SizeBusyObjects == 0);
        }

        [Test]
        public void Get1Object()
        {
            //create new pool
            var pool = new PoolTest();

            //pool 1 object
            var obj = pool.Get();

            //assert the free objects list after pool
            Assert.True(pool.SizeFreeObjects == PoolTest.Size - 1);

            //assert the busy objects list after pool
            Assert.True(pool.SizeBusyObjects == 1);
        }

        [Test]
        public void ReleaseObjects()
        {
            //create new pool
            var pool = new PoolTest();

            //pool 1 object
            var obj = pool.Get();

            //release pooled object
            pool.Release(obj);

            //assert the free objects list after pool
            Assert.True(pool.SizeFreeObjects == PoolTest.Size);

            //assert the busy objects list after pool
            Assert.True(pool.SizeBusyObjects == 0);
        }

        [Test]
        public void PoolAllAndRelease()
        {
            //create pool
            var pool = new PoolTest();

            //store all pooled objects
            var allPooledObjects = new List<PoolableObjectTest>();

            //pool all the start size
            for (var i = 0; i < pool.StartSize; i++)
            {
                var obj = pool.Get();
                allPooledObjects.Add(obj);
            }

            //assert the free objects list after pool
            Assert.True(pool.SizeFreeObjects == 0);

            //assert the busy objects list after pool
            Assert.True(pool.SizeBusyObjects == PoolTest.Size);

            foreach (var obj in allPooledObjects)
                pool.Release(obj);

            //assert the free objects list after pool
            Assert.True(pool.SizeFreeObjects == PoolTest.Size);

            //assert the busy objects list after pool
            Assert.True(pool.SizeBusyObjects == 0);
        }

        [Test]
        public void ExceptionThrowsReleaseNullObject()
        {
            var pool = new PoolTest();

            void releaseNull()
            {
                pool.Release(null);
            }

            Assert.Throws<GenericPooler<PoolableObjectTest>.GenericPoolerArgumentException>(releaseNull);
        }
    }
}