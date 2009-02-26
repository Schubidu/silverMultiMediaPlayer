using System;
using System.Net;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace MultiMediaPlayer.MediaData
{
	/// <summary>
	/// represents a MediaItem
	/// </summary>
	public class MediaItem
	{
		public MediaItem() { }

		/// <summary>
		/// Kind of Content
		/// </summary>
		public ContentType Content { get; set; }
		/// <summary>
		/// Duration of MediaItem
		/// </summary>
		public TimeSpan Duration { get; set; }
		/// <summary>
		/// use duration from xml or use naturelduration of video
		/// </summary>
		public bool UseDuration { get; set; }
		/// <summary>
		/// Url to previewthumb
		/// </summary>
		public string ThumbUrl { get; set; }
		/// <summary>
		/// Skipping is possibility
		/// </summary>
		public bool IsSkippable { get; set; }
		/// <summary>
		/// Skip possible after duration of seconds
		/// </summary>
		public TimeSpan SkipAfter { get; set; }
		/// <summary>
		/// IsLast in list
		/// </summary>
		public bool IsLast { get; set; }
		/// <summary>
		/// IsFirst in list
		/// </summary>
		public bool IsFirst { get; set; }
		/// <summary>
		/// Such meta
		/// </summary>
		public Meta Metadata { get; set; }
		/// <summary>
		/// Markers for undertitle
		/// </summary>
		public List<Marker> Markers { get; set; }
		/// <summary>
		/// Such trackingdata
		/// </summary>
		public Tracking TrackingData { get; set; }

		protected MediaType itemType = MediaType.Unknown;
		/// <summary>
		/// Kind of MediaItem
		/// </summary>
		public MediaType ItemType { get { return itemType;} }

		//public Uri MediaUrl { get; set; }
		//public string PictureUrl { get; set; }
		//public bool IsPlayable { get { return (Duration.Ticks > 0); } }

		/// <summary>
		/// Fill MediaItem with Values
		/// </summary>
		/// <param name="element">Element from XML</param>
		protected void FillMediaItem(System.Xml.Linq.XElement element) {
			Content = GetContentType((string)element.Attribute("content"));
			IsSkippable = GetIsSkippable((string)element.Element("skippable"));
			if (IsSkippable)
				SkipAfter = GetDuration(element.Element("skippable"), "after");
			if (element.Element("duration") != null)
				Duration = GetDuration((string)element.Element("duration"));
			//MediaUrl = GetUri(mItem.Element("media"), "href");
			ThumbUrl = GetString(element.Element("thumb"), "href");
			//PictureUrl = GetString(mItem.Element("picture"), "href"),;

			if (element.Element("meta") != null)
			{
				XElement meta = element.Element("meta");
				Metadata = new Meta
				{
					Title = GetString(meta.Element("title")),
					Description = GetString(meta.Element("description")),
					PublishDate = GetPuplishDate(GetString(meta.Element("publishdate"))),
				};
			}

			if (element.Element("markers") != null)
			{
				XElement markers = element.Element("markers");
				var m = from marker in markers.Descendants("marker")
						select new Marker()
						{
							Name = GetString(marker.Attribute("name")),
							Text = GetString(marker),
							Start = GetDuration(GetString(marker.Attribute("start"))),
							End = GetDuration(GetString(marker.Attribute("end"))),
						};
				Markers = m.ToList<Marker>();
			}

			if (element.Element("tracking") != null)
			{
				XElement tracking = element.Element("tracking");
				TrackingData = GetTracking(tracking);
			}
		}


		/// <summary>
		/// Helper to get enum ContentType
		/// </summary>
		/// <param name="str">ContentType</param>
		/// <returns></returns>
		private ContentType GetContentType(string str)
		{
			switch (str.ToLower())
			{
				case ("content"): return ContentType.Content;
				case ("ad"): return ContentType.Ad;
				default: return ContentType.Unknown;
			}
		}

		/// <summary>
		/// Helper to get bool IsSkippable
		/// </summary>
		/// <param name="str">bool as string</param>
		/// <returns>bool</returns>
		private bool GetIsSkippable(string str)
		{
			return (str.ToLower() == "true");
		}

		/// <summary>
		/// Helper to get TimeSpan Duration
		/// </summary>
		/// <param name="duration">Duration as string</param>
		/// <returns>TimeSpan</returns>
		private TimeSpan GetDuration(string duration)
		{
			if (duration == String.Empty)
			{
				duration = "0";
			}
			TimeSpan helper = new TimeSpan(0, 0, int.Parse(duration));
			return helper;
		}

		/// <summary>
		/// Helper to get TimeSpan Duration
		/// </summary>
		/// <param name="duration">String from XML-Attribute</param>
		/// <param name="xElement">Element from XML</param>
		/// <param name="p">Attributename</param>
		/// <returns>TimeSpan</returns>
		private TimeSpan GetDuration(XElement xElement, string p)
		{
			if (xElement != null && xElement.Attribute(p) != null)
				return GetDuration((string)xElement.Attribute(p));
			return TimeSpan.Zero;
		}

		/// <summary>
		/// Helper to get Tracking
		/// </summary>
		/// <param name="duration">String from XML-Attribute</param>
		/// <param name="element">Element from XML</param>
		/// <returns>TrackingItem</returns>
		private Tracking GetTracking(XElement element)
		{
			if (element != null)
			{
				return new Tracking
				{
					Started = GetTrackingItem(element.Element("started")),
					Ended = GetTrackingItem(element.Element("ended")),
					Skipped = GetTrackingItem(element.Element("skipped")),
					Stopped = GetTrackingItem(element.Element("stopped")),
				};
			}
			return null;
		}

		/// <summary>
		/// Helper to get TrackingItem
		/// </summary>
		/// <param name="duration">String from XML-Attribute</param>
		/// <param name="element">Element from XML</param>
		/// <returns>TrackingItem</returns>
		private TrackingItem GetTrackingItem(XElement element)
		{
			if (element != null)
			{
				return new TrackingItem
				{
					TrackUrl = GetUri(element.Attribute("url")),
					TrackValue = GetString(element.Attribute("value")),
					JsFunction = GetString(element.Attribute("jsfunction")),
				};
			}
			return null;
		}

		/// <summary>
		/// Helper to get string Source
		/// </summary>
		/// <param name="str"></param>
		/// <param name="xAttribute">Attribute from XML</param>
		/// <returns></returns>
		protected string GetString(XAttribute xAttribute)
		{
			if (xAttribute != null)
			{
				return (string)xAttribute;
			}
			return String.Empty;
		}
		/// <summary>
		/// Helper to get string Source
		/// </summary>
		/// <param name="str"></param>
		/// <param name="element">Element from XML</param>
		/// <returns></returns>
		protected string GetString(XElement element)
		{
			if (element != null)
			{
				return ((string)element).Trim();
			}
			return String.Empty;
		}

		/// <param name="xElement">Element from XML</param>
		/// <param name="p">Attributename</param>
		protected string GetString(XElement xElement, string p)
		{
			if (xElement != null)
				return GetString(xElement.Attribute(p));
			return String.Empty;
		}

		/// <summary>
		/// Helper to get Uri Source
		/// </summary>
		/// <param name="str"></param>
		/// <param name="xAttribute">Attribute from XML</param>
		/// <returns></returns>
		protected Uri GetUri(XAttribute xAttribute)
		{
			if (xAttribute != null)
			{
				return new Uri((string)xAttribute, UriKind.RelativeOrAbsolute);
			}
			return null;
		}
		/// <summary>
		/// Helper to get Uri Source
		/// </summary>
		/// <param name="xElement">Element from XML</param>
		/// <param name="p">Attriibutename</param>
		protected Uri GetUri(XElement xElement, string p)
		{
			if (xElement != null)
				return GetUri(xElement.Attribute(p));
			return null;
		}


		/// <summary>
		/// Helper to get DateTime PuplishDate
		/// </summary>
		/// <param name="str">DateTime as string</param>
		/// <returns>DateTime</returns>
		private DateTime GetPuplishDate(string str)
		{
			try
			{
				return DateTime.Parse(str);
			}
			catch (Exception e)
			{
				return DateTime.Now;
			}
		}
	}
}
