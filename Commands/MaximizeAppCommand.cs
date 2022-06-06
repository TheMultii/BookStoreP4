using System.Windows;

namespace BookStoreP4.Commands {
    public class MaximizeAppCommand : CommandBase {
        public override void Execute(object? parameter) {
            if (Application.Current.MainWindow.WindowState == WindowState.Maximized) {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            } else {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
        }
    }
}
