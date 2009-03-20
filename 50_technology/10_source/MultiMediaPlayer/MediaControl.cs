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
	public class MediaControl : Control
	{
		public MediaControl()
		{

		}

		public virtual void SetData(Object obj)
		{
			MediaData.MediaItem item = obj as MediaData.MediaItem; 
			this.MediaItem = item;
			this.Content = item.Content;
			this.Duration = item.Duration;
			this.IsFirst = item.IsFirst;
			this.IsLast = item.IsLast;
			this.IsSkippable = item.IsSkippable;
			this.ItemType = item.ItemType;
			this.Markers = item.Markers;
			this.MetaData = item.Metadata;
			this.SkipAfter = item.SkipAfter;
			this.ThumbUrl = item.ThumbUrl;
			this.TrackingData = item.TrackingData;
			this.UseDuration = item.UseDuration;
		}

		#region MediaItem (DependencyProperty)

		/// <summary>
		/// A description of the property.
		/// </summary>
		public MediaData.MediaItem MediaItem
		{
			get { return (MediaData.MediaItem)GetValue(MediaItemProperty); }
			set 
			{
				SetValue(MediaItemProperty, value); 
			}
		}
		public static readonly DependencyProperty MediaItemProperty =
			DependencyProperty.Register("MediaItem", typeof(MediaData.MediaItem), typeof(MediaControl),
			  new PropertyMetadata(null));

		#endregion

		#region Content (DependencyProperty)

		/// <summary>
		/// Kind of Content
		/// </summary>
		public MediaData.ContentType Content
		{
			get { return (MediaData.ContentType)GetValue(ContentProperty); }
			set { SetValue(ContentProperty, value); }
		}
		public static readonly DependencyProperty ContentProperty =
			DependencyProperty.Register("Content", typeof(MediaData.ContentType), typeof(MediaControl),
			  new PropertyMetadata(MediaData.ContentType.Unknown));

		#endregion

		#region Duration (DependencyProperty)

		/// <summary>
		/// Duration of MediaItem
		/// </summary>
		public TimeSpan Duration
		{
			get { return (TimeSpan)GetValue(DurationProperty); }
			set { SetValue(DurationProperty, value); }
		}
		public static readonly DependencyProperty DurationProperty =
			DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(MediaControl),
			  new PropertyMetadata(TimeSpan.Zero));

		#endregion

		#region IsFirst (DependencyProperty)

		/// <summary>
		/// IsFirst Item in List
		/// </summary>
		public bool IsFirst
		{
			get { return (bool)GetValue(IsFirstProperty); }
			set { SetValue(IsFirstProperty, value); }
		}
		public static readonly DependencyProperty IsFirstProperty =
			DependencyProperty.Register("IsFirst", typeof(bool), typeof(MediaControl),
			  new PropertyMetadata(false));

		#endregion

		#region IsLast (DependencyProperty)

		/// <summary>
		/// Is last item in list.
		/// </summary>
		public bool IsLast
		{
			get { return (bool)GetValue(IsLastProperty); }
			set { SetValue(IsLastProperty, value); }
		}
		public static readonly DependencyProperty IsLastProperty =
			DependencyProperty.Register("IsLast", typeof(bool), typeof(MediaControl),
			  new PropertyMetadata(false));

		#endregion

		#region IsSkippable (DependencyProperty)

		/// <summary>
		/// Skipping is possiple
		/// </summary>
		public bool IsSkippable
		{
			get { return (bool)GetValue(IsSkippableProperty); }
			set { SetValue(IsSkippableProperty, value); }
		}
		public static readonly DependencyProperty IsSkippableProperty =
			DependencyProperty.Register("IsSkippable", typeof(bool), typeof(MediaControl),
			  new PropertyMetadata(false));

		#endregion

		#region ItemType (DependencyProperty)

		/// <summary>
		/// Kind of MediaItem
		/// </summary>
		public MediaData.MediaType ItemType
		{
			get { return (MediaData.MediaType)GetValue(ItemTypeProperty); }
			set { SetValue(ItemTypeProperty, value); }
		}
		public static readonly DependencyProperty ItemTypeProperty =
			DependencyProperty.Register("ItemType", typeof(MediaData.MediaType), typeof(MediaControl),
			  new PropertyMetadata(MediaData.MediaType.Unknown));

		#endregion

		#region Markers (DependencyProperty)

		/// <summary>
		/// markes for undertitle
		/// </summary>
		public System.Collections.Generic.List<MediaData.Marker> Markers
		{
			get { return (System.Collections.Generic.List<MediaData.Marker>)GetValue(MarkersProperty); }
			set { SetValue(MarkersProperty, value); }
		}
		public static readonly DependencyProperty MarkersProperty =
			DependencyProperty.Register("Markers", typeof(System.Collections.Generic.List<MediaData.Marker>), typeof(MediaControl),
			  new PropertyMetadata(null));

		#endregion

		#region MetaData (DependencyProperty)

		/// <summary>
		/// Such meta
		/// </summary>
		public MediaData.Meta MetaData
		{
			get { return (MediaData.Meta)GetValue(MetaDataProperty); }
			set { SetValue(MetaDataProperty, value); }
		}
		public static readonly DependencyProperty MetaDataProperty =
			DependencyProperty.Register("MetaData", typeof(MediaData.Meta), typeof(MediaControl),
			  new PropertyMetadata(null));

		#endregion

		#region SkipAfter (DependencyProperty)

		/// <summary>
		/// Skip possible after duration of seconds
		/// </summary>
		public TimeSpan SkipAfter
		{
			get { return (TimeSpan)GetValue(SkipAfterProperty); }
			set { SetValue(SkipAfterProperty, value); }
		}
		public static readonly DependencyProperty SkipAfterProperty =
			DependencyProperty.Register("SkipAfter", typeof(TimeSpan), typeof(MediaControl),
			  new PropertyMetadata(TimeSpan.Zero));

		#endregion

		#region ThumbUrl (DependencyProperty)

		/// <summary>
		/// Url to previewthumb
		/// </summary>
		public string ThumbUrl
		{
			get { return (string)GetValue(ThumbUrlProperty); }
			set { SetValue(ThumbUrlProperty, value); }
		}
		public static readonly DependencyProperty ThumbUrlProperty =
			DependencyProperty.Register("ThumbUrl", typeof(string), typeof(MediaControl),
			  new PropertyMetadata(String.Empty));

		#endregion

		#region TrackingData (DependencyProperty)

		/// <summary>
		/// Such trackingdata
		/// </summary>
		public MediaData.Tracking TrackingData
		{
			get { return (MediaData.Tracking)GetValue(TrackingDataProperty); }
			set { SetValue(TrackingDataProperty, value); }
		}
		public static readonly DependencyProperty TrackingDataProperty =
			DependencyProperty.Register("TrackingData", typeof(MediaData.Tracking), typeof(MediaControl),
			  new PropertyMetadata(null));

		#endregion

		#region UseDuration (DependencyProperty)

		/// <summary>
		/// use duration from xml or use naturelduration of video
		/// </summary>
		public bool UseDuration
		{
			get { return (bool)GetValue(UseDurationProperty); }
			set { SetValue(UseDurationProperty, value); }
		}
		public static readonly DependencyProperty UseDurationProperty =
			DependencyProperty.Register("UseDuration", typeof(bool), typeof(MediaControl),
			  new PropertyMetadata(true));

		#endregion

	}
}
