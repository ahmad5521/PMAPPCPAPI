using PMAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PMAPP.Views.InputViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EachUpdateTextInputCancellableView : ContentView
    {
        // public event handler to expose 
        // the Save button's click event
        public EventHandler SaveButtonEventHandler { get; set; }

        // public event handler to expose 
        // the Cancel button's click event
        public EventHandler CancelButtonEventHandler { get; set; }

        // public string to expose the 
        // text Entry input's value
        public string TextInputResult { get; set; }

        //public static readonly BindableProperty IsValidationLabelVisibleProperty =
        //    BindableProperty.Create(
        //        nameof(IsValidationLabelVisible),
        //        typeof(bool),
        //        typeof(KMUpdateTextInputCancellableView),
        //        false, BindingMode.OneWay, null,
        //        (bindable, value, newValue) =>
        //        {
        //            if ((bool)newValue)
        //            {
        //                ((KMUpdateTextInputCancellableView)bindable).ValidationLabel
        //                    .IsVisible = true;
        //            }
        //            else
        //            {
        //                ((KMUpdateTextInputCancellableView)bindable).ValidationLabel
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

        public EachUpdateTextInputCancellableView(string titleText, string placeHolderText,
            string saveButtonText, string cancelButtonText, string validationText, WorkOrderParametersView wopv)
        {
            InitializeComponent();

            // update the Element's textual values
            
            SaveButton.Text = saveButtonText;
            CancelButton.Text = cancelButtonText;
            //ValidationLabel.Text = validationText;


            pName.Text = wopv.ParameterName;
            totalAmmount.Text = wopv.Amount.ToString();
            doneAmmount.Text = wopv.DoneAmount.ToString();

            // handling events to expose to public
            SaveButton.Clicked += SaveButton_Clicked;
            CancelButton.Clicked += CancelButton_Clicked;
            updateValue.TextChanged += InputEntry_TextChanged;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(updateValue.Text))
            {
                ValidationLabel.Text = "لا يمكن ارسال قيمة فارغة!";
                ValidationLabel.IsVisible = true;
                return;
            }
            if (int.Parse(updateValue.Text) > int.Parse(totalAmmount.Text))
            {
                ValidationLabel.Text = "القيمة اكبر من الاجمالي!";
                ValidationLabel.IsVisible = true;
                return;
            }

            SaveButtonEventHandler?.Invoke(this, e);
        }

        private void CancelButton_Clicked(object sender, EventArgs e)
        {
            // invoke the event handler if its being subscribed
            CancelButtonEventHandler?.Invoke(this, e);
        }

        private void InputEntry_TextChanged(object sender,
            TextChangedEventArgs e)
        {
            ValidationLabel.IsVisible = false;
            // update the public string value 
            // accordingly to the text Entry's value
            TextInputResult = updateValue.Text;
        }
    }

}