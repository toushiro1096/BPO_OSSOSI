using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SSC.Entity
{
    [DataContract]
    public class WTR1
    {
        [DataMember(Name = "StyleName", Order = 1, EmitDefaultValue = true)]
        public string ItemCode { get; set; }
        [DataMember(Name = "Quantity", Order = 2, EmitDefaultValue = true)]
        public decimal Quantity { get; set; }
        [DataMember(Name = "CodeAlmOrigen", Order = 3, EmitDefaultValue = true)]
        public string Filler { get; set; }
        [DataMember(Name = "CodeAlmDestino", Order = 4, EmitDefaultValue = true)]
        public string ToWhsCODE { get; set; }
        [DataMember(Name = "LstCampoUsuario", Order = 5, EmitDefaultValue = true)]
        public List<CampoUsuario> LstCampoUsuario { get; set; }

        public WTR1()
        {
            this.ItemCode = string.Empty;
            this.Quantity = 0;
            this.Filler = string.Empty;
            this.ToWhsCODE = string.Empty;
            this.LstCampoUsuario = new List<CampoUsuario>();
        }
    }
}
