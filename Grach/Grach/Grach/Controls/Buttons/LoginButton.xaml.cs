using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Grach.Controls.Buttons
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginButton
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), 
            typeof(ICommand), typeof(LoginButton), null);

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), 
            typeof(object), typeof(LoginButton), null);

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string),
            typeof(LoginButton), null, propertyChanging: null, propertyChanged: OnTextChanged);

        
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
        
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        
        public LoginButton()
        {
            InitializeComponent();
        }
        
        private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is LoginButton button))
            {
                return;
            }
            if(newValue is string text)
            {
                button.label.Text = text.ToUpper();
            }
        }

        private void ButtonTapped(object sender, EventArgs e)
        {
            Command?.Execute(CommandParameter);
        }
    }
}