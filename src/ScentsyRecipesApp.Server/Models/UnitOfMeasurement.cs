using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ScentsyRecipesApp.Server.Models
{
    /// <summary>
    /// Simple reference to the unit types
    /// <see cref="Ingredient"/> and 
    /// <see cref="RecipeIngredient"/> may 
    /// be partitioned into.
    /// </summary>
    public class UnitOfMeasurement : BaseModel
    {
        #region Public Properties
        /// <summary>
        /// A full name descriptor of the current Unit.
        /// Generally only used when selecting a 
        /// <see cref="Ingredient.DefaultUnitOfMeasurement"/> value.
        /// </summary>
        [Required(ErrorMessage = "Measurement Name")]
        [StringLength(15, ErrorMessage = "Measurement name should be less than or equal to fifteen characters.")]
        public String FullName { get; set; }

        /// <summary>
        /// The current Unit's Abbreviation, used when 
        /// rendering a full <see cref="Recipe"/>.
        /// </summary>
        [Required(ErrorMessage = "Measurement Abbreviation")]
        [StringLength(5, ErrorMessage = "Measurement abbreviation should be less than or equal to five characters.")]
        [RegularExpression(@"\w+", ErrorMessage = "Measurement abbreviation may only contain alpha numeric characters.")]
        public String Abbreviation { get; set; }
        #endregion
    }
}
