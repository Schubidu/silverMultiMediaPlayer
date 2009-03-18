using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MultiMediaPlayer
{
	public class VideoControl : MediaControl
	{
		public VideoControl() {
			ItemType = MediaData.MediaType.Video;
		}

		public override void SetData(Object obj)
		{
			MediaData.VideoItem item = obj as MediaData.VideoItem;
			MediaUrl = item.MediaUrl;
			base.SetData(obj);
		}

		#region MediaUrl (DependencyProperty)

		/// <summary>
		/// A description of the property.
		/// </summary>
		public Uri MediaUrl
		{
			get { return (Uri)GetValue(MediaUrlProperty); }
			set { SetValue(MediaUrlProperty, value); }
		}
		public static readonly DependencyProperty MediaUrlProperty =
			DependencyProperty.Register("MediaUrl", typeof(Uri), typeof(VideoControl),
			  new PropertyMetadata(null));

		#endregion

	}
}
