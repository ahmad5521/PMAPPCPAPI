using PMAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMAPP.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PMAPP.ViewModels
{
    public class UsersViewModel : INotifyPropertyChanged
    {
        RestClientService rcs = new RestClientService();

        public event PropertyChangedEventHandler PropertyChanged;


        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        //private List<ViewUsersAchievementPercentage> data;

        //public List<ViewUsersAchievementPercentage> Data {
        //    get
        //    {
        //        return this.data;
        //    }

        //    set
        //    {
        //        if (value != this.data)
        //        {
        //            this.data = value;
        //            NotifyPropertyChanged();
        //        }
        //    }
        //}

        public UsersViewModel()
        {
            getData();
        }

        private async void getData()
        {          
            //Data = await rcs.getAllUsersAchievementPercentageAsync();
        }
    }
}
