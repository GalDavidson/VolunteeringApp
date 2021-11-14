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
            this.ShowEmailError = string.IsNullOrEmpty(PhoneNum);
        }

        #endregion

        #region סיסמה
        private const int MIN_PASS_CHARS = 6;

        private bool showPassError;

        public bool ShowPassError
        {
            get => showPassError;
            set
            {
                showPassError = value;
                OnPropertyChanged("ShowPassError");
            }
        }

        private string pass;

        public string Pass
        {
            get => pass;
            set
            {
                pass = value;
                ValidatePass();
                OnPropertyChanged("Pass");
            }
        }

        private string passError;

        public string PassError
        {
            get => passError;
            set
            {
                passError = value;
                OnPropertyChanged("PassError");
            }
        }

        private void ValidatePass()
        {
            this.ShowPassError = string.IsNullOrEmpty(Pass);
            
            if (!this.ShowPassError)
            {
                if (Pass.Length < MIN_PASS_CHARS)
                {
                    this.ShowPassError = true;
                    this.PassError = ERROR_MESSAGES.BAD_PASSWORD;
                }
            }
            else
                this.PassError = ERROR_MESSAGES.REQUIRED_FIELD;
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

        //This contact is a reference to the updated or new created contact
        private Association theAssociation;
        //For adding a new contact, uc will be null
        //For updates the user contact object should be sent to the constructor
        public AssoRegisterViewModel(Association uc = null)
        {
            //create a new user contact if this is an add operation
            if (uc == null)
            {
                App theApp = (App)App.Current;
                uc = new Association()
                {
                    AssociationId = theApp.CurrentUser.Id,
                    Email = "",
                    UserName = "",
                    InformationAbout = "",
                    PhoneNum = "",
                    Pass = ""
                };

                //Setup default image photo
                this.ProfileImgSrc = DEFAULT_PHOTO_SRC;
                this.imageFileResult = null; //mark that no picture was chosen

            }
            else
            {
                //set the path url to the contact photo
                VolunteeringAPIProxy proxy = VolunteeringAPIProxy.CreateProxy();
                //Create a source with cache busting!
                Random r = new Random();
                this.ProfileImgSrc = proxy.GetBasePhotoUri() + uc.AssociationId + $".jpg?{r.Next()}";
            }

            this.theAssociation = uc;
            this.UsernameError = ERROR_MESSAGES.REQUIRED_FIELD;
            this.Email = ERROR_MESSAGES.REQUIRED_FIELD;
            this.EmailError = ERROR_MESSAGES.BAD_EMAIL;

            this.ShowEmailError = false;
            this.ShowLastNameError = false;
            this.ShowEmailError = false;
            this.ContactPhones = new ObservableCollection<Models.ContactPhone>(uc.ContactPhones);
            this.SaveDataCommand = new Command(() => SaveData());
            this.Name = uc.FirstName;
            this.LastName = uc.LastName;
            this.Email = uc.Email;
        }

        //This function validate the entire form upon submit!
        private bool ValidateForm()
        {
            //Validate all fields first
            ValidateLastName();
            ValidateName();
            ValidateEmail();

            //check if any validation failed
            if (ShowLastNameError ||
                ShowNameError || ShowEmailError)
                return false;
            return true;
        }

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

        //This event is fired after the new contact is generated in the system so it can be added to the list of contacts
        public event Action<UserContact, UserContact> ContactUpdatedEvent;

        //The command for saving the contact
        public Command SaveDataCommand { protected set; get; }
        private async void SaveData()
        {
            if (ValidateForm())
            {

                this.theContact.FirstName = this.Name;
                this.theContact.LastName = this.LastName;
                this.theContact.Email = this.Email;
                this.theContact.ContactPhones = this.ContactPhones.ToList();


                ServerStatus = "מתחבר לשרת...";
                await App.Current.MainPage.Navigation.PushModalAsync(new Views.ServerStatusPage(this));
                ContactsAPIProxy proxy = ContactsAPIProxy.CreateProxy();
                UserContact newUC = await proxy.UpdateContact(this.theContact);
                if (newUC == null)
                {
                    await App.Current.MainPage.DisplayAlert("שגיאה", "שמירת איש הקשר נכשלה", "בסדר");
                    await App.Current.MainPage.Navigation.PopModalAsync();
                }
                else
                {
                    if (this.imageFileResult != null)
                    {
                        ServerStatus = "מעלה תמונה...";

                        bool success = await proxy.UploadImage(new FileInfo()
                        {
                            Name = this.imageFileResult.FullPath
                        }, $"{newUC.ContactId}.jpg");
                    }
                    ServerStatus = "שומר נתונים...";
                    //if someone registered to get the contact added event, fire the event
                    if (this.ContactUpdatedEvent != null)
                    {
                        this.ContactUpdatedEvent(newUC, this.theContact);
                    }
                    //close the message and add contact windows!

                    await App.Current.MainPage.Navigation.PopAsync();
                    await App.Current.MainPage.Navigation.PopModalAsync();
                }
            }
            else
                await App.Current.MainPage.DisplayAlert("שמירת נתונים", " יש בעיה עם הנתונים בדוק ונסה שוב", "אישור", FlowDirection.RightToLeft);
        }

        ///The following command handle the pick photo button
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
