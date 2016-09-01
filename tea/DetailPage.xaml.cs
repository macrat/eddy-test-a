using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace tea {
	public partial class DetailPage: ContentPage {
		private InfoModel info;

		public DetailPage(InfoModel i) {
			info = i;

			InitializeComponent();

			Title = i.Name;
			Image.Source = ImageSource.FromUri(new Uri(new Uri("http://i.crat.jp"), i.ImageUrl));
			NameLabel.Text = i.Title;
			DetailLabel.Text = i.Description.Replace("<br>\n", "\n").Replace("<br>", "\n");
		}

		public void OpenBrowser(object sender, System.EventArgs e) {
			Device.OpenUri(new Uri(new Uri("http://i.crat.jp"), info.Path));
		}
	}
}

