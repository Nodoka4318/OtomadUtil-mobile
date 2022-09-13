using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Android.Graphics;
using OtomadUtil.Media;

namespace OtomadUtil {
	public partial class EditPage : ContentPage {
		private VideoState _video;
		private bool _isVideoInitialized = false;

		public EditPage() {
			InitializeComponent();
			preview.BackgroundColor = Xamarin.Forms.Color.Black;
			//preview.Source = "/storage/emulated/0/Download/70816243-AF2B-4716-A7F8-12E0CB18EF0E.png";

			// var filepick = PickVideoFile();
			
			pv_slider.MaximumTrackColor = Xamarin.Forms.Color.Gray;
			pv_slider.MinimumTrackColor = Xamarin.Forms.Color.Brown;
			pv_slider.ThumbColor = Xamarin.Forms.Color.Gray;
			pv_slider.IsEnabled = false;
			filePick_Button.Clicked += filePick_Button_Clicked;
			frameLabel.Text = "- / -";

			// events
			pv_slider.ValueChanged += Pv_Slider_ValueChanged;
		}

		private async void Pv_Slider_ValueChanged(object sender, EventArgs e) {
			if (_isVideoInitialized) {
				//preview.Source = _video.GetFrameAsImageSource((int)pv_slider.Value, 0);
				var task = _video.UpdateFrame((int)pv_slider.Value);
				frameLabel.Text = $"{(int)pv_slider.Value} / {_video.Source.Info.FrameLength}";
				if (task.IsCompletedSuccessfully) {
					preview.Source = _video.CurrentState;
				}
			}
		}

		private void filePick_Button_Clicked(object sender, EventArgs e) {
			var file = PickVideoFile();
		}

		private async Task<FileResult> PickVideoFile() {
			var options = new PickOptions {
				FileTypes = FilePickerFileType.Videos,
			};

			try {
				var filepick = await FilePicker.PickAsync(options);

				var vi = VideoInfo.FromPath(filepick.FullPath);
				//DisplayAlert("h", vi.Height.ToString(), "ok");
				InitVideo(filepick.FullPath);

				return filepick;
			} catch (Exception ex) {
				DisplayAlert("error", ex.ToString(), "abort");
				return null;
			}
		}

		private void InitVideo(string path) {
			_video = new VideoState(new Video(path, true));
			preview.Source = _video.CurrentState;
			//_video.InitializeImageSourceArray();
			//preview.Source = _video.GetFrameAsImageSource(0, 100);
			pv_slider.Value = 0;
			pv_slider.Minimum = 0;
			pv_slider.Maximum = _video.Source.Info.FrameLength - 1;
			pv_slider.IsEnabled = true;
			_isVideoInitialized = true;
			frameLabel.Text = $"1 / {_video.Source.Info.FrameLength}";
		}
	}
}