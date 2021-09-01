using DataAccessLayer.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    /// <summary>
    /// Summary description for InboxReadControllerTest
    /// </summary>
    [TestClass]
    public class InboxReadTest
    {
        private static InboxReadDAL _InboxReadDAL;
        public InboxReadTest()
        {
            _InboxReadDAL = new InboxReadDAL();
        }
    }
}
