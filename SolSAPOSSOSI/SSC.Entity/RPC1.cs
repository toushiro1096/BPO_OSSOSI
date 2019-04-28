using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SSC.Entity
{
    [DataContract]
    public class RPC1
    {
        [DataMember(Name = "ItemCode", Order = 1, EmitDefaultValue = true)]
        public string ItemCode { get; set; }
        [DataMember(Name = "ItemName", Order = 1, EmitDefaultValue = true)]
        public string ItemName { get; set; }
        [DataMember(Name = "TaxCode", Order = 2, EmitDefaultValue = true)]
        public string TaxCode { get; set; }
        [DataMember(Name = "WtLiable", Order = 3, EmitDefaultValue = true)]
        public string WtLiable { get; set; }
        [DataMember(Name = "DiscountPercent", Order = 4, EmitDefaultValue = true)]
        public double DiscountPercent { get; set; }
        [DataMember(Name = "Quantity", Order = 5, EmitDefaultValue = true)]
        public double Quantity { get; set; }
        [DataMember(Name = "LineNum", Order = 6, EmitDefaultValue = true)]
        public int LineNum { get; set; }
        [DataMember(Name = "BaseEntry", Order = 7, EmitDefaultValue = true)]
        public int BaseEntry { get; set; }
        [DataMember(Name = "WhsCode", Order = 8, EmitDefaultValue = true)]
        public string WhsCode { get; set; }
        [DataMember(Name = "LstCampoUsuario", Order = 9, EmitDefaultValue = true)]
        public List<CampoUsuario> LstCampoUsuario { get; set; }

        public RPC1()
        {
            this.ItemCode = string.Empty;
            this.ItemName = string.Empty;
            this.TaxCode = string.Empty;
            this.WtLiable = string.Empty;
            this.DiscountPercent = 0;
            this.Quantity = 0;
            this.LineNum = 0;
            this.BaseEntry = 0;
            this.WhsCode = string.Empty;
            this.LstCampoUsuario = new List<CampoUsuario>();
        }

    }
}
