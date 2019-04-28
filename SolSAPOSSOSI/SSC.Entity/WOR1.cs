using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SSC.Entity
{
    [DataContract]
    public class WOR1
    {
        [DataMember(Name = "ItemCode", Order = 1, EmitDefaultValue = true)]
        public string ItemCode { get; set; }
        [DataMember(Name = "Quantity ", Order = 2, EmitDefaultValue = true)]
        public double Quantity { get; set; }
        [DataMember(Name = "Warehouse", Order = 3, EmitDefaultValue = true)]
        public string Warehouse { get; set; }
        [DataMember(Name = "PlannedQty", Order = 4, EmitDefaultValue = true)]
        public double PlannedQty { get; set; }
        [DataMember(Name = "WarehouseCode", Order = 5, EmitDefaultValue = true)]
        public string WarehouseCode { get; set; }
        [DataMember(Name = "JrnlMemo", Order = 6, EmitDefaultValue = true)]
        public string JrnlMemo { get; set; }
        [DataMember(Name = "Comments", Order = 7, EmitDefaultValue = true)]
        public string Comments { get; set; }
        [DataMember(Name = "LstCampoUsuario", Order = 8, EmitDefaultValue = true)]
        public List<CampoUsuario> LstCampoUsuario { get; set; }
        public WOR1()
        {
            this.ItemCode = string.Empty;
            this.Quantity = 0;
            this.Warehouse = string.Empty;
            this.PlannedQty = 0;
            this.WarehouseCode = string.Empty;
            this.JrnlMemo = string.Empty;
            this.Comments = string.Empty;
            this.LstCampoUsuario = new List<CampoUsuario>();
        }
    }
}
