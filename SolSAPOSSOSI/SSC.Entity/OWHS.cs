using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SSC.Entity
{
    [DataContract]
    public class OWHS
    {
        [DataMember(Name = "CodeAlm", Order = 1, EmitDefaultValue = true)]
        public string WhsCode { get; set; }
        [DataMember(Name = "Name", Order = 2, EmitDefaultValue = true)]
        public string WhsName { get; set; }
        [DataMember(Name = "Street", Order = 3, EmitDefaultValue = true)]
        public string Street { get; set; }
        [DataMember(Name = "City", Order = 4, EmitDefaultValue = true)]
        public string City { get; set; }
        [DataMember(Name = "LstCampoUsuario", Order = 5, EmitDefaultValue = true)]
        public List<CampoUsuario> LstCampoUsuario { get; set; }
        public OWHS()
        {
            this.WhsCode = string.Empty;
            this.WhsName = string.Empty;
            this.Street = string.Empty;
            this.City = string.Empty;
            this.LstCampoUsuario = new List<CampoUsuario>();
        }
    }
}
