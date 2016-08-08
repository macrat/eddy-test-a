using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using CoreBluetooth;

using Estimote;


namespace tea.iOS
{
	public class Application: CBCentralManagerDelegate
	{
		// This is the main entry point of the application.
		static void Main(string[] args)
		{
			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main(args, null, "AppDelegate");

			var manager = new CBCentralManager();
			manager.ScanForPeripherals(CBUUID.FromString("FEAA"));
		}

		public override void UpdatedState(CBCentralManager central) {
			Console.WriteLine("updated: " + central);
		}

		public override void DiscoveredPeripheral(CBCentralManager central, CBPeripheral peripheral, NSDictionary advertisementData, NSNumber RSSI) {
			Console.WriteLine("discovered: " + advertisementData);
		}
	}
}

