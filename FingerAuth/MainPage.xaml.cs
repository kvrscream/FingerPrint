using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FingerAuth.ViewModels;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Xamarin.Forms;

namespace FingerAuth
{
    public partial class MainPage : ContentPage
    {
        public AuthViewModel AuthVM { get; set; }
        public MainPage()
        {
            InitializeComponent();
            this.AuthVM = new AuthViewModel();
            this.BindingContext = this.AuthVM;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<AuthViewModel>(this, "dialog", async (msg) =>
            {
                bool permission = await CrossFingerprint.Current.IsAvailableAsync(true);

                if(permission)
                {
                    AuthenticationRequestConfiguration instruction = new AuthenticationRequestConfiguration("Prove que você tem dedos.", "Posicione seu dedo no sensor de digital!");
                    var auth = await CrossFingerprint.Current.AuthenticateAsync(instruction);
                    if (auth.Authenticated)
                    {
                        this.AuthVM.AuthResult = "Parabéns você foi autenticado com sucesso!";
                        this.AuthVM.ResultColor = "#32E64D";
                        this.AuthVM.IsAuthenticated = true;
                    }
                    else
                    {
                        this.AuthVM.AuthResult = "Ops! Você não pode ser autenticado!";
                        this.AuthVM.ResultColor = "#E66832";
                        this.AuthVM.IsAuthenticated = true;
                    }
                }
                else
                {
                    await DisplayAlert("Atenção", "Seu dispositivo não tem suporte para leitura de digital!", "Ok");
                }
            });
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<AuthViewModel>(this, "dialog");
        }



    }



}
