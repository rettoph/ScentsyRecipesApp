using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ScentsyRecipesApp.Server.Models
{
    /// <summary>
    /// The Primary Recipe model. This will have many
    /// <see cref="RecipeIngredient"/>, contain steps,
    /// and more.
    /// </summary>
    public class Recipe : BaseModel
    {
        #region Constructors
        /// <summary>
        /// Build a new <see cref="Recipe"/> instance from 
        /// recieved <see cref="SqlDataReader"/> data passed 
        /// via <paramref name="reader"/>.
        /// </summary>
        /// <param name="reader"></param>
        public Recipe(SqlDataReader reader) : base(reader)
        {

        }
        #endregion
    }
}
