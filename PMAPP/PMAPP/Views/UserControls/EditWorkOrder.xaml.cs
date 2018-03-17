using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PMAPP.Models;
using PMAPP.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Text.RegularExpressions;
using Syncfusion.SfBusyIndicator.XForms;

namespace PMAPP.Views.UserControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditWorkOrder : ContentPage
    {
        private int ID;
        RestClientService rcs = new RestClientService();
        WorkOrders wo = new WorkOrders();

        //public editForm() => InitializeComponent();
        public EditWorkOrder(int ID)
        {
            
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.ID = ID;
            popelateData(ID);
        }

        private async void popelateData(int ID)
        {
            await BeBusy(true);
            try
            {                
                //WorkOrders wo = new WorkOrders();
                wo = await rcs.getWorkOrderByIDAsync(ID);
                var lookups = await rcs.GetLookUPsData();


                //ID
                woNo.Text = wo.Number;

                //Projects
                List<Projects> ProjectsList = new List<Projects>();
                ProjectsList = lookups.projects;
                projectName.ItemsSource = ProjectsList;
                projectName.ItemDisplayBinding = new Binding("Name");
                projectName.SelectedItem = ProjectsList.Find(p => p.ID == wo.project_FKID);


                //Contractors
                List<Contractors> ContractorstList = new List<Contractors>();
                ContractorstList = lookups.contractors;
                contractor.ItemsSource = ContractorstList;
                contractor.ItemDisplayBinding = new Binding("Name");
                contractor.SelectedItem = ContractorstList.Find(p => p.ID == wo.contractors_FKID);

                //Place
                List<Locations> LocationstList = new List<Locations>();
                LocationstList = lookups.locations;
                location.ItemsSource = LocationstList;
                location.ItemDisplayBinding = new Binding("Name");
                location.SelectedItem = LocationstList.Find(p => p.ID == wo.location_FKID);

                //Length
                //Length.Text = wo.totalLenght.ToString();

                //startDate
                StartDate.Date = wo.startingDate.Value;

                //ParameterCatagory
                //List<ParameterCategory> ParameterCategoryList = new List<ParameterCategory>();
                //ParameterCategoryList = await rcs.getAllParameterCategoryAsync();
                //parameterCategory.ItemsSource = ParameterCategoryList;
                //parameterCategory.ItemDisplayBinding = new Binding("Name");
                //parameterCategory.SelectedItem = ParameterCategoryList.Find(p => p.ID == wo.ParameterCategory_FKID);

                //VoltageLevels
                List<VoltageLevels> VoltageLevelstList = new List<VoltageLevels>();
                VoltageLevelstList = lookups.voltageLevels;
                voltageLevel.ItemsSource = VoltageLevelstList;
                voltageLevel.ItemDisplayBinding = new Binding("Name");
                voltageLevel.SelectedItem = VoltageLevelstList.Find(p => p.ID == wo.voltageLevels_FKID);

                //workOrderValue
                workOrderValue.Text = wo.value.ToString();

                //Users
                List<Users> WorkOrderUserList = new List<Users>();
                WorkOrderUserList = lookups.users;
                workOrderUser.ItemsSource = WorkOrderUserList;
                workOrderUser.ItemDisplayBinding = new Binding("Name");
                workOrderUser.SelectedItem = WorkOrderUserList.Find(p => p.ID == wo.user_FKID);



                await BeBusy(false);
            }
            catch(Exception err)
            {
                await DisplayAlert("", err.Message, "ok");
            }
        }


        private void cancelEvent_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void saveEvent_Tapped(object sender, EventArgs e)
        {
            await BeBusy(true);
            string digitRegex = @"^[0-9]+$";
            //if(Length.Text == null || !Regex.IsMatch(Length.Text, digitRegex))
            //{
            //    await BeBusy(false);
            //    await DisplayAlert("خطأ", "الرجاء ادخال قيمة صحيحة لطول المشروع", "ok");
            //    return;
            //}
            if (workOrderValue.Text == null || !Regex.IsMatch(workOrderValue.Text, digitRegex))
            {
                await BeBusy(false);
                await DisplayAlert("خطأ", "الرجاء ادخال قيمة صحيحة لاجمالي المبلغ", "ok");
                return;
            }

            var result = await DisplayAlert("تحذير", "عند تحديث بيانات أمر العمل سوف يتم حذف بيانات المتابعة السابقة لنفس امر العمل" + Environment.NewLine + Environment.NewLine + "هل تريد الاستمرار؟؟", "نعم", "الغاء");

            if(result)
            {                
                WorkOrders newWO = new WorkOrders();
                try
                {
                    newWO.ID = wo.ID;
                    newWO.project_FKID = ((Projects)projectName.SelectedItem).ID;
                    newWO.contractors_FKID = ((Contractors)contractor.SelectedItem).ID;
                    newWO.location_FKID = ((Locations)location.SelectedItem).ID;
                    //newWO.totalLenght = int.Parse(Length.Text);
                    newWO.startingDate = DateTime.Parse(StartDate.Date.ToString());
                    //newWO.ParameterCategory_FKID = ((ParameterCategory)parameterCategory.SelectedItem).ID;
                    newWO.voltageLevels_FKID = ((VoltageLevels)voltageLevel.SelectedItem).ID;

                    //if (workOrderStatus.SelectedItem != null)
                    //    newWO.workOrderStatus_FKID = ((WorkOrderStatus)workOrderStatus.SelectedItem).ID;
                    if (workOrderUser.SelectedItem != null)
                        newWO.user_FKID = ((Users)workOrderUser.SelectedItem).ID; ;

                    newWO.workOrderStatus_FKID = 1;
                    newWO.value = long.Parse(workOrderValue.Text);
                    newWO.Number = woNo.Text;
                    newWO.dateTimeStamp = DateTime.Now;
                    newWO.maxDueDays = wo.maxDueDays;

                    bool isOk = await rcs.DeleteWorkOrderAsync(wo.ID);

                    if (isOk)
                    {
                        await BeBusy(false);
                        await DisplayAlert("تم التحديث", "تم التحديث بنجاح", "موافق");
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await BeBusy(false);
                        await DisplayAlert("خطأ", "توجد مشكلة", "موافق");
                    }
                }
                catch (Exception err)
                {
                    await BeBusy(false);
                    await DisplayAlert("Error", err.InnerException.ToString(), "Ok");
                }

            }
            else
            {
                return;
            }
        }

        private async Task BeBusy(bool args)
        {
            if(args)
            {
                var layout = new StackLayout
                {
                    IsVisible = true,
                    BackgroundColor = Color.FromHex("#ffffff"),
                    Opacity = .8
                };
                AbsoluteLayout.SetLayoutBounds(layout, new Rectangle(.5, .5, 1, 1));
                AbsoluteLayout.SetLayoutFlags(layout, AbsoluteLayoutFlags.All);


                var bysyIndic = new SfBusyIndicator
                {
                    AnimationType = AnimationTypes.DoubleCircle,
                    ViewBoxWidth = 70,
                    Title = "please wait..",
                    IsBusy = true,
                    ViewBoxHeight = 70,
                    TextColor = Color.FromHex("#47525e")
                };

                AbsoluteLayout.SetLayoutBounds(bysyIndic, new Rectangle(.5, .5, 1, 1));
                AbsoluteLayout.SetLayoutFlags(bysyIndic, AbsoluteLayoutFlags.All);


                main.Children.Add(layout);
                main.Children.Add(bysyIndic);
            }
            else
            {
                main.Children.Remove(main.Children.Last());
                main.Children.Remove(main.Children.Last());
            }
                        
        }
    }
}