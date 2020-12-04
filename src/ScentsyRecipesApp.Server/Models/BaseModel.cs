using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScentsyRecipesApp.Server.Models
{
    /// <summary>
    /// A base model conataining default implementation
    /// used by all custom models within the current 
    /// WebApp.
    /// </summary>
    public abstract class BaseModel
    {
        #region Public Properties
        /// <summary>
        /// The unique identifier & primary key.
        /// </summary>
        public Int32 Id { get; set; }
        #endregion
    }
}
