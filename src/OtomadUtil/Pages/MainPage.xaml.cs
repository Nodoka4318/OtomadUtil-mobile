using System;
using System.IO;
using Xamarin.Forms;

namespace OtomadUtil {
	public partial class MainPage : ContentPage {
		public MainPage() {
			InitializeComponent();
			toolsButton.Text = "ツール";
			createVidButton.Text = "動画を作成";
			
			createVidButton.Clicked += createVidButton_Clicked;
			
			// test();
		}
		
		private void createVidButton_Clicked(object sender, EventArgs e) {
			Navigation.PushAsync(new EditPage(), true);
		}
		
		void test() {
			var score = "dbdbqb";
			var tokens = OtomadUtil.Core.Score.Tokenize(score, 120, 60);
			for (int i = 0; i < tokens.Count; i++) {
				DisplayAlert(score, tokens[i].beatLength.ToString(), "ok");
			}
		}
	}
}