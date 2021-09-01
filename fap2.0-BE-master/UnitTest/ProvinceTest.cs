using DataAccessLayer.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    /// <summary>
    /// Summary description for ProvinceTest
    /// </summary>
    [TestClass]
    public class ProvinceTest
    {
        private static ProvinceDAL _ProvinceDAL;
        public ProvinceTest()
        {
            _ProvinceDAL = new ProvinceDAL();
        }

        [TestMethod]
        public void GetAll()
        {
            // Act
            var response = _ProvinceDAL.GetAll();
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(65, response.Count);
        }
    }
}
