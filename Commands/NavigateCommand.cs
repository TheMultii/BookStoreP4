using BookStoreP4.Services;

namespace BookStoreP4.Commands {
    public class NavigateCommand : CommandBase {

        private readonly NavigationService _navigationSerivce;

        public NavigateCommand(NavigationService navigationService) {
            _navigationSerivce = navigationService;
        }

        public override void Execute(object? parameter) {
            _navigationSerivce.Navigate();
        }
    }
}
