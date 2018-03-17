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
    public partial class reportPage : ContentPage
    {
        public reportPage(int v, int result)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            getReport(v, result);
        }

        private async void getReport(int v, int result)
        {
            await BeBusy(true);
            if (v == 1 && result == 0)
            {
                headerTitle.Text = "احصائيات المقاولين";
                reportContractors report = new reportContractors();
                content.Children.Add(report);

            }
            else if (v == 2)
            {
                headerTitle.Text = "احصائيات المشاريع";
                reportProjects report = new reportProjects(result);
                content.Children.Add(report);
            }
            await BeBusy(false);
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