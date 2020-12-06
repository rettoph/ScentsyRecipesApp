using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ScentsyRecipesApp.Library
{
    /// <summary>
    /// A base model conataining default implementation
    /// used by all custom models within the current 
    /// WebApp.
    /// </summary>
    public abstract class BaseModel : IDisposable
    {
        #region Public Properties
        /// <summary>
        /// The unique identifier & primary key.
        /// </summary>
        public Int32 Id { get; private set; }

        /// <summary>
        /// Indicates that the current instance exists
        /// in the database.
        /// </summary>
        public Boolean Real { get; private set; }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Used when loading new data from the database
        /// within a <see cref="ModelContext{TModel}"/> 
        /// instance. This should update the internal object
        /// & reflect its changes as needed.
        /// </summary>
        /// <param name="reader"></param>
        protected internal virtual void Read(SqlDataReader reader)
        {
            this.Real = true;
            this.Id = Convert.ToInt32(reader["Id"]);
        }

        /// <summary>
        /// Simple helper method automatically invoked when the
        /// model was inserted within the database via
        /// <see cref="ModelContext{TModel}.Insert(TModel)"/>.
        /// </summary>
        protected internal virtual void Inserted()
        {

        }

        /// <summary>
        /// Simple helper method automatically invoked when the
        /// the model was updated within the database via
        /// <see cref="ModelContext{TModel}.Update(TModel)" />
        /// </summary>
        protected internal virtual void Updated()
        {

        }

        /// <summary>
        /// Update internal values based on recieved form data.
        /// </summary>
        /// <param name="form"></param>
        public abstract void Read(IFormCollection form);

        /// <summary>
        /// The default string method will be the current 
        /// <see cref="Id"/> value, used when writing
        /// foreign keys to the database.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            => this.Id.ToString();

        public virtual void Dispose()
        {
            this.Real = false;
            this.Id = default;
        }
        #endregion
    }
}
