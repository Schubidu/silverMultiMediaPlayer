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
	public class MediaDurationTextBox : TextBox
	{
		public MediaDurationTextBox()
		{

		}
		
		private void fillText() 
		{
			if (Duration.Hours > 0 || FullDuration.Hours > 0)
			{
				Text = String.Format(FormatText, formatDuration(Duration, 0, 8), formatDuration(FullDuration, 0, 8));
			}
			else
			{
				Text = String.Format(FormatText, formatDuration(Duration), formatDuration(FullDuration));
			}
		}

		private string formatDuration(TimeSpan time)
		{
			return time.ToString().Substring(3, 5);
		}

		private string formatDuration(TimeSpan time, int startindex, int length)
		{
			if (startindex < 0) startindex = 0;
			if (length < startindex) length = startindex;
			return time.ToString().Substring(startindex, length);
		}

		#region FormatText (DependencyProperty)

		/// <summary>
		/// A description of the property.
		/// </summary>
		public string FormatText
		{
			get { return (string)GetValue(FormatTextProperty); }
			set { SetValue(FormatTextProperty, value); }
		}
		public static readonly DependencyProperty FormatTextProperty =
			DependencyProperty.Register("FormatText", typeof(string), typeof(MediaDurationTextBox),
			  new PropertyMetadata("{0} / {1}"));

		#endregion


		#region FullDuration (DependencyProperty)

		/// <summary>
		/// A description of the property.
		/// </summary>
		public TimeSpan FullDuration
		{
			get { return (TimeSpan)GetValue(FullDurationProperty); }
			set { SetValue(FullDurationProperty, value); }
		}
		public static readonly DependencyProperty FullDurationProperty =
			DependencyProperty.Register("FullDuration", typeof(TimeSpan), typeof(MediaDurationTextBox),
			  new PropertyMetadata(TimeSpan.Zero,new PropertyChangedCallback(FullDurationChanged)));

		private static void FullDurationChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
		{
			MediaDurationTextBox p = dp as MediaDurationTextBox;
			p.fillText();
		}

		#endregion

		#region Duration (DependencyProperty)

		/// <summary>
		/// A description of the property.
		/// </summary>
		public TimeSpan Duration
		{
			get { return (TimeSpan)GetValue(DurationProperty); }
			set { SetValue(DurationProperty, value); }
		}
		public static readonly DependencyProperty DurationProperty =
			DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(MediaDurationTextBox),
			  new PropertyMetadata(TimeSpan.Zero, new PropertyChangedCallback(DurationChanged)));

		private static void DurationChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
		{
			MediaDurationTextBox p = dp as MediaDurationTextBox;
			p.fillText();
		}

		#endregion


	}
}
