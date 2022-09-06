using System;
using System.Linq;
using System.Collections.Generic;

namespace OtomadUtil.Core {
	public class ScoreToken {
		public TokenType type;
		public double actualFrameLength;
		public double beatLength;
		
		public ScoreToken(TokenType tt, double af, double bl) {
			type = tt;
			actualFrameLength = af;
			beatLength = bl;
		}
	}
	
	public enum TokenType {
		R, // +
		L, // -
		S, // halt
	}
}