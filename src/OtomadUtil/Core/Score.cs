using System;
using System.Linq;
using System.Collections.Generic;

namespace OtomadUtil.Core {
	public class Score {
		public List<char> Source { get; private set; }
		public int Length { get; private set; }
		public int Bpm { get; private set; }
		public int Fps { get; private set; }

		public Score(string source, int bpm, int fps) {
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
			this.Fps = fps;
		}



		public char GetChar(int index) {
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

		public static List<ScoreToken> Tokenize(string src, int bpm, int fps) {
			var score = new Score(src, bpm, fps);
			var tokens = new List<ScoreToken>();

			for (int i = 0; i < score.Length; i++) {
				var note = score.GetChar(i);

				int ext = 0; // how many times current note needs to be extended
				if (note != '-') {
					if (i + 1 < score.Length) {
						char hyc = score.GetChar(i + 1);
						while (hyc == '-') {
							ext++;
							if (i + ext + 1 < score.Length) {
								hyc = score.GetChar(i + ext + 1);
							} else {
								break;
							}
						}
					}
					i += ext;
				} else {
					throw new Exception($"invalid note has been read at {i}, val:{note.ToString()}");
				}

				var notelen = GetNoteLength(note) * ((double)ext + 1.0);
				var notetype = GetNoteType(note);
				var framelen = score.GetActualFrameLength(notelen);

				tokens.Add(new ScoreToken(notetype, framelen, notelen));
			}
			
			tokens.Reverse();
			return tokens;
		}
		
		public List<ScoreToken> Tokenize() {
			return Tokenize(new string(Source.ToArray()), Bpm, Fps); // ふつうじっそうぎゃくじゃね??
		}

		public double GetActualFrameLength(double notelen) {
			double framePerBeat = Fps * 60 / Bpm;
			return framePerBeat * notelen;
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

		public static TokenType GetNoteType(char c) {
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