using System;

namespace tea {
	public class Eddystone {
		public int Rssi;
		public int TxPower;
		public Uri Url;

		public double Proximity {
			get {
				return Math.Pow(10.0, (TxPower - Rssi) / 20.0);
			}
		}
	}
}
