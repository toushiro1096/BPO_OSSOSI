using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SSC.Entity
{
    public class PRQ1
    {

        [DataMember(Name = "ItemCode", Order = 1, EmitDefaultValue = true)]
        public string ItemCode { get; set; }
        [DataMember(Name = "Quantity", Order = 2, EmitDefaultValue = true)]
        public double Quantity { get; set; }
        [DataMember(Name = "RequiredDate", Order = 3, EmitDefaultValue = true)]
        public string RequiredDate { get; set; }
        [DataMember(Name = "WarehouseCode", Order = 4, EmitDefaultValue = true)]
        public string WarehouseCode { get; set; }
        [DataMember(Name = "CostingCode", Order = 5, EmitDefaultValue = true)]
        public string CostingCode { get; set; }
        [DataMember(Name = "LineNum", Order = 6, EmitDefaultValue = true)]
        public int LineNum { get; set; }
        [DataMember(Name = "Price", Order = 7, EmitDefaultValue = true)]
        public Double Price { get; set; }
        [DataMember(Name = "LstCampoUsuario", Order = 8, EmitDefaultValue = true)]
        public List<CampoUsuario> LstCampoUsuario { get; set; }
        [DataMember(Name = "ItemName", Order = 9, EmitDefaultValue = true)]
        public string ItemName { get; set; }
        [DataMember(Name = "AccountCode", Order = 10, EmitDefaultValue = true)]
        public string AccountCode { get; set; }
        public PRQ1()
        {
            this.ItemCode = string.Empty;
            this.Quantity = 0;
            this.RequiredDate = string.Empty;
            this.WarehouseCode = string.Empty;
            this.CostingCode = string.Empty;
            this.LineNum = 0;
            this.Price = 0;
            this.LstCampoUsuario = new List<CampoUsuario>();
            this.ItemName = string.Empty;
            this.AccountCode = string.Empty;
        }
    }
}
