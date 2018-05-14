using Doodler.ViewModels;
using DoodlerCore;
using System.Windows;

namespace Doodler.Views
{
    /// <summary>
    /// Interaction logic for PollView.xaml
    /// </summary>
    public partial class PollView
    {
        public PollView()
        {
            InitializeComponent();
        }

        public Poll Poll
        {
            get => (Poll) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Poll), typeof(Poll),
                typeof(PollView), new FrameworkPropertyMetadata
                {
                    BindsTwoWayByDefault = true,
                    PropertyChangedCallback = PropertyChangedCallback
                });

        private static void PropertyChangedCallback(DependencyObject dependency, DependencyPropertyChangedEventArgs args)
        {
            if (dependency is PollView view)
            {
                if (view.DataContext is PollViewModel model)
                {
                    model.Poll = view.Poll;
                }
            }
        }
    }
}
