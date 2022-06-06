using System.Windows;

namespace BookStoreP4.Commands {
    public class MinimizeAppCommand : CommandBase {
        public override void Execute(object? parameter) {
            Application.Current.Windows[0].WindowState = WindowState.Minimized;
        }
    }
}
