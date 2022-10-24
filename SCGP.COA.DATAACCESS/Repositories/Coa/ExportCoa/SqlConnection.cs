using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.Data.SqlClient;
using SCGP.COA.COMMON.Attributes;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SCGP.COA.DATAACCESS.Repositories.Coa.ExportCoa
{
    public class SqlConnectDb
    {
        public static DataTable SP_GEToDbTbl(string ptSql, string ptConStr)
        {
            try
            {
                var oDbTbl = new DataTable();
                var oDbCon = new SqlConnection(ptConStr);
                oDbCon.Open();
                var oDbAdt = new SqlDataAdapter(ptSql, oDbCon);
                oDbAdt.Fill(oDbTbl);
                oDbCon.Close();
                return oDbTbl;
            }
            catch (SqlException oEx)
            {
                throw oEx;
            }
        }
        public static DataTable SP_CallSP( string ptConStr, string ptSPName, string? ptBatch)
        {
            try
            {
                var oDbTbl = new DataTable();
                using SqlConnection con = new SqlConnection(ptConStr);
                using SqlCommand cmd = new SqlCommand(ptSPName, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@BATCH_NUMBER", SqlDbType.VarChar).Value = ptBatch;
                con.Open();
                cmd.ExecuteNonQuery();
                var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(oDbTbl);
                return oDbTbl;
            }
            catch (Exception)
            {
                throw;
            }
           
        }
    }
}
