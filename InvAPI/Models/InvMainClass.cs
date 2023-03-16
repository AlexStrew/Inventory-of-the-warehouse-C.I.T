using Newtonsoft.Json;

namespace InvAPI.Models
{
    public class InvMainClass
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("inv_num")]
        public string invNum { get; set; }

        [JsonProperty("payment_num")]
        public int? PaymentNum { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("invoice")]
        public string Invoice { get; set; }

        [JsonProperty("nomenclature_id")]
        public int? NomenclatureId { get; set; }

        [JsonProperty("name_device")]
        public string NameDevice { get; set; }

        [JsonProperty("id_workplace")]
        public int IdWorkplace { get; set; }

        [JsonProperty("name_workplace")]
        public string NameWorkplace { get; set; }

        [JsonProperty("id_movement")]
        public int IdMovement { get; set; }

        [JsonProperty("date_move")]
        public DateTime? DateMove { get; set; }

        [JsonProperty("id_company")]
        public int IdCompany { get; set; }

        [JsonProperty("company_name")]
        public string CompanyName { get; set; }

        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("date_creation")]
        public DateTime DateCreation { get; set; }

        [JsonProperty("date_change")]
        public DateTime DateChange { get; set; }

    }
}
