namespace BookStoreP4.Commands {
    public class ShowAuthorModalCommand : CommandBase {
        public override void Execute(object? parameter) {
            System.Windows.MessageBox.Show("Marcel Gańczarczyk", "Projekt na Programowanie IV", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }
    }
}
