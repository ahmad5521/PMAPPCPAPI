using PMAPP.Models;
using PMAPP.Services;
using Syncfusion.SfBusyIndicator.XForms;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace PMAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        RestClientService rcs = new RestClientService();
        public Login()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnAppearing()
        {
            if(main.Children.Count > 1)
                BeBusy(false);
            base.OnAppearing();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await BeBusy(true);
            DataStore.setUserName(userNameEntry.Text);
            DataStore.setPassword(passwordEntry.Text);
            string digitRegex = @"^[0-9]+$";
            if ((userNameEntry.Text == null || !Regex.IsMatch(userNameEntry.Text, digitRegex)))
            {
                await BeBusy(false);
                await DisplayAlert("خطأ", "الرجاء ادخال اسم مستخدم صحيح", "ok");
                return;
            }
            else if(passwordEntry.Text == null)
            {
                await BeBusy(false);
                await DisplayAlert("خطأ", "الرجاء ادخال كلمة سر صحيحة", "ok");
                return;
            }            
            try
            {
                var userData = await IsValidUser(userNameEntry.Text, passwordEntry.Text);
                if (userData != null)
                {
                    userNameEntry.Text = "";
                    passwordEntry.Text = "";
                    await Navigation.PushAsync(new Monitoring());
                }
            }
            catch(Exception err)
            {
                await BeBusy(false);
                await DisplayAlert("خطأ", "اسم المستخدم او كلمة السر غير صحيحة", "موافق");
            }
        }

        private async Task<Users> IsValidUser(string userName, string password)
        {
            var user = await rcs.GetUserByUserNameAndPassword(userName, password);
            if(user != null)
            {
                DataStore.setUserID(user.ID);
                DataStore.setName(user.Name);
                DataStore.setModirate(user.modirate);
                DataStore.setRegon(user.regon);
                DataStore.setRole(user.role);
            }
            return user;
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
                    Title = "Please Wait..",
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