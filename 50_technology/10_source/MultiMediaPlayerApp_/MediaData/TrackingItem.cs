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
	public class TrackingItem
	{
		/// <summary>
		/// Fired when javascript function or url cannot reached
		/// </summary>
		public event TrackingItemEventHandler TrackingFailed;

		/// <summary>
		/// Fired when all successed
		/// </summary>
		public event TrackingItemEventHandler TrackingSuccess;
		/// <summary>
		/// vars and methods for tracking mediaevents
		/// </summary>
		public TrackingItem()
		{
		}

		/// <summary>
		/// trackingurl
		/// </summary>
		public Uri TrackUrl { get; set; }
		/// <summary>
		/// first param for javascript function
		/// </summary>
		public string TrackValue { get; set; }
		/// <summary>
		/// name of javascript function which fired from track
		/// </summary>
		public string JsFunction { get; set; }
		/// <summary>
		/// JsFunction string not empty
		/// </summary>
		public bool HasJsFunction { get { return (JsFunction != String.Empty); } }
		/// <summary>
		/// Track mediaevent
		/// </summary>
		/// <param name="time">timespan when event fired</param>
		public void Track(TimeSpan time)
		{
		}

		/// <summary>
		/// Track mediaevent
		/// </summary>
		public void Track()
		{
			throw new System.NotImplementedException();
		}

		private void OnTrackingFailed()
		{
			throw new System.NotImplementedException();
		}

		private void OnTrackingSuccessed()
		{
			throw new System.NotImplementedException();
		}

	}

	/// <summary>
	/// handle tracking events
	/// </summary>
	/// <param name="t">last trackingItem</param>
	public delegate void TrackingItemEventHandler(TrackingItem t);
}
