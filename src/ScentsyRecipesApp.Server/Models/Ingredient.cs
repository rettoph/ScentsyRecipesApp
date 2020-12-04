using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScentsyRecipesApp.Server.Models
{
    /// <summary>
    /// General model containing simple ingredient
    /// data. Just to save on DP space we will store
    /// ingredient names seperatly and reference them
    /// within <see cref="RecipeIngredient"/>s when
    /// creating new <see cref="Recipe"/>.
    /// </summary>
    public class Ingredient : BaseModel
    {
        #region Public Properties
        /// <summary>
        /// A unique 
        /// </summary>
        public Int32 Id { get; set; }

        /// <summary>
        /// A custom human readable nameof the current
        /// <see cref="Ingredient"/>
        /// </summary>
        [Required(ErrorMessage = "Enter Ingredient Name")]
        [StringLength(15, ErrorMessage = "Ingredient name should be less than or equal to fifteen characters.")]
        public String Name { get; set; }

        /// <summary>
        /// The default unit to be used with this
        /// particular ingredient within a
        /// <see cref="RecipeIngredient"/>
        /// if an ovveride is node defined by
        /// <see cref="RecipeIngredient.UnitOfMeasurement"/>.
        /// </summary>
        [Required(ErrorMessage = "Set Ingredient's Default Unit of Measurement.")]
        public UnitOfMeasurement DefaultUnitOfMeasurement { get; set; }
        #endregion
    }
}
