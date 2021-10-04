using APlus.DTO.ReadDTO;
using APlus.DTO.WriteDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APlus.Application.Interface
{
    /// <summary>
    /// Perform CRUD Operations on the Entity InventoryItem
    /// </summary>
    public interface IInventoryItemService
    {
        /// <summary>
        /// Create a new Inventory item
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Null or The Id of the new Item created</returns>
        Task<Guid> AddItem(InventoryItemDTOW model, string UserId = null);

        /// <summary>
        /// Create a new Inventory item
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Null or The Id of the new Item created</returns>
        Task AddItems(List<InventoryItemDTOW> model, string UserId = null);

        /// <summary>
        /// Get a single inventory item
        /// </summary>
        /// <param name="ItemId"></param>
        /// <returns>Null or the Object of the Inventory Item</returns>
        Task<InventoryItemDTO> GetItem(Guid ItemId);

        /// <summary>
        /// Get a list of all Inventory Items
        /// </summary>
        /// <returns>Empty List or List of all Items</returns>
        Task<List<InventoryItemDTO>> GetItems();

        /// <summary>
        /// Update Item
        /// </summary>
        /// <param name="model"></param>
        /// <param name="ItemId">Item Id for Update</param>
        /// <returns></returns>
        Task UpdateItem(InventoryItemDTOW model, Guid ItemId, string UserId = null);

        /// <summary>
        /// Delete Inventory Item from DB
        /// </summary>
        /// <param name="ItemId"></param>
        /// <returns></returns>
        Task DeleteItem(Guid ItemId);
    }
}
