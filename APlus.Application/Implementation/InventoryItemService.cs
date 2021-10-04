using APlus.Application.Interface;
using APlus.DataAccess.UnitOfWork.Interface;
using APlus.Domain.Entities;
using APlus.DTO.Mapper;
using APlus.DTO.ReadDTO;
using APlus.DTO.WriteDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APlus.Application.Implementation
{
    public class InventoryItemService : IInventoryItemService
    {
        private readonly IUnitOfWork<InventoryItem> _itemUoW;

        public InventoryItemService(IUnitOfWork<InventoryItem> ItemUoW)
        {
            _itemUoW = ItemUoW;
        }

        public async Task<Guid> AddItem(InventoryItemDTOW model, string UserId = null)
        {
            try
            {
                if (model == null) return Guid.Empty;

                var NewItem = new InventoryItem()
                {
                    Name = model.Name,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    DateAdded = DateTime.UtcNow,
                    AddedBy = UserId,
                };

                await _itemUoW.Repository.Add(NewItem);
                await _itemUoW.SaveAsync();

                return NewItem.Id;

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task AddItems(List<InventoryItemDTOW> model, string UserId = null)
        {
            try
            {
                List<InventoryItem> newitems = model.Select(x => new InventoryItem()
                {
                    Name = x.Name,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    DateAdded = DateTime.UtcNow,
                    AddedBy = UserId
                }).ToList();

                await _itemUoW.Repository.AddRange(newitems);
                await _itemUoW.SaveAsync();

                return;

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task DeleteItem(Guid ItemId)
        {
            try
            {
                await _itemUoW.Repository.Delete(ItemId);
                await _itemUoW.SaveAsync();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<InventoryItemDTO> GetItem(Guid ItemId)
        {
            InventoryItem item = await _itemUoW.Repository.ReadSingle(ItemId);

            if (item == null) return null;

            return Map.InventoryItems(new List<InventoryItem>() { item }).FirstOrDefault();
        }

        public async Task<List<InventoryItemDTO>> GetItems()
        {
            IEnumerable<InventoryItem> items = await _itemUoW.Repository.ReadAll();

            return Map.InventoryItems(items.OrderByDescending(x=>x.DateAdded).ToList());
        }

        public async Task UpdateItem(InventoryItemDTOW model, Guid ItemId, string UserId = null)
        {
            try
            {
                //fetch the item to update

                InventoryItem ItemForUpdate = await _itemUoW.Repository.ReadSingle(ItemId);

                if (ItemForUpdate == null) return;

                ItemForUpdate.Name = model.Name;
                ItemForUpdate.Price = model.Price;
                ItemForUpdate.Quantity = model.Quantity;
                ItemForUpdate.LastUpdated = DateTime.UtcNow;
                ItemForUpdate.LastUpdatedBy = UserId;

                _itemUoW.Repository.Update(ItemForUpdate);
                await _itemUoW.SaveAsync();
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}
