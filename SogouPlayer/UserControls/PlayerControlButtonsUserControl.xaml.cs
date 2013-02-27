using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace SogouPlayer.UserControls
{
    /// <summary>
    /// Interaction logic for PlayerButtonsUserControl.xaml
    /// </summary>
    public partial class PlayerControlButtonsUserControl : UserControl
    {
        #region Constructor
        public PlayerControlButtonsUserControl()
        {
            InitializeComponent();
        }
        #endregion

        #region Dependency Property
        public bool IsPaused
        {
            get { return (bool)GetValue(IsPausedProperty); }
            set { SetValue(IsPausedProperty, value); }
        }
        public static DependencyProperty IsPausedProperty = DependencyProperty.Register("IsPaused"
            , typeof(bool)
            , typeof(PlayerControlButtonsUserControl)
            , new PropertyMetadata(false, new PropertyChangedCallback((DependencyObject obj, DependencyPropertyChangedEventArgs args) =>
            {
                if (args != null && obj is PlayerControlButtonsUserControl)
                {
                    PlayerControlButtonsUserControl temp = obj as PlayerControlButtonsUserControl;
                    bool isPaused = (bool)args.NewValue;
                    if (isPaused)
                    {
                        temp.imagePlay.Source = new BitmapImage(new Uri("/SogouPlayer;component/Images/PauseButton_MouseEnter.png", UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
                        temp.imagePlay.Source = new BitmapImage(new Uri("/SogouPlayer;component/Images/PlayButton_MouseEnter.png", UriKind.RelativeOrAbsolute));
                    }
#if DEBUG
                    Debug.WriteLine("IsPaused: {0}", isPaused);
#endif
                }
            })));

        #endregion

        #region Event
        private void buttonPrevious_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            IsPaused = !IsPaused;
        }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {

        }

        private void imagePrevious_MouseEnter(object sender, MouseEventArgs e)
        {
            imagePrevious.Source = new BitmapImage(new Uri("/SogouPlayer;component/Images/PreviousButton_MouseEnter.png", UriKind.RelativeOrAbsolute));
        }

        private void imagePlay_MouseEnter(object sender, MouseEventArgs e)
        {
            if (IsPaused)
                imagePlay.Source = new BitmapImage(new Uri("/SogouPlayer;component/Images/PauseButton_MouseEnter.png", UriKind.RelativeOrAbsolute));
            else
                imagePlay.Source = new BitmapImage(new Uri("/SogouPlayer;component/Images/PlayButton_MouseEnter.png", UriKind.RelativeOrAbsolute));
        }

        private void imageNext_MouseEnter(object sender, MouseEventArgs e)
        {
            imageNext.Source = new BitmapImage(new Uri("/SogouPlayer;component/Images/NextButton_MouseEnter.png", UriKind.RelativeOrAbsolute));
        }

        private void imagePrevious_MouseLeave(object sender, MouseEventArgs e)
        {
            imagePrevious.Source = new BitmapImage(new Uri("/SogouPlayer;component/Images/PreviousButton_MouseLeave.png", UriKind.RelativeOrAbsolute));
        }

        private void imagePlay_MouseLeave(object sender, MouseEventArgs e)
        {
            if (IsPaused)
                imagePlay.Source = new BitmapImage(new Uri("/SogouPlayer;component/Images/PauseButton_MouseLeave.png", UriKind.RelativeOrAbsolute));
            else
                imagePlay.Source = new BitmapImage(new Uri("/SogouPlayer;component/Images/PlayButton_MouseLeave.png", UriKind.RelativeOrAbsolute));
        }

        private void imageNext_MouseLeave(object sender, MouseEventArgs e)
        {
            imageNext.Source = new BitmapImage(new Uri("/SogouPlayer;component/Images/NextButton_MouseLeave.png", UriKind.RelativeOrAbsolute));
        }
        #endregion

    }
}
