using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MatchedBetAssistant.Model;
using MatchedBetAssistant.ViewModel.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchedBetAssistant.ViewModel.Account
{
    public sealed class SessionLoginViewModel : ViewModelBase
    {
        private BetfairService assistant;
        private string applicationId;
        private string sessionToken;
        private RelayCommand loginCommand;

        public SessionLoginViewModel(BetfairService assistant)
        {
            this.assistant = assistant;
        }

        public string SessionToken
        {
            get
            {
                return sessionToken;
            }
            set
            {
                if (sessionToken == value)
                    return;
                sessionToken = value;

                RaisePropertyChanged(() => SessionToken);
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ?? (loginCommand = new RelayCommand(Login, CanLogin));
            }
        }

        private void Login()
        {
            var loggedOn = this.assistant.Login(sessionToken);

            if (loggedOn != null)
            {
                Messenger.Default.Send<LoggedOnMessage>(new LoggedOnMessage(loggedOn));
            }
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(this.sessionToken);
        }
    }
}
