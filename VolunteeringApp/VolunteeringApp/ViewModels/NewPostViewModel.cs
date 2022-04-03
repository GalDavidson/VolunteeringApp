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

            this.imgSrc = DEFAULT_PHOTO_SRC;
            this.imageFileResult = null; //mark that no picture was chosen
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
                Post newP = new Post
                {
                    Caption = this.Caption
                };

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
                        }, $"P{p.Caption}.jpg");

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
