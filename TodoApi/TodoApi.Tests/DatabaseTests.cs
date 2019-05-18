using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Transactions;

namespace TodoApi.Tests
{
    public class DatabaseTests
    {
        
        public const string ConnectionString = @"Data Source=DESKTOP-BGH8GGH\SQLEXPRESS01;Initial Catalog=Todo;Integrated Security=True";
        private TransactionScope transaction;

        [TestInitialize]
        public void SetupData()
        {
            transaction = new TransactionScope();

            string sql = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "testdata.sql"));

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }
        [TestCleanup]
        public void CleanupData()
        {
            transaction.Dispose();
        }

        public int GetRowCount(string table)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {table}", conn);

                int rowCount = Convert.ToInt32(cmd.ExecuteScalar());

                return rowCount;
            }
        }
    }
}
