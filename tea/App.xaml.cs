using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace tea
{
	public partial class App : Application
	{
		public EventHandler< IEnumerable<Eddystone> > onFoundEddystone;

		public App()
		{
			InitializeComponent();

			var page = new TeaPage();
			MainPage = new NavigationPage(page);

			onFoundEddystone += (sender, e) => page.onFoundEddystone(sender, e);
		}
	}
}
