using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PMAPP.CustomControls
{
    class ImageButton : Image
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(ImageButton), null);

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create("CommandParameter", typeof(object), typeof(ImageButton), null);

        public event EventHandler ItemTapped = (e, a) => { };

        public ImageButton()
        {
            Initialize();
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        private ICommand TransitionCommand
        {
            get
            {
                return new Command(async () =>
                {
                    AnchorX = 0.48;
                    AnchorY = 0.48;
                    Command?.Execute(CommandParameter);

                    ItemTapped(this, EventArgs.Empty);
                });
            }
        }

        public void Initialize()
        {
            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = TransitionCommand
            });
        }
    }
}