using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SSC.Entity
{
    [DataContract]
    public class OITM
    {
        [DataMember(Name = "StyleName", Order = 1, EmitDefaultValue = true)]
        public string ItemCode { get; set; }
        [DataMember(Name = "Desc1", Order = 2, EmitDefaultValue = true)]
        public string ItemName { get; set; }
        [DataMember(Name = "Desc2", Order = 3, EmitDefaultValue = true)]
        public string FrgnName { get; set; }
        [DataMember(Name = "Desc3", Order = 4, EmitDefaultValue = true)]
        public string Vigente { get; set; }
        [DataMember(Name = "BrandCode", Order = 5, EmitDefaultValue = true)]
        public Int32 FirmCode { get; set; }
        [DataMember(Name = "BrandName", Order = 6, EmitDefaultValue = true)]
        public string FirmName { get; set; }
        [DataMember(Name = "UDF1Description", Order = 7, EmitDefaultValue = true)]
        public string BuyUnitMsr { get; set; }
        [DataMember(Name = "DeptCode", Order = 8, EmitDefaultValue = true)]
        public int CodigoDep { get; set; }
        [DataMember(Name = "DeptName", Order = 9, EmitDefaultValue = true)]
        public string DescDep { get; set; }
        [DataMember(Name = "ClassCode", Order = 10, EmitDefaultValue = true)]
        public string CodigoClase { get; set; }
        [DataMember(Name = "ClassName", Order = 11, EmitDefaultValue = true)]
        public string DescClase { get; set; }
        [DataMember(Name = "SubClassCode", Order = 12, EmitDefaultValue = true)]
        public string CodigoSubClase { get; set; }
        [DataMember(Name = "SubClassName", Order = 13, EmitDefaultValue = true)]
        public string DescSubClase { get; set; }
        [DataMember(Name = "Cost", Order = 14, EmitDefaultValue = true)]
        public decimal LastPurPrcS { get; set; }
        [DataMember(Name = "FcCost", Order = 15, EmitDefaultValue = true)]
        public decimal LastPurPrcD { get; set; }
        [DataMember(Name = "RetailPrice", Order = 15, EmitDefaultValue = true)]
        public decimal Price { get; set; }
        [DataMember(Name = "UPC", Order = 16, EmitDefaultValue = true)]
        public string CodeBars { get; set; }
        [DataMember(Name = "ALU", Order = 17, EmitDefaultValue = true)]
        public string ALU { get; set; }
        [DataMember(Name = "TypeCode", Order = 18, EmitDefaultValue = true)]
        public string TypeCode { get; set; }
        [DataMember(Name = "LstCampoUsuario", Order = 19, EmitDefaultValue = true)]
        public List<CampoUsuario> LstCampoUsuario { get; set; }
        public OITM()
        {
            this.ItemCode = string.Empty;
            this.ItemName = string.Empty;
            this.FrgnName = string.Empty;
            this.Vigente = string.Empty;
            this.FirmCode = 0;
            this.FirmName = string.Empty;
            this.BuyUnitMsr = string.Empty;
            this.CodigoDep = 0;
            this.DescDep = string.Empty;
            this.CodigoClase = string.Empty;
            this.DescClase = string.Empty;
            this.CodigoSubClase = string.Empty;
            this.DescSubClase = string.Empty;
            this.LastPurPrcS = 0;
            this.LastPurPrcD = 0;
            this.Price = 0;
            this.CodeBars = string.Empty;
            this.ALU = string.Empty;
            this.TypeCode = string.Empty;
            this.LstCampoUsuario = new List<CampoUsuario>();
        }
    }

}
