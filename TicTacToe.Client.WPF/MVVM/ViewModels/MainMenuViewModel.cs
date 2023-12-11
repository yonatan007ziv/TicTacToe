using System.Windows.Input;
using TicTacToe.Client.WPF.DI.Interfaces;
using TicTacToe.Client.WPF.MVVM.Core.Base;

namespace TicTacToe.Client.WPF.MVVM.ViewModels;

class MainMenuViewModel : BaseViewModel
{
	private readonly INavigationService navigationService;

	private ICommand singleplayerCmd;
	public ICommand SingleplayerCmd
	{
		get => singleplayerCmd;
		set
		{
			singleplayerCmd = value;
			OnPropertyChanged();
		}
	}

	private ICommand multiplayerCmd;
	public ICommand MultiplayerCmd
	{
		get => multiplayerCmd;
		set
		{
			multiplayerCmd = value;
			OnPropertyChanged();
		}
	}

	public MainMenuViewModel(INavigationService navigationService)
	{
		this.navigationService = navigationService;

		singleplayerCmd = new RelayCommand(obj => singleplayerClick(), obj => true);
		multiplayerCmd = new RelayCommand(obj => multiplayerClick(), obj => true);
	}

	private void singleplayerClick()
	{
		navigationService.NavigateTo<SingleplayerViewModel>();
	}

	private void multiplayerClick()
	{
		navigationService.NavigateTo<MultiplayerViewModel>();
	}
}