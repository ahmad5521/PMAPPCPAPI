using PMAPP.Models;
using Plugin.RestClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace PMAPP.Services
{
    public class RestClientService
    {
        //192.168.1.4
        //ahmadasirii7-001-site1.gtempurl.com

        public string url = "http://api.injaz-tabuk.com";
        //public string url = "http://192.168.1.4";
        //public string url = "http://localhost:50042";
        //localhost:52173

        public async Task<List<WorkOrderView>> getWorkOrderViewByIDAsync(int v)
        {
            RestClient<WorkOrderView> restClient = new RestClient<WorkOrderView>();

            restClient.WebServiceUrl = url + "/api/workOrderViews/GetWorkOrderViewsByID/" + v + "/";

            var WorkOrderViewList = await restClient.GetAsync();
            
            return WorkOrderViewList;
        }

        public async Task<List<WorkOrderView>> getTodatWorkOrderViewByIDAsync(int v)
        {
            RestClient<WorkOrderView> restClient = new RestClient<WorkOrderView>();
            
            restClient.WebServiceUrl = url + "/api/WorkOrderViews/GetWorkOrderViewTodayByID/" + v + "/" ;

            var WorkOrderViewList = await restClient.GetAsync();

            return WorkOrderViewList;
        }

        public async Task<List<WorkOrdersPhotos>> getAllImagesByWorkOrderIDAsync( int v)
        {
            RestClient<WorkOrdersPhotos> restClient = new RestClient<WorkOrdersPhotos>();
            
            restClient.WebServiceUrl = url + "/api/WorkOrdersPhotoes/GetWorkOrdersPhoto/" + v + "/";

            var WorkOrdersPhotosList = await restClient.GetAsync();

            return WorkOrdersPhotosList;
        }

        public async Task<List<V_Contractor_noOfWorkorders_workorderType>> getV_Contractor_noOfWorkorders_workorderTypeViewsAsync()
        {
            RestClient<V_Contractor_noOfWorkorders_workorderType> restClient = new RestClient<V_Contractor_noOfWorkorders_workorderType>();

            restClient.WebServiceUrl = url + "/api/V_Contractor_noOfWorkorders_workorderType/GetV_Contractor_noOfWorkorders_workorderTypeViews/";

            var V_Contractor_noOfWorkorders_workorderTypesList = await restClient.GetAsync();

            return V_Contractor_noOfWorkorders_workorderTypesList;
        }

        public async Task<List<V_Project_totalLengths_project_contractor>> get_V_Project_totalLengths_project_contractor_Async(object v)
        {
            RestClient<V_Project_totalLengths_project_contractor> restClient = new RestClient<V_Project_totalLengths_project_contractor>();

            restClient.WebServiceUrl = url + "/api/V_Project_totalLengths_project_contractor/GetV_Project_totalLengths_project_contractor/" + v + "/";

            var V_Project_totalLengths_project_contractorList = await restClient.GetAsync();

            return V_Project_totalLengths_project_contractorList;
        }

        public async Task<Users> GetUserByUserNameAndPassword(string userName, string password)
        {
            RestClient<Users> restClient = new RestClient<Users>();
            
            restClient.WebServiceUrl = url + "/api/Users/GetUserByUserNameAndPassword/" + userName + "/" + password + "/";

            var user = await restClient.GetNotListAsync();

            return user;
        }

        public async Task<bool> DeletePhotoAsync(int pID)
        {
            RestClient<WorkOrders> restClient = new RestClient<WorkOrders>();

            restClient.WebServiceUrl = url + "/api/WorkOrdersPhotoes/DeleteWorkOrdersPhoto/";

            var result = await restClient.DeleteAsync(pID);

            return result;
        }

        public async Task<LookUPs> GetLookUPsData()
        {
            RestClient<LookUPs> restClient = new RestClient<LookUPs>();

            restClient.WebServiceUrl = url + "/api/LookUPs/GetLookUPs/";

            var LookUPs = await restClient.GetNotListAsync();

            return LookUPs;
        }

        public async Task<bool> getWorkOrderByNumberAsyncTOF(int v)
        {
            RestClient<WorkOrders> restClient = new RestClient<WorkOrders>();

            restClient.WebServiceUrl = url + "/api/WorkOrders/GetWorkOrderByNumber/" + v + "/";

            var wo = await restClient.GetAsync();

            if (wo.Count == 0)
                return false;
            else
                return true;
        }

        public async Task<int> getWorkOrderByNumberAsync(int v)
        {
            RestClient<WorkOrders> restClient = new RestClient<WorkOrders>();

            restClient.WebServiceUrl = url + "/api/WorkOrders/GetWorkOrderByNumber/" + v + "/";

            var wo = await restClient.GetAsync();

            return wo.First().ID;
        }

        public async Task<List<V_Contractor_noOfAchievedWorkorders_workorderType>> getV_Contractor_noOfAchievedWorkorders_workorderTypeAsync()
        {
            RestClient<V_Contractor_noOfAchievedWorkorders_workorderType> restClient = new RestClient<V_Contractor_noOfAchievedWorkorders_workorderType>();

            restClient.WebServiceUrl = url + "/api/V_Contractor_noOfAchievedWorkorders_workorderType/GetV_Contractor_noOfAchievedWorkorders_workorderType/";

            var V_Contractor_noOfAchievedWorkorders_workorderTypeList = await restClient.GetAsync();

            return V_Contractor_noOfAchievedWorkorders_workorderTypeList;
        }
        
        public async Task<WorkOrderParametersView> getWorkOredrParametersByPrameterNoAsync(int id)
        {
            RestClient<WorkOrderParametersView> restClient = new RestClient<WorkOrderParametersView>();

            restClient.WebServiceUrl = url + "/api/WorkOrderParametersViews/GetWorkOrderParametersView/" + id + "/";

            var WorkOrderParametersView = await restClient.GetNotListAsync();

            return WorkOrderParametersView;
        }

        public async Task<bool> PutWorkOrderParameterUpdateAsync(int id, WorkOrdersParameters woptoUpdate)
        {
            RestClient<WorkOrdersParameters> restClient = new RestClient<WorkOrdersParameters>();

            restClient.WebServiceUrl = url + "/api/WorkOrdersParameters/PutWorkOrdersParameter/";

            var result = await restClient.PutAsync(id, woptoUpdate);

            return result;
        }

        public async Task<List<V_Project_totalAmounts_project_contractor>> get_V_Project_totalAmounts_project_contractor_Async(int v)
        {
            RestClient<V_Project_totalAmounts_project_contractor> restClient = new RestClient<V_Project_totalAmounts_project_contractor>();

            restClient.WebServiceUrl = url + "/api/V_Project_totalAmounts_project_contractor_worktype1/GetV_Project_totalAmounts_project_contractor_worktype1/" + v + "/";

            var V_Project_totalAmounts_project_contractorList = await restClient.GetAsync();

            return V_Project_totalAmounts_project_contractorList;
        }

        public async Task<List<WorkOrderParametersView>> getWorkOredrParametersByWONoAsync(int v)
        {
            RestClient<WorkOrderParametersView> restClient = new RestClient<WorkOrderParametersView>();

            restClient.WebServiceUrl = url + "/api/WorkOrderParametersViews/GetWorkOrderParametersByWONoView/" + v + "/";

            var WorkOrderParametersView = await restClient.GetAsync();

            return WorkOrderParametersView;
        }

        public async Task<WorkOrders> getWorkOrderByIDAsync(int v)
        {
            RestClient<WorkOrders> restClient = new RestClient<WorkOrders>();

            restClient.WebServiceUrl = url + "/api/WorkOrders/GetWorkOrder/" + v + "/";

            var WorkOrder = await restClient.GetNotListAsync();

            return WorkOrder;
        }

        public async Task<WorkOrdersPhotos> getImageByIDAsync(int v)
        {
            RestClient<WorkOrdersPhotos> restClient = new RestClient<WorkOrdersPhotos>();

            restClient.WebServiceUrl = url + "/api/WorkOrdersPhotos/GetWorkOrdersPhotos/" + v + "/";

            var imagePhoto = await restClient.GetNotListAsync();

            return imagePhoto;
        }

        public async Task<List<Locations>> getLocationsAsync()
        {
            RestClient<Locations> restClient = new RestClient<Locations>();

            restClient.WebServiceUrl = url + "/api/Locations/GetLocations/";

            var LocationsList = await restClient.GetAsync();

            return LocationsList;
        }

        public async Task<List<Projects>> getAllProjectsAsync()
        {
            RestClient<Projects> restClient = new RestClient<Projects>();

            restClient.WebServiceUrl = url + "/api/Projects/GetProjects/";

            var ProjectsList = await restClient.GetAsync();

            return ProjectsList;
        }

        public async Task<List<V_Project_percentOfAchievement_contractors>> get_V_Project_percentOfAchievement_contractors_Async(int v)
        {
            RestClient<V_Project_percentOfAchievement_contractors> restClient = new RestClient<V_Project_percentOfAchievement_contractors>();

            restClient.WebServiceUrl = url + "/api/V_Project_percentOfAchievement_contractors/GetV_Project_percentOfAchievement_contractors/" + v + "/";

            var V_Project_percentOfAchievement_contractorsList = await restClient.GetAsync();

            return V_Project_percentOfAchievement_contractorsList;
        }

        public async Task<List<ParameterCategory>> getAllParameterCategoryAsync()
        {
            RestClient<ParameterCategory> restClient = new RestClient<ParameterCategory>();

            restClient.WebServiceUrl = url + "/api/ParameterCategories/GetParameterCategories/";

            var ParameterCategoryList = await restClient.GetAsync();

            return ParameterCategoryList;
        }

        public async Task<List<Parameters>> getAllParameterBYCategoryAsync(int v)
        {
            RestClient<Parameters> restClient = new RestClient<Parameters>();

            restClient.WebServiceUrl = url + "/api/Parameters/GetParameterbYCatagoryNO/" + v + "/";

            var ParametersList = await restClient.GetAsync();

            return ParametersList;
        }

        public async Task<List<WorkOrderStatus>> getAllWorkOrderStatusAsync()
        {
            RestClient<WorkOrderStatus> restClient = new RestClient<WorkOrderStatus>();
            
            restClient.WebServiceUrl = url + "/api/WorkOrderStatus/GetWorkOrderStatus/";

            var WorkOrderStatusList = await restClient.GetAsync();

            return WorkOrderStatusList;
        }

        public async Task<List<VoltageLevels>> getAllVoltageLevelsAsync()
        {
            RestClient<VoltageLevels> restClient = new RestClient<VoltageLevels>();

            restClient.WebServiceUrl = url + "/api/VoltageLevels/GetVoltageLevels/";
            
            var VoltageLevelsList = await restClient.GetAsync();

            return VoltageLevelsList;
        }

        public async Task<List<Contractors>> getAllContractorsAsync()
        {
            RestClient<Contractors> restClient = new RestClient<Contractors>();

            restClient.WebServiceUrl = url + "/api/Contractors/GetContractors/";


            var ContractorsList = await restClient.GetAsync();

            return ContractorsList;
        }

        public async Task<List<Users>> getAllUsersAsync()
        {
            RestClient<Users> restClient = new RestClient<Users>();

            restClient.WebServiceUrl = url + "/api/Users/GetUsers/";
            
            var UsersList = await restClient.GetAsync();

            return UsersList;
        }

        public async Task<bool> PostWorkOrderUpdateAsync(WorkOrders newWO)
        {
            RestClient<WorkOrders> restClient = new RestClient<WorkOrders>();

            restClient.WebServiceUrl = url + "/api/WorkOrders/PostWorkOrder/";

            var result = await restClient.PostAsync(newWO);

            return result;
        }

        public async Task<bool> PostWorkOrderPhotoAsync(WorkOrdersPhotos wop)
        {
            RestClient<WorkOrdersPhotos> restClient = new RestClient<WorkOrdersPhotos>();
            
            restClient.WebServiceUrl = url + "/api/WorkOrdersPhotoes/PostWorkOrdersPhoto/";            

            var result = await restClient.PostAsync(wop);

            return result;
        }

        public async Task<bool> PostWorkOrderParametersAsync(List<WorkOrdersParameters> lwop)
        {
            RestClient<List<WorkOrdersParameters>> restClient = new RestClient<List<WorkOrdersParameters>>();

            restClient.WebServiceUrl = url + "/api/WorkOrdersParameters/PostWorkOrdersParameter/";

            var result = await restClient.PostAsync(lwop);

            return result;
        }

        public async Task<bool> DeleteWorkOrderAsync(int ID)
        {
            RestClient<WorkOrders> restClient = new RestClient<WorkOrders>();

            restClient.WebServiceUrl = url + "/api/WorkOrders/DeleteWorkOrder/";

            var result = await restClient.DeleteAsync(ID);

            return result;
        }
    }
}
