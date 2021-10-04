using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace APlus.DTO.ReadDTO
{
    public class InventoryItemDTO
    {
        [JsonProperty("item_id")]
        public Guid Id { get; set; }

        [JsonProperty("item_name")]
        public string Name { get; set; }

        [JsonProperty("item_quantity")]
        public int Quantity { get; set; }

        [JsonProperty("item_price")]
        public decimal Price { get; set; }

        [JsonProperty("date_created")]
        public string DateAdded { get; set; }

        [JsonProperty("created_by_id")]
        public string AddedBy { get; set; }

        [JsonProperty("created_by_name")]
        public string AddedByName { get; set; }


        [JsonProperty("last_modified_by")]
        public string LasModifiedBy { get; set; }

        [JsonProperty("last_modified_by_name")]
        public string LastModifiedByName { get; set; }


        [JsonProperty("last_modified")]
        public string LastModified { get; set; }
    }
}
