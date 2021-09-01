using DataAccessLayer.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    /// <summary>
    /// Summary description for ClassControllerTest
    /// </summary>
    [TestClass]
    public class ClassTest
    {
        private static ClassDAL _ClassDAL;
        public ClassTest()
        {
            _ClassDAL = new ClassDAL();
        }

        [TestMethod]
        public void GetAll()
        {
            // Act
            var response = _ClassDAL.GetAll();
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(2, response.Count);
        }
    }
}
