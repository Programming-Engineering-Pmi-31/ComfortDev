using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using ComfortDevClient.Pages;
using ComfortDevClient.ViewModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Threading.Tasks;
using System.Threading;

namespace ComfortDevClient.ViewModel
{
    class IdleTestsSwitch : ViewModelBase
    {
        private Page idlePage;
        private Page testsPage;

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

        public bool TestDone;
        public IdleTestsSwitch()
        {
            testsPage = new TestsPage();
            idlePage = new IdlePage();
            
            FrameOpasity = 1;
            
            TestDone = false;
            if (!TestDone)
                CurrentPage = testsPage;
            else
                CurrentPage = idlePage;
        }

        public ICommand SwitchCommand
        {
            get { return new RelayCommand(() => {
                if (CurrentPage == testsPage) SlowOpasity(idlePage);
                else if (CurrentPage == idlePage) SlowOpasity(testsPage);
            }); }
        }
        public ICommand ToIdlePage
        {
            get { return new RelayCommand(() => SlowOpasity(idlePage)); }
        }

        public async void SlowOpasity(Page page)
        {
            await Task.Factory.StartNew(() =>
            {
                for (double i = 1.0; i > 0.0; i -= 0.1)
                {
                    FrameOpasity = i;
                    Thread.Sleep(25);
                }
                CurrentPage = page;
                for (double i = 0.0; i < 1.1; i += 0.1)
                {
                    FrameOpasity = i;
                    Thread.Sleep(25);
                }
            });
        }

    }
}