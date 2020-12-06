using Microsoft.AspNetCore.Mvc;
using ScentsyRecipesApp.Library;
using ScentsyRecipesApp.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScentsyRecipesApp.Server.Controllers
{
    /// <summary>
    /// A simple Controller designed to handle
    /// API specific endpoints.
    /// 
    /// This is primarily used when creating a
    /// <see cref="Recipe"/> instance.
    /// </summary>
    [Route("api")]
    public class ApiController : Controller
    {
        #region Private Fields
        private ModelContext<Recipe> _recipes;
        private ModelContext<Ingredient> _ingredients;
        private ModelContext<MeasurementType> _measurementType;
        #endregion

        #region Constructors
        public ApiController(
            ModelContext<Recipe> recipes,
            ModelContext<Ingredient> ingredients,
            ModelContext<MeasurementType> measurementType)
        {
            _recipes = recipes;
            _ingredients = ingredients;
            _measurementType = measurementType;
        }
        #endregion

        [Route("measurement-types")]
        public IActionResult MeasurementTypes()
        {
            return Json(_measurementType.All());
        }
    }
}
