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

    public static class ERROR_MESSAGES
    {
        public const string REQUIRED_FIELD = "זהו שדה חובה";
        public const string BAD_EMAIL = "מייל לא תקין";
        public const string EMAIL_EXIST = "Email is already exist";
        public const string BAD_PHONE = "Phone number is not valid";
        public const string BAD_USERNAME = "This username is already exist. Please try another one:)";
        public const string GENERAL_ERROR = "Something went bad. Please try again";
        public const string BAD_PASSWORD = "Password has to be 6 charechters minimum";
        public const string BAD_DATE = "You must be older than today years old to use this app!";
    }

    public class AssoRegisterViewModel : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

            this.ShowConditions = false; 

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
                //this.emailError = null;
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
            this.ShowConditions = false;

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
            this.ShowConditions = false;

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
            this.ShowConditions = false;

            this.ShowPhoneNumError = string.IsNullOrEmpty(PhoneNum);
            if (ShowPhoneNumError)
                this.PhoneNumError = ERROR_MESSAGES.REQUIRED_FIELD;
            else
            {
                if (PhoneNum.Length < 10)
                    this.PhoneNumError = "מס' הטלפון קצר מדי, הוא צריך להכיל בעל 10 ספרות לפחות ";
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
            this.ShowConditions = false;

            if (string.IsNullOrEmpty(Password))
            {
                this.ShowPasswordError = true;
                this.PasswordError = ERROR_MESSAGES.REQUIRED_FIELD;
                return;
            }

            else
            {
                if (!Regex.IsMatch(this.Password, @"^ (?=.*?[A - Z])(?=.*?[a - z])(?=.*?[0 - 9])(?=.*?[#?!@$%^&*-]).{8,}$"))
                {
                    this.ShowPasswordError = true;
                    this.PasswordError = "הסיסמה אינה תקינה";
                    return;
                }

                else if (Password.Length > 0 && Password.Length < MIN_PASS_CHARS)
                {
                    this.PasswordError = "הסיסמה חייבת לכלול לפחות 8 תווים";
                    this.ShowPasswordError = true;
                    return;
                }

                ShowPasswordError = false;
            }
        }
      

        private string verPassword;
        public string VerPassword
        {
            get
            {
                return this.verPassword;
            }
            set
            {
                if (this.verPassword != value)
                {
                    this.verPassword = value;
                    ValidateVerPassword();

                    OnPropertyChanged("VerPassword");
                }
            }
        }

        private bool showVerPasswordError;
        public bool ShowVerPasswordError
        {
            get => showVerPasswordError;
            set
            {
                showVerPasswordError = value;
                OnPropertyChanged("ShowVerPasswordError");
            }
        }

        private string verPasswordError;
        public string VerPasswordError
        {
            get => verPasswordError;
            set
            {
                verPasswordError = value;
                OnPropertyChanged("VerPasswordError");
            }
        }


        private void ValidateVerPassword()
        {

            this.ShowConditions = false;


            if (string.IsNullOrEmpty(VerPassword))
            {
                this.ShowVerPasswordError = true;
                this.VerPasswordError = ERROR_MESSAGES.REQUIRED_FIELD;
                return;
            }

            else if (VerPassword != Password)
            {
                VerPasswordError = "הסיסמאות חייבות להיות תואמות";
                ShowVerPasswordError = true;
            }
            else
                this.ShowVerPasswordError = false;

        }
        #endregion

        #region סניפים
        public List<Branch> Branches
        {
            get
            {
                if (((App)App.Current).LookupTables!= null)
                    return ((App)App.Current).LookupTables.Branches;
                return new List<Branch>();
            }
        }

        private Branch branch;
        public Branch Branch
        {
            get { return branch; }
            set
            {
                branch = value;
                OnPropertyChanged("Branch");
            }
        }
        #endregion סניפים

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
                if (this.searchTerm != value)
                {

                    this.searchTerm = value;
                    OnTextChanged(value);
                    OnPropertyChanged("SearchTerm");
                }
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

            //List<OccupationalArea> occupationalAreas = (List<OccupationalArea>)occuAreasList;
            //foreach (OccupationalArea a in occupationalAreas)
            //{
            //    SelectedOccuAreas.Add(a);
            //}
        }
        #endregion

        #region SaveAreas
        public ICommand SaveAreas => new Command(OnSaveOccupationalArea);
        public async void OnSaveOccupationalArea()
        {
            foreach (OccupationalArea a in selectedOccuAreas)
            {
                OccupationalAreas += a.OccupationName + "," + " ";
            }
            OccupationalAreas = OccupationalAreas.Substring(0, OccupationalAreas.Length - 2);
            if (selectedOccuAreas.Count == 0) { OccupationalAreas = "לא נבחרו תחומי עיסוק"; }

            //await PopupNavigation.Instance.PopAsync();
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


        public event Action<Association, Association> ContactUpdatedEvent;
        public ICommand SubmitCommand { protected set; get; }

        private bool ValidateForm()
        {
            ValidateEmail();
            ValidateUsername();
            ValidateInformationAbout();
            ValidatePhoneNum();
            ValidatePassword();
            ValidateVerPassword();

            //check if any validation failed
            if (ShowUsernameError || ShowEmailError || ShowInformationAboutError || ShowPhoneNumError || ShowPasswordError || ShowVerPasswordError)
                return false;
            return true;
        }


        private Association theAssociation;
        public AssoRegisterViewModel()
        {
            this.ShowEmailError = false;
            this.ShowUsernameError = false;
            this.ShowInformationAboutError = false;
            this.ShowPhoneNumError = false;
            this.ShowPasswordError = false;
            this.ShowVerPasswordError = false;
            ShowConditions = false;
            this.selectedOccuAreas = new List<OccupationalArea>();

            filteredOccuAreas = new ObservableCollection<OccupationalArea>();

            InitOccuAreas();
            this.SubmitCommand = new Command(OnSubmit);

            this.imageFileResult = null;

        }

        public async void OnSubmit()
        {
            App app = (App)App.Current;

            if (ValidateForm())
            {
                Association association = new Association
                {
                    Email = Email,
                    UserName = Username,
                    InformationAbout = InformationAbout,
                    PhoneNum = phoneNum,
                    Pass = Password,
                    ActionDate = DateTime.Today
                };

                VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
                Association asso = await proxy.RegAssoAsync(association);

                if (asso == null)
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
                        }, $"{asso.AssociationId}.jpg");
                    }
                    ServerStatus = "שומר נתונים...";
                    //if someone registered to get the contact added event, fire the event
                    if (this.ContactUpdatedEvent != null)
                    {
                        this.ContactUpdatedEvent(asso, this.theAssociation);
                    }

                    Page p = new NavigationPage(new Views.HomePage());
                    App.Current.MainPage = p;
                }
            }
            else
            {
                ShowNextError = true;
                NextError = "אירעה שגיאה! לא ניתן להמשיך בהרשמה";
            }
        }


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


    }
}

