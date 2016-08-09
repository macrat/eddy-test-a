using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;

using Newtonsoft.Json;
using Xamarin.Forms;


namespace tea {
	public partial class TeaPage: ContentPage {
		 public EventHandler< IEnumerable<Eddystone> > onFoundEddystone;

		public TeaPage() {
			InitializeComponent();

			listView.ItemTemplate = new DataTemplate(typeof(ImageCell));
			listView.ItemTemplate.SetBinding(ImageCell.TextProperty, "Name");
			listView.ItemTemplate.SetBinding(ImageCell.DetailProperty, "Detail");
			listView.ItemTemplate.SetBinding(ImageCell.ImageSourceProperty, "Image");

			listView.ItemTapped += async (sender, e) => {
				((ListView)sender).SelectedItem = null;
				var info = ((Cell)e.Item).Info;
				if(info != null) {
					await Navigation.PushAsync(new DetailPage(((Cell)e.Item).Info));
				}
			};

			var listItems = new ObservableCollection<Cell>();
			listView.ItemsSource = listItems;

			onFoundEddystone += (sender, e) => {
				var tmp = new ObservableCollection<Cell>();

				foreach(var x in e.OrderBy(x => x.Proximity)){
					try {
						var cell = listItems.Where(y => x.Url == y.Eddy.Url).First();
						cell.Eddy = x;
						tmp.Add(cell);
					} catch(InvalidOperationException) {
						tmp.Add(new Cell(x));
					}
				}

				listView.ItemsSource = listItems = tmp;
			};
		}


		private class Cell: ViewCell {
			public Eddystone Eddy;

			public InfoModel Info {
				get;
				private set;
			}

			public string Name {
				get;
				private set;
			}

			public string Detail {
				get;
				private set;
			}

			public ImageSource Image {
				get;
				private set;
			}

			public Cell(Eddystone e) {
				Eddy = e;
				Name = "loading...";

				Download();
			}

			private async void Download() {
				using(var client = new HttpClient()) {
					client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
					Info = JsonConvert.DeserializeObject<InfoModel>(await client.GetStringAsync(Eddy.Url));
					Image = ImageSource.FromUri(new Uri(Eddy.Url, Info.ThumbnailUrl));
					Name = Info.Name;
					Detail = Info.Title;
				}
			}
		}
	}
}

