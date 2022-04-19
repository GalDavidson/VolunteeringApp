using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using VolunteeringApp.Services;
using VolunteeringApp.Models;
using VolunteeringApp.ViewModels;
using VolunteeringApp.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.Essentials;

namespace VolunteeringApp.ViewModels
{
    public class NewPostViewModel: INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region שם פרטי
        private bool showEventNameError;

        public bool ShowEventNameError
        {
            get => showEventNameError;
            set
            {
                showEventNameError = value;
                OnPropertyChanged("ShowEventNameError");
            }
        }

        private string eventName;

        public string EventName
        {
            get => eventName;
            set
            {
                eventName = value;
                ValidateEventName();
                OnPropertyChanged("EventName");
            }
        }

        private string eventNameError;

        public string EventNameError
        {
            get => eventNameError;
            set
            {
                eventNameError = value;
                OnPropertyChanged("EventNameError");
            }
        }

        private void ValidateEventName()
        {
            this.ShowConditions = false;

            this.ShowEventNameError = string.IsNullOrEmpty(EventName);
            if (ShowEventNameError)
                EventNameError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region מיקום האירוע
        private bool showLocationError;

        public bool ShowLocationError
        {
            get => showLocationError;
            set
            {
                showLocationError = value;
                OnPropertyChanged("ShowLocationError");
            }
        }

        private string location;

        public string Location
        {
            get => location;
            set
            {
                location = value;
                ValidateLocation();
                OnPropertyChanged("Location");
            }
        }

        private string locationError;

        public string LocationError
        {
            get => locationError;
            set
            {
                locationError = value;
                OnPropertyChanged("LocationError");
            }
        }

        private void ValidateLocation()
        {
            this.ShowConditions = false;

            this.ShowLocationError = string.IsNullOrEmpty(Location);
            if (ShowLocationError)
                LocationError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region תיאור הפוסט
        private bool showCaptionError;

        public bool ShowCaptionError
        {
            get => showCaptionError;
            set
            {
                showCaptionError = value;
                OnPropertyChanged("ShowCaptionError");
            }
        }

        private string caption;

        public string Caption
        {
            get => caption;
            set
            {
                caption = value;
                ValidateCaption();
                OnPropertyChanged("Caption");
            }
        }

        private string captionError;

        public string CaptionError
        {
            get => captionError;
            set
            {
                captionError = value;
                OnPropertyChanged("CaptionError");
            }
        }

        private void ValidateCaption()
        {
            this.ShowConditions = false;

            this.ShowCaptionError = string.IsNullOrEmpty(Caption);
            if (ShowCaptionError)
                CaptionError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region תחומי עיסוק

        private List<OccupationalArea> allOccupationalAreas;
        private ObservableCollection<OccupationalArea> filteredOccuAreas;
        public ObservableCollection<OccupationalArea> FilteredOccuAreas
        {
            get
            {
                return this.filteredOccuAreas;
            }
            set
            {
                if (this.filteredOccuAreas != value)
                {

                    this.filteredOccuAreas = value;
                    OnPropertyChanged("FilteredOccuAreas");
                }
            }
        }

        private string searchTerm;
        public string SearchTerm
        {
            get
            {
                return this.searchTerm;
            }
            set
            {
                //if (this.searchTerm != value)
                //{
                this.searchTerm = value;
                OnTextChanged(value);
                OnPropertyChanged("SearchTerm");
                //}
            }
        }

        private void InitOccuAreas()
        {
            IsRefreshing = true;
            App theApp = (App)App.Current;
            this.allOccupationalAreas = theApp.LookupTables.OccupationalAreas;


            //Copy list to the filtered list
            this.FilteredOccuAreas = new ObservableCollection<OccupationalArea>(this.allOccupationalAreas.OrderBy(a => a.OccupationName));
            SearchTerm = String.Empty;
            IsRefreshing = false;
        }

        private string newOccuArea;
        public string NewOccuArea
        {
            get => newOccuArea;
            set
            {
                newOccuArea = value;
                OnPropertyChanged("NewOccuArea");
            }
        }
        #endregion תחומי עיסוק

        #region Search
        public void OnTextChanged(string search)
        {
            //Filter the list of contacts based on the search term
            if (this.allOccupationalAreas == null)
                return;
            if (String.IsNullOrWhiteSpace(search) || String.IsNullOrEmpty(search))
            {
                foreach (OccupationalArea a in this.allOccupationalAreas)
                {
                    if (!this.FilteredOccuAreas.Contains(a))
                        this.FilteredOccuAreas.Add(a);
                }
            }
            else
            {
                foreach (OccupationalArea a in this.allOccupationalAreas)
                {
                    string occuAreaString = $"{a.OccupationName}";

                    if (!this.FilteredOccuAreas.Contains(a) &&
                        occuAreaString.Contains(search))
                        this.FilteredOccuAreas.Add(a);
                    else if (this.FilteredOccuAreas.Contains(a) &&
                        !occuAreaString.Contains(search))
                        this.FilteredOccuAreas.Remove(a);
                }
            }

            this.FilteredOccuAreas = new ObservableCollection<OccupationalArea>(this.FilteredOccuAreas.OrderBy(a => a.OccupationName));
        }
        #endregion

        #region AreaSelection
        List<OccupationalArea> selectedOccuAreas;
        public List<OccupationalArea> SelectedOccuAreas
        {
            get
            {
                return selectedOccuAreas;
            }
            set
            {
                if (selectedOccuAreas != value)
                {
                    selectedOccuAreas = value;
                }
            }
        }

        private string occupationalAreas;

        public string OccupationalAreas
        {
            get => occupationalAreas;
            set
            {
                occupationalAreas = value;
                OnPropertyChanged("OccupationalAreas");
            }
        }



        public ICommand UpdateOccuArea => new Command(OnPressedOccuArea);
        public async void OnPressedOccuArea(object occuAreasList)
        {
            SelectedOccuAreas.Clear();
            OccupationalAreas = string.Empty;
            if (occuAreasList is IList<object>)
            {
                List<object> list = ((IList<object>)occuAreasList).ToList();
                foreach (object a in list)
                {
                    SelectedOccuAreas.Add((OccupationalArea)a);

                }
                if (selectedOccuAreas.Count == 0) { OccupationalAreas = "לא נבחרו תחומי עיסוק"; }
            }
        }
        #endregion

        #region Add New Area
        public ICommand AddOccuArea => new Command(OnAddOccuArea);
        public async void OnAddOccuArea()
        {

            if (string.IsNullOrEmpty(NewOccuArea))
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "!לא ניתן להוסיף ערך זה", "בסדר");
                return;
            }

            OccupationalArea NewOccuAr = new OccupationalArea
            {
                OccupationName = NewOccuArea
            };

            bool IsExist = false;
            if (allOccupationalAreas.Contains(NewOccuAr)) { IsExist = true; }

            if (!IsExist)
            {
                VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
                bool ok = await proxy.AddOccupationalArea(NewOccuAr);

                if (ok)
                {
                    allOccupationalAreas.Add(NewOccuAr);
                    await App.Current.MainPage.DisplayAlert("", "בסדר", "!הוספת תחום עיסוק בהצלחה");
                }
                else if (!ok)
                {
                    await App.Current.MainPage.DisplayAlert("בסדר", "הוספת תחום עיסוק נכשלה", "שגיאה");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("בסדר", "תחום עיסוק זה כבר קיים במערכת", "שגיאה");
            }

        }
        #endregion

        #region Refresh
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                if (this.isRefreshing != value)
                {
                    this.isRefreshing = value;
                    OnPropertyChanged(nameof(IsRefreshing));
                }
            }
        }
        public ICommand RefreshCommand => new Command(OnRefresh);
        public void OnRefresh()
        {
            InitOccuAreas();
        }
        #endregion

