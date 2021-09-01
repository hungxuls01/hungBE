using DataAccessLayer.DAL;
using Entity.CategoryStudent;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    /// <summary>
    /// Summary description for CategoryStudentControllerTest
    /// </summary>
    [TestClass]
    public class CategoryStudentTest
    {
        private static CategoryStudentDAL _CategoryStudentDAL;
        public CategoryStudentTest()
        {
            _CategoryStudentDAL = new CategoryStudentDAL();
        }

        [TestMethod]
        public void GetByStudentid()
        {
            // Act
            var response = _CategoryStudentDAL.GetByStudentid(2);
            // Assert
            Assert.IsNotNull(response);
            //Assert.AreEqual(6, response.Count);
        }

        [TestMethod]
        public void Insert()
        {
            // Arrange

            var obj = new CreateCategoryStudent();
            obj.userid = 2;
            obj.name = "test unit";
            // Act
            _CategoryStudentDAL.Insert(obj);
            var response = _CategoryStudentDAL.GetByStudentid(2);
            // Assert
            Assert.IsNotNull(response);
            //Assert.AreEqual(7, response.Count);
        }
    }
}
