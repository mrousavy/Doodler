using System.Windows;

namespace Doodler.Views
{
    /// <summary>
    ///     Interaction logic for ErrorDialogView.xaml
    /// </summary>
    public partial class ErrorDialogView
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string),
                typeof(ErrorDialogView), new FrameworkPropertyMetadata
                {
                    BindsTwoWayByDefault = true
                });

        public ErrorDialogView()
        {
            InitializeComponent();
            TextBlock.DataContext = this;
        }

        public string Text
        {
            get => (string) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
    }
}