        #region הצגת בעיות
        private bool showConditions;

        public bool ShowConditions
        {
            get => showConditions;
            set
            {
                showConditions = value;
                OnPropertyChanged("ShowConditions");
            }
        }
        #endregion

        #region הבעיה הבאה
        private string nextError;

        public string NextError
        {
            get => nextError;
            set
            {
                nextError = value;
                OnPropertyChanged("NextError");
            }
        }

        private bool showNextError;

        public bool ShowNextError
        {
            get => showNextError;
            set
            {
                showNextError = value;
                OnPropertyChanged("ShowNextError");
            }
        }
        #endregion הבעיה הבאה

        #region מקור התמונה
        private string imgSrc;

        public string ImgSrc
        {
            get => imgSrc;
            set
            {
                imgSrc = value;
                OnPropertyChanged("ImgSrc");
            }
        }
        private const string DEFAULT_PHOTO_SRC = "defaultphoto.jpg";
        #endregion

        #region serverStatus
        private string serverStatus;
        public string ServerStatus
        {
            get { return serverStatus; }
            set
            {
                serverStatus = value;
                OnPropertyChanged("ServerStatus");
            }
        }
        #endregion serverStatus

        public NewPostViewModel()
        {
            this.ShowCaptionError = false;

            this.ImgSrc = DEFAULT_PHOTO_SRC;
            this.imageFileResult = null; //mark that no picture was chosen
            this.selectedOccuAreas = new List<OccupationalArea>();
            this.filteredOccuAreas = new ObservableCollection<OccupationalArea>();

            InitOccuAreas();

            this.SubmitCommand = new Command(OnSubmit);
        }

