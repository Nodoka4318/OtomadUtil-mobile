using System;
using System.Linq;
using System.Collections.Generic;
using OtomadUtil.Media;

namespace OtomadUtil.Core {
	public class Flipping {
		private VideoInfo _vinfo;
		private Score _score;
		
		Flipping(Video video, Score score) {
			_vinfo = video.Info;
			_score = score;
		}
		
		/// <summary>
		/// returns an array of frame indexes
		/// </summary>
		public static int[] Flip(VideoInfo vinfo, Score score, int min, int max) {
			var findex = new List<int>();
			
			if (min < 0 || max > vinfo.FrameLength) {
				throw new InvalidOperationException("invalid range has been given");
			}
			
			int range = max - min + 1;
			double div = (double)range / (double)score.Fps; // TODO
			var tokens = score.Tokenize();
			
			for (int i = 0; i < tokens.Count; i++) {
				var t = tokens[i];
				// TODO
			}
			
			return findex.ToArray();
		}
	}
}