namespace BookStoreP4.Commands {
    public class CloseAppCommand : CommandBase {
        public override void Execute(object? parameter) {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
