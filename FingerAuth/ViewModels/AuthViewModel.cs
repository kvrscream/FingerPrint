using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace FingerAuth.ViewModels
{
    public class AuthViewModel : BaseViewModel
    {
        bool _isAuthenticated = false;
        public bool IsAuthenticated
        {
            get
            {
                return _isAuthenticated;
            }
            set
            {
                _isAuthenticated = value;
                OnPropertChanged();
                OnPropertChanged(nameof(IsAuthenticated));
            }
        }



        string _authResult;
        public string AuthResult
        {
            get
            {
                return _authResult;
            }
            set
            {
                _authResult = value;
                OnPropertChanged();
                OnPropertChanged(nameof(AuthResult));
            }
        }

        string _resultColor = "#E66832";
        public string ResultColor
        {
            get
            {
                return _resultColor;
            }
            set
            {
                _resultColor = value;
                OnPropertChanged();
                OnPropertChanged(nameof(ResultColor));
            }
        }

        public ICommand DialogCommand { get; set; }

        public AuthViewModel()
        {
            DialogCommand = new Command(() =>
            {
                MessagingCenter.Send(this, "dialog");
            });
        }


    }
}
