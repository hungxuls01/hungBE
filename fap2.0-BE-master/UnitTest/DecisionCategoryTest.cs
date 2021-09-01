using DataAccessLayer.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    /// <summary>
    /// Summary description for DecisionCategoryControllerTest
    /// </summary>
    [TestClass]
    public class DecisionCategoryTest
    {
        private static DecisionCategoryDAL _DecisionCategoryDAL;
        public DecisionCategoryTest()
        {
            _DecisionCategoryDAL = new DecisionCategoryDAL();
        }

        [TestMethod]
        public void GetAll()
        {
            // Act
            var response = _DecisionCategoryDAL.GetAll();
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(13, response.Count);
        }

        [TestMethod]
        public void GetById()
        {
            // Act
            var response = _DecisionCategoryDAL.GetById(9);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(9, response.id);
            Assert.AreEqual("DDNPT", response.code.Trim());
        }

    }
}
