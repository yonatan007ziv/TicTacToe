using TicTacToe.Client.WPF.DI.Interfaces;
using TicTacToe.Client.WPF.MVVM.Core.Base;

namespace TicTacToe.Client.WPF.DI.Implementations;

class NavigationService : BaseViewModel, INavigationService
{
	private readonly IViewModelFactory viewModelFactory;

	private BaseViewModel currentView = null!;
	public BaseViewModel CurrentView
	{
		get => currentView;
		set
		{
			currentView = value;
			OnPropertyChanged();
		}
	}

	public NavigationService(IViewModelFactory viewModelFactory)
	{
		this.viewModelFactory = viewModelFactory;
	}

	public void NavigateTo<T>() where T : BaseViewModel
	{
		CurrentView = viewModelFactory.CreateViewModel<T>();
	}
}