using Grach.Extensions;
using Prism.Mvvm;
using Xamarin.Essentials;

namespace Grach.ViewModels.Base
{
    public class BindableModelBase : BindableBase
    {
        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        protected bool IsConnected => Connectivity.NetworkAccess.IsConnected();
    }
}