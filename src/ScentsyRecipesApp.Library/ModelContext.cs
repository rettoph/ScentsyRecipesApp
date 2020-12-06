using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using ScentsyRecipesApp.Library.Extensions.System.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;
using System.Reflection;
using System.Diagnostics;

namespace ScentsyRecipesApp.Library
{
    /// <summary>
    /// Provides some basic model binding in an effort
    /// to emulate EF Core. While its not nearly as
    /// functional, this app doesnt require any crazy
    /// actions so its fine.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class ModelContext<TModel>
        where TModel : BaseModel
    {
        #region Private Fields
        /// <summary>
        /// The current scope's provider. Generally passed 
        /// into <see cref="BaseModel.Read(SqlDataReader, IServiceProvider)"/>
        /// in case the <see cref="{TModel}"/> instance has
        /// any relationships that should be pulled from
        /// the <see cref="IServiceProvider"/>.
        /// </summary>
        private IServiceProvider _provider;

        /// <summary>
        /// The current active db connection,
        /// used to query data.
        /// </summary>
        private SqlConnection _connection;

        /// <summary>
        /// A cache of loaded TModel objects broken by the 
        /// raw Id value recieved by a <see cref="SqlDataReader"/>.
        /// </summary>
        private Dictionary<Object, TModel> _cache;

        /// <summary>
        /// The table mapped to the recieved 
        /// <see cref="{TModel}"/> type.
        /// 
        /// Simply add a <see cref="TableAttribute"/>
        /// attribute to the <see cref="{TModel}"/>
        /// class.
        /// </summary>
        private String _table;

        /// <summary>
        /// A list of all properties containing mapped columns within
        /// the defined <see cref="{TModel}"/>. This will be used to
        /// auto general update/create queries.
        /// </summary>
        private Dictionary<PropertyInfo, String> _columns;
        #endregion

        #region Constructor
        public ModelContext(IServiceProvider provider, SqlConnection connection)
        {
            _provider = provider;
            _connection = connection;
            _table = (typeof(TModel).GetCustomAttributes(false).FirstOrDefault(attr => attr is TableAttribute) as TableAttribute).Name;
            _columns = typeof(TModel).GetProperties()
                .Where(pi => pi.GetCustomAttribute<ColumnAttribute>() != default)
                .ToDictionary(
                    keySelector: pi => pi,
                    elementSelector: pi => pi.GetCustomAttribute<ColumnAttribute>().Name);
            _cache = new Dictionary<Object, TModel>();
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Return all instances.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TModel> All()
            => this.Where("1=1");

        /// <summary>
        /// Return all models that match the recieved
        /// where clause.
        /// </summary>
        /// <param name="whereClause">Partial SQL query that takes place immediately after the WHERE keyword in a full query.</param>
        /// <returns></returns>
        public IEnumerable<TModel> Where(String whereClause)
        {
            foreach (SqlDataReader reader in _connection.Query($"SELECT * FROM {_table} WHERE {whereClause};"))
            { // For each row returned by the query...
                // Load the raw id once.
                var rawId = reader["Id"];

                if (!_cache.ContainsKey(rawId))
                { // If there is not cached model with this id yet...
                    // Create a new instance & cache it...
                    _cache[rawId] = ActivatorUtilities.CreateInstance<TModel>(_provider);
                    // Read the row data into the cached value...
                    _cache[rawId].Read(reader);
                }

                // Return the cached value.
                yield return _cache[rawId];
            }
        }

        /// <summary>
        /// Create a brand new db entry
        /// base off the recieved instance.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public TModel Insert(TModel instance)
        {
            if (instance.Real) // Ensure that the model isntance doesnt already exist in the db
                throw new Exception("Unable to insert pre-existant instances. Run Update() instead.");

            // Next, construct the insert query components...
            var columns = String.Join(',', _columns.Values);
            var values = String.Join(',', _columns.Keys.Select(pi => $"'{pi.GetValue(instance)}'"));

            // Insert the column then import the full data back into the instance.
            foreach(var reader in _connection.Query($"INSERT INTO {_table} ({columns}) OUTPUT INSERTED.* VALUES({values});"))
                instance.Read(reader);

            // Alert the instance it is being inserted...
            instance.Inserted();
            instance.Updated();

            return instance;
        }

        public TModel Update(TModel instance)
        {
            if (!instance.Real) // Ensure that the model instance already exists in the db
                throw new Exception("Unable to update non-existant instances. Run Insert() first.");

            // Next, construct the update query components...
            var set = String.Join(',', _columns.Select(kvp => $"{kvp.Value}='{kvp.Key.GetValue(instance)}'"));

            // Update the row values...
            _connection.Execute($"UPDATE {_table} SET {set} WHERE Id={instance.Id};");

            // Alert the intance is it being updated...
            instance.Updated();

            return instance;
        }

        /// <summary>
        /// Simple helper method that will either update or
        /// insert incoming values as needed.
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public TModel UpdateOrInsert(TModel instance)
        {
            if (instance.Real)
                return this.Update(instance);
            else
                return this.Insert(instance);
        }

        public void Delete(TModel instance)
        {
            if (!instance.Real) // Ensure that the model instance already exists in the db
                throw new Exception("Unable to delete non-existant instances. Run Insert() first.");

            _connection.Execute($"DELETE FROM {_table} WHERE Id={instance.Id}");
            instance.Dispose();
        }

        public void DeleteMany(IEnumerable<TModel> instances)
        {
            foreach (TModel instance in instances.ToList())
                if(instance.Real)
                    this.Delete(instance);
        }

        /// <summary>
        /// Find & return a specific instance by the recieved id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TModel GetById(Int32 id)
        {
            if (_cache.ContainsKey(id))
                return _cache[id];

            return this.Where("Id=" + id).FirstOrDefault();
        }

        /// <summary>
        /// Find & return a specific instance by the recieved id.
        /// If there is no item at that id, create a new
        /// unlinked instance.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TModel GetByIdOrDefault(Int32 id)
            => this.GetById(id) ?? this.Create();

        /// <summary>
        /// Create a new unlicked instance of the described 
        /// <see cref="TModel"/>. This instance is not yet saved
        /// to the database.
        /// </summary>
        /// <returns></returns>
        public TModel Create(Action<TModel> setup = null)
        {
            var instance = ActivatorUtilities.CreateInstance<TModel>(_provider); ;
            setup?.Invoke(instance);

            return instance;
        }
        #endregion
    }
}
