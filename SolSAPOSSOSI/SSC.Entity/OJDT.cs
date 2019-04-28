using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SSC.Entity
{
    [DataContract]
    public class OJDT
    {
        [DataMember(Name = "ReferenceDate", Order = 1, EmitDefaultValue = true)]
        public string ReferenceDate { get; set; }
        [DataMember(Name = "TaxDate", Order = 2, EmitDefaultValue = true)]
        public string TaxDate { get; set; }
        [DataMember(Name = "DueDate", Order = 3, EmitDefaultValue = true)]
        public string DueDate { get; set; }
        [DataMember(Name = "Memo", Order = 4, EmitDefaultValue = true)]
        public string Memo { get; set; }
        [DataMember(Name = "Reference", Order = 5, EmitDefaultValue = true)]
        public string Reference { get; set; }
        [DataMember(Name = "Reference2", Order = 6, EmitDefaultValue = true)]
        public string Reference2 { get; set; }
        [DataMember(Name = "Reference3", Order = 7, EmitDefaultValue = true)]
        public string Reference3 { get; set; }
        [DataMember(Name = "LstJDT1", Order = 8, EmitDefaultValue = true)]
        public List<JDT1> LstJDT1 { get; set; }


    }
}
