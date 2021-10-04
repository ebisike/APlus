using APlus.Domain.Entities;
using APlus.DTO.ReadDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APlus.DTO.Mapper
{
    public static class Map
    {
        public static List<InventoryItemDTO> InventoryItems(ICollection<InventoryItem> inventoryItems)
        {
            List<InventoryItemDTO> items = inventoryItems.Select(x => new InventoryItemDTO()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Quantity = x.Quantity,
                DateAdded = x.DateAdded.ToString("dddd dd MMMM yyyy"),
                AddedBy = x.AddedBy,
                LasModifiedBy = x.LastUpdatedBy,
                LastModified = x.LastUpdated.HasValue ? x.LastUpdated.Value.ToString("dddd dd MMMM yyyy") : string.Empty
            }).ToList();

            return items;
        }
    }
}
