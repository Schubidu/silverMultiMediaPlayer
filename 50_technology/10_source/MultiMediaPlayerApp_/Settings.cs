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
	public static class Settings
	{
		public static Uri ApplicationKeyUrl { get { return new Uri("/applicationkey.xml", UriKind.Relative); }}
	}
}
