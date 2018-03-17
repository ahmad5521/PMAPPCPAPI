using PMAPP.Models;
using PMAPP.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PMAPP.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TEST2 : ContentPage
    {

        string XAMLMENUE = "         < StackLayout >" +
                   "< BoxView HeightRequest = \"30\" />" +
                  " < Image HeightRequest = \"50\" WidthRequest=\"50\" HorizontalOptions=\"Center\" VerticalOptions=\"Start\" Source=\"login.png\"/>" +
                  " <BoxView HeightRequest = \"30\" />" +
                  " < ListView x:Name=\"EmployeeView\">" +
                       "<ListView.ItemTemplate>" +
                       "<DataTemplate>" +
                              " <ViewCell xfg:CellGloss.BackgroundColor=\"#f8f9fa\">" +
                                  " <Grid>" +
                                      " <Grid.ColumnDefinitions>" +
                                           "<ColumnDefinition Width = \"*\" />" +
                                           "< ColumnDefinition Width=\"auto\"/>" +
                                       "</Grid.ColumnDefinitions>" +
                                       "<Label Text = \"{Binding Display}\"" +
                                      "Grid.Column=\"1\" "+
                                       "FontFamily=\"DroidKufi-Regular.ttf#DroidKufi-Regular\"" +
                                       "IsVisible=\"True\"" +
                                       "TextColor=\"Black\"" +
                                       "FontSize=\"12\"" +
                                       "VerticalTextAlignment=\"Center\"" +
                                       "HorizontalTextAlignment=\"End\"/>" +
                                        "<Label Text = \"{Binding Data}\"" +
                                       "Grid.Column=\"0\"" +
                                       "FontFamily=\"DroidKufi-Regular.ttf#DroidKufi-Regular\"" +
                                       "IsVisible=\"True\"" +
                                       "TextColor=\"Black\"" +
                                       "FontSize=\"12\"" +
                                       "VerticalTextAlignment=\"Center\"" +
                                       "HorizontalTextAlignment=\"End\"/>" +
                                    "</Grid>" +
                                "</ViewCell>" +
                            "</DataTemplate>" +
                        "</ListView.ItemTemplate>" +
                    "</ListView>" +
                    "<StackLayout HeightRequest = \"200\" Orientation=\"Horizontal\" VerticalOptions=\"Start\" HorizontalOptions=\"Center\">" +
                        "<Button x:Name=\"exitBtn\" VerticalOptions=\"Start\" Text=\"خروج\"   HorizontalOptions=\"Center\"" +
                               " FontFamily=\"DroidKufi-Regular.ttf#DroidKufi-Regular\" HeightRequest=\"50\"" +
                               " BackgroundColor=\"#47525e\" TextColor=\"White\" Clicked=\"exitBtn_Clicked\"/>" +
                        "<Button x:Name=\"backBtn\" VerticalOptions=\"Start\" Text=\"رجوع\"   HorizontalOptions=\"Center\"" +
                              "  FontFamily=\"DroidKufi-Regular.ttf#DroidKufi-Regular\" HeightRequest=\"50\" " +
                              "  BackgroundColor=\"#47525e\" TextColor=\"White\" Clicked=\"backBtn_Clicked\"/>" +
                    "</StackLayout>" +
               " </StackLayout>";

        RestClientService rcs = new RestClientService();
        List<WorkOrdersPhotos> lwop = new List<WorkOrdersPhotos>();
        public TEST2()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }


       

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await ShowMaster();
        }




       



        private async Task ShowMaster()
        {
            var empty = new StackLayout
            {
                IsVisible = true,
                //BackgroundColor = Color.FromHex("#ffffff"),
                BackgroundColor = Color.Gray,
                Opacity = .4,
                
            };
            var emptyGestureRecognizer = new TapGestureRecognizer();
            emptyGestureRecognizer.Tapped += EmptyGestureRecognizer_Tapped;
            empty.GestureRecognizers.Add(emptyGestureRecognizer);
            AbsoluteLayout.SetLayoutBounds(empty, new Rectangle(0, 1,.2, 1));
            AbsoluteLayout.SetLayoutFlags(empty, AbsoluteLayoutFlags.All);


	        // CONSTRACTE MENUE HEAR
            var mnu = new StackLayout
            {
                IsVisible = true,
                BackgroundColor = Color.Red,
                Opacity = 1
            };
            AbsoluteLayout.SetLayoutBounds(mnu, new Rectangle(1, 1, .8, 1));
            AbsoluteLayout.SetLayoutFlags(mnu, AbsoluteLayoutFlags.All);

            BoxView box = new BoxView
            {
                HeightRequest = 30
            };

            Image img = new Image
            {
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = 50,
                WidthRequest = 50,
                Source = "login.png"
            };

            BoxView box2 = new BoxView
            {
                HeightRequest = 30
            };





            var mainLayout = new StackLayout
            {
                IsVisible = true
            };
            
            main.Children.Add(mnu);
            main.Children.Add(empty);            
        }

        private void EmptyGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            main.Children.Remove(main.Children.Last());
            main.Children.Remove(main.Children.Last());
        }

    }
}