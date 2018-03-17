using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMAPP.Models;
using PMAPP.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.SfBusyIndicator.XForms;

namespace PMAPP.Views.UserControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ParametersView : ContentPage
    {
        private int wOID;
        RestClientService rcs = new RestClientService();
        
        
        public ParametersView(int wOID)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            populateList(wOID);
        }
        private async void populateList(int v)
        {
            await BeBusy(true);
            List<WorkOrderParametersView> wop = await rcs.getWorkOredrParametersByWONoAsync(v);
            if (wop.Count == 0)
                Nothing.IsVisible = true;
            else
            {
                wopListView.ItemsSource = wop;
                BindingContext = wop;
            }

            foreach (var item in wop)
            {
                if (item.putID == 1)
                {
                    item.what = "اجمالي الطول (KM):";
                    item.data = item.Lenght.ToString();
                    item.injaz = "تم انجاز (KM) : " + item.DoneLength;
                }
                else if (item.putID == 2)
                {
                    item.what = "     اجمالي العدد:";
                    item.data = item.Amount.ToString();
                    item.injaz = "تم انجاز (عدد) : " + item.DoneAmount;
                }
                else if (item.putID == 3)
                {
                    if (item.isDone)
                        item.injaz = "تم الانجاز";
                    else
                        item.injaz = "لم يتم الانجاز";
                }
                else if (item.putID == 4)
                {
                    item.injaz = "تم الانجاز بنسبة : " + item.DoneAmount.ToString() + " %";
                }


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