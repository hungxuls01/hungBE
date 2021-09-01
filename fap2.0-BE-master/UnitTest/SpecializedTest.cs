using DataAccessLayer.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    /// <summary>
    /// Summary description for SpecializedTest
    /// </summary>
    [TestClass]
    public class SpecializedTest
    {
        private static SpecializedDAL _SpecializedDAL;
        public SpecializedTest()
        {
            _SpecializedDAL = new SpecializedDAL();
        }

        [TestMethod]
        public void GetAll()
        {
            // Act
            var response = _SpecializedDAL.GetAll();
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(5, response.Count);
        }
    }
}
