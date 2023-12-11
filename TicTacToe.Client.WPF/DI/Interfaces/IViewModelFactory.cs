using TicTacToe.Client.WPF.MVVM.Core.Base;

namespace TicTacToe.Client.WPF.DI.Interfaces;

interface IViewModelFactory
{
	BaseViewModel CreateViewModel<T>() where T : BaseViewModel;
}