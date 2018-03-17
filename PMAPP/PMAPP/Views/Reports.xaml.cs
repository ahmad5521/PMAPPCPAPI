using PMAPP.Models;
using PMAPP.Views.InputViews;
using PMAPP.Views.UserControls;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfBusyIndicator.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PMAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Reports : ContentPage
    {

        ObservableCollection<SettingsData> _settings = new ObservableCollection<SettingsData>();
        public Reports()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
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

        private async void tapsColoring()
        {
            await BeBusy(true);
            headerTitle.Text = "التقارير";
            tap1.BackgroundColor = Color.Transparent;
            tap2.BackgroundColor = Color.Transparent;
            tap3.BackgroundColor = Color.FromHex("#47525e");
            await Task.Delay(600);
            await BeBusy(false);
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PopToRootAsync();
            return true;
        }

        private async void moniEvent_Tapped(object sender, EventArgs e)
        {
            await monitoring.ScaleTo(0.8, 50, Easing.Linear);
            await Task.Delay(100);
            await monitoring.ScaleTo(1, 50, Easing.Linear);  
            Page p = this;
            //Navigation.PopAsync();
            await Navigation.PushAsync(new Monitoring());
            Navigation.RemovePage(p);
        }

        private async void entryEvent_Tapped(object sender, EventArgs e)
        {
            await entry.ScaleTo(0.8, 50, Easing.Linear);
            await Task.Delay(100);
            await entry.ScaleTo(1, 50, Easing.Linear);
            Page p = this;
            //Navigation.PopAsync();
            await Navigation.PushAsync(new EntryPage());
            Navigation.RemovePage(p);

        }

        private async void contractorsReport_Clicked(object sender, EventArgs e)
        {
            if (DataStore.getRole().Equals("normal"))
            {
                await crf.ScaleTo(0.8, 50, Easing.Linear);
                await Task.Delay(100);
                await crf.ScaleTo(1, 50, Easing.Linear);
                await DisplayAlert("خطأ", "ليس لديك صلاحية :)", "موافق");
            }
            else
            {
                await crf.ScaleTo(0.8, 50, Easing.Linear);
                await Task.Delay(100);
                await crf.ScaleTo(1, 50, Easing.Linear);
                await Navigation.PushAsync(new reportPage(1, 0));
            }            
        }

        private async void projectsReport_Clicked(object sender, EventArgs e)
        {
            if (DataStore.getRole().Equals("normal"))
            {
                await prf.ScaleTo(0.8, 50, Easing.Linear);
                await Task.Delay(100);
                await prf.ScaleTo(1, 50, Easing.Linear);
                await DisplayAlert("خطأ", "ليس لديك صلاحية :)", "موافق");
            }
            else
            {
                await prf.ScaleTo(0.8, 50, Easing.Linear);
                await Task.Delay(100);
                await prf.ScaleTo(1, 50, Easing.Linear);
                var result = await OpenSelectableInputAlertDialog();
                if (result != null)
                    await Navigation.PushAsync(new reportPage(2, int.Parse(result)));
                else
                    return;
            }            
        }

        private async Task<string> OpenSelectableInputAlertDialog()
        {
            // create the TextInputView
            var inputView = new ProjectsSelectableInputView(
                "الرجاء اختيار المشروع :",
                "موافق", "الغاء", "يجب اختيار مشروع اذا رغبت في المواصلة!");

            // create the Transparent Popup Page
            // of type string since we need a string return
            var popup = new InputAlertDialogBase<string>(inputView);

            // subscribe to the TextInputView's Button click event
            inputView.SaveButtonEventHandler +=
                (sender, obj) =>
                {
                    if (!string.IsNullOrEmpty(((ProjectsSelectableInputView)sender).SelectionResult))
                    {
                        //((ProjectsSelectableInputView)sender).IsValidationLabelVisible = false;
                        popup.PageClosedTaskCompletionSource.SetResult(((ProjectsSelectableInputView)sender).SelectionResult);
                    }
                    else
                    {
                        //((ProjectsSelectableInputView)sender).IsValidationLabelVisible = true;
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


        private void settings_ItemTapped(object sender, EventArgs e)
        {
            empty.IsVisible = true;
            Master.IsVisible = true;
        }

        private async void exitBtn_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Exit", "Do you wan't to exit the App?", "Yes", "No");
            if (answer)
            {
                Master.IsVisible = false;
                await BeBusy(true);
                await Task.Delay(2000);
                await BeBusy(false);
                await Navigation.PopToRootAsync();
            }
            else return;
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