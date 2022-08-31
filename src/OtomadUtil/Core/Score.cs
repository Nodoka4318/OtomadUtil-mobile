using System;
using System.Linq;
using System.Collections.Generic;

namespace OtomadUtil.Core {
	public class Score {
		public List<char> Source { get; private set; }
		public int Length { get; private set; }
		public int Bpm { get; private set; }

		public Score(string source, int bpm) {
			bool isComment = false;
			var _tempLits = new List<char>();
			//コメントアウト飛ばし読み 
			for (int i = 0; i < source.Length; i++) {
				var c = source[i];
				if (isComment) {
					if (c == ')')
						isComment = false;
					else if (c == '(')
						throw new Exception($"コメント中に丸括弧「()」を含めることはできません。\nindex: {i}");
					continue;
				}

				if (c == '(') {
					isComment = true;
					continue;
				} else if (c == ')') {
					throw new Exception($"コメントの始まり「(」がありません。\nindex: {i}");
				}

				_tempLits.Add(c);
			}

			if (isComment)
				throw new Exception($"コメントの終わり「)」がありません。\nindex: {source.Length - 1}");

			this.Source = _tempLits.Where(
				c => AllowedToken.Contains(c)
			).ToList();
			this.Length = this.Source.Count;
			this.Bpm = bpm;
		}



		public char? GetChar(int index) {
			if (index > Length || index < 0)
				throw new InvalidOperationException($"failed in getting char at {index}");
			return Source[index];
		}

		public static List<char> AllowedToken {
			get {
				var _temp = new List<char>();
				_temp.Add('o'); //全音符 
				_temp.Add('c'); //全音符逆 
				_temp.Add('d'); //四分音符 
				_temp.Add('b'); //四分音符逆 
				_temp.Add('q'); //八分音符 
				_temp.Add('p'); //八分音符逆 
				_temp.Add('-'); //伸ばし 
				_temp.Add('_'); //全休符 
				_temp.Add('s'); //四分休符 
				_temp.Add('r'); //八分休符 
				return _temp;
			}
		}
		
		public static List<ScoreToken> Tokenize(string src, int bpm) {
			var score = new Score(src, bpm);
			var tokens = new List<ScoreToken>();
			
			for (int i = 0; i < score.Length; i++) {
				var note = score.GetChar(i);
				
				// TODO
			}
			
			return tokens;
		}
		
		public static double GetNoteLength(char c) {
			double l;
			switch (c) {
				case 'd':
				case 'b':
				case 's':
					l = 1.0; break;
				case 'q':
				case 'p':
				case 'r':
					l = 0.5; break;
				case 'o':
				case 'c':
				case '_':
					l = 4.0; break;
				default:
					throw new InvalidOperationException("invalid note given");
			}
			return l;
		}
		
		public static double GetNoteType(char c) {
			TokenType tt;
			switch (c) {
				case 'd':
				case 'q':
				case 'o':
					tt = TokenType.R; break;
				case 'b':
				case 'p':
				case 'c':
					tt = TokenType.L; break;
				case 's':
				case 'r':
				case '_':
					tt = TokenType.S; break;
				default:
					throw new InvalidOperationException("invalid note given");
			}
			return tt;
		}
	}
}