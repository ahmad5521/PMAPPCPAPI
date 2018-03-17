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
    public partial class IsDoneUpdateTextInputCancellableView : ContentView
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

        public IsDoneUpdateTextInputCancellableView(string titleText, string placeHolderText,
            string saveButtonText, string cancelButtonText, string validationText, WorkOrderParametersView wopv)
        {
            InitializeComponent();

            // update the Element's textual values
            
            SaveButton.Text = saveButtonText;
            CancelButton.Text = cancelButtonText;
            //ValidationLabel.Text = validationText;
            //isDone.Text = wopv.isDone.ToString();

            if (wopv.isDone == true)
            {
                isDone.Text = "تم الانجاز";
                updateValue.Text = "true";
                isDoneSwich.IsToggled = true;
            }
            else
            {
                isDoneSwich.IsToggled = false;
                isDone.Text = "لم يتم الانجاز";
                updateValue.Text = "false";
            }

            pName.Text = wopv.ParameterName;
            //totalLenth.Text = wopv.Lenght.ToString();
            //doneLenth.Text = wopv.DoneLength.ToString();

            // handling events to expose to public
            SaveButton.Clicked += SaveButton_Clicked;
            CancelButton.Clicked += CancelButton_Clicked;
            updateValue.TextChanged += InputEntry_TextChanged;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            //if (int.Parse(updateValue.Text) > int.Parse(totalLenth.Text))
            //{
            //    ValidationLabel.Text = "القيمة اكبر من الاجمالي!";
            //    ValidationLabel.IsVisible = true;
            //    return;
            //}
            //if (string.IsNullOrEmpty(updateValue.Text))
            //{
            //    ValidationLabel.Text = "لا يمكن ارسال قيمة فارغة!";
            //    ValidationLabel.IsVisible = true;
            //    return;
            //}

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

        private void isDoneSwich_Toggled(object sender, ToggledEventArgs e)
        {
            if(isDoneSwich.IsToggled == true)
            {
                isDone.Text = "تم الانجاز";
                updateValue.Text = "true";
            }
            else
            {
                isDone.Text = "لم يتم الانجاز";
                updateValue.Text = "false";
            }
            
        }
    }

}