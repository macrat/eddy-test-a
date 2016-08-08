using System;
using System.Linq;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using EstimoteSdk;


namespace tea.Droid
{
	[Activity(Label = "tea.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, BeaconManager.IServiceReadyCallback
	{
		private BeaconManager beaconManager;
		private string scanID = null;
		private App app;

		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			app = new App();
			LoadApplication(app);
		}

		protected override void OnStart() {
			base.OnStart();

			if(Android.Bluetooth.BluetoothAdapter.DefaultAdapter == null) {
				Toast.MakeText(this, "Not found bluetooth", ToastLength.Long).Show();
			} else {
				if(!Android.Bluetooth.BluetoothAdapter.DefaultAdapter.IsEnabled) {
					Android.Bluetooth.BluetoothAdapter.DefaultAdapter.Enable();
				}

				beaconManager = new BeaconManager(this);
				beaconManager.Eddystone += (sender, e) => app.onFoundEddystone(
						sender,
						from x in e.Eddystones
						where x.IsUrl
						select new Eddystone { Rssi = x.Rssi, TxPower = x.CalibratedTxPower, Url = new Uri(x.Url) });
				beaconManager.Connect(this);
			}
		}

		protected override void OnStop() {
			base.OnStop();

			if(scanID != null) {
				beaconManager.StopEddystoneScanning(scanID);
				scanID = null;
			}
		}

		public void OnServiceReady() {
			scanID = beaconManager.StartEddystoneScanning();
		}

		protected override void OnDestroy() {
			base.OnDestroy();
			beaconManager.Disconnect();
		}
	}
}
