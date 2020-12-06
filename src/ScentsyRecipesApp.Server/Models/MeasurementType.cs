using Microsoft.AspNetCore.Http;
using ScentsyRecipesApp.Library;
using ScentsyRecipesApp.Library.Extensions.System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
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
    [Table("MeasurementTypes")]
    public class MeasurementType : BaseModel
    {
        #region Public Properties
        /// <summary>
        /// A full name descriptor of the current Unit.
        /// Generally only used when selecting a 
        /// <see cref="Ingredient.DefaultMeasurementType"/> value.
        /// </summary>
        [Required(ErrorMessage = "Name required.")]
        [StringLength(15, ErrorMessage = "Name should be less than or equal to fifteen characters.")]
        [Column("Name")]
        public String Name { get; set; }

        /// <summary>
        /// The current Unit's Abbreviation, used when 
        /// rendering a full <see cref="Recipe"/>.
        /// </summary>
        [Required(ErrorMessage = "Abbreviation required.")]
        [StringLength(5, ErrorMessage = "Abbreviation should be less than or equal to five characters.")]
        [RegularExpression(@"[a-zA-Z]+", ErrorMessage = "Abbreviation may only contain alpha numeric characters.")]
        [Column("Abbreviation")]
        public String Abbreviation { get; set; }
        #endregion

        #region Helper Methods
        /// <inheritdoc />
        protected override void Read(SqlDataReader reader)
        {
            base.Read(reader);

            this.Name = Convert.ToString(reader["Name"]);
            this.Abbreviation = Convert.ToString(reader["Abbreviation"]);
        }

        /// <inheritdoc />
        public override void Read(IFormCollection form)
        {
            this.Name = form["Name"];
            this.Abbreviation = form["Abbreviation"];
        }
        #endregion
    }
}
