using System;
using System.Linq;
using System.Collections.Generic;
using Android.Media;

namespace OtomadUtil.Media {
	public class VideoInfo {
		public string Path { get; set; }
		public int Bitrate { get; set; }
		public int FrameLength { get; set; }
		public int Height { get; set; }
		public int Width { get; set; }

		public static VideoInfo FromPath(string path) {
			VideoInfo info = new VideoInfo();

			var retriever = new MediaMetadataRetriever();
			retriever.SetDataSource(path);
			
			info.Path = path;
			info.Bitrate = int.Parse(retriever.ExtractMetadata(MetadataKey.Bitrate));
			info.FrameLength = int.Parse(retriever.ExtractMetadata(MetadataKey.VideoFrameCount));
			info.Height = int.Parse(retriever.ExtractMetadata(MetadataKey.VideoHeight));
			info.Width = int.Parse(retriever.ExtractMetadata(MetadataKey.VideoHeight));
			
			retriever.Dispose();
			return info;
		}
	}
}