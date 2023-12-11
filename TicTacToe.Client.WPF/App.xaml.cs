using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using TicTacToe.Client.WPF.DI;

namespace TicTacToe.Client.WPF;

public partial class App : Application
{
	private readonly IServiceProvider provider;
	public App()
	{
		provider = new ServiceRegisterer(new ServiceCollection()).BuildProvider();
	}

	private void Application_Startup(object sender, StartupEventArgs e)
	{
		provider.GetRequiredService<Window>().Show();
	}
}