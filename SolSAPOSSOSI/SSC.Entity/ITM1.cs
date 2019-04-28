using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SSC.Entity
{

    public class ITM1
    {
        [DataMember(Name = "PricelevelId", Order = 1, EmitDefaultValue = true)]
        public Int32 ListNum { get; set; }
        [DataMember(Name = "Pricelevelname", Order = 2, EmitDefaultValue = true)]
        public string ListName { get; set; }
        [DataMember(Name = "StyleName", Order = 3, EmitDefaultValue = true)]
        public string ItemCode { get; set; }
        [DataMember(Name = "Desc1", Order = 4, EmitDefaultValue = true)]
        public string ItemName { get; set; }
        [DataMember(Name = "Price", Order = 5, EmitDefaultValue = true)]
        public decimal Price { get; set; }
        [DataMember(Name = "Currencyid", Order = 6, EmitDefaultValue = true)]
        public Int32 CurrenCyId { get; set; }
        [DataMember(Name = "LstCampoUsuario", Order = 7, EmitDefaultValue = true)]
        public List<CampoUsuario> LstCampoUsuario { get; set; }
        public ITM1()
        {
            this.ListNum = 0;
            this.ListName = string.Empty;
            this.ItemCode = string.Empty;
            this.ItemName = string.Empty;
            this.Price = 0;
            this.CurrenCyId = 0;
            this.LstCampoUsuario = new List<CampoUsuario>();
        }
    }

}
