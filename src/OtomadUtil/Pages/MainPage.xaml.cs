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
		}
		
		private void createVidButton_Clicked(object sender, EventArgs e) {
			Navigation.PushAsync(new EditPage(), true);
		}
	}
}