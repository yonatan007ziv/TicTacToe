using System.Windows.Input;
using TicTacToe.Client.WPF.DI.Interfaces;
using TicTacToe.Client.WPF.MVVM.Core.Base;
using TicTacToe.Client.WPF.MVVM.Models;

namespace TicTacToe.Client.WPF.MVVM.ViewModels;

class SingleplayerViewModel : BaseViewModel
{
	private readonly SingleplayerModel model = new SingleplayerModel();
	private readonly INavigationService navigationService;

	public ICommand EasyCmd
	{
		get => model.EasyCmd;
		set
		{
			model.EasyCmd = value;
			OnPropertyChanged();
		}
	}
	public ICommand MediumCmd
	{
		get => model.MediumCmd;
		set
		{
			model.MediumCmd = value;
			OnPropertyChanged();
		}
	}
	public ICommand ImpossibleCmd
	{
		get => model.ImpossibleCmd;
		set
		{
			model.ImpossibleCmd = value;
			OnPropertyChanged();
		}
	}

    public SingleplayerViewModel(INavigationService navigationService)
    {
		this.navigationService = navigationService;
		EasyCmd = new RelayCommand(obj => EasyClick(), obj => true);
		MediumCmd = new RelayCommand(obj => MediumClick(), obj => true);
		ImpossibleCmd = new RelayCommand(obj => ImpossibleClick(), obj => true);
	}

	private void EasyClick()
	{
		navigationService.NavigateTo<SingleplayerGameEasyViewModel>();
	}
	private void MediumClick()
	{
		navigationService.NavigateTo<SingleplayerGameMediumViewModel>();
	}

	private void ImpossibleClick()
	{
		navigationService.NavigateTo<SingleplayerGameImpossibleViewModel>();
	}
}