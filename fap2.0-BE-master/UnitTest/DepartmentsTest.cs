using DataAccessLayer.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    /// <summary>
    /// Summary description for DepartmentsControllerTest
    /// </summary>
    [TestClass]
    public class DepartmentsTest
    {
        private static DepartmentsDAL _DepartmentsDal;
        public DepartmentsTest()
        {
            _DepartmentsDal = new DepartmentsDAL();
        }


        [TestMethod]
        public void GetAll()
        {
            // Act
            var response = _DepartmentsDal.GetAll();
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(2, response.Count);
        }
    }
}
