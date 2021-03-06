﻿using Microsoft.AspNetCore.Http;
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
    [Route("")]
    public class RecipeController : Controller
    {
        #region Private Fields
        private ModelContext<Recipe> _recipes;
        private ModelContext<RecipeIngredient> _recipeIngredients;
        #endregion

        #region Constructors
        public RecipeController(
            ModelContext<Recipe> recipes,
            ModelContext<RecipeIngredient> recipeIngredients)
        {
            _recipes = recipes;
            _recipeIngredients = recipeIngredients;
        }
        #endregion

        [Route("")]
        public IActionResult All()
        {
            return View(_recipes.All());
        }

        [Route("{id}")]
        public IActionResult View(Int32 id)
        {
            return View(_recipes.GetById(id));
        }

        [Route("Create")]
        [Route("{id}/Edit")]
        public IActionResult EditOrCreate(Int32 id = default, IFormCollection form = default)
        {
            // Load the measurement type to be edited or create a new one...
            var recipe = _recipes.GetByIdOrDefault(id);

            if (this.Request.Method == "POST")
            { // If the page was just submitted...
                recipe.Read(form);

                if (this.TryValidateModel(recipe))
                { // If the model's new values are valid...
                    _recipes.UpdateOrInsert(recipe);
                    return Redirect($"/");
                }
            }

            return View("EditOrCreate", recipe);
        }

        [Route("{id}/Delete")]
        public IActionResult Delete(Int32 id)
        {
            _recipes.Delete(_recipes.GetById(id));

            return Redirect("/");
        }
    }
}
