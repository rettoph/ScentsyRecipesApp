using ScentsyRecipesApp.Server.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ScentsyRecipesApp.Server.Extensions.System.Data.SqlClient
{
    /// <summary>
    /// Implement simple helper methods to ease the use
    /// of a <see cref="SqlConnection"/>.
    /// </summary>
    public static class SqlConnectionExtensions
    {
        /// <summary>
        /// Helper method that will utilize reflection
        /// and attempt to create <typeparamref name="T"/>
        /// instances based on the recieved Sql <paramref name="query"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="BaseModel"/> implementation to be returned.</typeparam>
        /// <param name="con">The current connection.</param>
        /// <param name="query">The select query to be excecuted.</param>
        /// <param name="query">The <see cref="CommandType"/> to be utilized when build a <see cref="SqlCommand"/> instance.</param>
        /// <returns></returns>
        public static IEnumerable<T> Select<T>(this SqlConnection con, String query, CommandType type = CommandType.Text)
            where T : BaseModel
        {
            // Build a new command instance...
            var command = new SqlCommand(query, con);
            command.CommandType = type;

            // Create a new data reader...
            var reader = command.ExecuteReader();

            while (reader.Read()) // While there is data to be read, create & return a T instance
                yield return Activator.CreateInstance(typeof(T), reader) as T;
        }
    }
}
