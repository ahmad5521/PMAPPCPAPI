using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using PMAPP.CustomControls;
using PMAPP.Droid;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyDatePicker), typeof(MyDatePickerRenderer))]
namespace PMAPP.Droid
{
    public class MyDatePickerRenderer : DatePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);
            Control.Gravity = GravityFlags.Center;
            Control.TextSize = 12;
        }
    }
}