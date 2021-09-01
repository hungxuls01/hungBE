using DataAccessLayer.DAL;
using Entity.Combobox;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Controllers;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace UnitTest
{
    /// <summary>
    /// Summary description for CampusControllerTest
    /// </summary>
    [TestClass]
    public class CampusTest
    {
        private static CampusDAL _CampusDAL;

        public CampusTest()
        {
            _CampusDAL = new CampusDAL();
        }

        [TestMethod]
        public void GetAll()
        {
            // Act
            var response = _CampusDAL.GetAll();
            // Assert
            Assert.IsNotNull(response);
            Assert.AreEqual(4, response.Count);
        }
    }
}
