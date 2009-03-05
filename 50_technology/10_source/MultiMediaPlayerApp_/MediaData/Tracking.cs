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
	public class Tracking
	{
		/// <summary>
		/// All kind of Tracking
		/// </summary>
		public Tracking()
		{
		}

		/// <summary>
		/// Media started
		/// </summary>
		public TrackingItem Started { get; set; }
		/// <summary>
		/// Media ended
		/// </summary>
		public TrackingItem Ended { get; set; }
		/// <summary>
		/// Media skipped
		/// </summary>
		public TrackingItem Skipped { get; set; }
		/// <summary>
		/// Media stopped
		/// </summary>
		public TrackingItem Stopped { get; set; }
	}
}
