using System;
using System.Linq;
using System.Collections.Generic;

namespace OtomadUtil.Core {
	public class ScoreToken {
		public TokenType type;
		public int actualFrameLength;
		public int beatLength;
		
		public ScoreToken(TokenType tt, int af, int bl) {
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