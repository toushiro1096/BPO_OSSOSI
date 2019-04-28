using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SSC.Entity
{
    public class ORTT
    {
        [DataMember(Name = "RateDate", Order = 1, EmitDefaultValue = true)]
        public string RateDate { get; set; }
        [DataMember(Name = "Currency", Order = 2, EmitDefaultValue = true)]
        public string Currency { get; set; }
        [DataMember(Name = "Rate", Order = 3, EmitDefaultValue = true)]
        public decimal Rate { get; set; }
        [DataMember(Name = "LstCampoUsuario", Order = 4, EmitDefaultValue = true)]
        public List<CampoUsuario> LstCampoUsuario { get; set; }
        public ORTT()
        {
            this.RateDate = string.Empty;
            this.Currency = string.Empty;
            this.Rate = 0;
            this.LstCampoUsuario = new List<CampoUsuario>();
        }
    }
}
