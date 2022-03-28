﻿using System;
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
            this.ShowConditions = false;

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
