﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace tea {
	public partial class DetailPage: ContentPage {
		private InfoModel info;

		public DetailPage(InfoModel i) {
			info = i;

			InitializeComponent();

			Title = i.Name;
			Image.Source = ImageSource.FromUri(new Uri(new Uri("http://yaya:5000"), i.ImageUrl));
			NameLabel.Text = i.Title;
			DetailLabel.Text = i.Description;
		}

		public void OpenBrowser(object sender, System.EventArgs e) {
			Device.OpenUri(new Uri(new Uri("http://yaya:5000"), info.Path));
		}
	}
}

