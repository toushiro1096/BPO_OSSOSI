using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SSC.Entity
{
    [DataContract]
    public class ITT1
    {
        [DataMember(Name = "Father", Order = 1, EmitDefaultValue = true)]
        public string Father { get; set; }
        [DataMember(Name = "Children", Order = 2, EmitDefaultValue = true)]
        public string Children { get; set; }
        [DataMember(Name = "ChildrenName", Order = 3, EmitDefaultValue = true)]
        public string ChildrenName { get; set; }
        [DataMember(Name = "Childrenbuyunitmsr", Order = 4, EmitDefaultValue = true)]
        public string Childrenbuyunitmsr { get; set; }
        [DataMember(Name = "Quantity", Order = 5, EmitDefaultValue = true)]
        public decimal Quantity { get; set; }
        [DataMember(Name = "LstCampoUsuario", Order = 6, EmitDefaultValue = true)]
        public List<CampoUsuario> LstCampoUsuario { get; set; }
        public ITT1()
        {
            this.Father = string.Empty;
            this.Children = string.Empty;
            this.ChildrenName = string.Empty;
            this.Childrenbuyunitmsr = string.Empty;
            this.Quantity = 0;
            this.LstCampoUsuario = new List<CampoUsuario>();
        }
    }

}
