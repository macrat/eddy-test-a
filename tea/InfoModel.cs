using System;

using Newtonsoft.Json;


namespace tea {
	[JsonObject("InfoModel")]
	public class InfoModel {
		[JsonProperty("name")]
		public string Name;

		[JsonProperty("title")]
		public string Title;

		[JsonProperty("description")]
		public string Description;

		[JsonProperty("path")]
		public string Path;

		[JsonProperty("image")]
		public string ImageUrl;

		[JsonProperty("thumbnail")]
		public string ThumbnailUrl;
	}
}

