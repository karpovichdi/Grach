using Grach.Extensions;
using Grach.ViewModels.Base;
using Prism.Navigation;


namespace Grach.ViewModels
{
    public class MainViewModel : ViewModelBase /*ITabbedViewModel*/
    {
        public FirstTabViewModel FirstViewModel { get; private set; }
        public SecondTabViewModel SecondViewModel { get; private set; }
        public ThirdTabViewModel FirdViewModel { get; private set; }

        public MainViewModel(INavigationService navigationService)
            : base(navigationService) { }

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