using TicTacToe.Client.WPF.MVVM.Core.Base;

namespace TicTacToe.Client.WPF.DI.Interfaces;

interface INavigationService
{
	void NavigateTo<T>() where T : BaseViewModel;
}