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

namespace MultiMediaPlayer.MediaData
{
	/// <summary>
	/// represents a video
	/// </summary>
	public sealed class VideoItem : MediaItem
	{
		/// <summary>
		/// VideoItem: represents a video
		/// </summary>
		/// <param name="element">Element of XML</param>
		public VideoItem(System.Xml.Linq.XElement element)
		{ 
			itemType = MediaType.Video;
			FillMediaItem(element);
			MediaUrl = GetUri(element.Element("media"), "href");
		}

		/// <summary>
		/// Url to mediafile
		/// </summary>
		public Uri MediaUrl { get; set; }
	}
}
