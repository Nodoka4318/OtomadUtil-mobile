using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Essentials;
using Android.Graphics;

namespace OtomadUtil.Media.Extensions {
	public static class MediaExtensions {
		public static Stream ToStream(this Bitmap bmp, int quality) {
			var ms = new MemoryStream();
			bmp.Compress(Bitmap.CompressFormat.Png, quality, ms);
			return ms;
		}

		public static ImageSource ToImageSource(this Bitmap bmp, int quality) {
			return ImageSource.FromStream(() => {
				var ms = new MemoryStream();
				bmp.Compress(Bitmap.CompressFormat.Png, quality, ms);
				ms.Position = 0;
				return ms;
			});
		}
	}
}