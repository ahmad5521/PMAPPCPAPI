using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PMAPP.Behaviors
{
    public class IntegerBehavior : Behavior<Entry>
    {
        private const string digitRegex = @"^[0-9]+$";


        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += Bindable_TextChanged;
        }

        private void Bindable_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry;
            bool isValid;
            entry = (Entry)sender;
            try
            {
                isValid = Regex.IsMatch(e.NewTextValue, digitRegex);
                entry.TextColor = isValid ? Color.Default : Color.Red;
            }
            catch
            {

            }
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= this.Bindable_TextChanged;
        }

    }
}
