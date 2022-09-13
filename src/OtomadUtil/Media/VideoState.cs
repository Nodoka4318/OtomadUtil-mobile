using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OtomadUtil.Media {
	public class VideoState {
		public ImageSource CurrentState => _state;
		public Video Source => _video;
		
		private ImageSource _state;
		private Video _video;
		private ImageSource[] _frames;
		private bool _caching;
		
		public VideoState(Video video) {
			_video = video;
			//_caching = caching;
			_frames = new ImageSource[video.Info.FrameLength];
			this.UpdateFrame(0);
		}
		
		public async Task UpdateFrame(int index) {
			if (index < 0 || index > _video.Info.FrameLength)
				throw new InvalidOperationException($"given index(:{index}) is out of video frame length");
			
			if (_frames[index] != null) {
				_state = _frames[index];
				return;
			}
			
			_frames[index] = _video.GetFrameAsImageSource(index, 0);
			_state = _frames[index];
		}
	}
}