using Microsoft.AspNetCore.Mvc;
using ScentsyRecipesApp.Server.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ScentsyRecipesApp.Server.Controllers
{
    public class UnitOfMeasurementController : Controller
    {
        #region Private Fields
        private SqlConnection _connection;
        #endregion

        #region Constructors
        public UnitOfMeasurementController(SqlConnection connection)
        {
            _connection = connection;
            _connection.Open();
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult All()
        {
            var units = UnitOfMeasurement.GetAll(_connection);

            return View(units);
        }
    }
}
