using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ScentsyRecipesApp.Library;
using ScentsyRecipesApp.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScentsyRecipesApp.Server.Controllers
{
    [Route("Ingredients")]
    public class IngredientController : Controller
    {
        #region Private Fields
        private ModelContext<Ingredient> _ingredients;
        #endregion

        #region Constructors
        public IngredientController(
            ModelContext<Ingredient> ingredients)
        {
            _ingredients = ingredients;
        }
        #endregion

        [Route("")]
        public IActionResult All()
        {
            return View(_ingredients.All());
        }

        [Route("Create")]
        [Route("{id}/Edit")]
        public IActionResult EditOrCreate(Int32 id = default, IFormCollection form = default)
        {
            // Load the measurement type to be edited or create a new one...
            var ingredient = _ingredients.GetByIdOrDefault(id);

            if (this.Request.Method == "POST")
            { // If the page was just submitted...
                ingredient.Read(form);

                if (this.TryValidateModel(ingredient))
                { // If the model's new values are valid...
                    _ingredients.UpdateOrInsert(ingredient);
                    return Redirect($"/Ingredients");
                }
            }

            return View("EditOrCreate", ingredient);
        }

        [Route("{id}/Delete")]
        public IActionResult Delete(Int32 id)
        {
            // Load the instance then delete it...
            _ingredients.Delete(_ingredients.GetById(id));

            return Redirect("/Ingredients");
        }
    }
}
