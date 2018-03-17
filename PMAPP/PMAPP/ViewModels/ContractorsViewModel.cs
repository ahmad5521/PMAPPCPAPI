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
    public class ContractorsViewModel : INotifyPropertyChanged
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


        //V_Contractor_noOfWorkorders_workorderType
        private List<V_Contractor_noOfWorkorders_workorderType> _Contractor_noOfWorkorders_projects;

        public List<V_Contractor_noOfWorkorders_workorderType> Contractor_noOfWorkorders_projects
        {
            get
            {
                return this._Contractor_noOfWorkorders_projects;
            }

            set
            {
                if (value != this._Contractor_noOfWorkorders_projects)
                {
                    this._Contractor_noOfWorkorders_projects = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private List<V_Contractor_noOfWorkorders_workorderType> _Contractor_noOfWorkorders_twsilat;

        public List<V_Contractor_noOfWorkorders_workorderType> Contractor_noOfWorkorders_twsilat
        {
            get
            {
                return this._Contractor_noOfWorkorders_twsilat;
            }

            set
            {
                if (value != this._Contractor_noOfWorkorders_twsilat)
                {
                    this._Contractor_noOfWorkorders_twsilat = value;
                    NotifyPropertyChanged();
                }
            }
        }

        //V_Contractor_noOfAchievedWorkorders_workorderType

        private List<V_Contractor_noOfAchievedWorkorders_workorderType> _V_Contractor_noOfAchievedWorkorders_workorderType_twsilat;

        public List<V_Contractor_noOfAchievedWorkorders_workorderType> V_Contractor_noOfAchievedWorkorders_workorderType_twsilat
        {
            get
            {
                return this._V_Contractor_noOfAchievedWorkorders_workorderType_twsilat;
            }

            set
            {
                if (value != this._V_Contractor_noOfAchievedWorkorders_workorderType_twsilat)
                {
                    this._V_Contractor_noOfAchievedWorkorders_workorderType_twsilat = value;
                    NotifyPropertyChanged();
                }
            }
        }


        //V_Contractor_noOfAchievedWorkorders_workorderType
        private List<V_Contractor_noOfAchievedWorkorders_workorderType> _V_Contractor_noOfAchievedWorkorders_workorderType_projects;

        public List<V_Contractor_noOfAchievedWorkorders_workorderType> V_Contractor_noOfAchievedWorkorders_workorderType_projects
        {
            get
            {
                return this._V_Contractor_noOfAchievedWorkorders_workorderType_projects;
            }

            set
            {
                if (value != this._V_Contractor_noOfAchievedWorkorders_workorderType_projects)
                {
                    this._V_Contractor_noOfAchievedWorkorders_workorderType_projects = value;
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


        //IsChartlVisible
        private bool _IsChartlVisible;

        public bool IsChartlVisible
        {
            get
            {
                return this._IsChartlVisible;
            }

            set
            {
                if (value != this._IsChartlVisible)
                {
                    this._IsChartlVisible = value;
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

        public ContractorsViewModel()
        {
            getData();
        }

        private async void getData()
        {
            IsBusy = true;
            getChart1();
            getchart2();
            IsBusy = false;
        }

        private async void getchart2()
        {

            var list = (List<V_Contractor_noOfAchievedWorkorders_workorderType>)await rcs.getV_Contractor_noOfAchievedWorkorders_workorderTypeAsync();
            if(list.Count > 0)
            {
                IsLableVisible = false;
                IsChartlVisible = true;
                var groupedList = list.GroupBy(u => u.contractorName)
                                            .Select(grp => grp.ToList())
                                            .ToList();

                List<V_Contractor_noOfAchievedWorkorders_workorderType> twsilats = new List<V_Contractor_noOfAchievedWorkorders_workorderType>();
                List<V_Contractor_noOfAchievedWorkorders_workorderType> projects = new List<V_Contractor_noOfAchievedWorkorders_workorderType>();
                V_Contractor_noOfAchievedWorkorders_workorderType_projects = new List<V_Contractor_noOfAchievedWorkorders_workorderType>();
                V_Contractor_noOfAchievedWorkorders_workorderType_twsilat = new List<V_Contractor_noOfAchievedWorkorders_workorderType>();


                foreach (var item in groupedList)
                {
                    projects.Clear();
                    twsilats.Clear();
                    if (item.Count() == 1)
                    {
                        V_Contractor_noOfAchievedWorkorders_workorderType tmp = new V_Contractor_noOfAchievedWorkorders_workorderType();
                        if (item.Where(p => p.workorderTypeName == "مشاريع").Count() > 0)
                        {
                            tmp.contractorName = item.First().contractorName;
                            tmp.noOfCompleted = 0;
                            tmp.noOfNotCompleted = 0;
                            tmp.workorderTypeName = "توصيلات";

                            projects = item.Where(p => p.workorderTypeName == "مشاريع").ToList();
                            twsilats.Add(tmp);
                        }
                        if (item.Where(p => p.workorderTypeName == "توصيلات").Count() > 0)
                        {
                            tmp.contractorName = item.First().contractorName;
                            tmp.noOfCompleted = 0;
                            tmp.noOfNotCompleted = 0;
                            tmp.workorderTypeName = "مشاريع";
                            try
                            {
                                projects.Add(tmp);
                            }
                            catch (Exception exx)
                            {

                            }
                            twsilats = item.Where(p => p.workorderTypeName == "توصيلات").ToList();
                        }
                    }
                    else
                    {
                        projects = item.Where(p => p.workorderTypeName == "مشاريع").ToList();
                        twsilats = item.Where(p => p.workorderTypeName == "توصيلات").ToList();
                    }

                    V_Contractor_noOfAchievedWorkorders_workorderType_projects.AddRange(projects);
                    V_Contractor_noOfAchievedWorkorders_workorderType_twsilat.AddRange(twsilats);


                }
            }
            else
            {
                IsLableVisible = true;
                IsChartlVisible = false;
            }
            
        }

        private async void getChart1()
        {
            var list = (List<V_Contractor_noOfWorkorders_workorderType>)await rcs.getV_Contractor_noOfWorkorders_workorderTypeViewsAsync();
            if (list.Count > 0)
            {
                IsLableVisible = false;
                IsChartlVisible = true;
                var groupedList = list.GroupBy(u => u.contractorName)
                                            .Select(grp => grp.ToList())
                                            .ToList();

                

                List<V_Contractor_noOfWorkorders_workorderType> twsilats = new List<V_Contractor_noOfWorkorders_workorderType>();
                List<V_Contractor_noOfWorkorders_workorderType> projects = new List<V_Contractor_noOfWorkorders_workorderType>();
                Contractor_noOfWorkorders_projects = new List<V_Contractor_noOfWorkorders_workorderType>();
                Contractor_noOfWorkorders_twsilat = new List<V_Contractor_noOfWorkorders_workorderType>();


                foreach (var item in groupedList)
                {
                    projects.Clear();
                    twsilats.Clear();
                    if (item.Count() == 1)
                    {
                        V_Contractor_noOfWorkorders_workorderType tmp = new V_Contractor_noOfWorkorders_workorderType();
                        if (item.Where(p => p.workorderTypeName == "مشاريع").Count() > 0)
                        {
                            tmp.contractorName = item.First().contractorName;
                            tmp.noOfWorkOrders = 0;
                            tmp.workorderTypeName = "توصيلات";
                            projects = item.Where(p => p.workorderTypeName == "مشاريع").ToList();
                            twsilats.Add(tmp);
                        }
                        if (item.Where(p => p.workorderTypeName == "توصيلات").Count() > 0)
                        {
                            tmp.contractorName = item.First().contractorName;
                            tmp.noOfWorkOrders = 0;
                            tmp.workorderTypeName = "مشاريع";
                            projects.Add(tmp);                            
                            twsilats = item.Where(p => p.workorderTypeName == "توصيلات").ToList();
                        }
                    }
                    else
                    {
                        projects = item.Where(p => p.workorderTypeName == "مشاريع").ToList();
                        twsilats = item.Where(p => p.workorderTypeName == "توصيلات").ToList();
                    }

                    Contractor_noOfWorkorders_projects.AddRange(projects);
                    Contractor_noOfWorkorders_twsilat.AddRange(twsilats);


                }
            }
            else
            {

                IsLableVisible = true;
                IsChartlVisible = false;
            }

            
        }


    }
}
