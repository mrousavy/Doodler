using Doodler.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Doodler.Views
{
    /// <summary>
    /// Interaction logic for PollsView.xaml
    /// </summary>
    public partial class PollsView : UserControl
    {
        public PollsView()
        {
            InitializeComponent();
        }

        private void ScrollViewerSizeChanged(object sender, SizeChangedEventArgs e)
        {
            double width = ScrollViewer.ActualWidth;
            if (DataContext is PollsViewModel model)
                model.TileColumns = (int) width / 300; // 154 is a Tile's width
        }
    }
}
