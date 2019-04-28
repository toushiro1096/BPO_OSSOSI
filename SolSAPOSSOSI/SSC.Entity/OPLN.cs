using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSC.Entity
{
    public class OPLN
    {
        public Int16 ListNum { get; set; }
        public string ListName { get; set; }
        public Int16 BASE_NUM { get; set; }
        public decimal Factor { get; set; }
        public Int16 RoundSys { get; set; }
        public Int16 GroupCode { get; set; }
        public string DataSource { get; set; }
        public Int32 SPPCounter { get; set; }
        public Int16 UserSign { get; set; }
        public string IsGrossPrc { get; set; }
        public Int32 LogInstanc { get; set; }
        public Int16 UserSign2 { get; set; }
        public DateTime UpdateDate { get; set; }
        public string ValidFor { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public DateTime CreateDate { get; set; }
        public string PrimCurr { get; set; }
        public string AddCurr1 { get; set; }
        public string AddCurr2 { get; set; }
        public string RoundRule { get; set; }
        public decimal ExtAmount { get; set; }
        public string RndFrmtInt { get; set; }
        public string RndFrmtDec { get; set; }
        public List<CampoUsuario> LstCampoUsuario { get; set; }
        public OPLN()
        {
            this.ListNum = 0;
            this.ListName = string.Empty;
            this.BASE_NUM = 0;
            this.Factor = 0;
            this.RoundSys = 0;
            this.GroupCode = 0;
            this.DataSource = string.Empty;
            this.SPPCounter = 0;
            this.UserSign = 0;
            this.IsGrossPrc = string.Empty;
            this.LogInstanc = 0;
            this.UserSign2 = 0;
            this.UpdateDate = DateTime.Now;
            this.ValidFor = string.Empty;
            this.ValidFrom = DateTime.Now;
            this.ValidTo = DateTime.Now;
            this.CreateDate = DateTime.Now;
            this.PrimCurr = string.Empty;
            this.AddCurr1 = string.Empty;
            this.AddCurr2 = string.Empty;
            this.RoundRule = string.Empty;
            this.ExtAmount = 0;
            this.RndFrmtInt = string.Empty;
            this.RndFrmtDec = string.Empty;
            this.LstCampoUsuario = new List<CampoUsuario>();
        }
            
    }

}
