using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace SC.Model.Common.Connection
{
    internal class DbConnection : DbCon, IDisposable
    {
        #region DbDataVariables
        internal static readonly DbData DbSys = new DbData()
        {
            Name = "dbSigloCont",
            Host = "localhost",
            Port = "1433",
            User = "sa",
            Password = "P@$$w0rd",
            Engine = DbEngine.MsSQL
        };
        internal static readonly DbData DbAud = new DbData()
        {
            Name = "dbSigloCont_Aud",
            Host = "localhost",
            Port = "1433",
            User = "sa",
            Password = "P@$$w0rd",
            Engine = DbEngine.MsSQL
        };
        #endregion

        #region Variables
        private System.Data.Common.DbConnection con = null;
        private string conStr = null;
        private Action<DbCommand> devPrm = null;
        private Func<DbParameter, string> prpNamFrmPrm = null;
        #endregion

        #region Properties
        protected override System.Data.Common.DbConnection Con => con;
        protected override string ConStr => conStr;
        public override bool ReadPropertiesFromParameters { get; set; } = true;
        public override bool SaveDerivedParameters { get; set; } = true;
        #endregion

        #region Methods
        protected override string GetErrorFromException(Exception ex)
        {
            return base.GetErrorFromException(ex);
        }

        protected override void GetParametersFromDb(DbCommand Command)
        {
            devPrm?.Invoke(Command);
        }
        protected override string PropertyNameFromDbParameter(DbParameter Parameter)
        {
            return prpNamFrmPrm?.Invoke(Parameter);
        }

        internal static void BeginTransactions(params DbConnection[] ConnectionArray)
        {
            foreach (var Con in ConnectionArray)
                Con.BeginTransaction();
        }
        internal static void CommitTransactions(params DbConnection[] ConnectionArray)
        {
            foreach (var Con in ConnectionArray)
                Con.CommitTransaction();
        }
        internal static void AbortTransactions(params DbConnection[] ConnectionArray)
        {
            foreach (var Con in ConnectionArray)
                Con.AbortTransaction();
        }

        public void Dispose()
        {
            CloseConnection();
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Constructors
        public DbConnection()
        {
            DbData Data = new DbData()
            {
                Host = "200.14.248.248,1440", //"10.0.0.12",
                Engine = DbEngine.MsSQL,
                //Name = "OssosiFtp_Wrk",
                Name = "OssosiFtp",
                User = "sa",
                Password ="P@ssw0rd" //"abcDEF123"
            };

            DbName = Data.Host + "_" + Data.Name;

            SaveDerivedParameters = false;
            ReadPropertiesFromParameters = true;

            switch (Data.Engine)
            {
                case DbEngine.MsSQL:
                    con = new SqlConnection();
                    conStr = $"Data Source={Data.Host};Initial Catalog={Data.Name};Persist Security Info=True;User ID={Data.User};Password={Data.Password}";
                    devPrm = (Command) => SqlCommandBuilder.DeriveParameters(Command as SqlCommand);
                    prpNamFrmPrm = (Parameter) => Parameter.ParameterName.Substring(1);
                    break;
            }

            OpenConnection();
        }
        ~DbConnection()
        {
            Dispose();
        }
        #endregion
    }
}
