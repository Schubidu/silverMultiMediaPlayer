using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Browser;
using System.Xml.Linq;
using System.ComponentModel;

namespace MultiMediaPlayer.MediaData
{
	public class MediaCollection : INotifyPropertyChanged
	{
		//private string MediaPathId = "MediaXMLPath";

		// Bindable Collection
		public ObservableCollection<MediaItem> MediaItems { get; set; }

		private Uri mediaXml;
		public Uri MediaXml {
			get {
				return mediaXml;
			}
			set {
				mediaXml = value;
				if (HtmlPage.IsEnabled)
					LoadMediaXML(mediaXml);
			}
		}

		public MediaCollection()
		{
		}

		private void LoadMediaXML(Uri mediaXml)
		{
			// Use a normal webclient to fetch the feed
			var wc = new WebClient();
			// New lambda style delegate declaration
			wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
			wc.DownloadStringAsync(mediaXml);
		}

		void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
		{
			Object s = sender;
			try
			{
				var doc = XDocument.Parse(e.Result);
				var items = doc.Descendants().First().Elements();
				MediaItems = new ObservableCollection<MediaItem>();
				foreach (var item in items)
				{
					MediaItem mItem = new MediaItem();
					switch (item.Name.ToString().ToLower())
					{
						case "video":
							mItem = new VideoItem(item);
							break;
						case "audio":
							mItem = new AudioItem(item);
							break;
						case "picture":
							mItem = new PictureItem(item);
							break;
					}
					mItem.IsFirst = (items.First().Equals(item));
					mItem.IsLast = (items.Last().Equals(item));
					MediaItems.Add(mItem);
				}
				if(PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("MediaItems"));
			}
			catch (Exception ex)
			{
				//HtmlPage.Window.Alert(ex.Message);
			};
		}

		public event PropertyChangedEventHandler PropertyChanged;

		#region XMLHelper
		/// <summary>
		/// Helps to get XMLPath
		/// </summary>
		/// <returns>Uri to XML</returns>
		Uri GetXML()
		{
			return new Uri("/media.xml", UriKind.RelativeOrAbsolute);
			//try
			//{
			//    HtmlDocument htmlDoc;
			//    htmlDoc = HtmlPage.Document;
			//    var elemMediagalleryPath = htmlDoc.GetElementById(MediaPathId);
			//    string path = (elemMediagalleryPath != null) ? elemMediagalleryPath.GetAttribute("href") : HtmlPage.Plugin.GetProperty("initParams").ToString();
			//    if (path == String.Empty) throw new XmlUrlNotFoundException();
			//    return new Uri(path, UriKind.RelativeOrAbsolute);
			//}
			//catch (XmlUrlNotFoundException e) {
			//    HtmlPage.Window.Alert(e.Message);
			//    return null;
			//}
		}
		#endregion
	}
}
