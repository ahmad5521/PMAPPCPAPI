using PMAPP.Models;
using PMAPP.Services;
using Syncfusion.SfBusyIndicator.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PMAPP.Views.UserControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewWorkOrder : ContentPage
    {
        RestClientService rcs = new RestClientService();
        private int v, v1;

        public NewWorkOrder()
        {
            
        }

        public NewWorkOrder(int v, int v1)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.v = v;
            this.v1 = v1;
            PopelateData(v);
        }

        private async void PopelateData(int pcNo)
        {
            BeBusy(true);

            var lookups = await rcs.GetLookUPsData();

            //Projects
            List<Projects> ProjectsList = new List<Projects>();
            ProjectsList = lookups.projects;
            projectName.ItemsSource = ProjectsList;
            projectName.ItemDisplayBinding = new Binding("Name");

            //Contractors
            List<Contractors> ContractorstList = new List<Contractors>();
            ContractorstList = lookups.contractors;
            contractor.ItemsSource = ContractorstList;
            contractor.ItemDisplayBinding = new Binding("Name");

            //locations
            List<Locations> LocationstList = new List<Locations>();
            LocationstList = lookups.locations;
            location.ItemsSource = LocationstList;
            location.ItemDisplayBinding = new Binding("Name");

            ////ParameterCatagory
            //List<ParameterCategory> ParameterCategoryList = new List<ParameterCategory>();
            //ParameterCategoryList = lookups.parameterCatagories;
            //parameterCategory.ItemsSource = ParameterCategoryList;
            //parameterCategory.ItemDisplayBinding = new Binding("Name");

            //VoltageLevels
            List<VoltageLevels> VoltageLevelstList = new List<VoltageLevels>();
            VoltageLevelstList = lookups.voltageLevels;
            voltageLevel.ItemsSource = VoltageLevelstList;
            voltageLevel.ItemDisplayBinding = new Binding("Name");

            //Users
            List<Users> WorkOrderUserList = new List<Users>();
            WorkOrderUserList = lookups.users;
            workOrderUser.ItemsSource = WorkOrderUserList;
            workOrderUser.ItemDisplayBinding = new Binding("Name");

            if(DataStore.getRole().Equals("normal"))
            {
                workOrderUserLbl.IsVisible = false;
                workOrderUser.IsVisible = false;
            }

            BeBusy(false);
        }

        private void cancelEvent_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void SaveEvent_Tapped(object sender, EventArgs e)
        {
            BeBusy(true);
            string digitRegex = @"^[0-9]+$";
            
            if (woNo.Text == null || !Regex.IsMatch(woNo.Text, digitRegex))
            {
                BeBusy(false);
                await DisplayAlert("خطأ", "الرجاء ادخال قيمة صحيحة لرقم أمر العمل", "ok");
                return;
            }
            if(v1 == 1)
            {
                if (projectName.SelectedIndex == -1)
                {
                    BeBusy(false);
                    await DisplayAlert("خطأ", "الرجاء تحديد اسم المشروع", "ok");
                    return;
                }
            }            
            if (contractor.SelectedIndex == -1)
            {
                BeBusy(false);
                await DisplayAlert("خطأ", "الرجاء تحديد اسم المقاول", "ok");
                return;
            }
            if (location.SelectedIndex == -1)
            {
                BeBusy(false);
                await DisplayAlert("خطأ", "الرجاء تحديد الموقع", "ok");
                return;
            }
            //if (Length.Text == null || !Regex.IsMatch(Length.Text, digitRegex))
            //{
            //    BeBusy(false);
            //    await DisplayAlert("خطأ", "الرجاء ادخال قيمة صحيحة لطول المشروع", "ok");
            //    return;
            //}
            //if (parameterCategory.SelectedIndex == -1)
            //{
            //    BeBusy(false);
            //    await DisplayAlert("خطأ", "الرجاء تحديد نوع المشروع", "ok");
            //    return;
            //}
            if (voltageLevel.SelectedIndex == -1)
            {
                BeBusy(false);
                await DisplayAlert("خطأ", "الرجاء تحديد الجهد", "ok");
                return;
            }
            //if (parameterCategory.SelectedIndex == -1)
            //{
            //    BeBusy(false);
            //    await DisplayAlert("خطأ", "الرجاء تحديد نوع المشروع", "ok");
            //    return;
            //}
            if (workOrderValue.Text == null || !Regex.IsMatch(workOrderValue.Text, digitRegex))
            {
                BeBusy(false);
                await DisplayAlert("خطأ", "الرجاء ادخال قيمة صحيحة لاجمالي المبلغ", "ok");
                return;
            }
            //if (workOrderUser.SelectedIndex == -1)
            //{
            //    BeBusy(false);
            //    await DisplayAlert("خطأ", "الرجاء تحديد المستخدم", "ok");
            //    return;
            //}
            bool isExist = await rcs.getWorkOrderByNumberAsyncTOF(int.Parse(woNo.Text));
             if (isExist)
            {
                BeBusy(false);
                await DisplayAlert("خطأ", "رقم امر العمل موجود مسبقا", "ok");
                return;
            }
            WorkOrders newWO = new WorkOrders();
            List<Parameters> lp = new List<Parameters>();
            try
            {
                newWO.Number = woNo.Text;
                if (projectName.SelectedItem != null )
                    newWO.project_FKID = ((Projects)projectName.SelectedItem).ID;
                else
                    newWO.project_FKID = null;                
                newWO.contractors_FKID = ((Contractors)contractor.SelectedItem).ID;
                newWO.location_FKID = ((Locations)location.SelectedItem).ID;
                newWO.startingDate = DateTime.Parse(StartDate.Date.ToString());
                newWO.ParameterCategory_FKID = v;
                newWO.voltageLevels_FKID = ((VoltageLevels)voltageLevel.SelectedItem).ID;
                if (workOrderUser.SelectedItem != null)
                    newWO.user_FKID = ((Users)workOrderUser.SelectedItem).ID;
                else
                    newWO.user_FKID = DataStore.getUserID();
                newWO.workOrderStatus_FKID = 1;
                newWO.dateTimeStamp = DateTime.Now;
                newWO.value = long.Parse(workOrderValue.Text);
                newWO.maxDueDays = 40;



                bool isOk = await rcs.PostWorkOrderUpdateAsync(newWO);

                if (isOk)
                {
                    BeBusy(false);
                    await DisplayAlert("تمت الاضافة", "تمت لاضافة بنجاح", "موافق");
                    //await Navigation.PopAsync();

                }
                else
                {
                    BeBusy(false);
                    await DisplayAlert("خطأ", "توجد مشكلة", "موافق");
                }
            }
            catch
            {
                BeBusy(false);
                await DisplayAlert("خطأ", "حدث خطأ ما!", "ok");
            }

            var woNO = await rcs.getWorkOrderByNumberAsync(int.Parse(woNo.Text));
            

            lp = await rcs.getAllParameterBYCategoryAsync(v);
            bool isSelectParameter = false;
            foreach (var item in lp)
            {
                if (item.ParameterWight == null)
                    isSelectParameter = true;
            }

            
            
            if (isSelectParameter)
                await Navigation.PushAsync(new NewWorkOrderSelectParameter(newWO, lp, woNO));
                
            else
            {
                await Navigation.PushAsync(new NewWorkOrderConti(newWO, lp, woNO, 2));
            }
        }

        private void BeBusy(bool args)
        {
            if (args)
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