using System.Windows.Controls;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Threading;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace ComfortDevClient.ViewModel
{

    class MainViewModel : ViewModelBase
    {
        private Page Log;
        private Page Regist;

        private Page _currentPage;
        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                RaisePropertyChanged(() => CurrentPage);
            }
        }

        private double _frameOpasity;
        public double FrameOpasity
        {
            get { return _frameOpasity; }
            set
            {
                _frameOpasity = value;
                RaisePropertyChanged(() => FrameOpasity);
            }
        }
        public MainViewModel()
        {
            Log = new Pages.Login();
            Regist = new Pages.Registration();

            CurrentPage = Log;
            FrameOpasity = 1;
        }
        public ICommand bToRegistr
        {
            get { return new RelayCommand(() => {
                if (CurrentPage == Log) { SlowOpasity(Regist); }
                else if (CurrentPage == Regist) { SlowOpasity(Log); }
            }); }
        }
        public ICommand bToLogin
        {
            get { return new RelayCommand(() => SlowOpasity(Log)); }
        }
        public async void SlowOpasity(Page page)
        {
            await Task.Factory.StartNew(() =>
            {
                for (double i = 1.0; i > 0.0; i -= 0.1)
                {
                    FrameOpasity = i;
                    Thread.Sleep(50);
                }
                CurrentPage = page;
                for (double i = 0.0; i < 1.1; i += 0.1)
                {
                    FrameOpasity = i;
                    Thread.Sleep(50);
                }
            });
        }
    }
}
