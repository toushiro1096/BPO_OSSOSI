using SC.Model.Common.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS.App.Model
{
    public class modFile 
    {
        public int      Id          { get; set; }
        public string   Name        { get; set; }
        public string   Path        { get; set; }
        public string   UplFtpUsr   { get; set; }
        public string   UplDatetime { get; set; }
        public string   PrcFtpUsr   { get; set; }
        public DateTime PrcDatetime { get; set; }

        public string Error { get; set; }

        public bool Upload()
        {
            using (var con = new DbConnection() { ErrorSetter = (e) => { Error = e; } })
            {
                var _ret = con.GetObject("FilesUpl", this);
                return (_ret != null && (Id = (int)_ret) > 0) ;
            }
                
        }
        public bool Process()
        {
            using (var con = new DbConnection() { ErrorSetter = (e) => { Error = e; } })
                return (con.GetInt("FilesPrc",this) > 0) ;

        }
        public List<modFile> List()
        {
            
            var iRet = new List<modFile>();
            using (var con = new DbConnection() { ErrorSetter = (e) => { Error = e; } })
                using (var drd = con.GetDataReader("FilesLst",this))
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
