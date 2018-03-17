using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMAPP.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Syncfusion.SfBusyIndicator.XForms;
using PMAPP.Services;
using PMAPP.Behaviors;

namespace PMAPP.Views.UserControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewWorkOrderConti : ContentPage
    {
        RestClientService rcs = new RestClientService();
        private WorkOrders newWO;
        private List<Parameters> lp;
        private List<WorkOrdersParameters> lwop;
        Grid myGrid = new Grid();
        List<itemsUnitValue> items = new List<itemsUnitValue>();
        int woNO, backCount;

        public NewWorkOrderConti(WorkOrders newWO, List<Parameters> lwop, int woNO, int backCount)
        {
            this.backCount = backCount;
            this.woNO = woNO;
            this.newWO = newWO;
            this.lp = lwop;
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            generateGrid();
        }


        private void generateGrid()
        {
            //km
            var myListKM = lp.Where(p => p.parameterUnitType_FKID == 1).ToList();
            int totalRows = myListKM.Count();
            int row = 0;
            for (int i = 0; i < totalRows; i++)
            {
                Label a = new Label
                {
                    Text = myListKM[i].Name,
                    Margin = new Thickness(0, 20, 20, 0),
                    HorizontalOptions = LayoutOptions.End,
                    FontFamily = "DroidKufi-Bold.ttf#DroidKufi-Bold",
                    FontSize = 14
                };
                myGrid.Children.Add(a, 1, row);


                Entry entry = new Entry
                {
                    HorizontalOptions = LayoutOptions.Center,
                    Margin = new Thickness(0, 0, 0, 0),
                    WidthRequest = 100,
                    BackgroundColor = Color.White,
                    Keyboard = Keyboard.Numeric,
                    Placeholder = "كيلومتر",
                    FontFamily = "DroidKufi-Bold.ttf#DroidKufi-Bold",
                    FontSize = 12,
                    HorizontalTextAlignment = TextAlignment.Center
                };

                IntegerBehavior ib = new IntegerBehavior();
                

                items.Add(new itemsUnitValue { pid = myListKM[i].ID, id_control = entry.Id.ToString() });
                entry.TextChanged += (object sender, TextChangedEventArgs e) =>
                {
                    foreach (var item in items)
                    {
                        if (item.id_control == entry.Id.ToString())
                        {
                            item.value = decimal.Parse(entry.Text);
                        }
                    }
                };

                myGrid.Children.Add(entry, 0, row);
                row++;
            }

            //amount
            var myListAmount = lp.Where(p => p.parameterUnitType_FKID == 2).ToList();
            totalRows = myListAmount.Count();
            for (int i = 0; i < totalRows; i++)
            {
                Label a = new Label
                {
                    Text = myListAmount[i].Name,
                    Margin = new Thickness(0, 20, 20, 0),
                    HorizontalOptions = LayoutOptions.End,
                    FontFamily = "DroidKufi-Bold.ttf#DroidKufi-Bold",
                    FontSize = 14
                };
                myGrid.Children.Add(a, 1, row);


                Entry entry = new Entry
                {
                    HorizontalOptions = LayoutOptions.Center,
                    Margin = new Thickness(0, 0, 0, 0),
                    WidthRequest = 100,
                    BackgroundColor = Color.White,
                    Keyboard = Keyboard.Numeric,
                    Placeholder = "عدد",
                    FontFamily = "DroidKufi-Bold.ttf#DroidKufi-Bold",
                    FontSize = 12,
                    HorizontalTextAlignment = TextAlignment.Center
                };

                items.Add(new itemsUnitValue { pid = myListAmount[i].ID, id_control = entry.Id.ToString() });
                entry.TextChanged += (object sender, TextChangedEventArgs e) =>
                {
                    foreach (var item in items)
                    {
                        if (item.id_control == entry.Id.ToString())
                        {
                            item.value = decimal.Parse(entry.Text);
                        }
                    }
                    string m = "";
                };

                myGrid.Children.Add(entry, 0, row);
                row++;
            }

            content.Children.Add(myGrid);
        }

        private void back_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void next_Clicked(object sender, EventArgs e)
        {
            await BeBusy(true);
            WorkOrdersParameters wop = new WorkOrdersParameters();
            lwop = new List<WorkOrdersParameters>();
            foreach (var item in items)
            {
                if (item.value == null || item.value <= 0)
                {
                    //myGrid.FindByName<Entry>(item.id_control).TextColor = Color.Red;
                    await DisplayAlert("", "الرجاءادخال قيمة صحيحة للجميع", "ok");
                    return;
                }
            }

            foreach (var item in lp)
            {
                if (item.parameterUnitType_FKID == 4)
                {
                    lwop.Add(new WorkOrdersParameters
                    {
                        workOrder_FKID = newWO.ID,
                        parameter_FKID = item.ID,
                        lastUpdate = DateTime.Now,
                        weightAmount = int.Parse(item.ParameterWight.ToString()),
                        isDone = false,
                        DoneAmount = 0,
                        DoneLength = 0,
                        Amount = 100,
                    });
                }
                else
                {
                    lwop.Add(new WorkOrdersParameters
                    {
                        workOrder_FKID = newWO.ID,
                        parameter_FKID = item.ID,
                        lastUpdate = DateTime.Now,
                        weightAmount = int.Parse(item.ParameterWight.ToString()),
                        isDone = false,
                        DoneAmount = 0,
                        DoneLength = 0
                    });
                }
            }

            foreach (var wopItem in lwop)
            {
                wopItem.workOrder_FKID = woNO;
                foreach (var item in items)
                {
                    if (wopItem.parameter_FKID == item.pid)
                    {
                        foreach (var pItem in lp)
                        {
                            if (wopItem.parameter_FKID == pItem.ID)
                            {
                                if (pItem.parameterUnitType_FKID == 1)
                                {
                                    wopItem.Lenght = int.Parse(item.value.ToString());
                                }
                                else if (pItem.parameterUnitType_FKID == 2)
                                {
                                    wopItem.Amount = int.Parse(item.value.ToString());
                                }
                            }
                        }
                    }
                }
            }



            try
            {
                bool isOk = await rcs.PostWorkOrderParametersAsync(lwop);

                if (isOk)
                {
                    await BeBusy(false);
                    await DisplayAlert("تمت الاضافة", "تمت لاضافة بنجاح", "موافق");
                    for (var counter = 1; counter < backCount; counter++)
                    {
                        Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
                    }
                    await Navigation.PopAsync();

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


    class itemsUnitValue
    {
        public string id_control { get; set; }
        public int pid { get; set; }
        public decimal value { get; set; }
    }
    
}