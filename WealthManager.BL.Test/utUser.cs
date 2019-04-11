using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WealthManager.BL.Test
{
    [TestClass]
    public class utUser
    {
        [TestMethod]
        public void LoadUserTest()
        {
            CUser user = new CUser(Guid.NewGuid(),"Casey", "Kelpinski", "casey@kelpinski.com", "password");
            user.Load();
            Assert.IsTrue(user.Id != null);
        }
    }
}
