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
        public int? MoveId { get; set; }

        [JsonProperty("companyId")]
        public int CompanyId { get; set; }

        [JsonProperty("paymentNum")]
        public int PaymentNum { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("invoice")]
        public string Invoice { get; set; }


        [JsonProperty("dateInvCreate")]
        public DateTime DateInv { get; set; }

    }


    public class invAdding
    {       
        [JsonProperty("nomenclatureId")]
        public int NomenclatureId { get; set; }

        [JsonProperty("companyId")]
        public int CompanyId { get; set; }

        [JsonProperty("paymentNum")]
        public string PaymentNum { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("invoice")]
        public string Invoice { get; set; }

        [JsonProperty("dateInvCreate")]
        public DateTime DateInv { get; set; }

     
        

        [JsonProperty("idPlacement")]
        public int IdPlacement { get; set; }
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

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        
    }
    public class Nomenclature
    {
        [JsonProperty("idNomenclature")]
        public int IdNomenclature { get; set; }

        [JsonProperty("nameDevice")]
        public string NameDevice { get; set; }


        [JsonProperty("date_creation")]
        public DateTime DateCreation { get; set; }

        [JsonProperty("date_change")]
        public DateTime DateChange { get; set; }


        [JsonProperty("inventories")]
        public List<object> Inventories { get; set; }

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

        [JsonProperty("name_placement")]
        public string NamePlacement { get; set; }

        [JsonProperty("name_device")]
        public string NameDevice { get; set; }

        [JsonProperty("id_placement")]
        public int IdPlacement { get; set; }
        [JsonProperty("id_subject")]
        public int IdSubject { get; set; }

        [JsonProperty("name_subject")]
        public string NameSubject { get; set; }

        [JsonProperty("id_movement")]
        public int IdMovement { get; set; }

        [JsonProperty("date_move")]
        public DateTime DateMove { get; set; }

        [JsonProperty("id_company")]
        public int IdCompany { get; set; }

        [JsonProperty("company_name")]
        public string CompanyName { get; set; }

        [JsonProperty("date_creation")]
        public DateTime DateCreation { get; set; }

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

 



    public class RevisionItems
    {
        [JsonProperty("idQueue")]
        public int IdQueue { get; set; }

        [JsonProperty("parentId")]
        public int ParentId { get; set; }

        [JsonProperty("inventoryId")]
        public int InventoryId { get; set; }

        [JsonProperty("dateScan")]
        public DateTime DateScan { get; set; }

        [JsonProperty("isDone")]
        public bool IsDone { get; set; }
    }


}
