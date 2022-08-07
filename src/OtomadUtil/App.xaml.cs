using System;
using System.IO;
using Xamarin.Forms;

namespace OtomadUtil {
	public partial class App : Application {
		public App() {
			InitializeComponent();
			MainPage = new NavigationPage(new MainPage()) {
				
			};
		}
	}
}