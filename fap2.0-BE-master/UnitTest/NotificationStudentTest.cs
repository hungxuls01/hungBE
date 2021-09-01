using DataAccessLayer.DAL;
using Entity.NotificationStudent;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    /// <summary>
    /// Summary description for NotificationStudentTest
    /// </summary>
    [TestClass]
    public class NotificationStudentTest
    {
        private static NotificationStudentDAL _NotificationStudentDAL;
        public NotificationStudentTest()
        {
            _NotificationStudentDAL = new NotificationStudentDAL();
        }

        [TestMethod]
        public void Insert()
        {
            // Arrange
            var obj = new CreateNotificationStudent();
            obj.categoryid = 20;
            obj.notificationid = 98;
            // Act
            _NotificationStudentDAL.Insert(obj);
            var response = _NotificationStudentDAL.GetAll();
            // Assert
            //Assert.IsNotNull(response);
            //Assert.AreEqual(1, response);
        }
    }
}
