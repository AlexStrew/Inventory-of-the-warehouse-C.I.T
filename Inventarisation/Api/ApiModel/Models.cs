using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventarisation.Api.ApiModel
{
   public class Inventory
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

    public class Company
    {
        [JsonProperty("idCompany")]
        public int IdCompany { get; set; }

        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        [JsonProperty("inventories")]
        public List<object> Inventories { get; set; }
    }

    public class Employers
    {
        [JsonProperty("idEmpolyer")]
        public int IdEmpolyer { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("patronymic")]
        public string Patronymic { get; set; }

        [JsonProperty("workplaces")]
        public List<object> Workplaces { get; set; }
    }
    public class Nomenclature
    {
        [JsonProperty("idNomenclature")]
        public int IdNomenclature { get; set; }

        [JsonProperty("nameDevice")]
        public string NameDevice { get; set; }

        [JsonProperty("countDevice")]
        public int CountDevice { get; set; }

        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("date_creation")]
        public DateTime DateCreation { get; set; }

        [JsonProperty("date_change")]
        public DateTime DateChange { get; set; }


        [JsonProperty("inventories")]
        public List<object> Inventories { get; set; }

        [JsonProperty("workplaces")]
        public List<object> Workplaces { get; set; }
    }

    public class InvMain
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("inv_num")]
        public string InvNum { get; set; }

        [JsonProperty("payment_num")]
        public int PaymentNum { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("invoice")]
        public string Invoice { get; set; }

        [JsonProperty("nomenclature_id")]
        public int NomenclatureId { get; set; }

        [JsonProperty("name_device")]
        public string NameDevice { get; set; }

        [JsonProperty("id_workplace")]
        public int IdWorkplace { get; set; }

        [JsonProperty("name_workplace")]
        public string NameWorkplace { get; set; }

        [JsonProperty("id_movement")]
        public int IdMovement { get; set; }

        [JsonProperty("date_move")]
        public DateTime DateMove { get; set; }

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
