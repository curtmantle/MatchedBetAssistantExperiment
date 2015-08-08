using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MatchedBetAssistant.ViewModel.Account;
using MatchedBetAssistant.ViewModel.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchedBetAssistant.Model;
using MatchedBetAssistant.ViewModel.MarketSelection;
namespace MatchedBetAssistant.ViewModel
{
    public class MainWindowViewModel : ViewModelBase, IDisposable
    {
        private ViewModelBase accountView;
        private ViewModelBase mainView;

        private BetfairService assistant;

        public MainWindowViewModel()
        {
            RegisterMessages();
            this.assistant = new BetfairService();
            this.assistant.ReadApplicationKey();

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

        public ViewModelBase MainView
        {
            get
            {
                return this.mainView;
            }
            set
            {
                this.mainView = value;

                RaisePropertyChanged(() => MainView);
            }
        }

        private void RegisterMessages()
        {
            Messenger.Default.Register<LoggedOnMessage>(this, LoggedOn);
        }

        private void LoggedOn(LoggedOnMessage msg)
        {
            this.AccountView = new AccountSummaryViewModel(msg.Account);
            this.MainView = new EventSelectorViewModel(this.assistant);
        }


        public void Dispose()
        {
            if (assistant != null)
            {
                assistant.Dispose();
                assistant = null;
            }
        }

    }
}
