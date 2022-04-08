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
    public class VolRegisterViewModel : INotifyPropertyChanged
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
        }
        #endregion

        #region שם פרטי
        private bool showNameError;

        public bool ShowNameError
        {
            get => showNameError;
            set
            {
                showNameError = value;
                OnPropertyChanged("ShowNameError");
            }
        }

        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                ValidateName();
                OnPropertyChanged("Name");
            }
        }

        private string nameError;

        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged("NameError");
            }
        }

        private void ValidateName()
        {
            this.ShowConditions = false;

            this.ShowNameError = string.IsNullOrEmpty(Name);
            if (ShowNameError)
                NameError = ERROR_MESSAGES.REQUIRED_FIELD;
        }
        #endregion

        #region שם משפחה
        private bool showLastNameError;

        public bool ShowLastNameError
        {
            get => showLastNameError;
            set
            {
                showLastNameError = value;
                OnPropertyChanged("ShowLastNameError");
            }
        }

        private string lastName;

        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                ValidateLastName();
                OnPropertyChanged("LastName");
            }
        }

        private string lastnameError;

        public string LastNameError
        {
            get => lastnameError;
            set
            {
                lastnameError = value;
                OnPropertyChanged("LastNameError");
            }
        }

        private void ValidateLastName()
        {
            this.ShowConditions = false;

            this.ShowLastNameError = string.IsNullOrEmpty(LastName);
            if (ShowLastNameError)
                LastNameError = ERROR_MESSAGES.REQUIRED_FIELD;
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
            //this.ShowConditions = false;

            this.ShowUsernameError = string.IsNullOrEmpty(Username);
            if (ShowUsernameError)
                UsernameError = ERROR_MESSAGES.REQUIRED_FIELD;
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
                if (Password.Length > 0 && Password.Length < MIN_PASS_CHARS)
                {
                    this.PasswordError = "הסיסמה חייבת לכלול לפחות 8 תווים";
                    this.ShowPasswordError = true;
                }
                else
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

        #region מגדר
        public ObservableCollection<Gender> Genders { get; }

        public void CreateGenderCollection()
        {
            if (((App)App.Current).LookupTables != null)
            {
                List<Gender> gendersList = ((App)App.Current).LookupTables.Genders;
                foreach (Gender g in gendersList)
                {
                    this.Genders.Add(g);
                }
            }
        }

        private Gender gender;
        public Gender Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }

        #endregion

        #region Add New Gender

        private string newGender;
        public string NewGender
        {
            get => newGender;
            set
            {
                newGender = value;
                OnPropertyChanged("NewGender");
            }
        }

        public ICommand AddGender => new Command(OnAddGender);
        public async void OnAddGender()
        {

            if (string.IsNullOrEmpty(NewGender))
            {
                await App.Current.MainPage.DisplayAlert("שגיאה", "!לא ניתן להוסיף ערך זה", "בסדר");
                return;
            }

            Gender newG = new Gender
            {
                GenderType = NewGender
            };

            bool IsExist = false;
            if (Genders.Contains(newG)) { IsExist = true; }

            if (!IsExist)
            {
                VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
                bool ok = await proxy.AddGender(newG);

                if (ok)
                {
                    Genders.Add(newG);
                    //SearchBranch = "";
                    await App.Current.MainPage.DisplayAlert("", "בסדר", "!הוספת מגדר בהצלחה");
                }
                else if (!ok)
                {
                    await App.Current.MainPage.DisplayAlert("בסדר", "הוספת המגדר נכשלה", "שגיאה");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("בסדר", "מגדר זה כבר קיים במערכת", "שגיאה");
            }

        }
        #endregion

        #region תאריך לידה

        private DateTime entryBirthDate = DateTime.Now;
        public DateTime EntryBirthDate
        {
            get => this.entryBirthDate;
            set
            {
                if (value != this.entryBirthDate)
                {
                    this.entryBirthDate = value;
                    OnPropertyChanged("EntryBirthDate");
                }
            }
        }

        private bool showBirthDateError;
        private bool ShowBirthDateError
        {
            get => showBirthDateError;
            set
            {
                showBirthDateError = value;
                OnPropertyChanged("ShowBirthDateError");
            }
        }

        private DateTime birthdate;
        private DateTime Birthdate
        {
            get => birthdate;
            set
            {
                birthdate = value;
                ValidateBirthDate();
                OnPropertyChanged("Birthdate");
            }
        }

        private bool birthDateError;
        private bool BirthDateError
        {
            get => birthDateError;
            set
            {
                birthDateError = value;
                OnPropertyChanged("BirthDateError");
            }
        }

        private const int MIN_AGE = 14;

        private void ValidateBirthDate()
        {
            TimeSpan ts = DateTime.Now - this.EntryBirthDate;
            this.ShowBirthDateError = ts.TotalDays < (MIN_AGE * 365);
        }
        #endregion

        #region מקור התמונה
        private string profileImgSrc;

        public string ProfileImgSrc
        {
            get => profileImgSrc;
            set
            {
                profileImgSrc = value;
                OnPropertyChanged("ProfileImgSrc");
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

        public ICommand SubmitCommand { protected set; get; }

        private bool ValidateForm()
        {
            ValidateName();
            ValidateLastName();
            ValidateEmail();
            ValidateUsername();
            ValidatePassword();
            ValidateVerPassword();

            //check if any validation failed
            if (ShowNameError || ShowLastNameError || ShowUsernameError || ShowEmailError || ShowPasswordError || ShowVerPasswordError)
                return false;
            return true;
        }

        public VolRegisterViewModel()
        {
            this.ShowNameError = false;
            this.ShowLastNameError = false;
            this.ShowEmailError = false;
            this.ShowUsernameError = false;
            this.ShowPasswordError = false;
            this.ShowVerPasswordError = false;
            this.ShowConditions = false;
            this.SubmitCommand = new Command(OnSubmit);

            this.profileImgSrc = DEFAULT_PHOTO_SRC;
            this.imageFileResult = null; //mark that no picture was chosen
            Genders = new ObservableCollection<Gender>();
            CreateGenderCollection();
        }

        public async void OnSubmit()
        {
            App app = (App)App.Current;

            if (ValidateForm())
            {
                Volunteer volunteer = new Volunteer
                {
                    FName = Name,
                    LName = LastName,
                    Email = Email,
                    UserName = Username,
                    Pass = Password,
                    ActionDate = DateTime.Today,
                    GenderId = this.Gender.GenderId,
                    BirthDate = EntryBirthDate
                };

                volunteer.Gender = new Gender
                {
                    GenderId = this.Gender.GenderId,
                    GenderType = this.Gender.GenderType
                };

                VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
                Volunteer v = await proxy.RegVolAsync(volunteer);

                if (v == null)
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
                        }, $"V{v.VolunteerId}.jpg");

                        if (success)
                        {
                            ProfileImgSrc = v.ImgSource;
                        }
                    }
                    ServerStatus = "שומר נתונים...";

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
