using TicTacToe.Client.WPF.DI.Interfaces;
using TicTacToe.Client.WPF.MVVM.Core.Base;
using TicTacToe.Client.WPF.MVVM.ViewModels;

namespace TicTacToe.Client.WPF.MVVM.Core;

class MainWindowViewModel : BaseViewModel
{
	private INavigationService _navigationService;
	public INavigationService NavigationService
	{
		get => _navigationService;
		set
		{
			_navigationService = value;
			OnPropertyChanged();
		}
	}

	public MainWindowViewModel(INavigationService navigationService)
	{
		_navigationService = navigationService;
		NavigationService.NavigateTo<MainMenuViewModel>();
	}
}