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
    public class UpdateAssoViewModel
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        #region תמונה
        private string userImgSrc;
        public string UserImgSrc
        {
            get => userImgSrc;
            set
            {
                userImgSrc = value;
                OnPropertyChanged("UserImgSrc");
            }
        }
        private const string DEFAULT_PHOTO_SRC = "defaultphoto.jpg";
        #endregion

        #region דואר אלקטרוני
        private bool showEmailError;

        public bool ShowEmailError
        {
            get => showEmailError;
            set
            {
                showEmailError = value;
                OnPropertyChanged("ShowEmailError");
            }
        }

        private string email;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                ValidateEmail();
                OnPropertyChanged("Email");
            }
        }

        private string emailError;

        public string EmailError
        {
            get => emailError;
            set
            {
                emailError = value;
                OnPropertyChanged("EmailError");
            }
        }

        private void ValidateEmail()
        {

            this.ShowEmailError = string.IsNullOrEmpty(Email);
            if (!this.ShowEmailError)
            {
                if (!Regex.IsMatch(this.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                {
                    this.ShowEmailError = true;
                    this.EmailError = ERROR_MESSAGES.BAD_EMAIL;
                }
            }
            else
                this.EmailError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region פרטי שם משתמש
        private bool showUsernameError;

        public bool ShowUsernameError
        {
            get => showUsernameError;
            set
            {
                showUsernameError = value;
                OnPropertyChanged("ShowUsernameError");
            }
        }

        private string username;

        public string Username
        {
            get => username;
            set
            {
                username = value;
                ValidateUsername();
                OnPropertyChanged("Username");
            }
        }

        private string usernameError;

        public string UsernameError
        {
            get => usernameError;
            set
            {
                usernameError = value;
                OnPropertyChanged("UsernameError");
            }
        }

        private void ValidateUsername()
        {

            this.ShowUsernameError = string.IsNullOrEmpty(Username);
            if (ShowUsernameError)
                UsernameError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region מידע על

        private bool showInformationAboutError;

        public bool ShowInformationAboutError
        {
            get => showInformationAboutError;
            set
            {
                showInformationAboutError = value;
                OnPropertyChanged("ShowInformationAboutError");
            }
        }

        private string informationAbout;

        public string InformationAbout
        {
            get => informationAbout;
            set
            {
                informationAbout = value;
                ValidateInformationAbout();
                OnPropertyChanged("InformationAbout");
            }
        }

        private string informationAboutError;

        public string InformationAboutError
        {
            get => informationAboutError;
            set
            {
                informationAboutError = value;
                OnPropertyChanged("InformationAboutError");
            }
        }

        private void ValidateInformationAbout()
        {

            this.ShowInformationAboutError = string.IsNullOrEmpty(InformationAbout);
            if (ShowInformationAboutError)
                InformationAboutError = ERROR_MESSAGES.REQUIRED_FIELD;

        }

        #endregion

        #region מס טלפון

        private bool showPhoneNumError;

        public bool ShowPhoneNumError
        {
            get => showPhoneNumError;
            set
            {
                showPhoneNumError = value;
                OnPropertyChanged("ShowPhoneNumError");
            }
        }

        private string phoneNum;

        public string PhoneNum
        {
            get => phoneNum;
            set
            {
                phoneNum = value;
                ValidatePhoneNum();
                OnPropertyChanged("PhoneNum");
            }
        }

        private string phoneNumError;

        public string PhoneNumError
        {
            get => phoneNumError;
            set
            {
                phoneNumError = value;
                OnPropertyChanged("PhoneNumError");
            }
        }

        private void ValidatePhoneNum()
        {
            this.ShowPhoneNumError = false;

            this.ShowPhoneNumError = string.IsNullOrEmpty(PhoneNum);
            if (ShowPhoneNumError)
            {
                this.PhoneNumError = ERROR_MESSAGES.REQUIRED_FIELD;
                this.ShowPhoneNumError = true;
                return;
            }
            else if (PhoneNum.Length < 10)
            {
                this.PhoneNumError = "מס' הטלפון קצר מדי, הוא צריך להכיל 10 ספרות לפחות ";
                this.ShowPhoneNumError = true;
            }
        }

        #endregion

        #region סיסמה

        private const int MIN_PASS_CHARS = 8;
        private string password;
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                if (this.password != value)
                {
                    this.password = value;
                    ValidatePassword();

                    OnPropertyChanged("Password");
                }
            }
        }

        private bool showPasswordError;
        public bool ShowPasswordError
        {
            get => showPasswordError;
            set
            {
                showPasswordError = value;
                OnPropertyChanged("ShowPasswordError");
            }
        }

        private string passwordError;
        public string PasswordError
        {
            get => passwordError;
            set
            {
                passwordError = value;
                OnPropertyChanged("PasswordError");
            }
        }

        private void ValidatePassword()
        {

            if (string.IsNullOrEmpty(Password))
            {
                this.ShowPasswordError = true;
                this.PasswordError = ERROR_MESSAGES.REQUIRED_FIELD;
                return;
            }

            else
            {
                if (Password.Length > 0 && Password.Length < MIN_PASS_CHARS)
                {
                    this.PasswordError = "הסיסמה חייבת לכלול לפחות 8 תווים";
                    this.ShowPasswordError = true;
                    //return;
                }

                else
                {
                    ShowPasswordError = false;
                }
            }
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

        #region סניפים
        private List<Branch> allBranches;
        private ObservableCollection<Branch> filteredBranches;
        public ObservableCollection<Branch> FilteredBranches
        {
            get
            {
                return this.filteredBranches;
            }
            set
            {
                if (this.filteredBranches != value)
                {

                    this.filteredBranches = value;
                    OnPropertyChanged("FilteredBranches");
                }
            }
        }

        private string searchBranch;
        public string SearchBranch
        {
            get
            {
                return this.searchBranch;
            }
            set
            {
                //if (this.searchBranch != value)
                //{
                this.searchBranch = value;
                OnTypeChanged(value);
                OnPropertyChanged("SearchBranch");
                //}
            }
        }

        private void InitBranches()
        {
            IsRefresh = true;
            App theApp = (App)App.Current;
            this.allBranches = theApp.LookupTables.Branches;


            //Copy list to the filtered list
            this.filteredBranches = new ObservableCollection<Branch>(this.allBranches.OrderBy(b => b.BranchLocation));
            SearchBranch = String.Empty;
            IsRefresh = false;
        }

        private string newBranch;
        public string NewBranch
        {
            get => newBranch;
            set
            {
                newBranch = value;
                OnPropertyChanged("NewBranch");
            }
        }
        #endregion סניפים

        #region Search Branches
        public void OnTypeChanged(string searching)
        {
            //Filter the list of contacts based on the search term
            if (this.allBranches == null)
                return;
            if (String.IsNullOrWhiteSpace(searching) || String.IsNullOrEmpty(searching))
            {
                foreach (Branch b in this.allBranches)
                {
                    if (!this.FilteredBranches.Contains(b))
                        this.FilteredBranches.Add(b);
                }
            }
            else
            {
                foreach (Branch b in this.allBranches)
                {
                    string branchesString = $"{b.BranchLocation}";

                    if (!this.FilteredBranches.Contains(b) &&
                        branchesString.Contains(searching))
                        this.FilteredBranches.Add(b);
                    else if (this.FilteredBranches.Contains(b) &&
                        !branchesString.Contains(searching))
                        this.FilteredBranches.Remove(b);
                }
            }

            this.FilteredBranches = new ObservableCollection<Branch>(this.FilteredBranches.OrderBy(b => b.BranchLocation));
        }
        #endregion

        #region Refresh Branches
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
        public ICommand RefreshBranchesCommand => new Command(OnRefreshBranches);
        public void OnRefreshBranches()
        {
            InitBranches();
        }
        #endregion

        #region BranchSelection
        List<Branch> selectedBranches;
        public List<Branch> SelectedBranches
        {
            get
            {
                return selectedBranches;
            }
            set
            {
                if (selectedBranches != value)
                {
                    selectedBranches = value;
                }
            }
        }

        private string branches;

        public string Branches
        {
            get => branches;
            set
            {
                branches = value;
                OnPropertyChanged("branches");
            }
        }



        public ICommand UpdateBranches => new Command(OnPressedBranches);
        public async void OnPressedBranches(object branchesList)
        {
            SelectedBranches.Clear();
            Branches = string.Empty;
            if (branchesList is IList<object>)
            {
                List<object> list = ((IList<object>)branchesList).ToList();
                foreach (object b in list)
                {
                    SelectedBranches.Add((Branch)b);

                }
                if (selectedBranches.Count == 0) { Branches = "לא נבחרו סניפים"; }
            }
        }
        #endregion

        #region Add New Branch
        public ICommand AddBranch => new Command(OnAddBranch);
        public async void OnAddBranch()
        {

            if (string.IsNullOrEmpty(NewBranch))
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "!לא ניתן להוסיף ערך זה", "בסדר");
                return;
            }

            Branch newBranches = new Branch
            {
                BranchLocation = NewBranch
            };

            bool IsExist = false;
            if (allBranches.Contains(newBranches)) { IsExist = true; }

            if (!IsExist)
            {
                VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
                bool ok = await proxy.AddBranch(newBranches);

                if (ok)
                {
                    allBranches.Add(newBranches);
                    //SearchBranch = "";
                    await App.Current.MainPage.DisplayAlert("", "בסדר", "!הוספת מיקום בהצלחה");
                }
                else if (!ok)
                {
                    await App.Current.MainPage.DisplayAlert("בסדר", "הוספת המיקום נכשלה", "שגיאה");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("בסדר", "מיקום זה כבר קיים במערכת", "שגיאה");
            }

        }
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

        public Command SaveDataCommand { protected set; get; }

        #region Constructor
        public UpdateAssoViewModel()
        {
            App theApp = (App)App.Current;

            Association currentUser = (Association)theApp.CurrentUser;
            VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();

            this.Email = currentUser.Email;
            this.Username = currentUser.UserName;
            this.Password = currentUser.Pass;
            this.UserImgSrc = proxy.GetBasePhotoUri() + $"A{currentUser.AssociationId}.jpg";
            this.PhoneNum = currentUser.PhoneNum;
            this.InformationAbout = currentUser.InformationAbout;

            this.ShowEmailError = false;
            this.ShowUsernameError = false;
            this.ShowInformationAboutError = false;
            this.ShowPhoneNumError = false;
            this.ShowPasswordError = false;
            this.selectedOccuAreas = new List<OccupationalArea>();
            this.filteredOccuAreas = new ObservableCollection<OccupationalArea>();

            this.selectedBranches = new List<Branch>();
            this.filteredBranches = new ObservableCollection<Branch>();

            InitOccuAreas();
            InitBranches();

            this.SaveDataCommand = new Command(() => SaveData());
        }
        #endregion

        #region ValidateForm
        private bool ValidateForm()
        {
            //Validate all fields first
            //ValidateEmail();
            //ValidateUsername();
            ValidateInformationAbout();
            ValidatePhoneNum();
            ValidatePassword();

            //check if any validation failed
            if (/*ShowUsernameError || ShowEmailError ||*/ ShowInformationAboutError || ShowPhoneNumError || ShowPasswordError)
                return false;
            return true;
        }
        #endregion

        #region SaveData
        private async void SaveData()
        {
            if (ValidateForm())
            {
                ServerStatus = "מתחבר לשרת...";
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));

                App theApp = (App)App.Current;
                Association newUser = new Association()
                {
                    AssociationId = ((Association)theApp.CurrentUser).AssociationId,
                    Email = this.Email,
                    UserName = this.Username,
                    Pass = this.Password,
                    PhoneNum = this.PhoneNum,
                    InformationAbout = this.InformationAbout
                };

                foreach (OccupationalArea o in selectedOccuAreas)
                {
                    OccupationalAreasOfAssociation oc = new OccupationalAreasOfAssociation
                    {
                        OccupationalArea = o,
                        Association = newUser
                    };
                    newUser.OccupationalAreasOfAssociations.Add(oc);
                }

                foreach (Branch b in selectedBranches)
                {
                    BranchesOfAssociation br = new BranchesOfAssociation
                    {
                        Branch = b,
                        Association = newUser
                    };
                    newUser.BranchesOfAssociations.Add(br);
                }


                VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
                Association user = await proxy.UpdateAssociation(newUser);

                if (user == null)
                {
                    await App.Current.MainPage.Navigation.PopModalAsync();
                    await App.Current.MainPage.DisplayAlert("שגיאה", "העדכון נכשל", "אישור", FlowDirection.RightToLeft);
                }
                else
                {
                    if (this.imageFileResult != null)
                    {
                        ServerStatus = "מעלה תמונה...";

                        bool success = await proxy.UploadImage(new FileInfo()
                        {
                            Name = this.imageFileResult.FullPath
                        }, $"A{newUser.AssociationId}.jpg");

                        if (success)
                        {
                            UserImgSrc = newUser.ImgSource;
                        }
                    }
                    ServerStatus = "שומר נתונים...";

                    theApp.CurrentUser = user;
                    await App.Current.MainPage.Navigation.PopModalAsync();

                    Page p = new Views.HomePage();
                    App.Current.MainPage = p;
                    //await App.Current.MainPage.DisplayAlert("עדכון", "העדכון בוצע בהצלחה", "אישור", FlowDirection.RightToLeft);
                }
            }
            else
                await App.Current.MainPage.DisplayAlert("שמירת נתונים", " יש בעיה עם הנתונים בדוק ונסה שוב", "אישור", FlowDirection.RightToLeft);
        }
        #endregion   

        FileResult imageFileResult;
        public event Action<ImageSource> SetImageSourceEvent;
        #region PickImage
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
        #endregion

        //The following command handle the take photo button
        #region CameraImage
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
