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

namespace MultiMediaPlayer.Views
{
	public class ErrorPlayer : Control
	{
		public ErrorPlayer()
		{
			DefaultStyleKey = typeof(ErrorPlayer);
		}

		#region Text (DependencyProperty)

		/// <summary>
		/// Errortext for Output
		/// </summary>
		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}
		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register("Text", typeof(string), typeof(ErrorPlayer),
			  new PropertyMetadata(String.Empty));

		#endregion

	}
}
