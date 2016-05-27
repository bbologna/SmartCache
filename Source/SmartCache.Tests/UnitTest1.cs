using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Runtime.Caching;
using System.Collections;
using System.Collections.Generic;
using SmartCache.Tests.Fakes;

namespace SmartCache.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private ICacheConfig config;
        private IEnlapsedEvent enlapsedEvent;

        [TestInitialize]
        public void Initialize()
        {
            config = Substitute.For<ICacheConfig>();
            config.LockListMaxSize.Returns(5);
            config.MaxReports.Returns(5);
            config.MaxSummarySize.Returns(5);
            config.SlidingExpiration.Returns(50);
            config.AbsoluteExpiration.Returns(100);
            enlapsedEvent = Substitute.For<IEnlapsedEvent>();
        }

        /// <summary>
        /// Basic functional test
        /// </summary>
        [TestMethod]
        public void ReturnsValue()
        {
            // Arrange
            var itemsLogic = new Items();
            var cacheUnderTest = new SmartCache<QueryCriteria, Item>(enlapsedEvent, MemoryCache.Default, "Item", itemsLogic.GetItem, config);
            var queryCriteria = new QueryCriteria("1", true);

            // Act
            var result = cacheUnderTest.Invoke(queryCriteria);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, itemsLogic.TimesCalled);
        }

        /// <summary>
        /// While enlapsedEvent is not fired it continues to invoke proxied function.
        /// </summary>
        [TestMethod]
        public void ContinuesToInvokeFunction()
        {
            // Arrange
            var itemsLogic = new Items();
            var cacheUnderTest = new SmartCache<QueryCriteria, Item>(enlapsedEvent, MemoryCache.Default, "Item", itemsLogic.GetItem, config);
            var queryCriteria = new QueryCriteria("1", true);

            // Act
            var result1 = cacheUnderTest.Invoke(queryCriteria);
            var result2 = cacheUnderTest.Invoke(queryCriteria);
            var result3 = cacheUnderTest.Invoke(queryCriteria);

            // Assert
            Assert.AreEqual(3, itemsLogic.TimesCalled);
            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.IsNotNull(result3);
        }
    }
}