        public ICommand SubmitCommand { protected set; get; }

        private bool ValidateForm()
        {
            ValidateCaption();

            //check if any validation failed
            if (ShowCaptionError)
                return false;
            return true;
        }

        public async void OnSubmit()
        {

            if (ValidateForm())
            {

                App theApp = (App)App.Current;
                Association a = (Association)theApp.CurrentUser;

                Post newP = new Post
                {
                    Caption = this.Caption,
                    AssociationId = a.AssociationId,

                };

                foreach (OccupationalArea o in selectedOccuAreas)
                {
                    OccupationalAreasOfPost oc = new OccupationalAreasOfPost
                    {
                        OccupationalAreaId = o.OccupationalAreaId,
                        PostId = newP.PostId
                    };
                    newP.OccupationalAreasOfPosts.Add(oc);
                }

                VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
                Post p = await proxy.NewPost(newP);

                if (p == null)
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "הרשמה נכשלה, בדוק את הפרטים המוקלדים", "בסדר");
                }
                else
                {
                    if (this.imageFileResult != null)
                    {
                        ServerStatus = "מעלה תמונה...";

                        bool success = await proxy.UploadImage(new FileInfo()
                        {
                            Name = this.imageFileResult.FullPath
                        }, $"P{p.PostId}.jpg");

                        if (success)
                        {
                            ImgSrc = p.ImgSource;
                        }
                    }
                    ServerStatus = "שומר נתונים...";

                    Page page = new NavigationPage(new Views.HomePage());
                    App.Current.MainPage = page;

                }
            }
            else
            {
                ShowNextError = true;
                NextError = "אירעה שגיאה! לא ניתן להמשיך בהרשמה";
            }
        }

        #region new pic
        FileResult imageFileResult;
        public event Action<ImageSource> SetImageSourceEvent;
        public ICommand PickImageCommand => new Command(OnPickImage);
        public async void OnPickImage()
        {
            FileResult result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions()
            {
                Title = "בחר תמונה"
            });

            if (result != null)
            {
                this.imageFileResult = result;

                var stream = await result.OpenReadAsync();
                ImageSource imgSource = ImageSource.FromStream(() => stream);
                if (SetImageSourceEvent != null)
                    SetImageSourceEvent(imgSource);
            }
        }

        ///The following command handle the take photo button
        public ICommand CameraImageCommand => new Command(OnCameraImage);
        public async void OnCameraImage()
        {
            var result = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions()
            {
                Title = "צלם תמונה"
            });

            if (result != null)
            {
                this.imageFileResult = result;
                var stream = await result.OpenReadAsync();
                ImageSource imgSource = ImageSource.FromStream(() => stream);
                if (SetImageSourceEvent != null)
                    SetImageSourceEvent(imgSource);
            }
        }

        #endregion
    }
}
