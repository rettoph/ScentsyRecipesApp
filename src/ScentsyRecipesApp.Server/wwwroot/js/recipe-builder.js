/**
 * This is a simple recipe builder. Ideally, id do this in VueJS
 * or Angular or something, but to keep this small and lightweight
 * I will just do this is pure javascript.
 * 
 * Ultimately, this file is responsible for maintaining a list of
 * recipe ingredients & steps. From here, the UI will be managed
 * & maintained. To kick off this script, a global value "InputRecipe"
 * must be defined.
 **/

if (typeof window.InputRecipe !== "undefined")
{ // There is a recipe defined. Build the UI now.
    var Ingredients = window.InputRecipe.Ingredients ?? [];
    var Steps = window.InputRecipe.Steps ?? [];
    var MeasurementTypeDropdowns = [];
    var MeasurementTypes = null;
    fetch("/api/measurement-types")
        .then(r => r.json())
        .then(data => {
            // Save the api data...
            MeasurementTypes = data;

            // Load all cached MeasurementTypeDropdowns instances...
            MeasurementTypeDropdowns.forEach(tryPopulateMeasurementTypeDropdown)
            MeasurementTypeDropdowns = null;
        });

    // Add the default ingredients...
    Ingredients.forEach(i => NewIngredient(i));

    // Add the default steps...
    Steps.forEach(s => NewStep(s));
}

/**
 * Simple function that will add a new RecipeStep input window.
 **/
function NewStep(data) {
    var container = document.createElement("div");
    container.className = "step-container container row";

    var c1 = formGroup("text-container", "col-md-12");
    c1.append(formLabel("Step"));

    var textarea = document.createElement("textarea");
    textarea.className = "form-control";
    textarea.textContent = data?.Text;
    textarea.name = "StepTexts[]";
    textarea.required = true;
    c1.append(textarea);

    container.append(c1);
    container.append(removeButton(() => {
        var scroll = window.scrollY;
        container.remove();
        setTimeout(() => window.scrollTo(0, scroll), 1);
    }));
    document.querySelector("#steps").append(container);
}

/**
 * Simple function that will add a new RecipeIngredient input window. 
 **/
function NewIngredient(data)
{
    var container = document.createElement("div");
    container.className = "ingredient-container container row";

    var c1 = formGroup("name-container", "col-md-6");
    c1.append(formLabel("Name"));
    c1.append(formInput("IngredientNames[]", "text", data?.Ingredient.Name));

    var c2 = formGroup("amount-container", "col-md-3 col-sm-6");
    c2.append(formLabel("Amount"));
    c2.append(formInput("IngredientAmounts[]", "number", data?.Amount));

    var c3 = formGroup("measurement-type-container", "col-md-3 col-sm-6");
    c3.append(formLabel("Measurement Type"));
    var dropdown = document.createElement("select");
    dropdown.name = "IngredientMeasurementTypes[]";
    dropdown.className = "form-control";
    dropdown.required = true;
    dropdown.setAttribute("data-initial-value", data?.MeasurementType.Id);
    tryPopulateMeasurementTypeDropdown(dropdown);
    c3.append(dropdown);

    container.append(c1);
    container.append(c2);
    container.append(c3);
    container.append(removeButton(() => {
        var scroll = window.scrollY;
        container.remove();
        setTimeout(() => window.scrollTo(0, scroll), 1);
    }));
    document.querySelector("#ingredients").append(container);
}

function formLabel(text) {
    var label = document.createElement("label");
    label.textContent = text;
    return label;
}

function formGroup(className, customClasses) {
    var container = document.createElement("div");
    container.className = className + " form-group " + customClasses;

    return container;
}

function formInput(name, type, value) {
    var input = document.createElement("input");
    input.name = name;
    input.className = "form-control";
    input.type = type;
    input.required = true;
    input.value = value ?? "";
    input.step = "0.01";

    return input;
}

function removeButton(onclick) {
    var remove = document.createElement("a");
    remove.className = "remove text-danger";
    remove.onclick = onclick;
    remove.textContent = "x";
    remove.href = "#";

    return remove;
}


function tryPopulateMeasurementTypeDropdown(dropdown) {
    if (MeasurementTypes === null) { // The data has yet to be fetched
        MeasurementTypeDropdowns.push(dropdown); // Cache it for now...
    }
    else { // The data has been fetched...
        while (dropdown.firstChild) // Clear the dropdown first...
            dropdown.removeChild(dropdown.firstChild);

        MeasurementTypes.forEach(type => {
            var option = document.createElement("option");
            option.textContent = `${type.name} (${type.abbreviation})`;
            option.value = type.id;

            dropdown.append(option);
        });

        dropdown.value = dropdown.getAttribute("data-initial-value");
    }
}