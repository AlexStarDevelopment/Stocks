using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WealthManager.PL;

namespace WealthManager.BL
{
    class CErrorLog
    {
        //props
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public void LogError(string message)
        {
            try
            {
                WealthDataContext oDc = new WealthDataContext();
                tblErrorLog err = new tblErrorLog();

                err.Id = Guid.NewGuid();
                err.Message = message;
                err.DateTime = DateTime.Now;

                oDc.tblErrorLogs.InsertOnSubmit(err);
                oDc.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
