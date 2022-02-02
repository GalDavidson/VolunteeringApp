using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using VolunteeringApp.Services;
using VolunteeringApp.Models;
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

        private string lastname;

        public string LastName
        {
            get => lastname;
            set
            {
                lastname = value;
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

        #region מגדר
        public List<Gender> Genders
        {
            get
            {
                if (((App)App.Current).LookupTables != null)
                    return ((App)App.Current).LookupTables.Genders;
                return new List<Gender>();
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

        public ICommand SubmitCommand { protected set; get; }

        private bool ValidateForm()
        {
            ValidateEmail();
            ValidateUsername();
            ValidatePhoneNum();
            ValidatePassword();
            ValidateVerPassword();

            //check if any validation failed
            if (ShowUsernameError || ShowEmailError || ShowPhoneNumError || ShowPasswordError || ShowVerPasswordError)
                return false;
            return true;
        }

        public VolRegisterViewModel()
        {
            this.ShowEmailError = false;
            this.ShowUsernameError = false;
            this.ShowPhoneNumError = false;
            this.ShowPasswordError = false;
            this.ShowVerPasswordError = false;
            ShowConditions = false;
            //this.selectedOccuAreas = new List<OccupationalArea>();

            //filteredOccuAreas = new ObservableCollection<OccupationalArea>();

            //InitOccuAreas();
            //this.SubmitCommand = new Command(OnSubmit);

            //this.AssoImgSrc = DEFAULT_PHOTO_SRC;
            //this.imageFileResult = null; //mark that no picture was chosen
        }
    }
}
