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

        [JsonProperty("moveId")]
        public int? MoveId { get; set; }

        [JsonProperty("companyId")]
        public int CompanyId { get; set; }

        [JsonProperty("paymentNum")]
        public string PaymentNum { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("invoice")]
        public string Invoice { get; set; }

        [JsonProperty("subject_id")]
        public int SubjectId { get; set; }

        [JsonProperty("dateInvCreate")]
        public DateTime DateInv { get; set; }

    }


    public class invAdding
    {
        [JsonProperty("serialNumber")]
        public string SerialNumber { get; set; }

        [JsonProperty("subjectId")]
        public int SubjectId { get; set; }

        [JsonProperty("companyId")]
        public int CompanyId { get; set; }

        [JsonProperty("paymentNum")]
        public string PaymentNum { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("invoice")]
        public string Invoice { get; set; }

     
                     
     
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


        //[JsonProperty("inventories")]
        //public List<object> Inventories { get; set; }

    }



    public class HistoryMove
    {
        [JsonProperty("id_movement")]
        public int IdMovement { get; set; }

        [JsonProperty("date_move")]
        public DateTime DateMove { get; set; }

        [JsonProperty("id_inventory")]
        public int IdInventory { get; set; }

        [JsonProperty("placement_id")]
        public int PlacementId { get; set; }

        [JsonProperty("name_placement")]
        public string NamePlacement { get; set; }

        [JsonProperty("planner")]
        public object Planner { get; set; }

        [JsonProperty("employer_id")]
        public int EmployerId { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }
    }


    public class InvMain
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("inv_num")]
        public string InvNum { get; set; }

        [JsonProperty("payment_num")]
        public string PaymentNum { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("invoice")]
        public string Invoice { get; set; }

        [JsonProperty("id_placement")]
        public int IdPlacement { get; set; }

        [JsonProperty("name_placement")]
        public string NamePlacement { get; set; }

        [JsonProperty("id_movement")]
        public int IdMovement { get; set; }

        [JsonProperty("date_move")]
        public DateTime DateMove { get; set; }

        [JsonProperty("id_company")]
        public int IdCompany { get; set; }

        [JsonProperty("company_name")]
        public string CompanyName { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("id_empolyer")]
        public int IdEmployer { get; set; }

        [JsonProperty("id_subject")]
        public int IdSubject { get; set; }

        [JsonProperty("name_subject")]
        public string NameSubject { get; set; }

        [JsonProperty("serial_number")]
        public string SerialNumber { get; set; }





        [JsonProperty("nomenclature_id")]
        public int NomenclatureId { get; set; }



        [JsonProperty("name_device")]
        public string NameDevice { get; set; }



        [JsonProperty("date_creation")]
        public DateTime DateCreation { get; set; }

        [JsonProperty("subject_id")]
        public int SubjectId { get; set; }





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


    public class Movements
    {
        [JsonProperty("idMovement")]
        public int IdMovement { get; set; }

        [JsonProperty("idInventory")]
        public int IdInventory { get; set; }

        [JsonProperty("dateMove")]
        public DateTime DateMove { get; set; }

        [JsonProperty("placementId")]
        public int PlacementId { get; set; }

        [JsonProperty("planner")]
        public string Planner { get; set; }

        [JsonProperty("employerId")]
        public int EmployerId { get; set; }
    }

    public class Subjects
    {
        [JsonProperty("idSubject")]
        public int IdSubject { get; set; }

        [JsonProperty("nameSubject")]
        public string NameSubject { get; set; }

        [JsonProperty("nomenId")]
        public int NomenId { get; set; }
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
