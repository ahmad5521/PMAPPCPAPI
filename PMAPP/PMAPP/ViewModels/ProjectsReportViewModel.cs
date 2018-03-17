using PMAPP.Models;
using PMAPP.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PMAPP.ViewModels
{
    public class ProjectsReportViewModel : INotifyPropertyChanged
    {
        RestClientService rcs = new RestClientService();
        public int projectNo = 0;


        public event PropertyChangedEventHandler PropertyChanged;


        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        //V_Project_totalLengths_project_contractor
        private List<V_Project_totalLengths_project_contractor> _V_Project_totalLengths_project_contractor;

        public List<V_Project_totalLengths_project_contractor> V_Project_totalLengths_project_contractor
        {
            get
            {
                return this._V_Project_totalLengths_project_contractor;
            }

            set
            {
                if (value != this._V_Project_totalLengths_project_contractor)
                {
                    this._V_Project_totalLengths_project_contractor = value;
                    NotifyPropertyChanged();
                }
            }
        }
        

        //IsKMChartVisible
        private bool _IsKMChartVisible;

        public bool IsKMChartVisible
        {
            get
            {
                return this._IsKMChartVisible;
            }

            set
            {
                if (value != this._IsKMChartVisible)
                {
                    this._IsKMChartVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }


        //KMGridNo
        private string _KMGridNo;

        public string KMGridNo
        {
            get
            {
                return this._KMGridNo;
            }

            set
            {
                if (value != this._KMGridNo)
                {
                    this._KMGridNo = value;
                    NotifyPropertyChanged();
                }
            }
        }


        //V_Project_totalAmounts_project_contractor
        private List<V_Project_totalAmounts_project_contractor> _V_Project_totalAmounts_project_contractor;

        public List<V_Project_totalAmounts_project_contractor> V_Project_totalAmounts_project_contractor
        {
            get
            {
                return this._V_Project_totalAmounts_project_contractor;
            }

            set
            {
                if (value != this._V_Project_totalAmounts_project_contractor)
                {
                    this._V_Project_totalAmounts_project_contractor = value;
                    NotifyPropertyChanged();
                }
            }
        }


        //IsAmountChartVisible
        private bool _IsAmountChartVisible;

        public bool IsAmountChartVisible
        {
            get
            {
                return this._IsAmountChartVisible;
            }

            set
            {
                if (value != this._IsAmountChartVisible)
                {
                    this._IsAmountChartVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }


        //AmountGridNo
        private string _AmountGridNo;

        public string AmountGridNo
        {
            get
            {
                return this._AmountGridNo;
            }

            set
            {
                if (value != this._AmountGridNo)
                {
                    this._AmountGridNo = value;
                    NotifyPropertyChanged();
                }
            }
        }


        //projectName
        private string _ProjectName;

        public string ProjectName
        {
            get
            {
                return this._ProjectName;
            }

            set
            {
                if (value != this._ProjectName)
                {
                    this._ProjectName = value;
                    NotifyPropertyChanged();
                }
            }
        }


        //IsLableVisible
        private bool _IsLableVisible;

        public bool IsLableVisible
        {
            get
            {
                return this._IsLableVisible;
            }

            set
            {
                if (value != this._IsLableVisible)
                {
                    this._IsLableVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }


        //IsTitelVisible
        private bool _IsTitelVisible;

        public bool IsTitelVisible
        {
            get
            {
                return this._IsTitelVisible;
            }

            set
            {
                if (value != this._IsTitelVisible)
                {
                    this._IsTitelVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }




        //V_Project_percentOfAchievement_contractors
        private List<V_Project_percentOfAchievement_contractors> _V_Project_percentOfAchievement_contractors;

        public List<V_Project_percentOfAchievement_contractors> V_Project_percentOfAchievement_contractors
        {
            get
            {
                return this._V_Project_percentOfAchievement_contractors;
            }

            set
            {
                if (value != this._V_Project_percentOfAchievement_contractors)
                {
                    this._V_Project_percentOfAchievement_contractors = value;
                    NotifyPropertyChanged();
                }
            }
        }


        //IsPercentChartVisible
        private bool _IsPercentChartVisible;

        public bool IsPercentChartVisible
        {
            get
            {
                return this._IsPercentChartVisible;
            }

            set
            {
                if (value != this._IsPercentChartVisible)
                {
                    this._IsPercentChartVisible = value;
                    NotifyPropertyChanged();
                }
            }
        }


        //PercentGridNo
        private string _PercentGridNo;

        public string PercentGridNo
        {
            get
            {
                return this._PercentGridNo;
            }

            set
            {
                if (value != this._PercentGridNo)
                {
                    this._PercentGridNo = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private bool isBusy;

        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }

            set
            {
                this.isBusy = value;
                NotifyPropertyChanged();
            }
        }

        public ProjectsReportViewModel(int v)
        {
            KMGridNo = "1";
            AmountGridNo = "2";
            PercentGridNo = "3";
            //IsTitelVisible = false;
            //IsKMChartVisible = false;
            //IsLableVisible = false;
            ProjectName = "";
            getData(v);
        }

        private async void getData(int v)
        {
            IsBusy = true;


            //KM
            V_Project_totalLengths_project_contractor = await rcs.get_V_Project_totalLengths_project_contractor_Async(v);
            if(V_Project_totalLengths_project_contractor.Count > 0)
            {
                KMGridNo = "1";
                IsTitelVisible = true;
                IsKMChartVisible = true;
                IsLableVisible = false;
                ProjectName = "المشروع: " + V_Project_totalLengths_project_contractor.First().projectName;
            }

            //Amount
            V_Project_totalAmounts_project_contractor = await rcs.get_V_Project_totalAmounts_project_contractor_Async(v);
            if (V_Project_totalAmounts_project_contractor.Count > 0)
            {
                if(V_Project_totalLengths_project_contractor.Count > 0)
                    AmountGridNo = "2";
                else
                    AmountGridNo = "1";
                IsTitelVisible = true;
                IsAmountChartVisible = true;
                IsLableVisible = false;
                ProjectName = "المشروع: " + V_Project_totalAmounts_project_contractor.First().projectName;
            }

            //Percent
            V_Project_percentOfAchievement_contractors = await rcs.get_V_Project_percentOfAchievement_contractors_Async(v);
            foreach (var item in V_Project_percentOfAchievement_contractors)
            {
                item.remaining = 100 - item.averagePercentageOfAchievementBasedOnWeights;
            }
            if (V_Project_percentOfAchievement_contractors.Count > 0)
            {
                if (V_Project_totalLengths_project_contractor.Count > 0)
                    if (V_Project_totalAmounts_project_contractor.Count > 0)
                        PercentGridNo = "3";
                    else
                        PercentGridNo = "2";
                else
                    if (V_Project_totalAmounts_project_contractor.Count > 0)
                        PercentGridNo = "2";
                    else
                        PercentGridNo = "1";
                IsTitelVisible = true;
                IsPercentChartVisible= true;
                IsLableVisible = false;
                ProjectName = "المشروع: " + V_Project_percentOfAchievement_contractors.First().projectName;
            }


            if (V_Project_totalAmounts_project_contractor.Count == 0 && V_Project_totalLengths_project_contractor.Count == 0)
            {
                IsTitelVisible = false;
                IsKMChartVisible = false;
                IsPercentChartVisible = false;
                IsLableVisible = true;
                IsAmountChartVisible = false;
            }

            IsBusy = false;
        }
    }
}