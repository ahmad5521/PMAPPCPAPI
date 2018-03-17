using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMAPP.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.SfBusyIndicator.XForms;

namespace PMAPP.Views.UserControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewWorkOrderSelectParameter : ContentPage
    {
        private WorkOrders newWO;
        private List<Parameters> lp = new List<Parameters>();
        private List<Parameters> LOPForOperations;
        List<myItems> items = new List<myItems>();
        Grid myGrid = new Grid();
        int woNO;

        public NewWorkOrderSelectParameter(WorkOrders newWO, List<Parameters> lp, int woNO)
        {
            this.woNO = woNO;
            this.newWO = newWO;
            this.lp = lp;
            //this.LOPForOperations = lp;
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            generateGrid();
        }
        
        private void generateGrid()
        {
            var myList = lp.Where(p => p.ParameterWight == null && p.Name != null).ToList();
            int totalRows = myList.Count();
            for (int i = 0; i < totalRows; i++)
            {
                Label a = new Label
                {
                    Text = myList[i].Name,
                    Margin = new Thickness(0, 20, 20, 0),
                    HorizontalOptions = LayoutOptions.End,
                    FontFamily = "DroidKufi-Bold.ttf#DroidKufi-Bold",
                    FontSize = 14
                };
                myGrid.Children.Add(a, 1, i);
                Switch sw = new Switch
                {
                    HorizontalOptions = LayoutOptions.Center,
                    Margin = new Thickness(0, 20, 0, 0)
                };
                //sw.Toggled += Sw_Toggled;

                items.Add(new myItems { id = a.Id.ToString(), name = myList[i].Name, pid = myList[i].ID, id_control = sw.Id.ToString(), isToggled = false });
                sw.Toggled += (object sender, ToggledEventArgs e) => 
                {
                    foreach (var item in items)
                    {
                        if(item.id_control==sw.Id.ToString())
                        {
                            item.isToggled = sw.IsToggled;
                        }
                    }                    
                };
                myGrid.Children.Add(sw, 0, i);
            }            
            content.Children.Add(myGrid);
        }

        private void back_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void next_Clicked(object sender, EventArgs e)
        {
            LOPForOperations = new List<Parameters>(lp);
            List<int> deletPID = new List<int>();
            foreach (var item in items)
            {
                if (!item.isToggled)
                    deletPID.Add(item.pid);
            }
            
            for (int i = 0; i < deletPID.Count; i++)                
                getCleanData(deletPID[i].ToString());
            
            double totalWaight = 0, nullCount = 0, remainWaight = 0, nullWainght = 0, rmainAfterDivid = 0;

            foreach (var item in LOPForOperations)//
            {
                if (item.ParameterWight != null)
                    totalWaight += double.Parse(item.ParameterWight.ToString());
                else
                    nullCount += 1; 
            }

            remainWaight = 100 - totalWaight;
            rmainAfterDivid = remainWaight % nullCount;
            nullWainght = (remainWaight - rmainAfterDivid) / nullCount;


            foreach (var item in LOPForOperations)
            {
                if (rmainAfterDivid == 0)
                {
                    
                    if (item.ParameterWight == null)
                        item.ParameterWight = nullWainght;
                }                    
                else
                {
                    if (item.ParameterWight == null)
                    {
                        item.ParameterWight = nullWainght + (rmainAfterDivid - (rmainAfterDivid - 1));
                        rmainAfterDivid--;
                    }
                    
                }


                            
            }

            await Navigation.PushAsync(new NewWorkOrderConti(newWO, LOPForOperations, woNO, 3));//
        }

        private void getCleanData(string v)
        {
            foreach (var item in LOPForOperations)//
            {
                if (item.ID == int.Parse(v))
                {
                    LOPForOperations.Remove(item);//
                    return;
                }
            }
        }        

        private void BeBusy(bool args)
        {
            if (args)
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

    class myItems
    {

        public string id { get; set; }
        public string id_control { get; set; }
        public string name { get; set; }
        public int pid { get; set; }
        public bool isToggled { get; set; }

    }

}