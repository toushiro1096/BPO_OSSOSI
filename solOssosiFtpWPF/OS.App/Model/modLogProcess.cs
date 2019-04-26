using SC.Model.Common.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS.App.Model
{
    public class modLogProcess
    {
        public int Id { get; set; }
        public int FPrcId { get; set; }
        public int FileId { get; set; }
        public int DocItem { get; set; }
        public string DocDate { get; set; }
        public string DocType { get; set; }
        public string DocSeries { get; set; }
        public string DocNumber { get; set; }
        public int PrcResult { get; set; }
        public string PrcDocEntry { get; set; }
        public string PrcError { get; set; }
        public DateTime PrcDateTime { get; set; }

        public string Error { get; set; }

        public bool Ins()
        {
            using (var con = new DbConnection() { ErrorSetter = (e) => { Error = e; } })
            {
                var _ret = con.GetObject("LogProcessIns", this);
                return (_ret != null && (Id = (int)_ret) > 0);
            }
        }
        public List<modFile> List()
        {

            var iRet = new List<modFile>();
            using (var con = new DbConnection() { ErrorSetter = (e) => { Error = e; } })
            using (var drd = con.GetDataReader("LogProcessList", this))
            {
                if (drd == null) return null;
                else
                    while (drd.Read())
                    {
                        var m = new modFile();
                        DbConnection.SetProperties(drd, m);
                        iRet.Add(m);
                    }
                return iRet;
            }
        }

    }
}
