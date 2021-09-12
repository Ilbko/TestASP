using Dapper;
using System;
using System.Data;
using System.Data.SqlClient;
using TestASP.Models.Base;

namespace TestASP.Models.Repositories
{
    public class AuthInfoRepository : IDisposable
    {
        private readonly string connStr = "Data Source=SQL5102.site4now.net;Initial Catalog=db_a78c05_tractor;User Id=db_a78c05_tractor_admin;Password=wXq-9Nf-a89-GsR";

        public void Insert(AuthInfo authInfo)
        {
            string procedure = "EXEC [AuthInfo_Insert] @AuthInfo_Time, @AuthInfo_Date";
            var values = new { AuthInfo_Time = authInfo.AuthInfo_Time, AuthInfo_Date = authInfo.AuthInfo_Date };

            using (IDbConnection db = new SqlConnection(connStr))
            {
                db.Open();

                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        db.Query(procedure, values, transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public void Dispose() => GC.SuppressFinalize(true);       
    }
}
