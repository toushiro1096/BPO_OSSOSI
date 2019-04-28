using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SSC.Entity
{
    public class RDR1
    {
        [DataMember(Name = "ItemCode", Order = 1, EmitDefaultValue = true)]
        public string ItemCode { get; set; }
        [DataMember(Name = "ItemDescription ", Order = 2, EmitDefaultValue = true)]
        public string ItemDescription { get; set; }
        [DataMember(Name = "Price ", Order = 3, EmitDefaultValue = true)]
        public double Price { get; set; }
        [DataMember(Name = "DiscountPercent ", Order = 4, EmitDefaultValue = true)]
        public double DiscountPercent { get; set; }
        [DataMember(Name = "Quantity ", Order = 5, EmitDefaultValue = true)]
        public double Quantity { get; set; }
        [DataMember(Name = "Almacen ", Order = 6, EmitDefaultValue = true)]
        public string WarehouseCode { get; set; }
        [DataMember(Name = "order", Order = 7, EmitDefaultValue = true)]
        public int order { get; set; }
        [DataMember(Name = "LstCampoUsuario", Order = 8, EmitDefaultValue = true)]
        public List<CampoUsuario> LstCampoUsuario { get; set; }
        [DataMember(Name = "TaxCode", Order = 9, EmitDefaultValue = true)]
        public string TaxCode { get; set; }
        [DataMember(Name = "CostingCode", Order = 10, EmitDefaultValue = true)]
        public string CostingCode { get; set; }
        [DataMember(Name = "AccountCode", Order = 10, EmitDefaultValue = true)]
        public string AccountCode { get; set; }
        public RDR1()
        {
            this.ItemCode = string.Empty;
            this.ItemDescription = string.Empty;
            this.Price = 0;
            this.DiscountPercent = 0;
            this.Quantity = 0;
            this.WarehouseCode = string.Empty;
            this.order = 0;
            this.LstCampoUsuario = new List<CampoUsuario>();
            this.TaxCode = string.Empty;
            this.CostingCode = string.Empty;
            this.AccountCode = string.Empty;
        }
    }
}
