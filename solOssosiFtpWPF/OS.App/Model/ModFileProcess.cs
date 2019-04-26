using SC.Model.Common.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS.App.Model
{
    public class ModFileProcess
    {
        public int      Id          { get; set; }
        public int      FileId      { get; set; }
        public DateTime PrcStart    { get; set; }
        public DateTime PrcEnd      { get; set; }
        public int      RegCount    { get; set; }
        public int      PrcCount    { get; set; }
        public int      PrcOk       { get; set; }
        public string   CompanyDB   { get; set; }
        public string   Observation { get; set; }

        public string Error { get; set; }


        public bool Obs()
        {
            using (var con = new DbConnection() { ErrorSetter = (e) => { Error = e; } })
            {
                var _ret = con.GetObject("FileProcessObs", this);
                return (_ret != null && (Id = (int)_ret) > 0);
            }
        }
        public bool Start()
        {
            using (var con = new DbConnection() { ErrorSetter = (e) => { Error = e; } })
            {
                var _ret = con.GetObject("FileProcessStart", this);
                return (_ret != null && (Id = (int)_ret) > 0);
            }
        }
        public bool End()
        {
            using (var con = new DbConnection() { ErrorSetter = (e) => { Error = e; } })
                return (con.GetInt("FileProcessEnd", this) > 0);
        }
    }
}
