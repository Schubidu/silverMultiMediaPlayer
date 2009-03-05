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
	public class Marker
	{
		public string Name { get; set; }
		public string Text { get; set; }
		public TimeSpan Start { get; set; }
		public TimeSpan End { get; set; }
	}
}
