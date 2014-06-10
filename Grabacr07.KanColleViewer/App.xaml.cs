﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Grabacr07.KanColleViewer.Models;
using Grabacr07.KanColleViewer.ViewModels;
using Grabacr07.KanColleViewer.Views;
using Grabacr07.KanColleWrapper;
using Livet;
using MetroRadiance;
using AppSettings = Grabacr07.KanColleViewer.Properties.Settings;
using Settings = Grabacr07.KanColleViewer.Models.Settings;

namespace Grabacr07.KanColleViewer
{
	public partial class App
	{
		public static ProductInfo ProductInfo { get; private set; }
		public static MainWindowViewModel ViewModelRoot { get; private set; }

		static App()
		{
			AppDomain.CurrentDomain.UnhandledException += (sender, args) => ReportException(sender, args.ExceptionObject as Exception);
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			this.DispatcherUnhandledException += (sender, args) => ReportException(sender, args.Exception);

			DispatcherHelper.UIDispatcher = this.Dispatcher;
			ProductInfo = new ProductInfo();

			Settings.Load();
			WindowsNotification.Notifier.Initialize();
			Helper.SetRegistryFeatureBrowserEmulation();

			KanColleClient.Current.Proxy.Startup(AppSettings.Default.LocalProxyPort);
			KanColleClient.Current.Proxy.UseProxyOnConnect = Settings.Current.EnableProxy;
			KanColleClient.Current.Proxy.UseProxyOnSSLConnect = Settings.Current.EnableSSLProxy;
			KanColleClient.Current.Proxy.UpstreamProxyHost = Settings.Current.ProxyHost;
			KanColleClient.Current.Proxy.UpstreamProxyPort = Settings.Current.ProxyPort;

			ResourceService.Current.ChangeCulture(Settings.Current.Culture);

			// Initialize translations
			KanColleClient.Current.Translations.EnableTranslations = Settings.Current.EnableTranslations;
			KanColleClient.Current.Translations.EnableAddUntranslated = Settings.Current.EnableAddUntranslated;
			KanColleClient.Current.Translations.ChangeCulture(Settings.Current.Culture);

			// Update notification and download new translations (if enabled)
			if (KanColleClient.Current.Updater.LoadVersion(AppSettings.Default.KCVUpdateUrl.AbsoluteUri))
			{
				if (Settings.Current.EnableUpdateNotification && KanColleClient.Current.Updater.IsOnlineVersionGreater(0, ProductInfo.Version.ToString()))
				{
					WindowsNotification.Notifier.Show(
						KanColleViewer.Properties.Resources.Updater_Notification_Title,
						string.Format(KanColleViewer.Properties.Resources.Updater_Notification_NewAppVersion, KanColleClient.Current.Updater.GetOnlineVersion(0)),
						() => Process.Start(KanColleClient.Current.Updater.GetOnlineVersion(0, true)));
				}

				if (Settings.Current.EnableUpdateTransOnStart)
				{
					if (KanColleClient.Current.Updater.UpdateTranslations(AppSettings.Default.XMLTransUrl.AbsoluteUri, Settings.Current.Culture, KanColleClient.Current.Translations) > 0)
					{
						WindowsNotification.Notifier.Show(
							KanColleViewer.Properties.Resources.Updater_Notification_Title,
							KanColleViewer.Properties.Resources.Updater_Notification_TransUpdate_Success,
							() => App.ViewModelRoot.Activate());

						KanColleClient.Current.Translations.ChangeCulture(Settings.Current.Culture);
					}
				}
			}

			ThemeService.Current.Initialize(this, Theme.Dark, Accent.Purple);
            
			ViewModelRoot = new MainWindowViewModel();
			this.MainWindow = new MainWindow { DataContext = ViewModelRoot };
			this.MainWindow.Show();
            
            if (Settings.Current.Orientation.Equals("Auto"))
            {
                SystemParameters.StaticPropertyChanged += SystemParameters_StaticPropertyChanged;
                updateMode();
            }
		}

		private void SystemParameters_StaticPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName.Equals("FullPrimaryScreenHeight") || e.PropertyName.Equals("FullPrimaryScreenWidth"))
			{
                updateMode();
			}
		}

        private void updateMode()
        {
            var window = System.Windows.Application.Current.MainWindow;
            if (SystemParameters.FullPrimaryScreenWidth >= SystemParameters.FullPrimaryScreenHeight)
            {
                Settings.Current.Orientation = "Horizontal";

                if (window != null && window.WindowState == System.Windows.WindowState.Normal)
                {
                    window.Height = 0;
					window.Width = 1440;
                }
            }
            else
            {
                Settings.Current.Orientation = "Vertical";

                if (window != null && window.WindowState == System.Windows.WindowState.Normal)
				{
					window.Width = 0;
					window.Height = 1000;
                }
            }
        }

		protected override void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);

			KanColleClient.Current.Proxy.Shutdown();

			WindowsNotification.Notifier.Dispose();
			Settings.Current.Save();
		}


		private static void ReportException(object sender, Exception exception)
		{
			#region const
			const string messageFormat = @"
===========================================================
ERROR, date = {0}, sender = {1},
{2}
";
			const string path = "error.log";
			#endregion

			try
			{
				var message = string.Format(messageFormat, DateTimeOffset.Now, sender, exception);

				Debug.WriteLine(message);
				File.AppendAllText(path, message);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
		}
	}
}
