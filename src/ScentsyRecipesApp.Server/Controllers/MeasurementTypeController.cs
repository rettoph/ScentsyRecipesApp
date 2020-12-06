using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ScentsyRecipesApp.Library;
using ScentsyRecipesApp.Server.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ScentsyRecipesApp.Server.Controllers
{
    [Route("MeasurementTypes")]
    public class MeasurementTypeController : Controller
    {
        #region Private Fields
        private ModelContext<MeasurementType> _measurementTypes;
        #endregion

        #region Constructors
        public MeasurementTypeController(ModelContext<MeasurementType> context)
        {
            _measurementTypes = context;
        }
        #endregion

        [Route("")]
        public IActionResult All()
            => View(_measurementTypes.All());

        [Route("Create")]
        [Route("{id}/Edit")]
        public IActionResult EditOrCreate(Int32 id = default, IFormCollection form = default)
        {
            // Load the measurement type to be edited or create a new one...
            var mt = _measurementTypes.GetByIdOrDefault(id);

            if(this.Request.Method == "POST")
            { // If the page was just submitted...
                mt.Read(form);

                if (this.TryValidateModel(mt))
                { // If the model's new values are valid...
                    _measurementTypes.UpdateOrInsert(mt);
                    return Redirect($"/MeasurementTypes");
                }
            }

            return View("EditOrCreate", mt);
        }

        [Route("{id}/Delete")]
        public IActionResult Delete(Int32 id)
        {
            // Load the instance then delete it...
            _measurementTypes.Delete(_measurementTypes.GetById(id));

            return Redirect("/MeasurementTypes");
        }
    }
}
