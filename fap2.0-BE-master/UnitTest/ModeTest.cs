using DataAccessLayer.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    /// <summary>
    /// Summary description for ModeTest
    /// </summary>
    [TestClass]
    public class ModeTest
    {
        private static ModeDAL _ModeDAL;
        public ModeTest()
        {
            _ModeDAL = new ModeDAL();
        }


        [TestMethod]
        public void GetAll()
        {
            // Act
            var response = _ModeDAL.GetAll();
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(3, response.Count);
        }
    }
}
