using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ScentsyRecipesApp.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ScentsyRecipesApp.Server.Models
{
    /// <summary>
    /// Represets a single step within a <see cref="Recipe"/>
    /// </summary>
    [Table("RecipeSteps")]
    public class RecipeStep : BaseModel
    {
        #region Private Fields
        /// <summary>
        /// <see cref="ModelContext{TModel}"/> instance for 
        /// <see cref="Recipe"/>. Used to manage the internal
        /// <see cref="Recipe"/> belongsTo relationship.
        /// </summary>
        private ModelContext<Recipe> _recipes;
        #endregion

        #region Public Properties
        /// <summary>
        /// The <see cref="Models.Recipe"/> this current
        /// <see cref="RecipeIngredient"/> is belongs
        /// to.
        /// </summary>
        [Column("RecipeId")]
        [JsonIgnore]
        public Recipe Recipe { get; set; }

        /// <summary>
        /// The full text value of the current step.
        /// </summary>
        [Column("Text")]
        public String Text { get; set; }
        #endregion

        #region Constructors
        public RecipeStep(
            ModelContext<Recipe> recipes)
        {
            _recipes = recipes;
        }
        #endregion

        #region Helper Methods
        /// <inheritdoc />
        protected override void Read(SqlDataReader reader)
        {
            base.Read(reader);

            this.Text = Convert.ToString(reader["Text"]);

            // Load all relationship instances...
            this.Recipe = _recipes.GetById(Convert.ToInt32(reader["RecipeId"]));
        }

        /// <inheritdoc />
        public override void Read(IFormCollection form)
        {
            this.Text = form["Text"];
        }
        #endregion
    }
}
