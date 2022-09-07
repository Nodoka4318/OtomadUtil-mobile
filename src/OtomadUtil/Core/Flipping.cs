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
			double div;
			var tokens = score.Tokenize();
			
			for (int i = 0; i < tokens.Count; i++) {
				var t = tokens[i];
				var temp = new List<int>();
				div = (double)range / t.actualFrameLength;
				
				if (t.type == TokenType.S) {
					if (findex.Count <= 0)
						throw new InvalidOperationException("do not start notes from -");
					
					var aframe = temp.Last();
					for (double d = min; Math.Floor(d) <= max; d += div) {
						temp.Add(aframe);
					}
					
					continue;
				}
				
				if (t.type == TokenType.L)
					div = -div;
					
				for (double d = min; Math.Floor(d) <= max; d += Math.Abs(div)) {
					var lframe = temp.Count <= 0 ? findex.Last() : temp.Last();
					temp.Add((int)Math.Round((double)lframe + div));
				}
				
				findex.AddRange(temp);
			}
			
			return findex.ToArray();
		}
	}
}