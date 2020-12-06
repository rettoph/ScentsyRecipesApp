using Microsoft.AspNetCore.Http;
using ScentsyRecipesApp.Library;
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
    /// General model containing simple ingredient
    /// data. Just to save on DP space we will store
    /// ingredient names seperatly and reference them
    /// within <see cref="RecipeIngredient"/>s when
    /// creating new <see cref="Recipe"/>.
    /// </summary>
    [Table("Ingredients")]
    public class Ingredient : BaseModel
    {
        #region Public Properties
        /// <summary>
        /// A custom human readable nameof the current
        /// <see cref="Ingredient"/>
        /// </summary>
        [Required(ErrorMessage = "Name required.")]
        [StringLength(150, ErrorMessage = "Name should be less than or equal to 150 characters.")]
        [Column("Name")]
        public String Name { get; set; }
        #endregion

        #region Helper Methods
        /// <inheritdoc />
        protected override void Read(SqlDataReader reader)
        {
            base.Read(reader);

            this.Name = Convert.ToString(reader["Name"]);
        }

        /// <inheritdoc />
        public override void Read(IFormCollection form)
        {
            this.Name = form["Name"];
        }
        #endregion
    }
}
