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
    [Table("RecipeIngredients")]
    public class RecipeIngredient : BaseModel
    {
        #region Private Fields
        /// <summary>
        /// <see cref="ModelContext{TModel}"/> instance for 
        /// <see cref="Recipe"/>. Used to manage the internal
        /// <see cref="Recipe"/> belongsTo relationship.
        /// </summary>
        private ModelContext<Recipe> _recipes;

        /// <summary>
        /// <see cref="ModelContext{TModel}"/> instance for 
        /// <see cref="Ingredient"/>. Used to manage the internal
        /// <see cref="Ingredient"/> belongsTo relationship.
        /// </summary>
        private ModelContext<Ingredient> _ingredients;

        /// <summary>
        /// <see cref="ModelContext{TModel}"/> instance for 
        /// <see cref="MeasurementType"/>. Used to manage the internal
        /// <see cref="MeasurementType"/> belongsTo relationship.
        /// </summary>
        private ModelContext<MeasurementType> _measurementTypes;
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
        /// The <see cref="Ingredient"/> this current
        /// <see cref="RecipeIngredient"/> is belongs
        /// to.
        /// </summary>
        [Column("IngredientId")]
        public Ingredient Ingredient { get; set; }

        /// <summary>
        /// The total amount of <see cref="Ingredient"/>
        /// required when building the current
        /// <see cref="Recipe"/>.
        /// </summary>
        [Column("Amount")]
        public Single Amount { get; set; }

        /// <summary>
        /// The measurement type used by
        /// the current <see cref="Ingredient"/>.
        /// </summary>
        [Column("MeasurementTypeId")]
        public MeasurementType MeasurementType { get; set; }
        #endregion

        #region Constructors
        public RecipeIngredient(
            ModelContext<Recipe> recipes,
            ModelContext<Ingredient> ingredients,
            ModelContext<MeasurementType> measurementTypes)
        {
            _recipes = recipes;
            _ingredients = ingredients;
            _measurementTypes = measurementTypes;
        }
        #endregion

        #region Helper Methods
        /// <inheritdoc />
        protected override void Read(SqlDataReader reader)
        {
            base.Read(reader);

            this.Amount = Convert.ToSingle(reader["Amount"]);

            // Load all relationship instances...
            this.Recipe = _recipes.GetById(Convert.ToInt32(reader["RecipeId"]));
            this.Ingredient = _ingredients.GetById(Convert.ToInt32(reader["IngredientId"]));
            this.MeasurementType = _measurementTypes.GetById(Convert.ToInt32(reader["MeasurementTypeId"]));
        }

        /// <inheritdoc />
        public override void Read(IFormCollection form)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
