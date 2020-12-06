using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ScentsyRecipesApp.Library.Extensions.System.Data.SqlClient
{
    /// <summary>
    /// Implement simple helper methods to ease the use
    /// of a <see cref="SqlConnection"/>.
    /// </summary>
    public static class SqlConnectionExtensions
    {
        /// <summary>
        /// Helper method that allows us to easily excecute
        /// a query & returns an enumerable for every row
        /// of data readable from a <see cref="SqlDataReader"/>
        /// instance. Note, the excecution of the query is lazily
        /// preformed based on the parsing of results.
        /// </summary>
        /// <param name="con">The current connection.</param>
        /// <param name="query">The query to be excecuted.</param>
        /// <param name="query">The <see cref="CommandType"/> to be utilized when build a <see cref="SqlCommand"/> instance.</param>
        /// <returns></returns>
        public static IEnumerable<SqlDataReader> Query(this SqlConnection con, String query, CommandType type = CommandType.Text)
        {
            // Build a new command instance...
            var command = new SqlCommand(query, con);
            command.CommandType = type;

            // Create a new data reader...
            using(var reader = command.ExecuteReader())
            {
                while (reader.Read()) // While there is data to be read, return the reader.
                    yield return reader;

                reader.Close();
            }
        }

        /// <summary>
        /// Non lazy helper method that allows us to easily excecute
        /// a query & returns an enumerable for every row
        /// of data readable from a <see cref="SqlDataReader"/>
        /// instance.
        /// </summary>
        /// <param name="con">The current connection.</param>
        /// <param name="query">The query to be excecuted.</param>
        /// <param name="query">The <see cref="CommandType"/> to be utilized when build a <see cref="SqlCommand"/> instance.</param>
        public static void Execute(this SqlConnection con, String query, CommandType type = CommandType.Text)
        {
            // Build a new command instance...
            var command = new SqlCommand(query, con);
            command.CommandType = type;

            using (var reader = command.ExecuteReader())
            {
                reader.Close();
            }
        }
    }
}
