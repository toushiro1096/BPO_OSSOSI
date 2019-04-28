using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SSC.Entity
{
    public class OPCH
    {
        [DataMember(Name = "DocDate", Order = 1, EmitDefaultValue = true)]
        public string DocDate { get; set; }
        [DataMember(Name = "DocDueDate", Order = 2, EmitDefaultValue = true)]
        public string DocDueDate { get; set; }
        [DataMember(Name = "TaxDate", Order = 3, EmitDefaultValue = true)]
        public string TaxDate { get; set; }
        [DataMember(Name = "TipoDoc", Order = 4, EmitDefaultValue = true)]
        public string U_LGS_TDOC { get; set; }
        [DataMember(Name = "SerieDoc", Order = 5, EmitDefaultValue = true)]
        public string U_LGS_SDOC { get; set; }
        [DataMember(Name = "NumDoc", Order = 6, EmitDefaultValue = true)]
        public string U_LGS_CDOC { get; set; }
        public enum TIPOMOV { VEN, CCH, DEG, COM, EAR };
        [DataMember(Name = "TipoMovimiento", Order = 7, EmitDefaultValue = true)]
        public TIPOMOV U_LGS_TIPO { get; set; }
        public enum ISeries { S1100, S1200, S1300, S1500, S1600, S1700 };
        [DataMember(Name = "Series", Order = 8, EmitDefaultValue = true)]
        public ISeries Series { get; set; }
        [DataMember(Name = "LstDocEntry", Order = 9, EmitDefaultValue = true)]
        public List<PCH1> LstPCH1 { get; set; }
        [DataMember(Name = "SlpCode", Order = 10, EmitDefaultValue = true)]
        public int SlpCode { get; set; }
        [DataMember(Name = "DocCurrency", Order = 11, EmitDefaultValue = true)]
        public string DocCurrency { get; set; }
        [DataMember(Name = "LstCampoUsuario", Order = 12, EmitDefaultValue = true)]
        public List<CampoUsuario> LstCampoUsuario { get; set; }
        [DataMember(Name = "DocEntry", Order = 13, EmitDefaultValue = true)]
        public string DocEntry { get; set; }
        [DataMember(Name = "Jrnlmemo", Order = 14, EmitDefaultValue = true)]
        public string Jrnlmemo { get; set; }
        [DataMember(Name = "CodigoExterno", Order = 15, EmitDefaultValue = true)]
        public string CodigoExterno { get; set; }
        public OPCH()
        {
            this.DocDate = string.Empty;
            this.DocDueDate = string.Empty;
            this.TaxDate = string.Empty;
            this.U_LGS_TDOC = string.Empty;
            this.U_LGS_SDOC = string.Empty;
            this.U_LGS_CDOC = string.Empty;
            this.U_LGS_TIPO = new TIPOMOV();
            this.Series = new ISeries();
            this.LstPCH1 = new List<PCH1>();
            this.SlpCode = 0;
            this.DocCurrency = string.Empty;
            this.LstCampoUsuario = new List<CampoUsuario>();
        }
    }
}
