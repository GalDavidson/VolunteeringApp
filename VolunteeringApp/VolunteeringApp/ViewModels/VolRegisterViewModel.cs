//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using System.ComponentModel;
//using System.Windows.Input;
//using Xamarin.Forms;
//using VolunteeringApp.Services;
//using VolunteeringApp.Models;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text.RegularExpressions;
//using Xamarin.Essentials;

//namespace VolunteeringApp.ViewModels
//{
//    public class VolRegisterViewModel : INotifyPropertyChanged
//    {

//        #region INotifyPropertyChanged
//        public event PropertyChangedEventHandler PropertyChanged;
//        protected void OnPropertyChanged(string propertyName)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }
//        #endregion

//        #region דואר אלקטרוני
//        private bool showEmailError;

//        public bool ShowEmailError
//        {
//            get => showEmailError;
//            set
//            {
//                showEmailError = value;
//                OnPropertyChanged("ShowEmailError");
//            }
//        }

//        private string email;

//        public string Email
//        {
//            get => email;
//            set
//            {
//                email = value;
//                ValidateEmail();
//                OnPropertyChanged("Email");
//            }
//        }

//        private string emailError;

//        public string EmailError
//        {
//            get => emailError;
//            set
//            {
//                emailError = value;
//                OnPropertyChanged("EmailError");
//            }
//        }

//        private void ValidateEmail()
//        {
//            this.ShowEmailError = string.IsNullOrEmpty(Email);
//            if (!this.ShowEmailError)
//            {
//                if (!Regex.IsMatch(this.Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
//                {
//                    this.ShowEmailError = true;
//                    this.EmailError = ERROR_MESSAGES.BAD_EMAIL;
//                }
//            }
//            else
//                this.EmailError = ERROR_MESSAGES.REQUIRED_FIELD;
//        }
//        #endregion

//        #region שם פרטי
//        private bool showNameError;

//        public bool ShowNameError
//        {
//            get => showNameError;
//            set
//            {
//                showNameError = value;
//                OnPropertyChanged("ShowNameError");
//            }
//        }

//        private string name;

//        public string Username
//        {
//            get => username;
//            set
//            {
//                username = value;
//                ValidateUsername();
//                OnPropertyChanged("Username");
//            }
//        }

//        private string usernameError;

//        public string UsernameError
//        {
//            get => usernameError;
//            set
//            {
//                usernameError = value;
//                OnPropertyChanged("UsernameError");
//            }
//        }

//        private void ValidateUsername()
//        {
//            this.ShowUsernameError = string.IsNullOrEmpty(Username);
//        }
//        #endregion

//        #region פרטי שם משתמש
//        private bool showUsernameError;

//        public bool ShowUsernameError
//        {
//            get => showUsernameError;
//            set
//            {
//                showUsernameError = value;
//                OnPropertyChanged("ShowUsernameError");
//            }
//        }

//        private string username;

//        public string Username
//        {
//            get => username;
//            set
//            {
//                username = value;
//                ValidateUsername();
//                OnPropertyChanged("Username");
//            }
//        }

//        private string usernameError;

//        public string UsernameError
//        {
//            get => usernameError;
//            set
//            {
//                usernameError = value;
//                OnPropertyChanged("UsernameError");
//            }
//        }

//        private void ValidateUsername()
//        {
//            this.ShowUsernameError = string.IsNullOrEmpty(Username);
//        }
//        #endregion

//        #region מס טלפון

//        private bool showPhoneNumError;

//        public bool ShowPhoneNumError
//        {
//            get => showPhoneNumError;
//            set
//            {
//                showPhoneNumError = value;
//                OnPropertyChanged("ShowPhoneNumError");
//            }
//        }

//        private string phoneNum;

//        public string PhoneNum
//        {
//            get => phoneNum;
//            set
//            {
//                phoneNum = value;
//                ValidatePhoneNum();
//                OnPropertyChanged("PhoneNum");
//            }
//        }

//        private string phoneNumError;

//        public string PhoneNumError
//        {
//            get => phoneNumError;
//            set
//            {
//                phoneNumError = value;
//                OnPropertyChanged("PhoneNumError");
//            }
//        }

//        private void ValidatePhoneNum()
//        {
//            this.ShowEmailError = string.IsNullOrEmpty(PhoneNum);
//        }

//        #endregion

//        #region סיסמה
//        private const int MIN_PASS_CHARS = 6;

//        private bool showPassError;

//        public bool ShowPassError
//        {
//            get => showPassError;
//            set
//            {
//                showPassError = value;
//                OnPropertyChanged("ShowPassError");
//            }
//        }

//        private string pass;

//        public string Pass
//        {
//            get => pass;
//            set
//            {
//                pass = value;
//                ValidatePass();
//                OnPropertyChanged("Pass");
//            }
//        }

//        private string passError;

//        public string PassError
//        {
//            get => passError;
//            set
//            {
//                passError = value;
//                OnPropertyChanged("PassError");
//            }
//        }

//        private void ValidatePass()
//        {
//            this.ShowPassError = string.IsNullOrEmpty(Pass);

//            if (!this.ShowPassError)
//            {
//                if (Pass.Length < MIN_PASS_CHARS)
//                {
//                    this.ShowPassError = true;
//                    this.PassError = ERROR_MESSAGES.BAD_PASSWORD;
//                }
//            }
//            else
//                this.PassError = ERROR_MESSAGES.REQUIRED_FIELD;
//        }


//        #endregion

//        #region מקור התמונה
//        private string profileImgSrc;

//        public string ProfileImgSrc
//        {
//            get => profileImgSrc;
//            set
//            {
//                profileImgSrc = value;
//                OnPropertyChanged("ProfileImgSrc");
//            }
//        }
//        private const string DEFAULT_PHOTO_SRC = "defaultphoto.jpg";
//        #endregion

//        class VolRegisterViewModel
//        {

//        }
//    }
//}
