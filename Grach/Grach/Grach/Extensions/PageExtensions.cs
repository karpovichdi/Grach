using Xamarin.Forms;

namespace Grach.Extensions
{
    public static class PageExtensions
    {
        public static bool IsModal(this Page page)
        {
            bool result = false;

            for (int i = page.Navigation.ModalStack.Count - 1; i > -1; i--)
            {
                if (page == page.Navigation.ModalStack[i] ||
                    page.Navigation.ModalStack[i] is NavigationPage navigationPage &&
                    navigationPage.CurrentPage == page)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
    }
}