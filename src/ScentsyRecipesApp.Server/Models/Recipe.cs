using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
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
    /// The Primary Recipe model. This will have many
    /// <see cref="RecipeIngredient"/>, contain steps,
    /// and more.
    /// </summary>
    [Table("Recipes")]
    public class Recipe : BaseModel
    {
        #region Private Fields
        /// <summary>
        /// <see cref="ModelContext{TModel}"/> instance for 
        /// <see cref="RecipeIngredient"/>. Used to manage the internal
        /// <see cref="Ingredients"/> hasMany relationship.
        /// </summary>
        private ModelContext<RecipeIngredient> _recipieIngredients;

        /// <summary>
        /// <see cref="ModelContext{TModel}"/> instance for 
        /// <see cref="RecipeStep"/>. Used to manage the internal
        /// <see cref="Steps"/> hasMany relationship.
        /// </summary>
        private ModelContext<RecipeStep> _recipeSteps;

        /// <summary>
        /// <see cref="ModelContext{TModel}"/> instance for 
        /// <see cref="Ingredient"/>. Used to manage the internal
        /// <see cref="Ingredients"/> hasMany relationship.
        /// </summary>
        private ModelContext<Ingredient> _ingredients;

        /// <summary>
        /// <see cref="ModelContext{TModel}"/> instance for 
        /// <see cref="MeasurementType"/>. Used to manage the internal
        /// <see cref="Ingredients"/> hasMany relationship.
        /// </summary>
        private ModelContext<MeasurementType> _measurementTypes;
        #endregion

        #region Public Properties
        /// <summary>
        /// A human readable name for the current <see cref="Recipe"/>
        /// </summary>
        [Required(ErrorMessage = "Name required.")]
        [StringLength(150, ErrorMessage = "Name should be less than or equal to 150 characters.")]
        [Column("Name")]
        public String Name { get; set; }

        /// <summary>
        /// The amount of people this recipe feeds.
        /// </summary>
        [Required(ErrorMessage = "Feeds required.")]
        [Column("Feeds")]
        public Int32 Feeds { get; set; }

        /// <summary>
        /// The time this recipe takes to cook.
        /// </summary>
        [Required(ErrorMessage = "Time required.")]
        [StringLength(15, ErrorMessage = "Name should be less than or equal to fifteen characters.")]
        [Column("Time")]
        public String Time { get; set; }

        /// <summary>
        /// A list of all required <see cref="RecipeIngredient"/>s needed
        /// to complete this <see cref="Recipe"/>.
        /// </summary>
        public List<RecipeIngredient> Ingredients { get; private set; }

        /// <summary>
        /// A list of all required <see cref="RecipeStep"/>s needed
        /// to complete this <see cref="Recipe"/>.
        /// </summary>
        public List<RecipeStep> Steps { get; private set; }
        #endregion

        #region Constructor
        public Recipe(
            ModelContext<RecipeIngredient> recipieIngredients,
            ModelContext<RecipeStep> recipieSteps,
            ModelContext<Ingredient> ingredients,
            ModelContext<MeasurementType> measurementTypes)
        {
            _recipieIngredients = recipieIngredients;
            _recipeSteps = recipieSteps;
            _ingredients = ingredients;
            _measurementTypes = measurementTypes;

            this.Ingredients = new List<RecipeIngredient>();
            this.Steps = new List<RecipeStep>();
        }
        #endregion

        #region Helper Methods
        /// <inheritdoc />
        protected override void Read(SqlDataReader reader)
        {
            base.Read(reader);

            this.Name = Convert.ToString(reader["Name"]);
            this.Feeds = Convert.ToInt32(reader["Feeds"]);
            this.Time = Convert.ToString(reader["Time"]);

            // Load the saved RecipeIngredients...
            this.Ingredients.AddRange(_recipieIngredients.Where($"RecipeId={this.Id}").ToList());

            // Load the saved RecipeSteps...
            this.Steps.AddRange(_recipeSteps.Where($"RecipeId={this.Id}").ToList());
        }

        /// <inheritdoc />
        public override void Read(IFormCollection form)
        {
            this.Name  = form["Name"];
            this.Feeds = Int32.Parse(form["Feeds"]);
            this.Time  = form["Time"];

            #region Ingredients
            // Its a little crude, but every time the recipe is updated lets just
            // Delete the old ingredients and create new ones based on submitted data
            // That way, we dont have to worry about repositioning or anything like that.
            _recipieIngredients.DeleteMany(this.Ingredients);
            this.Ingredients.Clear();

            if (form["IngredientNames[]"] is StringValues names 
                && form["IngredientAmounts[]"] is StringValues amounts 
                && form["IngredientMeasurementTypes[]"] is StringValues measurementTypes)
            { // Confure the hasMany relationship within the current recipe.
                for(var i=0; i<names.Count(); i++)
                {
                    this.Ingredients.Add(_recipieIngredients.Create(ingredient =>
                    {
                        ingredient.Recipe = this;
                        ingredient.Ingredient = _ingredients.Where($"Name='{names[i]}'").FirstOrDefault() ?? _ingredients.Insert(new Ingredient() { Name = names[i] });
                        ingredient.Amount = Single.Parse(amounts[i]);
                        ingredient.MeasurementType = _measurementTypes.GetById(Int32.Parse(measurementTypes[i]));
                    }));
                }
            }
            #endregion

            #region Steps
            // Similar to the Ingredients, we will delete all pre-existing steps
            // Then create new ones, removing all need to manage "order" when 
            // updating values.
            _recipeSteps.DeleteMany(this.Steps);
            this.Steps.Clear();

            if (form["StepTexts[]"] is StringValues texts)
            { // Confure the hasMany relationship within the current recipe.
                for (var i = 0; i < texts.Count(); i++)
                {
                    this.Steps.Add(_recipeSteps.Create(step =>
                    {
                        step.Recipe = this;
                        step.Text = texts[i];
                    }));
                }
            }
            #endregion
        }

        /// <inheritdoc />
        protected override void Inserted()
        {
            base.Inserted();

            foreach(var ingredient in this.Ingredients)
            { // When the database is just created, ensure that any added ingredients are also inserted.
                ingredient.Recipe = this;
                _recipieIngredients.UpdateOrInsert(ingredient);
            }

            foreach (var step in this.Steps)
            { // When the database is just created, ensure that any added steps are also inserted.
                step.Recipe = this;
                _recipeSteps.UpdateOrInsert(step);
            }
        }

        protected override void Updated()
        {
            base.Updated();

            foreach (var ingredient in this.Ingredients)
            { // When the database is just updated, ensure that any modified ingredients are also updated.
                if(!ingredient.Real || ingredient.Recipe != this)
                {
                    ingredient.Recipe = this;
                    _recipieIngredients.UpdateOrInsert(ingredient);
                }
            }

            foreach (var step in this.Steps)
            { // When the database is just updated, ensure that any modified steps are also updated.
                if (!step.Real || step.Recipe != this)
                {
                    step.Recipe = this;
                    _recipeSteps.UpdateOrInsert(step);
                }
            }
        }
        #endregion
    }
}
