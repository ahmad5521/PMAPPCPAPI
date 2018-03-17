using PMAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMAPP.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.SfBusyIndicator.XForms;
using PMAPP.Views.InputViews;
using Rg.Plugins.Popup.Services;

namespace PMAPP.Views.UserControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkOrderParametersPage : ContentPage
    {
        RestClientService rcs = new RestClientService();
        int n;
        public WorkOrderParametersPage(int number)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.n = number;
            //popelateData(number);
        }


        protected override bool OnBackButtonPressed()
        {
            //return base.OnBackButtonPressed();
            return true;
        }

        protected override void OnAppearing()
        {
            popelateData(n);
            base.OnAppearing();
        }

        private async void popelateData(int v)
        {
            await BeBusy(true);
            List<WorkOrderParametersView> wop = await rcs.getWorkOredrParametersByWONoAsync(v);
            if(wop.Count == 0)
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
                    item.injaz = "تم انجاز (KM) : " + item.DoneLength ;
                }
                else if(item.putID == 2)
                {
                    item.what = "     اجمالي العدد:";
                    item.data = item.Amount.ToString();
                    item.injaz = "تم انجاز (عدد) : " + item.DoneAmount; 
                }
                else if(item.putID == 3)
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

        private async void wopListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            string str = "";
            if (((WorkOrderParametersView)wopListView.SelectedItem).putID == 1 )
            {
                str = await OpenKMUpdateInputAlertDialog((WorkOrderParametersView)wopListView.SelectedItem);
                if(!string.IsNullOrEmpty(str))
                {
                    ((WorkOrderParametersView)wopListView.SelectedItem).DoneLength = int.Parse(str);
                    updateWOP((WorkOrderParametersView)wopListView.SelectedItem);
                }
            }
            else
            if (((WorkOrderParametersView)wopListView.SelectedItem).putID == 2)
            {
                str = await OpenEachUpdateInputAlertDialog((WorkOrderParametersView)wopListView.SelectedItem);
                if (!string.IsNullOrEmpty(str))
                {
                    ((WorkOrderParametersView)wopListView.SelectedItem).DoneAmount = int.Parse(str);
                    updateWOP((WorkOrderParametersView)wopListView.SelectedItem);
                }
            }
            else
            if (((WorkOrderParametersView)wopListView.SelectedItem).putID == 3)
            {
                str = await OpenIsDoneUpdateInputAlertDialog((WorkOrderParametersView)wopListView.SelectedItem);
                if (!string.IsNullOrEmpty(str))
                {
                    if(str == "true")
                    {
                        ((WorkOrderParametersView)wopListView.SelectedItem).isDone = true;
                        updateWOP((WorkOrderParametersView)wopListView.SelectedItem);
                    }
                    else
                    {
                        ((WorkOrderParametersView)wopListView.SelectedItem).isDone = false;
                        updateWOP((WorkOrderParametersView)wopListView.SelectedItem);
                    }
                    
                }
            }
            else
            if (((WorkOrderParametersView)wopListView.SelectedItem).putID == 4)
            {
                str = await OpenPercentageUpdateInputAlertDialog((WorkOrderParametersView)wopListView.SelectedItem);
                if (!string.IsNullOrEmpty(str))
                {
                    ((WorkOrderParametersView)wopListView.SelectedItem).DoneAmount = int.Parse(str);
                    updateWOP((WorkOrderParametersView)wopListView.SelectedItem);
                }
            }
        }

        private async void updateWOP(WorkOrderParametersView WOP)
        {
            await BeBusy(true);
            WorkOrdersParameters woptoUpdate = new WorkOrdersParameters();
            woptoUpdate.ID = WOP.wopID;
            woptoUpdate.workOrder_FKID = WOP.workOrder_FKID;
            woptoUpdate.parameter_FKID = WOP.parameter_FKID;
            woptoUpdate.Lenght = WOP.Lenght;
            woptoUpdate.Amount = WOP.Amount;
            woptoUpdate.isDone = WOP.isDone;
            woptoUpdate.lastUpdate = DateTime.Now;
            woptoUpdate.DoneLength = WOP.DoneLength;
            woptoUpdate.DoneAmount = WOP.DoneAmount;
            woptoUpdate.weightAmount = WOP.weightAmount;
            bool isOk = false;
            try
            {
                isOk = await rcs.PutWorkOrderParameterUpdateAsync(woptoUpdate.ID, woptoUpdate);
            }
            catch (Exception err)
            {

            }

            if (isOk)
            {
                popelateData(n);
                await BeBusy(false);
                await DisplayAlert("ok", "updated", "ok");
                //await Navigation.PopAsync();
            }
            else
            {
                await BeBusy(false);
                await DisplayAlert("Error", "Error", "ok");
                //await Navigation.PopAsync();
            }
        }
        


        private async Task<string> OpenIsDoneUpdateInputAlertDialog(WorkOrderParametersView wopv)
        {
            // create the TextInputView
            var inputView = new IsDoneUpdateTextInputCancellableView(
                "How's your day mate?", "enter here...", "تحديث", "الغاء", "Ops! Can't leave this empty!", wopv);

            // create the Transparent Popup Page
            // of type string since we need a string return
            var popup = new InputAlertDialogBase<string>(inputView);

            // subscribe to the TextInputView's Button click event
            inputView.SaveButtonEventHandler +=
                (sender, obj) =>
                {
                    if (!string.IsNullOrEmpty(((IsDoneUpdateTextInputCancellableView)sender).TextInputResult.ToString()))
                    {
                        //((KMUpdateTextInputCancellableView)sender).IsValidationLabelVisible = false;
                        popup.PageClosedTaskCompletionSource.SetResult(((IsDoneUpdateTextInputCancellableView)sender).TextInputResult);
                    }
                    else
                    {
                        //((KMUpdateTextInputCancellableView)sender).IsValidationLabelVisible = true;
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

        private async Task<string> OpenPercentageUpdateInputAlertDialog(WorkOrderParametersView wopv)
        {
            // create the TextInputView
            var inputView = new PercentageUpdateCancellableView(
                "How's your day mate?", "enter here...", "تحديث", "الغاء", "Ops! Can't leave this empty!", wopv);

            // create the Transparent Popup Page
            // of type string since we need a string return
            var popup = new InputAlertDialogBase<string>(inputView);

            // subscribe to the TextInputView's Button click event
            inputView.SaveButtonEventHandler +=
                (sender, obj) =>
                {
                    if (!string.IsNullOrEmpty(((PercentageUpdateCancellableView)sender).TextInputResult.ToString()))
                    {
                        //((KMUpdateTextInputCancellableView)sender).IsValidationLabelVisible = false;
                        popup.PageClosedTaskCompletionSource.SetResult(((PercentageUpdateCancellableView)sender).TextInputResult);
                    }
                    else
                    {
                        //((KMUpdateTextInputCancellableView)sender).IsValidationLabelVisible = true;
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

        private async Task<double> OpenPercentageSliderInputAlertDialog(WorkOrderParametersView wop)
        {
            // create the TextInputView
            var inputView = new PercentageUpdateSlider(
                "How much would you rate it?", 0, 10,
                "تحديث", "الغاء","", wop);

            // create the Transparent Popup Page
            // of type string since we need a string return
            var popup = new InputAlertDialogBase<double>(inputView);

            // subscribe to the TextInputView's Button click event
            inputView.SaveButtonEventHandler +=
                (sender, obj) =>
                {
                    if (((PercentageUpdateSlider)sender).SliderInputResult > 0)
                    {
                        //((PercentageUpdateSlider)sender).IsValidationLabelVisible = false;
                        popup.PageClosedTaskCompletionSource.SetResult(((SlidableInputView)sender).SliderInputResult);
                    }
                    else
                    {
                        //((PercentageUpdateSlider)sender).IsValidationLabelVisible = true;
                    }
                };

            // subscribe to the TextInputView's Button click event
            inputView.CancelButtonEventHandler +=
                (sender, obj) =>
                {
                    popup.PageClosedTaskCompletionSource.SetResult(0);
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

        private async Task<string> OpenEachUpdateInputAlertDialog(WorkOrderParametersView wopv)
        {
            // create the TextInputView
            var inputView = new EachUpdateTextInputCancellableView(
                "How's your day mate?", "enter here...", "تحديث", "الغاء", "Ops! Can't leave this empty!", wopv);

            // create the Transparent Popup Page
            // of type string since we need a string return
            var popup = new InputAlertDialogBase<string>(inputView);

            // subscribe to the TextInputView's Button click event
            inputView.SaveButtonEventHandler +=
                (sender, obj) =>
                {
                    if (!string.IsNullOrEmpty(((EachUpdateTextInputCancellableView)sender).TextInputResult.ToString()))
                    {
                        //((KMUpdateTextInputCancellableView)sender).IsValidationLabelVisible = false;
                        popup.PageClosedTaskCompletionSource.SetResult(((EachUpdateTextInputCancellableView)sender).TextInputResult);
                    }
                    else
                    {
                        //((KMUpdateTextInputCancellableView)sender).IsValidationLabelVisible = true;
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

        private async Task<string> OpenKMUpdateInputAlertDialog(WorkOrderParametersView wopv)
        {
            // create the TextInputView
            var inputView = new KMUpdateTextInputCancellableView(
                "How's your day mate?", "enter here...", "تحديث", "الغاء", "Ops! Can't leave this empty!", wopv);

            // create the Transparent Popup Page
            // of type string since we need a string return
            var popup = new InputAlertDialogBase<string>(inputView);

            // subscribe to the TextInputView's Button click event
            inputView.SaveButtonEventHandler +=
                (sender, obj) =>
                {
                    if (!string.IsNullOrEmpty(((KMUpdateTextInputCancellableView)sender).TextInputResult.ToString()))
                    {
                        //((KMUpdateTextInputCancellableView)sender).IsValidationLabelVisible = false;
                        popup.PageClosedTaskCompletionSource.SetResult(((KMUpdateTextInputCancellableView)sender).TextInputResult);
                    }
                    else
                    {
                        //((KMUpdateTextInputCancellableView)sender).IsValidationLabelVisible = true;
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

        private void cancelEvent_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void tackPicEvent_Tapped(object sender, EventArgs e)
        {

            var action = await DisplayActionSheet("الرجاء الاختيار", "الغاء", null, "التقاط صورة", "اختيار صورة");

            if(action.Equals("التقاط صورة"))
            {
                await DisplayAlert("تنبيه", "سوف تقوم بالتقاط صورة لأمر العمل" + Environment.NewLine + "بعد التقاط الصورة الرجاء منك الانتظار حتى تتم عملية رفع الصور", "موافق");
            }
            else if (action.Equals("اختيار صورة"))
            {
                await DisplayAlert("تنبيه", "سوف تقوم باختيار صورة لأمر العمل" + Environment.NewLine + "بعد اختيار الصورة الرجاء منك الانتظار حتى تتم عملية رفع الصور", "موافق");
            }



            Byte[] ImageBytes = null;
            try
            {
                if (action.Equals("التقاط صورة"))
                {
                    var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                        CompressionQuality = 25
                    });
                    //Convert to byte
                    using (var memoryStream = new System.IO.MemoryStream())
                    {
                        photo.GetStream().CopyTo(memoryStream);
                        ImageBytes = memoryStream.ToArray();
                    }
                }
                else if (action.Equals("اختيار صورة"))
                {
                    var photo = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions()
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                        CompressionQuality = 25
                    });
                    //Convert to byte
                    using (var memoryStream = new System.IO.MemoryStream())
                    {
                        photo.GetStream().CopyTo(memoryStream);
                        ImageBytes = memoryStream.ToArray();
                    }
                }

                

                await BeBusy(true);
                //write to server
                WorkOrdersPhotos wop = new WorkOrdersPhotos();
                wop.WOID_FK = n;
                wop.imageData = ImageBytes;
                wop.date = System.DateTime.Now;
                try
                {
                    bool isOk = await rcs.PostWorkOrderPhotoAsync(wop);
                    if (isOk)
                    {
                        await DisplayAlert("", "تم الاضافة بنجاح", "ok");
                        await BeBusy(false);
                    }
                    else
                    {
                        await DisplayAlert("", "خطأ", "ok");
                        await BeBusy(false);
                    }
                }
                catch (Exception err)
                {
                    await DisplayAlert("", err.Message, "ok");
                    await BeBusy(false);
                }
            }
            catch(Exception err)
            {

            }
            

            
        }

        private void galaryEvent_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new workOrderGalary(n));
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