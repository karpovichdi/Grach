using System;
using System.Collections.Generic;
using System.Windows.Input;
using Grach.Core.Helpers;
using Grach.Core.Interfaces;
using Grach.Extensions;
using Grach.Pages;
using Grach.ViewModels.Base;
using Prism;
using Prism.Navigation;
using Prism.Services.Dialogs;
using Xamarin.Forms;

namespace Grach.ViewModels
{
    public class FirstTabViewModel : ViewModelBase, IActiveAware
    {
        private bool isActive;

        public IList<int> Indexes { get; }

        public ObservableRangeCollection<MeetingViewModel> Meetings { get; set; }

        public event EventHandler IsActiveChanged;

        public ICommand NavigateToNavigationPageCommand { get; }

        public bool IsActive
        {
            get => isActive;
            set
            {
                SetProperty(ref isActive, value);
                this.Log(isActive.ToString());
            }
        }

        public FirstTabViewModel(ICommandResolver commandResolver,
                                 INavigationService navigationService,
                                 IDialogService dialogService,
                                 ILoggingServiceProvider logger)
            : base(navigationService, dialogService, logger, commandResolver)
        {
            Indexes = new List<int>(49);
            for (int i = 0; i < 49; i++)
                Indexes.Add(i);

            NavigateToNavigationPageCommand = new Command(NavigateToNavigationPage);
            
            Meetings = new ObservableRangeCollection<MeetingViewModel>();

            CreateFakeData();
        }

        private void CreateFakeData()
        {
            for (var i=0; i<10; i++)
            {
                var meeting = new MeetingViewModel
                {
                    Name = "Party name: " + i,
                    Author = "Author name: " + i,
                    Distance = i * 100 + " м"
                };
                
                Meetings.Add(meeting);
            }
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
        }

        private void NavigateToNavigationPage(object obj)
        {
            this.Log();
            NavigationService.NavigateAsync(nameof(NonModalView));
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            this.Log();
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            this.Log();
            base.OnNavigatedTo(parameters);
        }
    }
}