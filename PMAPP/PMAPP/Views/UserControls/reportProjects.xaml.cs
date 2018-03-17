using PMAPP.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PMAPP.Views.UserControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class reportProjects : ContentView
    {
        public reportProjects(int v)
        {
            InitializeComponent();
            this.BindingContext = new ProjectsReportViewModel(v);
        }
    }
}