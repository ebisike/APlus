using APlus.API.Data;
using APlus.Application.Interface;
using APlus.DTO.ReadDTO;
using APlus.DTO.WriteDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APlus.API.Controllers
{
    [Route("api/[controller]/items")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryItemService _inventory;
        private readonly UserManager<APlusUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public InventoryController(IInventoryItemService inventoryService, UserManager<APlusUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _inventory = inventoryService;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("get-all")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<InventoryItemDTO> items = await _inventory.GetItems();

                return Ok(items);
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong. we're trying to fix it. please try again");
            }
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                InventoryItemDTO item = await _inventory.GetItem(id);

                return Ok(MapApplicationUser(item));
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong. we're trying to fix it. please try again");
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add([FromBody] InventoryItemDTOW model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                //get the loggedin user if any
                string userId = GetUserId();

                Guid ItemId = await _inventory.AddItem(model, userId);

                if (ItemId == Guid.Empty) return BadRequest("Failed to add new item");

                return Ok("Item Added");
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong. we're trying to fix it. please try again");
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("add-multiple")]
        public async Task<IActionResult> AddMultiple([FromBody] List<InventoryItemDTOW> model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                //get the loggedin user if any
                string userId = GetUserId();

                await _inventory.AddItems(model, userId);

                return Ok("Item Added");
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong. we're trying to fix it. please try again");
            }
        }

        [AllowAnonymous]
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] InventoryItemDTOW model)
        {
            try
            {
                //verify that item to update exist
                InventoryItemDTO item = await _inventory.GetItem(id);

                if (item == null) return BadRequest("Item to update was not found");

                string userId = GetUserId();

                await _inventory.UpdateItem(model, id, userId);

                return Ok("Item Updated successfully");
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong. we're trying to fix it. please try again");
            }
        }


        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                //verify that item to delete exist
                InventoryItemDTO item = await _inventory.GetItem(id);

                if (item == null) return BadRequest("Item to delete was not found");

                await _inventory.DeleteItem(id);

                return Ok("Item deleted successfully");
            }
            catch (Exception e)
            {
                return BadRequest("Something went wrong. we're trying to fix it. please try again");
            }
        }

        private string GetUserId()
        {
            try
            {
                ClaimsIdentity identity = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;

                IEnumerable<Claim> Claims = identity.Claims;

                string userId = Claims.Last(x => x.Type == ClaimTypes.NameIdentifier).Value;

                if (string.IsNullOrWhiteSpace(userId)) return null;

                return userId;
            }
            catch (Exception)
            {

                return string.Empty;
            }
        }

        private async Task<List<InventoryItemDTO>> MapApplicationUser(List<InventoryItemDTO> items)
        {
            List<InventoryItemDTO> MappedItems = new List<InventoryItemDTO>();
            foreach (var item in items)
            {
                APlusUser CreatedBy = await userManager.FindByIdAsync(item.AddedBy);

                APlusUser ModifiedBy = await userManager.FindByIdAsync(item.LasModifiedBy);

                item.LastModifiedByName = ModifiedBy != null ? ModifiedBy.UserName : null;
                item.AddedByName = CreatedBy != null ? CreatedBy.UserName : null;

                MappedItems.Add(item);
            }

            return MappedItems;
        }

        private async Task<InventoryItemDTO> MapApplicationUser(InventoryItemDTO item)
        {
            APlusUser CreatedBy = await userManager.FindByIdAsync(item.AddedBy);

            APlusUser ModifiedBy = await userManager.FindByIdAsync(item.LasModifiedBy);

            item.LastModifiedByName = ModifiedBy != null ? ModifiedBy.UserName : null;
            item.AddedByName = CreatedBy != null ? CreatedBy.UserName : null;

            return item;
        }
    }
}
