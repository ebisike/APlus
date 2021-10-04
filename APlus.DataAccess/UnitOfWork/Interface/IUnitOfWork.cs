using APlus.DataAccess.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APlus.DataAccess.UnitOfWork.Interface
{
    public interface IUnitOfWork<T> where T: class
    {
        /// <summary>
        /// Get The Repository Pattern for the Entity
        /// </summary>
        IGenericRepository<T> Repository { get; }

        /// <summary>
        /// Save the changes made to the entity
        /// </summary>
        /// <returns>returns True if Succeessful otherwise false</returns>
        Task<bool> SaveAsync();


        /// <summary>
        /// Rolls back the changes to the last save_point
        /// </summary>
        /// <returns></returns>
        Task RollBack();
    }
}
