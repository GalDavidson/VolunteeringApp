using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using VolunteeringApp.Services;
using VolunteeringApp.Models;
using VolunteeringApp.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.Essentials;

namespace VolunteeringApp.ViewModels
{
    class AllPostsViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region סניפים
        private List<Post> allPosts;
        private ObservableCollection<Post> filteredPosts;
        public ObservableCollection<Post> FilteredPosts
        {
            get
            {
                return this.filteredPosts;
            }
            set
            {
                if (this.filteredPosts != value)
                {

                    this.filteredPosts = value;
                    OnPropertyChanged("FilteredPosts");
                }
            }
        }

        //private string searchBranch;
        //public string SearchBranch
        //{
        //    get
        //    {
        //        return this.searchBranch;
        //    }
        //    set
        //    {
        //        //if (this.searchBranch != value)
        //        //{
        //        this.searchBranch = value;
        //        OnTypeChanged(value);
        //        OnPropertyChanged("SearchBranch");
        //        //}
        //    }
        //}

        private void InitPosts()
        {
            IsRefresh = true;
            App theApp = (App)App.Current;
            this.allPosts = theApp.LookupTables.Posts;


            //Copy list to the filtered list
            this.filteredPosts = new ObservableCollection<Post>(this.allPosts.OrderBy(b => b.ActionDate));
            //SearchBranch = String.Empty;
            IsRefresh = false;
        }

        #endregion סניפים

        //#region Search Posts
        //public void OnTypeChanged(string searching)
        //{
        //    //Filter the list of contacts based on the search term
        //    if (this.allPosts == null)
        //        return;
        //    if (String.IsNullOrWhiteSpace(searching) || String.IsNullOrEmpty(searching))
        //    {
        //        foreach (Post p in this.allPosts)
        //        {
        //            if (!this.FilteredPosts.Contains(p))
        //                this.FilteredPosts.Add(p);
        //        }
        //    }
        //    else
        //    {
        //        foreach (Post p in this.allPosts)
        //        {
        //            string branchesString = $"{b.BranchLocation}";

        //            if (!this.FilteredPosts.Contains(p) &&
        //                branchesString.Contains(searching))
        //                this.FilteredPosts.Add(p);
        //            else if (this.FilteredPosts.Contains(p) &&
        //                !branchesString.Contains(searching))
        //                this.FilteredPosts.Remove(p);
        //        }
        //    }

        //    this.FilteredPosts = new ObservableCollection<Post>(this.FilteredPosts.OrderBy(b => b.ActionDate));
        //}
        //#endregion

        #region Refresh Posts
        private bool isRefresh;
        public bool IsRefresh
        {
            get => isRefresh;
            set
            {
                if (this.isRefresh != value)
                {
                    this.isRefresh = value;
                    OnPropertyChanged(nameof(IsRefresh));
                }
            }
        }
        public ICommand RefreshPostsCommand => new Command(OnRefreshPosts);
        public void OnRefreshPosts()
        {
            InitPosts();
        }
        #endregion

        #region PostSelection
        //List<Branch> selectedBranches;
        //public List<Branch> SelectedBranches
        //{
        //    get
        //    {
        //        return selectedBranches;
        //    }
        //    set
        //    {
        //        if (selectedBranches != value)
        //        {
        //            selectedBranches = value;
        //        }
        //    }
        //}

        //private string branches;

        //public string Branches
        //{
        //    get => branches;
        //    set
        //    {
        //        branches = value;
        //        OnPropertyChanged("branches");
        //    }
        //}


        //public ICommand UpdateBranches => new Command(OnPressedBranches);
        //public async void OnPressedBranches(object branchesList)
        //{
        //    SelectedBranches.Clear();
        //    Branches = string.Empty;
        //    if (branchesList is IList<object>)
        //    {
        //        List<object> list = ((IList<object>)branchesList).ToList();
        //        foreach (object b in list)
        //        {
        //            SelectedBranches.Add((Branch)b);
        //        }
        //        if (selectedBranches.Count == 0) { Branches = "לא נבחרו סניפים"; }
        //    }
        //}
        #endregion


        public AllPostsViewModel()
        {
            this.filteredPosts = new ObservableCollection<Post>();
            InitPosts();
        }



    }
}
