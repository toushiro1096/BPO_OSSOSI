using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SSC.Entity
{
    [DataContract]
    public class ORDR
    {
        [DataMember(Name = "DocEntry", Order = 0, EmitDefaultValue = true)]
        public string DocEntry { get; set; }
        [DataMember(Name = "CardCode", Order = 1, EmitDefaultValue = true)]
        public string CardCode { get; set; }
        [DataMember(Name = "CardName ", Order = 2, EmitDefaultValue = true)]
        public string CardName { get; set; }
        [DataMember(Name = "DocDate ", Order = 3, EmitDefaultValue = true)]
        public string DocDate { get; set; }
        [DataMember(Name = "DocDueDate", Order = 4, EmitDefaultValue = true)]
        public string DocDueDate { get; set; }
        [DataMember(Name = "DocTotal ", Order = 5, EmitDefaultValue = true)]
        public double DocTotal { get; set; }
        [DataMember(Name = "LstDetalle", Order = 6, EmitDefaultValue = true)]
        public List<RDR1> LstRDR1 { get; set; }
        [DataMember(Name = "Comments", Order = 7, EmitDefaultValue = true)]
        public string Comments { get; set; }
        [DataMember(Name = "SalesPersonCode", Order = 8, EmitDefaultValue = true)]
        public int SalesPersonCode { get; set; }
        [DataMember(Name = "TipoDoc", Order = 9, EmitDefaultValue = true)]
        public string U_LGS_TDOC { get; set; }
        [DataMember(Name = "SerieDoc", Order = 10, EmitDefaultValue = true)]
        public string U_LGS_SDOC { get; set; }
        [DataMember(Name = "NumDoc", Order = 11, EmitDefaultValue = true)]
        public string U_LGS_NUCE { get; set; }
        [DataMember(Name = "EfectivoSoles", Order = 12, EmitDefaultValue = true)]
        public double U_RDC_EfSo { get; set; }
        [DataMember(Name = "EfectivoDolares", Order = 13, EmitDefaultValue = true)]
        public double U_RDC_EfDo { get; set; }
        [DataMember(Name = "EntregadoSoles", Order = 14, EmitDefaultValue = true)]
        public double U_RDC_EnSo { get; set; }
        [DataMember(Name = "EntregadoDolares", Order = 15, EmitDefaultValue = true)]
        public double U_RDC_EnDo { get; set; }
        [DataMember(Name = "VueltoSoles", Order = 16, EmitDefaultValue = true)]
        public double U_RDC_VuSo { get; set; }
        [DataMember(Name = "VueltoDolares", Order = 17, EmitDefaultValue = true)]
        public double U_RDC_VuDo { get; set; }
        [DataMember(Name = "MonedaVuelto", Order = 18, EmitDefaultValue = true)]
        public string U_RDC_MonV { get; set; }
        [DataMember(Name = "TarjetaSoles", Order = 19, EmitDefaultValue = true)]
        public double U_RDC_TjSo { get; set; }
        [DataMember(Name = "TarjetaDolares", Order = 20, EmitDefaultValue = true)]
        public double U_RDC_TjDo { get; set; }
        [DataMember(Name = "JrnlMemo", Order = 21, EmitDefaultValue = true)]
        public string JrnlMemo { get; set; }


        [DataMember(Name = "CodAlmacen", Order = 22, EmitDefaultValue = true)]
        public string CodAlmacen { get; set; }


        [DataMember(Name = "LstPagoTarjeta", Order = 23, EmitDefaultValue = true)]
        public List<RCT3> LstRCT3 { get; set; }


        [DataMember(Name = "U_GS_EsExt", Order = 27, EmitDefaultValue = true)]
        public string U_GS_EsExt { get; set; }
        [DataMember(Name = "U_GS_NAC", Order = 28, EmitDefaultValue = true)]
        public string U_GS_NAC { get; set; }
        [DataMember(Name = "TaxDate", Order = 29, EmitDefaultValue = true)]
        public string TaxDate { get; set; }

        public enum TIPOMOV { VEN, DEG };
        [DataMember(Name = "TipoMovimiento", Order = 30, EmitDefaultValue = true)]
        public TIPOMOV U_LGS_TIPO { get; set; }
        [DataMember(Name = "CodigoExterno", Order = 31, EmitDefaultValue = true)]
        public string CodigoExterno { get; set; }
        [DataMember(Name = "LstCampoUsuario", Order = 32, EmitDefaultValue = true)]
        public List<CampoUsuario> LstCampoUsuario { get; set; }
        [DataMember(Name = "Reference2", Order = 33, EmitDefaultValue = true)]
        public string Reference2 { get; set; }
        public enum EnuTypeSell { Normal = 0, Employees = 1 };
        [DataMember(Name = "TypeSell", Order = 34, EmitDefaultValue = true)]
        public EnuTypeSell TypeSell { get; set; }
        public ORDR()
        {
            this.CardCode = string.Empty;
            this.CardName = string.Empty;
            this.DocDate = string.Empty;
            this.DocDueDate = string.Empty;
            this.DocTotal = 0;
            this.LstRDR1 = new List<RDR1>();
            this.Comments = string.Empty;
            this.SalesPersonCode = 0;
            this.U_LGS_TDOC = string.Empty;
            this.U_LGS_SDOC = string.Empty;
            this.U_LGS_NUCE = string.Empty;
            this.U_RDC_EfSo = 0;
            this.U_RDC_EfDo = 0;
            this.U_RDC_EnSo = 0;
            this.U_RDC_EnDo = 0;
            this.U_RDC_VuSo = 0;
            this.U_RDC_VuDo = 0;
            this.U_RDC_MonV = string.Empty;
            this.U_RDC_TjSo = 0;
            this.U_RDC_TjDo = 0;
            this.CodAlmacen = string.Empty;
            this.LstRCT3 = new List<RCT3>();
            this.U_GS_EsExt = string.Empty;
            this.U_GS_NAC = string.Empty;
            this.TaxDate = string.Empty;
            this.U_LGS_TIPO = TIPOMOV.VEN;
            this.LstCampoUsuario = new List<CampoUsuario>();
            this.Reference2 = string.Empty;
        }
    }
}
