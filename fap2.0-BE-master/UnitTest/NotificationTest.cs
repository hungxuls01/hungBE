using DataAccessLayer.DAL;
using Entity.Notification;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    /// <summary>
    /// Summary description for NotificationTest
    /// </summary>
    [TestClass]
    public class NotificationTest
    {
        private static NotificationDAL _NotificationDAL;
        public NotificationTest()
        {
            _NotificationDAL = new NotificationDAL();
        }

        [TestMethod]
        public void SelectPaging()
        {
            // Arrange
            var obj = new SearchingNotification();
            obj.Keyword = "";
            obj.Status = "-1";
            obj.Order = "id";
            obj.PageIndex = 0;
            obj.PageSize = 10;
            obj.types = -1;
            obj.categoryid = -1;
            obj.receiver = "";
            obj.userid = -1;
            obj.categoryStudentid = -1;
            obj.Departmentid = -1;
            obj.createDate = "";
            // Act
            var response = _NotificationDAL.SelectPaging(obj, 0, 10);
            // Assert
            //Assert.IsNotNull(response);
            //Assert.AreEqual(1, response.Item1);
            //Assert.AreEqual(1, response.Item2.Count);
        }

        [TestMethod]
        public void GetById()
        {
            // Act
            var response = _NotificationDAL.GetById(97);
            // Assert
            //Assert.IsNotNull(response);
            //Assert.AreEqual(97, response.id);
            //Assert.AreEqual("test", response.title.Trim());
        }

        [TestMethod]
        public void CountNotificationNotRead()
        {
            // Act
            var response = _NotificationDAL.CountNotificationNotRead(2);
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(0, response);
        }

        [TestMethod]
        public void Delete()
        {

            // Arrange
            var obj = new SearchingNotification();
            obj.Keyword = "";
            obj.Status = "-1";
            obj.Order = "id";
            obj.PageIndex = 0;
            obj.PageSize = 10;
            obj.types = -1;
            obj.categoryid = -1;
            obj.receiver = "";
            obj.userid = -1;
            obj.categoryStudentid = -1;
            obj.Departmentid = -1;
            obj.createDate = "";
            // Act
            _NotificationDAL.Delete(84);
            var response = _NotificationDAL.SelectPaging(obj, 0, 10);
            // Assert
            //Assert.IsNotNull(response);
            //Assert.AreEqual(0, response.Item1);
            //Assert.AreEqual(0, response.Item2.Count);
        }

    }
}
