using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SSC.Entity
{
    [DataContract]
    public class POR1
    {
        [DataMember(Name = "StoreNo", Order = 1, EmitDefaultValue = true)]
        public int WhsCode { get; set; }
        [DataMember(Name = "ALU", Order = 2, EmitDefaultValue = true)]
        public string ItemCode { get; set; }
        [DataMember(Name = "Linedesciption", Order = 3, EmitDefaultValue = true)]
        public string dscription { get; set; }
        [DataMember(Name = "LineId", Order = 4, EmitDefaultValue = true)]
        public int LineNum { get; set; }
        [DataMember(Name = "Qty", Order = 5, EmitDefaultValue = true)]
        public decimal Quantity { get; set; }
        [DataMember(Name = "Cost", Order = 6, EmitDefaultValue = true)]
        public decimal Price { get; set; }
        [DataMember(Name = "CurrencyId", Order = 7, EmitDefaultValue = true)]
        public int CurrencyId { get; set; }
        [DataMember(Name = "CostExt", Order = 8, EmitDefaultValue = true)]
        public decimal OpenSum { get; set; }
        [DataMember(Name = "Taxpercent", Order = 9, EmitDefaultValue = true)]
        public decimal VatPrcnt { get; set; }
        [DataMember(Name = "TaxCost", Order = 10, EmitDefaultValue = true)]
        public decimal LineVat { get; set; }
        [DataMember(Name = "LineStatus", Order = 11, EmitDefaultValue = true)]
        public string LineStatus { get; set; }
        [DataMember(Name = "LstCampoUsuario", Order = 12, EmitDefaultValue = true)]
        public List<CampoUsuario> LstCampoUsuario { get; set; }
        public POR1()
        {
            this.WhsCode = 0;
            this.ItemCode = string.Empty;
            this.dscription = string.Empty;
            this.LineNum = 0;
            this.Quantity = 0;
            this.Price = 0;
            this.CurrencyId = 0;
            this.OpenSum = 0;
            this.VatPrcnt = 0;
            this.LineVat = 0;
            this.LineStatus = string.Empty;
            this.LstCampoUsuario = new List<CampoUsuario>();
        }
    }

}
