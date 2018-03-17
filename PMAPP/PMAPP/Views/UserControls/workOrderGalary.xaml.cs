using PMAPP.Models;
using PMAPP.Services;
using Syncfusion.SfBusyIndicator.XForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PMAPP.Views.UserControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class workOrderGalary : ContentPage
    {
        RestClientService rcs = new RestClientService();
        List<WorkOrdersPhotos> lwop = new List<WorkOrdersPhotos>();
        int n;
        public workOrderGalary(int n)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.n = n;
            populateList(n);
        }
        private async void populateList(int v)
        {
            myList.ItemsSource = null;
            await BeBusy(true);
            lwop = await rcs.getAllImagesByWorkOrderIDAsync(v);
            foreach (WorkOrdersPhotos p in lwop)
            {
                Stream stream = new System.IO.MemoryStream(p.imageData);
                p.imageSource = ImageSource.FromStream(() => { return stream; });
            }

            if (lwop.Count > 0)
                myList.ItemsSource = lwop;
            else
                Nothing.IsVisible = true;

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

        private async void myList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            var answer = await DisplayAlert("تنبيه", "سوف تقوم بحذف صورة لامر العمل", "موافق", "الغاء");
            
            if (answer)
            {
                await BeBusy(true);
                int PID = ((WorkOrdersPhotos)myList.SelectedItem).id;
                try
                {

                    bool isOk = await rcs.DeletePhotoAsync(PID);

                    if (isOk)
                    {
                        await BeBusy(false);
                        await DisplayAlert("تمت العملية", "تم الحذف بنجاح", "موافق");
                        populateList(n);
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
}