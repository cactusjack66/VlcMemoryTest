using LibVLCSharp.Shared;
using System.Windows;

namespace VlcMemoryTest
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private MediaPlayer? m_MediaPlayer;

		public MainWindow()
		{
			InitializeComponent();
			Initialize();
		}

		public void Initialize()
		{
			var arguments = new List<string>
			{
				// un-comment this line and memory consumption will rise
				// $"--video-filter=croppadd{{cropleft=200,croptop=100,cropright=200,cropbottom=100}}"
			};

			var rtspArguments = new MediaConfiguration() { EnableHardwareDecoding = true }.Build();

			var libVlc = new LibVLC(arguments.ToArray());
			m_MediaPlayer = new MediaPlayer(libVlc);
			var m_MediaStream = new Media(libVlc, $"file_example_MP4_640_3MG.mp4", FromType.FromPath, rtspArguments);
			m_MediaPlayer.Media = m_MediaStream;
			VideoView.MediaPlayer = m_MediaPlayer;
			m_MediaStream.Dispose();
		}

		private void PlayButton_Click(object sender, RoutedEventArgs e)
		{
			if (m_MediaPlayer != null)
				ThreadPool.QueueUserWorkItem(_ => m_MediaPlayer.Play());
		}

		private void StopButton_Click(object sender, RoutedEventArgs e)
		{
			if (m_MediaPlayer != null)
				ThreadPool.QueueUserWorkItem(_ => m_MediaPlayer.Stop());
		}
	}
}