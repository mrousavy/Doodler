using System.Windows;
using Doodler.ViewModels;
using DoodlerCore;

namespace Doodler.Views
{
    /// <summary>
    ///     Interaction logic for PollView.xaml
    /// </summary>
    public partial class PollView
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Poll), typeof(Poll),
                typeof(PollView), new FrameworkPropertyMetadata
                {
                    BindsTwoWayByDefault = true,
                    PropertyChangedCallback = PropertyChangedCallback
                });

        public PollView()
        {
            InitializeComponent();
        }

        public Poll Poll
        {
            get => (Poll) GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        private static void PropertyChangedCallback(DependencyObject dependency,
            DependencyPropertyChangedEventArgs args)
        {
            if (dependency is PollView view)
                if (view.DataContext is PollViewModel model)
                    model.Poll = view.Poll;
        }
    }
}