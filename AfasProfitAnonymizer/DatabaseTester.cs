using System.Data.SqlClient;

namespace AfasProfitAnonymizer
{
    internal static class DatabaseTester
    {
        internal static int TestConnection(string serverName, string targetDatabase, string compareDatabase, bool shouldCompare)
        {

            if(serverName.Equals(string.Empty) || targetDatabase.Equals(string.Empty) || (!shouldCompare && compareDatabase.Equals(string.Empty)))
            {
                return -1;
            }

            int errorMessage = 0;

            SqlConnectionStringBuilder connectionBuilder = new SqlConnectionStringBuilder();
            connectionBuilder.DataSource = serverName;
            connectionBuilder.InitialCatalog = targetDatabase;
            connectionBuilder.IntegratedSecurity = true;

            using (SqlConnection connection = new SqlConnection(connectionBuilder.ConnectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (SqlException)
                {
                    return 1;
                }

                try
                {
                    SqlCommand command = new SqlCommand("SELECT TOP(1) AfasKnName FROM AfasKnBasicContact");
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    errorMessage += 2;
                }

                if (!shouldCompare)
                {
                    try
                    {
                        connection.Close();
                        SqlCommand command = new SqlCommand("SELECT TOP(1) AfasKnName FROM AfasKnBasicContact");
                        connectionBuilder.InitialCatalog = compareDatabase;
                        connection.ConnectionString = connectionBuilder.ConnectionString;
                        command.Connection = connection;
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException)
                    {
                        errorMessage += 3;
                    }
                }

                connection.Close();
            }

            return errorMessage;
                
        }
    }
}
