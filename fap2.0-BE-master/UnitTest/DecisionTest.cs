using DataAccessLayer.DAL;
using Entity.Decision;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    /// <summary>
    /// Summary description for DecisionControllerTest
    /// </summary>
    [TestClass]
    public class DecisionTest
    {
        private static DecisionDAL _DecisionDAL;
        public DecisionTest()
        {
            _DecisionDAL = new DecisionDAL();
        }


        [TestMethod]
        public void GetAll()
        {
            // Act
            var response = _DecisionDAL.GetAll();
            // Assert
            //Assert.IsNotNull(response);
            //Assert.AreEqual(19, response.Count);
        }

        [TestMethod]
        public void Insert()
        {
            // Arrange
            var obj = new CreateDecision();
            obj.name = "test unit";
            obj.note = "test unit 213213";
            obj.create_date = DateTime.Now;
            obj.categoryid = 9;
            obj.userid = 2;
            obj.classs = "test unit";
            obj.course = "test unit";
            obj.subject = "test unit";
            obj.exam = "test unit";
            obj.point = 5;
            // Act
            _DecisionDAL.InsertDecision(obj);
            var response = _DecisionDAL.GetAll();
            // Assert
            //Assert.IsNotNull(response);
            //Assert.AreEqual(20, response.Count);
        }

        [TestMethod]
        public void SelectPaging()
        {
            // Arrange
            var obj = new SearchingDecision();
            obj.categoryid = -1;
            obj.Departmentid = -1;
            obj.roleid = 3;
            obj.Keyword = "";
            obj.Status = "-1";
            obj.Order = "id";
            obj.PageIndex = 1;
            obj.PageSize = 10;
            obj.Orderdir = "DESC";
            // Act
            var response = _DecisionDAL.SelectPaging(obj, 1, 10);
            // Assert
            //Assert.IsNotNull(response);
            //Assert.AreEqual(23, response.Item1);
            //Assert.AreEqual(10, response.Item2.Count);
        }

        [TestMethod]
        public void GetById()
        {
            // Act
            var response = _DecisionDAL.GetById(51);
            // Assert
            //Assert.IsNotNull(response);
            //Assert.AreEqual(51, response.id);
            //Assert.AreEqual("test unit", response.name.Trim());
        }

        [TestMethod]
        public void AcceptDecision()
        {
            // Arrange
            var obj = new AcceptDecision();
            obj.id = 41;
            obj.status = 1;
            obj.reply = "OKE";
            obj.emailForward = "";
            // Act
            _DecisionDAL.AcceptDecision(obj);
            var Decision = _DecisionDAL.GetById(41);
            // Assert
            //Assert.IsNotNull(Decision);
            //Assert.AreEqual(41, Decision.id);
            //Assert.AreEqual(1, Decision.status);
            //Assert.AreEqual("OKE", Decision.reply);
        }


    }
}
