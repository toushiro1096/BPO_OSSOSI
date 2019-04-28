using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SSC.Entity
{
    [DataContract]
    public class OWTR
    {
        [DataMember(Name = "DocDate", Order = 1, EmitDefaultValue = true)]
        public string DocDate { get; set; }
        [DataMember(Name = "CodeAlmOrigen", Order = 2, EmitDefaultValue = true)]
        public string Filler { get; set; }
        [DataMember(Name = "CodeAlmDestino", Order = 3, EmitDefaultValue = true)]
        public string ToWhsCODE { get; set; }
        [DataMember(Name = "memo", Order = 4, EmitDefaultValue = true)]
        public string jrnlmemo { get; set; }
        [DataMember(Name = "Comments", Order = 5, EmitDefaultValue = true)]
        public string Comments { get; set; }
        [DataMember(Name = "ListaArticulos", Order = 6, EmitDefaultValue = true)]
        public List<WTR1> LstWTR1 { get; set; }
        [DataMember(Name = "TaxDate", Order = 7, EmitDefaultValue = true)]
        public string TaxDate { get; set; }

        public enum ISeries { S1100, S1200, S1300, S1500, S1600, S1700 };

        [DataMember(Name = "Series", Order = 8, EmitDefaultValue = true)]
        public ISeries Series { get; set; }
        [DataMember(Name = "LstCampoUsuario", Order = 9, EmitDefaultValue = true)]
        public List<CampoUsuario> LstCampoUsuario { get; set; }
        [DataMember(Name = "CodigoExterno", Order = 10, EmitDefaultValue = true)]
        public string CodigoExterno { get; set; }
        public OWTR()
        {
            this.DocDate = string.Empty;
            this.Filler = string.Empty;
            this.ToWhsCODE = string.Empty;
            this.jrnlmemo = string.Empty;
            this.Comments = string.Empty;
            this.LstWTR1 = new List<WTR1>();
            this.TaxDate = string.Empty;
            this.LstCampoUsuario = new List<CampoUsuario>();
            this.CodigoExterno = string.Empty;
        }
    }
}
