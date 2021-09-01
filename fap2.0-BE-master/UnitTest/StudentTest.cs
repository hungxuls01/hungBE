using DataAccessLayer.DAL;
using Entity.Student;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    /// <summary>
    /// Summary description for StudentTest
    /// </summary>
    [TestClass]
    public class StudentTest
    {
        private static StudentDAL _StudentDAL;
        public StudentTest()
        {
            _StudentDAL = new StudentDAL();
        }


        [TestMethod]
        public void SelectPaging()
        {
            // Arrange
            var obj = new SearchingStudent();
            obj.Keyword = "";
            obj.Status = "-1";
            obj.Order = "id";
            obj.PageIndex = 0;
            obj.PageSize = 10;
            obj.Orderdir = "DESC";
            // Act
            var response = _StudentDAL.SelectPaging(obj, 0, 10);
            // Assert
            Assert.IsNotNull(response);
            //Assert.AreEqual(3, response.Item1);
            //Assert.AreEqual(3, response.Item2.Count);
        }

        [TestMethod]
        public void GetById()
        {
            // Act
            var response = _StudentDAL.GetById(39);
            // Assert
            //Assert.IsNotNull(response);
            //Assert.AreEqual(2, response.id);
            Assert.AreEqual("SE05743", response.RollNumber.Trim());
        }

        [TestMethod]
        public void GetCombobox()
        {
            // Act
            var response = _StudentDAL.GetCombobox();
            // Assert
            Assert.IsNotNull(response);
            //Assert.AreEqual(6, response.Count);
        }

        [TestMethod]
        public void GetByEmail()
        {
            // Act
            var response = _StudentDAL.GetByEmail("hunghvse06068@fpt.edu.vn");
            // Assert
            //Assert.IsNotNull(response);
            Assert.AreEqual("SE05743", response.RollNumber.Trim());
            Assert.AreEqual(1, response.Campusid);
        }

        [TestMethod]
        public void Delete()
        {
            // Act
            _StudentDAL.Delete(36);
            var response = _StudentDAL.GetCombobox();
            // Assert
            Assert.IsNotNull(response);
            //Assert.AreEqual(5, response.Count);
        }

    }
}
