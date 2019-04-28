using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SSC.Entity
{
    [DataContract]
    public class JDT1
    {
        [DataMember(Name = "ShortName", Order = 1, EmitDefaultValue = true)]
        public string ShortName { get; set; }
        [DataMember(Name = "AccountCode", Order = 2, EmitDefaultValue = true)]
        public string AccountCode { get; set; }
        [DataMember(Name = "Credit", Order = 3, EmitDefaultValue = true)]
        public double Credit { get; set; }
        [DataMember(Name = "Debit", Order = 4, EmitDefaultValue = true)]
        public double Debit { get; set; }
    }
}
