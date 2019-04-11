using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WealthManager.PL;
using System.Collections.Generic;
using System.Linq;

namespace WealthManager.PL.Test
{
    [TestClass]
    public class utUser
    {
        [TestMethod]
        public void LoadTest()
        {
            WealthDataContext wdc = new WealthDataContext();

            List<tblUser> users = (from u in wdc.tblUsers select u).ToList();

            Assert.AreNotEqual(0, users.Count);
        }
        [TestMethod]
        public void InsertTest()
        {
            WealthDataContext wdc = new WealthDataContext();

            tblUser otbluser = new tblUser();

            otbluser.Id = Guid.NewGuid();
            otbluser.FirstName = "Test";
            otbluser.LastName = "Test";
            otbluser.Email = "test@test.com";
            otbluser.Password = "TestPassword";

            wdc.tblUsers.InsertOnSubmit(otbluser);
            wdc.SubmitChanges();

            tblUser users = (from u in wdc.tblUsers where u.Password == "TestPassword" select u).FirstOrDefault();

            Assert.IsNotNull(users);
        }
        [TestMethod]
        public void UpdateTest()
        {
            WealthDataContext wdc = new WealthDataContext();

            tblUser users = (from u in wdc.tblUsers where u.Password == "TestPassword" select u).FirstOrDefault();

            users.FirstName = "NewFirstNameTest";

            wdc.SubmitChanges();

            tblUser userUpdate = (from u in wdc.tblUsers where u.FirstName == "NewFirstNameTest" select u).FirstOrDefault();

            Assert.IsNotNull(userUpdate);
        }
        [TestMethod]
        public void DeleteTest()
        {
            WealthDataContext wdc = new WealthDataContext();

            tblUser users = (from u in wdc.tblUsers where u.Password == "TestPassword" select u).FirstOrDefault();

            wdc.tblUsers.DeleteOnSubmit(users);
            wdc.SubmitChanges();

            tblUser userDelete = (from u in wdc.tblUsers where u.Password == "TestPassword" select u).FirstOrDefault();

            Assert.IsNull(userDelete);
        }
    }
}
