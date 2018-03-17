using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PMAPP.Views.UserControls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PMAPP.Services;
using PMAPP.Models;
using System.Collections.ObjectModel;
using Syncfusion.SfBusyIndicator.XForms;

namespace PMAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Monitoring : ContentPage
    {
        RestClientService rcs = new RestClientService();
        List<WorkOrderView> searchList = new List<WorkOrderView>();
        ObservableCollection<SettingsData> _settings = new ObservableCollection<SettingsData>();


        public Monitoring()
        {
            InitializeComponent();
            head.RowSpacing = 0;
            head.ColumnSpacing = 0;
            NavigationPage.SetHasNavigationBar(this, false);
            allWorkOrder();
            tapsColoring();
            prepareMaster();
        }

        private void prepareMaster()
        {
            _settings.Add(new SettingsData { Display = "     المستخدم:", Data = DataStore.getName() });
            _settings.Add(new SettingsData { Display = "     الادارة  :", Data = DataStore.getModirate() });
            _settings.Add(new SettingsData { Display = "     القطاع  :", Data = DataStore.getRegon() });
            _settings.Add(new SettingsData { Display = "     تسجيل خروج", Data = " "});
            EmployeeView.ItemsSource = _settings;
        }

        protected override void OnAppearing()
        {
            allWorkOrder();
            tapsColoring();
            base.OnAppearing();
        }


        protected override void OnDisappearing()
        {
            sBar.Text = null;
            base.OnDisappearing();
        }

        private async void allWorkOrder()
        {
            await BeBusy(true);
            headerTitle.Text = "جميع أوامر العمل";
            var wovv = await rcs.getWorkOrderViewByIDAsync(DataStore.getUserID());
            if (wovv.Count > 0)
            {
                mainListView.ItemsSource = wovv;
                foreach (WorkOrderView wov in mainListView.ItemsSource)
                {
                    if (wov.ProjectName == null)
                        wov.ProjectName = "غير مرتبط بمشروع";
                    wov.Duration = (DateTime.Today.Date - wov.startingDate).Value.Days;
                }
                searchList = (List<WorkOrderView>)mainListView.ItemsSource;
                Nothing.IsVisible = false;
            }
            else { Nothing.IsVisible = true; }



            await BeBusy(false);
        }

        private void tapsColoring()
        {
            tap1.BackgroundColor = Color.FromHex("#47525e");
            tap2.BackgroundColor = Color.Transparent;
            tap3.BackgroundColor = Color.Transparent;
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopToRootAsync();
            return true;
        }
        
        private async void entryEvent_Tapped(object sender, EventArgs e)
        {
            await entry.ScaleTo(0.8, 50, Easing.Linear);
            await Task.Delay(100);
            await entry.ScaleTo(1, 50, Easing.Linear);
            Page p = this;
            await Navigation.PushAsync(new EntryPage(), true);
            Navigation.RemovePage(p);
        }

        private async void reportEvent_Tapped(object sender, EventArgs e)
        {
            await report.ScaleTo(0.8, 50, Easing.Linear);
            await Task.Delay(100);
            await report.ScaleTo(1, 50, Easing.Linear);
            Page p = this;
            await Navigation.PushAsync(new Reports());
            Navigation.RemovePage(p);
        }

        private async void todayWOEvent_Tapped(object sender, EventArgs e)
        {
            await BeBusy(true);
            var source = imgcontainar.Source as FileImageSource;
            var filename = source.File;

            if (filename == "myWO.gif") 
            {
                headerTitle.Text = "اوامر العمل الشخصية";
                imgcontainar.Source = "allWO.gif";
                var wovv = await rcs.getTodatWorkOrderViewByIDAsync(DataStore.getUserID());
                if (wovv.Count > 0)
                {
                    mainListView.ItemsSource = wovv;
                    foreach (WorkOrderView wov in mainListView.ItemsSource)
                        wov.Duration = (DateTime.Today.Date - wov.startingDate).Value.Days;
                    searchList = (List<WorkOrderView>)mainListView.ItemsSource;
                    Nothing.IsVisible = false;
                }
                else
                    Nothing.IsVisible = true;
                
            }
            else if(filename == "allWO.gif")
            {
                headerTitle.Text = "جميع أوامر العمل";
                imgcontainar.Source = "myWO.gif";
                var wovv = await rcs.getWorkOrderViewByIDAsync(DataStore.getUserID());
                if (wovv.Count > 0)
                {
                    mainListView.ItemsSource = wovv;
                    foreach (WorkOrderView wov in mainListView.ItemsSource)
                        wov.Duration = (DateTime.Today.Date - wov.startingDate).Value.Days;
                    searchList = (List<WorkOrderView>)mainListView.ItemsSource;
                    Nothing.IsVisible = false;
                }
                else
                    Nothing.IsVisible = true;
                
            }
            await BeBusy(false);
        }

        private async void mainListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await Navigation.PushAsync(new WorkOrderParametersPage(((WorkOrderView)mainListView.SelectedItem).ID));
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(!string.IsNullOrEmpty(sBar.Text))
            {
                mainListView.ItemsSource = searchList.Where(l => l.workOrderNo.ToLower().Contains(sBar.Text.ToLower())
                           || l.Contractor.ToLower().Contains(sBar.Text.ToLower())
                           || l.Location.ToLower().Contains(sBar.Text.ToLower())
                           || l.ProjectName.ToLower().Contains(sBar.Text.ToLower())
                           || l.ParametarCatagory.ToLower().Contains(sBar.Text.ToLower())
                           || l.VoltageLevels.ToLower().Contains(sBar.Text.ToLower())
                           || l.WorkOrderStatus.ToLower().Contains(sBar.Text.ToLower())
                           || l.userName.ToLower().Contains(sBar.Text.ToLower()));
            }
        }

        private async void settings_ItemTapped(object sender, EventArgs e)
        {
            Master.IsVisible = true;
            empty.IsVisible = true;
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

        private void empty_Tapped(object sender, EventArgs e)
        {
            empty.IsVisible = false;
            Master.IsVisible = false;
        }


        private async void EmployeeView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            if (((SettingsData)EmployeeView.SelectedItem).Data == " ")
            {
                var answer = await DisplayAlert("Exit", "Do you wan't to exit the App?", "Yes", "No");
                if (answer)
                {
                    Master.IsVisible = false;
                    empty.IsVisible = false;
                    await BeBusy(true);
                    await Task.Delay(2000);
                    await BeBusy(false);
                    await Navigation.PopToRootAsync();
                }
                else return;
            }
            else
            {
                return;
            }


        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("tel:920005804"));
        }
    }
}