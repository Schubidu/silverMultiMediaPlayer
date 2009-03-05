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

namespace MultiMediaPlayerApp.MediaData
{
	/// <summary>
	/// represents a picture
	/// </summary>
	public sealed class PictureItem : MediaItem
	{
		/// <summary>
		/// PictureItem: represents a picture
		/// </summary>
		/// <param name="element">Element of XML</param>
		public PictureItem(System.Xml.Linq.XElement element)
		{
			itemType = MediaType.Picture;
			FillMediaItem(element);
			PictureUrl = GetString(element.Element("picture"), "href");
		}
		/// <summary>
		/// Url to picture
		/// </summary>
		public string PictureUrl { get; set; }
		/// <summary>
		/// Picture has duration and is possible to play it
		/// </summary>
		public bool IsPlayable { get { return (Duration != TimeSpan.Zero); } }

	}
}
