using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMAPP.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using PMAPP.Services;
using System.IO;
using System.IO.Compression;

namespace PMAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TEST : ContentPage
    {

        RestClientService rcs = new RestClientService();
        List<WorkOrdersPhotos> lwop = new List<WorkOrdersPhotos>();
        ObservableCollection<SettingsData> _settings = new ObservableCollection<SettingsData>();
        public TEST()
        {
            InitializeComponent();
            CameraButton.Clicked += CameraButton_Clicked;
            GalaryButton.Clicked += GalaryButton_Clicked;
            //bind();
        }

        private void GalaryButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TEST2());
        }

        //private async void bind()
        //{
        //    lwop = await rcs.getAllImagesAsync();
        //    foreach (WorkOrdersPhotos p in lwop)
        //    {
        //        Stream stream = new System.IO.MemoryStream(p.imageData);
        //        p.imageSource = ImageSource.FromStream(() => { return stream; });
        //    }

        //    imageListView.BindingContext = lwop;
        //}

        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });


            biContainar.IsVisible = true;
            busyindicator.IsBusy = true;
            //Convert to byte
            Byte[] ImageBytes;
            using (var memoryStream = new System.IO.MemoryStream())
            {
                photo.GetStream().CopyTo(memoryStream);
                ImageBytes = memoryStream.ToArray();
            }



            //write to server
            WorkOrdersPhotos wop = new WorkOrdersPhotos();
            wop.WOID_FK = 24;
            wop.imageData = ImageBytes;
            wop.date = System.DateTime.Now;
            bool isOk = await rcs.PostWorkOrderPhotoAsync(wop);

            if (isOk)
            {
                await DisplayAlert("", "تم الاضافة بنجاح", "ok");
                biContainar.IsVisible = false;
                busyindicator.IsBusy = false;
            }
            else
            {
                await DisplayAlert("", "خطأ", "ok");
                biContainar.IsVisible = false;
                busyindicator.IsBusy = false;
            }

            //read from server
            //WorkOrdersPhotos wopRead = new WorkOrdersPhotos();
            //Byte[] readImage;
            //wopRead = await rcs.getImageByIDAsync(27);
            //readImage = wopRead.imageData;

            //display to control
            //Stream stream = new System.IO.MemoryStream(readImage);
            //PhotoImage.Source = ImageSource.FromStream(() => { return stream; });
            //biContainar.IsVisible = false;
            //busyindicator.IsBusy = false;

        }


        

    }
    
}