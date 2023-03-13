using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventarisation.Api.ApiModel
{
    public class InventoryModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("invNum")]
        public string InvNum { get; set; }

        [JsonProperty("nomenclatureId")]
        public int NomenclatureId { get; set; }

        [JsonProperty("moveId")]
        public int MoveId { get; set; }

        [JsonProperty("companyId")]
        public int CompanyId { get; set; }

        [JsonProperty("paymentNum")]
        public int PaymentNum { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("invoice")]
        public string Invoice { get; set; }

        [JsonProperty("workplaceId")]
        public int WorkplaceId { get; set; }

        [JsonProperty("company")]
        public object Company { get; set; }

        [JsonProperty("movements")]
        public List<object> Movements { get; set; }

        [JsonProperty("nomenclature")]
        public object Nomenclature { get; set; }

        [JsonProperty("workplaces")]
        public List<object> Workplaces { get; set; }

        [JsonProperty("writeOffs")]
        public List<object> WriteOffs { get; set; }
    }
}
