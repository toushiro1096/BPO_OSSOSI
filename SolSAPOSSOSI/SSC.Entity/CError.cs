using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SSC.Entity
{
    [DataContract]
    public class CError
    {
        [DataMember(Name = "Result", Order = 1, EmitDefaultValue = true)]
        public int Result { get; set; }
        [DataMember(Name = "Error", Order = 2, EmitDefaultValue = true)]
        public string Error { get; set; }
        [DataMember(Name = "DocEntry", Order = 3, EmitDefaultValue = true)]
        public string DocEntry { get; set; }
        [DataMember(Name = "JrnlMemo", Order = 4, EmitDefaultValue = true)]
        public string JrnlMemo { get; set; }
        [DataMember(Name = "CodigoExterno", Order = 5, EmitDefaultValue = true)]
        public string CodigoExterno { get; set; }
        [DataMember(Name = "Result2", Order = 6, EmitDefaultValue = true)]
        public int Result2 { get; set; }
        [DataMember(Name = "Error2", Order = 7, EmitDefaultValue = true)]
        public string Error2 { get; set; }
        [DataMember(Name = "DocEntry2", Order = 8, EmitDefaultValue = true)]
        public string DocEntry2 { get; set; }
        [DataMember(Name = "JrnlMemo2", Order = 9, EmitDefaultValue = true)]
        public string JrnlMemo2 { get; set; }
        [DataMember(Name = "CodigoExterno2", Order = 10, EmitDefaultValue = true)]
        public string CodigoExterno2 { get; set; }
        [DataMember(Name = "LstCodigoExterno", Order = 11, EmitDefaultValue = true)]
        public List<string> LstCodigoExterno { get; set; }

        public CError()
        {
            Result = 0;
            Error = string.Empty;
            DocEntry = string.Empty;
            JrnlMemo = string.Empty;
            CodigoExterno = string.Empty;
            Result2 = 0;
            Error2 = string.Empty;
            DocEntry2 = string.Empty;
            JrnlMemo2 = string.Empty;
            CodigoExterno2 = string.Empty;
            LstCodigoExterno = new List<string>();
        }
        public CError(int pResult, string pError, string pDocEntry, string pJrnlMemo, string pCodigoExterno)
        {
            Result = pResult;
            Error = pError;
            DocEntry = pDocEntry;
            JrnlMemo = pJrnlMemo;
            CodigoExterno = pCodigoExterno;
            Result2 = 0;
            Error2 = string.Empty;
            DocEntry2 = string.Empty;
            JrnlMemo2 = string.Empty;
            CodigoExterno2 = string.Empty;
            LstCodigoExterno = new List<string>();
        }
        public CError(string pJrnlMemo, string pCodigoExterno)
        {
            Result = -1;
            Error = string.Empty;
            DocEntry = string.Empty;
            JrnlMemo = pJrnlMemo;
            CodigoExterno = pCodigoExterno;
            Result2 = 0;
            Error2 = string.Empty;
            DocEntry2 = string.Empty;
            JrnlMemo2 = string.Empty;
            CodigoExterno2 = string.Empty;
            LstCodigoExterno = new List<string>();
        }

    }
}
