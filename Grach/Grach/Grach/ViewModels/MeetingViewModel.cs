using Xamarin.Forms;

namespace Grach.ViewModels
{
    public class MeetingViewModel : BindableObject
    {
        public string Name { get; set; }
        
        public string Author { get; set; }

        public string Distance { get; set; }
    }
}