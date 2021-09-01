using DataAccessLayer.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    /// <summary>
    /// Summary description for NotificationCategoryTest
    /// </summary>
    [TestClass]
    public class NotificationCategoryTest
    {
        private static NotificationCategoryDAL _NotificationCategoryDAL;
        public NotificationCategoryTest()
        {
            _NotificationCategoryDAL = new NotificationCategoryDAL();
        }

        [TestMethod]
        public void GetAll()
        {
            // Act
            var response = _NotificationCategoryDAL.GetAll();
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(4, response.Count);
        }
    }
}
