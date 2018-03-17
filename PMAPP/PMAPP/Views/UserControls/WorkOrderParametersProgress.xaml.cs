using PMAPP.Models;
using PMAPP.Services;
using Syncfusion.SfBusyIndicator.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PMAPP.Views.UserControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkOrderParametersProgress : ContentPage
    {
        RestClientService rcs = new RestClientService();
        WorkOrderParametersView wop;

        public WorkOrderParametersProgress(int id)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            popelateData(id);
        }
        

        private async void popelateData(int id)
        {
            await BeBusy(true);
            wop = await rcs.getWorkOredrParametersByPrameterNoAsync(id);
            parameterNameValue.Text = wop.ParameterName;
            isDoneValue.IsToggled = wop.isDone;
            lastUpdateValue.Text = wop.lastUpdate.ToString();
            update.IsEnabled = false;
            await BeBusy(false);
        }

        private void isDoneValue_Toggled(object sender, ToggledEventArgs e)
        {
            //if(isDoneValue.IsToggled)
                update.IsEnabled = true;
        }

        private async void update_Clicked(object sender, EventArgs e)
        {
            await BeBusy(true);
            WorkOrdersParameters woptoUpdate = new WorkOrdersParameters();
            woptoUpdate.ID = wop.wopID;
            woptoUpdate.workOrder_FKID = wop.workOrder_FKID;
            woptoUpdate.parameter_FKID = wop.parameter_FKID;
            woptoUpdate.Lenght = wop.Lenght;
            woptoUpdate.Amount = wop.Amount;
            woptoUpdate.isDone = isDoneValue.IsToggled;
            woptoUpdate.lastUpdate = DateTime.Now;
            bool isOk = false;
            try
            {
                isOk = await rcs.PutWorkOrderParameterUpdateAsync(woptoUpdate.ID, woptoUpdate);                
            }
            catch(Exception err)
            {

            }

            if (isOk)
            {
                await BeBusy(false);
                await DisplayAlert("ok", "updated", "ok");
                await Navigation.PopAsync();
            }
            else
            {
                await BeBusy(false);
                await DisplayAlert("Error", "Error", "ok");
                await Navigation.PopAsync();
            }

        }

        private void cancelEvent_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
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