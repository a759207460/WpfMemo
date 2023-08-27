using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace WpfDemo.Extensions
{
    public static class PassWordExtenxion
    {


        public static string GetPassWord(DependencyObject obj)
        {
            return (string)obj.GetValue(PassWordProperty);
        }

        public static void SetPassWord(DependencyObject obj, string value)
        {
            obj.SetValue(PassWordProperty, value);
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PassWordProperty =
            DependencyProperty.RegisterAttached("PassWord", typeof(string), typeof(PassWordExtenxion), new PropertyMetadata(string.Empty, OnPassWordPropertyChanged));

        public static void OnPassWordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs eventArgs)
        {
            var passWord = sender as PasswordBox;
            string pw = (string)eventArgs.NewValue;
            if (passWord != null && passWord.Password != pw)
            {
                passWord.Password = pw;
            }
        }
    }
    public class PassWordBehavior : Behavior<PasswordBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PasswordChanged += AssociatedObject_PasswordChanged;
        }

        private void AssociatedObject_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            string password = PassWordExtenxion.GetPassWord(passwordBox);
            if (passwordBox != null && passwordBox.Password != password)
            {
                PassWordExtenxion.SetPassWord(passwordBox, passwordBox.Password);
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PasswordChanged -= AssociatedObject_PasswordChanged;
        }
    }

}
