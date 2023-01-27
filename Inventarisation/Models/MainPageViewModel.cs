using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Net.Http;
using Prism.Commands;
using Prism.Mvvm;
using System.Threading.Tasks;
using Inventarisation.Api;
using Inventarisation.Api.ApiModel;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace Inventarisation.Models
{
    public class MainPageViewModel : BindableBase, INotifyPropertyChanged
    {
        private List<Inventory> _invent;
        private List<Nomenclature> _nomeclature;
        
        public List<Inventory> Inventorys
        {
            get { return _invent; }
            set { SetProperty(ref _invent, value); }
        }
        public List<Nomenclature> Nomenclatures
        {
            get { return _nomeclature; }
            set { SetProperty(ref _nomeclature, value); }
        }
        
        private bool _isLoadData;

        public bool IsLoadData
        {
            get { return _isLoadData; }
            set { SetProperty(ref _isLoadData, value); }
        }

        public MainPageViewModel()
        {
            GetEmployeeDetails();
        }

        private void GetEmployeeDetails()
        {
            var resultDetails = WebAPI.GetCall(Manager.invent);

            if (resultDetails.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Inventorys = resultDetails.Result.Content.ReadAsAsync<List<Inventory>>().Result;
                Nomenclatures = resultDetails.Result.Content.ReadAsAsync<List<Nomenclature>>().Result;
                IsLoadData = true;
            }
        }

       

    }

}
