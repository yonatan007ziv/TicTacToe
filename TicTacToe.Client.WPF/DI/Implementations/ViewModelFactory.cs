using Microsoft.Extensions.DependencyInjection;
using System;
using TicTacToe.Client.WPF.DI.Interfaces;
using TicTacToe.Client.WPF.MVVM.Core.Base;

namespace TicTacToe.Client.WPF.DI.Implementations;

class ViewModelFactory : IViewModelFactory
{
	private readonly IServiceProvider serviceProvider;

	public ViewModelFactory(IServiceProvider serviceProvider)
	{
		this.serviceProvider = serviceProvider;
	}

	public BaseViewModel CreateViewModel<T>() where T : BaseViewModel
	{
		return (BaseViewModel)serviceProvider.GetRequiredService(typeof(T));
	}
}