using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SSC.Entity
{
    [DataContract]
    public class OSLP
    {
        [DataMember(Name = "CodeEmployee", Order = 1, EmitDefaultValue = true)]
        public int SlpCode { get; set; }
        [DataMember(Name = "SlpNAme", Order = 2, EmitDefaultValue = true)]
        public string SlpNAme { get; set; }
        [DataMember(Name = "CodeAlm", Order = 3, EmitDefaultValue = true)]
        public string U_RDC_Alma { get; set; }
        [DataMember(Name = "LstCampoUsuario", Order = 4, EmitDefaultValue = true)]
        public List<CampoUsuario> LstCampoUsuario { get; set; }
        [DataMember(Name = "JrnlMemo", Order = 5, EmitDefaultValue = true)]
        public string JrnlMemo { get; set; }
        [DataMember(Name = "CodigoExterno", Order = 6, EmitDefaultValue = true)]
        public string CodigoExterno { get; set; }
        public OSLP()
        {
            this.SlpCode = 0;
            this.SlpNAme = string.Empty;
            this.U_RDC_Alma = string.Empty;
            this.LstCampoUsuario = new List<CampoUsuario>();
            this.JrnlMemo = string.Empty;
            this.CodigoExterno = string.Empty;
        }
    }
}
