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
        public int? CountDevice { get; set; }

        [JsonProperty("manufacturer")]
        public string? Manufacturer { get; set; }

        [JsonProperty("model")]
        public string? Model { get; set; }

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

    }
    public class QueueModel
    {
        [JsonProperty("id_list")]
        public int IdList { get; set; }

        [JsonProperty("id_parent")]
        public int IdParent { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }
    }

    public class Placements
    {
        [JsonProperty("idPlacement")]
        public int IdPlacement { get; set; }

        [JsonProperty("namePlacement")]
        public string NamePlacement { get; set; }
       
    }

    public class Workplace
    {
        [JsonProperty("idWorkplace")]
        public int IdWorkplace { get; set; }

        [JsonProperty("idInventory")]
        public int IdInventory { get; set; }

        [JsonProperty("nameWorkplace")]
        public string NameWorkplace { get; set; }

        [JsonProperty("placementIdWp")]
        public int PlacementIdWp { get; set; }

        [JsonProperty("mol")]
        public string Mol { get; set; }

        [JsonProperty("deviceId")]
        public int DeviceId { get; set; }

        [JsonProperty("employerId")]
        public int EmployerId { get; set; }

       
    }


    public class WorkplaceConnected
    {
        [JsonProperty("id_workplace")]
        public int IdWorkplace { get; set; }

        [JsonProperty("id_inventory")]
        public int IdInventory { get; set; }

        [JsonProperty("placement_id_wp")]
        public int PlacementIdWp { get; set; }

        [JsonProperty("name_workplace")]
        public string NameWorkplace { get; set; }

        [JsonProperty("name_placement")]
        public string NamePlacement { get; set; }

        [JsonProperty("id_empolyer")]
        public int IdEmpolyer { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }
    }



}
