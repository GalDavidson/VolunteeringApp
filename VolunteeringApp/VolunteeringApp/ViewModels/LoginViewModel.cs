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
    class LoginViewModel: INotifyPropertyChanged
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
                    //return;
                }

                else
                {
                    ShowPasswordError = false;
                }
            }
        }
        #endregion

        private const string OPENEYE_PHOTO_SRC = "openEye.png";
        private const string CLOSEDEYE_PHOTO_SRC = "closedEye.png";

        public ICommand SubmitCommand { protected set; get; }
        public ICommand MoveToRegister { protected set; get; }

        public LoginViewModel()
        {
            ShowPass = true;
            imgSource = OPENEYE_PHOTO_SRC;
            SubmitCommand = new Command(OnSubmit);
            MoveToRegister = new Command(OnTap);
            PassCommand = new Command(OnShowPass);
        }

        public void OnTap()
        {
            Page p = new NavigationPage(new Views.RegisterNavigation());
            App.Current.MainPage = p;
        }

        private bool showPass;
        public bool ShowPass
        {
            get { return showPass; }

            set
            {
                if (this.showPass != value)
                {
                    this.showPass = value;
                    OnPropertyChanged(nameof(ShowPass));
                }
            }
        }

        private string imgSource;
        public string ImgSource
        {
            get => imgSource;
            set
            {
                imgSource = value;
                OnPropertyChanged("ImgSource");
            }
        }

        public ICommand PassCommand { protected set; get; }
        public void OnShowPass()
        {
            if (ShowPass == false)
            { ShowPass = true; }

            else { ShowPass = false; }

            if (ImgSource == CLOSEDEYE_PHOTO_SRC)
            { ImgSource = OPENEYE_PHOTO_SRC; }

            else { ImgSource = CLOSEDEYE_PHOTO_SRC; }
        }

        public async void OnSubmit()
        {
            VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
            Object user = await proxy.LoginAsync(Email, Password);

            if (user == null)
            {
               await App.Current.MainPage.DisplayAlert("שגיאה", "התחברות נכשלה, נא לבדוק שם משתמש וסיסמה", "בסדר");
            }
            else
            {
                App app = (App)Application.Current;
                app.CurrentUser = user;
                if (user is Association)
                {
                    await App.Current.MainPage.DisplayAlert("", "התחברת", "בסדר");
                    Page p = new NavigationPage(new Views.AllEventsPage());
                    App.Current.MainPage = p;
                }
                if (user is Volunteer)
                {
                    await App.Current.MainPage.DisplayAlert("", "התחברת", "בסדר");
                    Page p = new NavigationPage(new Views.AllEventsPage());
                    App.Current.MainPage = p;
                }
                else if (user is AppAdmin) 
                {
                    await App.Current.MainPage.DisplayAlert("", "התחברת", "בסדר");
                    Page p = new NavigationPage(new Views.AllEventsPage());
                    App.Current.MainPage = p;
                }
            }
        }
    }
}