using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMAPP.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PMAPP.Models;


namespace PMAPP.Views.InputViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProjectsSelectableInputView : ContentView
    {
        RestClientService rcs = new RestClientService();

        // public event handler to expose 
        // the Save button's click event
        public EventHandler SaveButtonEventHandler { get; set; }

        // public event handler to expose 
        // the Cancel button's click event
        public EventHandler CancelButtonEventHandler { get; set; }

        // public string to expose the 
        // text Entry input's value
        public string SelectionResult { get; set; }

        //public static readonly BindableProperty IsValidationLabelVisibleProperty =
        //    BindableProperty.Create(
        //        nameof(IsValidationLabelVisible),
        //        typeof(bool),
        //        typeof(ParameterCatagoriesSelectableInputView),
        //        false, BindingMode.OneWay, null,
        //        (bindable, value, newValue) =>
        //        {
        //            if ((bool)newValue)
        //            {
        //                ((ParameterCatagoriesSelectableInputView)bindable).ValidationLabel
        //                    .IsVisible = true;
        //            }
        //            else
        //            {
        //                ((ParameterCatagoriesSelectableInputView)bindable).ValidationLabel
        //                    .IsVisible = false;
        //            }
        //        });

        /// <summary>
        /// Gets or Sets if the ValidationLabel is visible
        /// </summary>
        //public bool IsValidationLabelVisible
        //{
        //    get
        //    {
        //        return (bool)GetValue(IsValidationLabelVisibleProperty);
        //    }
        //    set
        //    {
        //        SetValue(IsValidationLabelVisibleProperty, value);
        //    }
        //}

        public ProjectsSelectableInputView(string titleText,
            string saveButtonText, string cancelButtonText, string validationText)
        {
            InitializeComponent();

            // update the Element's textual values
            TitleLabel.Text = titleText;

            //var template = new DataTemplate(typeof(TextCell));
            //template.SetBinding(TextCell.TextProperty, "String");

            filListCatString();

            //SaveButton.Text = saveButtonText;
            CancelButton.Text = cancelButtonText;
            ValidationLabel.Text = validationText;

            // handling events to expose to public
            //SaveButton.Clicked += SaveButton_Clicked;
            CancelButton.Clicked += CancelButton_Clicked;
            SelectionListView.ItemSelected += InputEntryOnItemSelected;
        }

        private async void filListCatString()
        {
            bi.IsBusy = true;
            List<string> myCat = new List<string>();
            var cat = await rcs.getAllProjectsAsync();
            foreach (Projects pc in cat)
                myCat.Add(pc.Name);
            SelectionListView.ItemsSource = cat;
            bi.IsBusy = false;
        }

        private void InputEntryOnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            Projects pc = new Projects();
            pc = (Projects)selectedItemChangedEventArgs.SelectedItem;
            SelectionResult = pc.ID.ToString();
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            // invoke the event handler if its being subscribed
            SaveButtonEventHandler?.Invoke(this, e);
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            // invoke the event handler if its being subscribed
            CancelButtonEventHandler?.Invoke(this, e);
        }

        private void SelectionListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            // invoke the event handler if its being subscribed
            SaveButtonEventHandler?.Invoke(this, e);
        }
        
    }
}




