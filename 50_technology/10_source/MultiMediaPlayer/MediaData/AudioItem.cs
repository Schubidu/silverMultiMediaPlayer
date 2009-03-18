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
	/// represents an audio
	/// </summary>
	public sealed class AudioItem : MediaItem
	{
		/// <summary>
		/// AudioItem: represents an audio
		/// </summary>
		/// <param name="element">Element of XML</param>
		public AudioItem(System.Xml.Linq.XElement element)
		{
			itemType = MediaType.Audio;
			FillMediaItem(element);
			MediaUrl = GetUri(element.Element("media"), "href");
			PictureUrl = GetString(element.Element("picture"), "href");
		}
		/// <summary>
		/// Url to mediafile
		/// </summary>
		public Uri MediaUrl { get; set; }
		/// <summary>
		/// Url to picture
		/// </summary>
		public string PictureUrl { get; set; }
	}
}
