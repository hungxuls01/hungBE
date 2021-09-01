using DataAccessLayer.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    /// <summary>
    /// Summary description for CapstoneProjectControllerTest
    /// </summary>
    [TestClass]
    public class CapstoneProjectTest
    {
        private static CapstoneProjectDAL _CapstoneProjectDal;
        public CapstoneProjectTest()
        {
            _CapstoneProjectDal = new CapstoneProjectDAL();
        }
        [TestMethod]
        public void GetAll()
        {
            // Act
            var response = _CapstoneProjectDal.GetAll();
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(2, response.Count);
        }

    }
}
