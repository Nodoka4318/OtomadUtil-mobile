using System;
using System.Linq;
using System.Collections.Generic;
using Android.Media;
using Android.Graphics;
using OtomadUtil.Media.Extensions;

namespace OtomadUtil.Media {
	public class Video {
		public string Path => _path;
		public VideoInfo Info => _info;

		private Bitmap[] _bitmapArray;
		private bool _isBitmapArrayInitialized = false;
		private string _path;
		private int _currentFrameIndex;
		private VideoInfo _info;
		private MediaMetadataRetriever _retriever;
		private bool _caching;
		private ImageSource[] _imageSourceCache;
		
		
		//private ImageSource sr;
		
		
		public Video(string path, bool caching) {
			_info = VideoInfo.FromPath(path);
			_retriever = new MediaMetadataRetriever();
			_retriever.SetDataSource(path);
			var m = new MediaExtractor();
			m.SetDataSource(path);
			_caching = caching;

			if (_caching) {
				_imageSourceCache = new ImageSource[_info.FrameLength];
			}
			
			//sr = GetFrameAsImageSource(2, 100);
		}

		public Bitmap GetFrame(int index) {
			if (index >= _info.FrameLength || index < 0) {
				throw new InvalidOperationException($"given index(:{index}) is out of video frame length");
			}

			if (_isBitmapArrayInitialized) {
				return _bitmapArray[index];
			}

			return _retriever.GetFrameAtIndex(index);
		}

		public ImageSource GetFrameAsImageSource(int index, int quality) {
			
			if (_imageSourceCache[index] == null) {
				_imageSourceCache[index] = GetFrame(index).ToImageSource(quality);
			}
			
			return _imageSourceCache[index];
			
			//var a = GetFrame(0);
			//return sr;
		}

		public void SaveFrameAsPng(int index, string folder, string filename) {
			filename += ".png";

			Java.IO.File file = new Java.IO.File(folder, filename);

			//保存
			using (var os = new System.IO.FileStream(file.AbsolutePath, System.IO.FileMode.Create)) {
				this.GetFrame(index).Compress(Bitmap.CompressFormat.Png, 100, os);
			}

		}

		public void InitializeImageSourceArray() {
			_bitmapArray = new Bitmap[_info.FrameLength];
			for (int i = 0; i < _info.FrameLength; i++) {
				//_imageSourceCache[i] = GetFrameAsImageSource(i, 0);
			}

			//_isBitmapArrayInitialized = true;
		}
	}
}