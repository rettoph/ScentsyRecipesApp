using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ScentsyRecipesApp.Server.Models
{
    public class RecipeIngredient : BaseModel
    {
        #region Public Properties
        /// <summary>
        /// The <see cref="Models.Recipe"/> this current
        /// <see cref="RecipeIngredient"/> is belongs
        /// to.
        /// </summary>
        public Recipe Recipe { get; set; }

        /// <summary>
        /// The <see cref="Ingredient"/> this current
        /// <see cref="RecipeIngredient"/> is belongs
        /// to.
        /// </summary>
        public Ingredient Ingredient { get; set; }

        /// <summary>
        /// The total amount of <see cref="Ingredient"/>
        /// required when building the current
        /// <see cref="Recipe"/>.
        /// </summary>
        public Single Amount { get; set; }

        /// <summary>
        /// The unit of measurement used by
        /// the current <see cref="Ingredient"/>. If null,
        /// <see cref="Ingredient.DefaultUnitOfMeasurement"/>
        /// will be used instead.
        /// </summary>
        public UnitOfMeasurement UnitOfMeasurement { get; set; }
        #endregion


        #region Constructors
        /// <summary>
        /// Build a new <see cref="Recipe"/> instance from 
        /// recieved <see cref="Ingredient"/> data passed 
        /// via <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader"></param>
        public RecipeIngredient(SqlDataReader reader) : base(reader)
        {

        }
        #endregion
    }
}
