using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SSC.Entity
{
    [DataContract]
    public class ORCT
    {

        [DataMember(Name = "DocEntry", Order = 0, EmitDefaultValue = true)]
        public string DocEntry { get; set; }
        [DataMember(Name = "CardCode", Order = 1, EmitDefaultValue = true)]
        public string CardCode { get; set; }
        [DataMember(Name = "DocDate", Order = 2, EmitDefaultValue = true)]
        public DateTime? DocDate { get; set; }
        public enum eDocTypte { rCustomer = 0, rAccount = 1, rSupplier = 2 }
        [DataMember(Name = "DocTypte", Order = 3, EmitDefaultValue = true)]
        public eDocTypte DocTypte { get; set; }
        [DataMember(Name = "DocCurrency", Order = 4, EmitDefaultValue = true)]
        public string DocCurrency { get; set; }
        [DataMember(Name = "CashSum", Order = 5, EmitDefaultValue = true)]
        public double CashSum { get; set; }
        [DataMember(Name = "CashAccount", Order = 6, EmitDefaultValue = true)]
        public string CashAccount { get; set; }
        [DataMember(Name = "LstCampoUsuario", Order = 7, EmitDefaultValue = true)]
        public List<CampoUsuario> LstCampoUsuario { get; set; }
        [DataMember(Name = "LstRCT3", Order = 8, EmitDefaultValue = true)]
        public List<RCT3> LstRCT3 { get; set; }
        [DataMember(Name = "JrnlMemo", Order = 9, EmitDefaultValue = true)]
        public string JrnlMemo { get; set; }
        [DataMember(Name = "CodigoExterno", Order = 10, EmitDefaultValue = true)]
        public string CodigoExterno { get; set; }
        public ORCT()
        {

        }
    }
}
