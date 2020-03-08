using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneTimeUseCache;
using System;
using System.Threading;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SimpleAddAndGet()
        {

            var key = Guid.NewGuid().ToString();
            var someOtherValues = DateTime.Now.ToString("O");

            var obj = new MyOTUO()
            {
                Key = key,
                SomeOtherValues = someOtherValues
            };

            OTUCContext<MyOTUO>.Current.Add(obj);
            var result = OTUCContext<MyOTUO>.Current.Get(key);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.SomeOtherValues, obj.SomeOtherValues);
            Assert.AreEqual(result.Key, obj.Key);
        }



        [TestMethod]
        public void LifeTimeTest()
        {
            var key = Guid.NewGuid().ToString();
            var someOtherValues = DateTime.Now.ToString("O");

            var obj = new MyOTUO()
            {
                Key = key,
                SomeOtherValues = someOtherValues
            };

            OTUCContext<MyOTUO>.LifeTime = new TimeSpan(0, 0, 3);
            OTUCContext<MyOTUO>.Current.Add(obj);
            Thread.Sleep(3001);
            var result = OTUCContext<MyOTUO>.Current.Get(key);

            Assert.IsNull(result);

        }
    }
}
