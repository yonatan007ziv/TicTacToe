using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using TicTacToe.Client.WPF.DI.Implementations;
using TicTacToe.Client.WPF.DI.Interfaces;
using TicTacToe.Client.WPF.MVVM.Core;
using TicTacToe.Client.WPF.MVVM.ViewModels;

namespace TicTacToe.Client.WPF.DI;

class ServiceRegisterer
{
	private ServiceCollection serviceCollection;

	public ServiceRegisterer(ServiceCollection serviceCollection)
	{
		this.serviceCollection = serviceCollection;
	}

	public IServiceProvider BuildProvider()
	{
		RegisterViewModels();

		serviceCollection.AddSingleton<Window, MainWindowView>(
			provider =>
				new MainWindowView() { DataContext = provider.GetRequiredService<MainWindowViewModel>() });

		serviceCollection.AddSingleton<IViewModelFactory, ViewModelFactory>(provider => new ViewModelFactory(provider));
		serviceCollection.AddSingleton<INavigationService, NavigationService>();

		return serviceCollection.BuildServiceProvider();
	}

	private void RegisterViewModels()
	{
		serviceCollection.AddSingleton<MainWindowViewModel>();
		serviceCollection.AddSingleton<MainMenuViewModel>();
		serviceCollection.AddSingleton<SingleplayerViewModel>();
		serviceCollection.AddSingleton<MultiplayerViewModel>();

		serviceCollection.AddSingleton<SingleplayerGameEasyViewModel>();
		serviceCollection.AddSingleton<SingleplayerGameMediumViewModel>();
		serviceCollection.AddSingleton<SingleplayerGameImpossibleViewModel>();
	}
}