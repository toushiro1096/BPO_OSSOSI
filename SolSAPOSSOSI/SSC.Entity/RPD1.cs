using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SSC.Entity
{
    [DataContract]
    public class RPD1
    {
        [DataMember(Name = "ItemCode", Order = 1, EmitDefaultValue = true)]
        public string ItemCode { get; set; }
        [DataMember(Name = "Quantity", Order = 2, EmitDefaultValue = true)]
        public double Quantity { get; set; }
        [DataMember(Name = "TaxCode", Order = 3, EmitDefaultValue = true)]
        public string TaxCode { get; set; }
        [DataMember(Name = "LstCampoUsuario", Order = 4, EmitDefaultValue = true)]
        public List<CampoUsuario> LstCampoUsuario { get; set; }
        public RPD1()
        {
            this.ItemCode = string.Empty;
            this.Quantity = 0;
            this.TaxCode = string.Empty;
            this.LstCampoUsuario = new List<CampoUsuario>();
        }
    }
}
