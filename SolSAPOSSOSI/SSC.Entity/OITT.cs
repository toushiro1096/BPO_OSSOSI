using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SSC.Entity
{
    public class OITT
    {
        [DataMember(Name = "Code", Order = 1, EmitDefaultValue = true)]
        public string Code { get; set; }
        [DataMember(Name = "Name", Order = 2, EmitDefaultValue = true)]
        public string Name { get; set; }
        [DataMember(Name = "PriceList", Order = 4, EmitDefaultValue = true)]
        public Int32 PriceList { get; set; }
        [DataMember(Name = "Qauntity", Order = 5, EmitDefaultValue = true)]
        public decimal Qauntity { get; set; }
        [DataMember(Name = "LstCampoUsuario", Order = 6, EmitDefaultValue = true)]
        public List<CampoUsuario> LstCampoUsuario { get; set; }

        public OITT()
        {
            this.Code = string.Empty;
            this.Name = string.Empty;
            this.PriceList = 0;
            this.Qauntity = 0;
            this.LstCampoUsuario = new List<CampoUsuario>();
        }
    }
}
