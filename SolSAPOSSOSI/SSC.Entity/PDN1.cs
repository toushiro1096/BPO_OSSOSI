using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SSC.Entity
{
    [DataContract]
    public class PDN1
    {

        [DataMember(Name = "ItemCode", Order = 1, EmitDefaultValue = true)]
        public string ItemCode { get; set; }
        [DataMember(Name = "Quantity", Order = 2, EmitDefaultValue = true)]
        public double Quantity { get; set; }
        [DataMember(Name = "WarehouseCode", Order = 3, EmitDefaultValue = true)]
        public string WarehouseCode { get; set; }
        [DataMember(Name = "Price", Order = 4, EmitDefaultValue = true)]
        public double Price { get; set; }
        [DataMember(Name = "Currency", Order = 5, EmitDefaultValue = true)]
        public string Currency { get; set; }
        [DataMember(Name = "DiscountPercent", Order = 6, EmitDefaultValue = true)]
        public double DiscountPercent { get; set; }
        [DataMember(Name = "LstCampoUsuario", Order = 7, EmitDefaultValue = true)]
        public List<CampoUsuario> LstCampoUsuario { get; set; }

        public PDN1()
        {
            this.ItemCode = string.Empty;
            this.Quantity = 0;
            this.WarehouseCode = string.Empty;
            this.Price = 0;
            this.Currency = string.Empty;
            this.DiscountPercent = 0;
            this.LstCampoUsuario = new List<CampoUsuario>();
        }
    }
}
