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
using PMAPP.Views.InputViews;
using Rg.Plugins.Popup.Services;

namespace PMAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryPage : ContentPage
    {

        RestClientService rcs = new RestClientService();
        List<WorkOrderView> searchList = new List<WorkOrderView>();
        ObservableCollection<SettingsData> _settings = new ObservableCollection<SettingsData>();

        public EntryPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            start();
            tapsColoring();
            prepareMaster();
        }

        private void prepareMaster()
        {
            _settings.Add(new SettingsData { Display = "     المستخدم:", Data = DataStore.getName() });
            _settings.Add(new SettingsData { Display = "     الادارة  :", Data = DataStore.getModirate() });
            _settings.Add(new SettingsData { Display = "     القطاع  :", Data = DataStore.getRegon() });
            _settings.Add(new SettingsData { Display = "     تسجيل خروج", Data = " " });
            EmployeeView.ItemsSource = _settings;
        }

        protected override void OnAppearing()
        {
            tapsColoring();
            start();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            sBar.Text = null;
            base.OnDisappearing();
        }

        private async void start()
        {
            await BeBusy(true);
            headerTitle.Text = "أوامر العمل";
            mainListView.ItemsSource = null;
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

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopToRootAsync();
            return true;
        }

        private void tapsColoring()
        {
            tap1.BackgroundColor = Color.Transparent;
            tap2.BackgroundColor = Color.FromHex("#47525e");
            //tap2.BackgroundColor = Color.FromHex("#19aa8d");
            tap3.BackgroundColor = Color.Transparent;
        }

        private async void moniEvent_Tapped(object sender, EventArgs e)
        {
            await monitoring.ScaleTo(0.8, 50, Easing.Linear);
            await Task.Delay(100);
            await monitoring.ScaleTo(1, 50, Easing.Linear);
            Page p = this;
            await Navigation.PushAsync(new Monitoring());
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
        
        private async void mainListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var action = await DisplayActionSheet("الرجاء الاختيار", "الغاء", null, "حذف أمر العمل", "عرض العناصر"); 
            if (action == "عرض العناصر")
            {
                WorkOrderView wo = new WorkOrderView();
                int WOID = ((WorkOrderView)mainListView.SelectedItem).ID;
                await Navigation.PushAsync(new ParametersView(WOID));
            }
            else if(action == "حذف أمر العمل")
            {
                await BeBusy(true);
                var result = await DisplayAlert("تحذير", "سوف يتم حذف امر العمل" + Environment.NewLine + Environment.NewLine + "هل تريد الاستمرار؟؟", "نعم", "الغاء");

                if (result)
                {
                    int WOID = ((WorkOrderView)mainListView.SelectedItem).ID;
                    try
                    {

                        bool isOk = await rcs.DeleteWorkOrderAsync(WOID);

                        if (isOk)
                        {
                            await BeBusy(false);
                            await DisplayAlert("تمت العملية", "تم الحذف بنجاح", "موافق");
                            OnAppearing();
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
        }

        private async void newWO_ItemTapped(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new NewWorkOrder());
            var result = await OpenSelectableInputAlertDialog();
            if (result != null)
            {
                int len = result.IndexOf("-");
                string catagory = result.Substring(0, len);
                string type = result.Substring(len + 1);


                await Navigation.PushAsync(new NewWorkOrder(int.Parse(catagory), int.Parse(type)));
            }
            else
                return;
        }

        private async Task<string> OpenSelectableInputAlertDialog()
        {
            // create the TextInputView
            var inputView = new ParameterCatagoriesSelectableInputView(
                "الرجاء اختيار نوع أمر العمل",
                "موافق", "الغاء", "يجب اختيار امر عمل اذا رغبت في المواصلة!");

            // create the Transparent Popup Page
            // of type string since we need a string return
            var popup = new InputAlertDialogBase<string>(inputView);

            // subscribe to the TextInputView's Button click event
            inputView.SaveButtonEventHandler +=
                (sender, obj) =>
                {
                    if (!string.IsNullOrEmpty(((ParameterCatagoriesSelectableInputView)sender).SelectionResult))
                    {
                        ((ParameterCatagoriesSelectableInputView)sender).IsValidationLabelVisible = false;
                        popup.PageClosedTaskCompletionSource.SetResult(((ParameterCatagoriesSelectableInputView)sender).SelectionResult);
                    }
                    else
                    {
                        ((ParameterCatagoriesSelectableInputView)sender).IsValidationLabelVisible = true;
                    }
                };

            // subscribe to the TextInputView's Button click event
            inputView.CancelButtonEventHandler +=
                (sender, obj) =>
                {
                    popup.PageClosedTaskCompletionSource.SetResult(null);
                };

            // Push the page to Navigation Stack
            await PopupNavigation.PushAsync(popup);

            // await for the user to enter the text input
            var result = await popup.PageClosedTask;

            // Pop the page from Navigation Stack
            await PopupNavigation.PopAsync();

            // return user inserted text value
            return result;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(sBar.Text))
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


        private void settings_ItemTapped(object sender, EventArgs e)
        {
            empty.IsVisible = true;
            Master.IsVisible = true;
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