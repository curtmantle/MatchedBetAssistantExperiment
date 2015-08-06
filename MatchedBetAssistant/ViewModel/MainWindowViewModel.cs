﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MatchedBetAssistant.ViewModel.Account;
using MatchedBetAssistant.ViewModel.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchedBetAssistant.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewModelBase accountView;
        private Model.BetAssistant assistant;

        public MainWindowViewModel()
        {
            RegisterMessages();
            this.assistant = new Model.BetAssistant();

            this.AccountView = new SessionLoginViewModel(assistant);
        }

        public ViewModelBase AccountView
        {
            get { return accountView; }
            private set
            {
                this.accountView = value;

                RaisePropertyChanged(() => AccountView);
            }
        }

        private void RegisterMessages()
        {
            Messenger.Default.Register<LoggedOnMessage>(this, LoggedOn);
        }

        private void LoggedOn(LoggedOnMessage obj)
        {
            this.AccountView = new AccountSummaryViewModel(this.assistant.Account);
        }
    }
}