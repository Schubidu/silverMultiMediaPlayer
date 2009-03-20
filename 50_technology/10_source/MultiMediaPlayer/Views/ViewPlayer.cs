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
	public class ViewPlayer : Control
	{
		protected Panel m_rootElement;

		public ViewPlayer() {
		}

		public override void OnApplyTemplate()
		{
			m_rootElement = GetTemplateChild("RootElement") as Panel;
			init();
		}

		protected virtual void init()
		{
		}


		#region MediaCollect (DependencyProperty)

		/// <summary>
		/// All Mediaelements
		/// </summary>
		public MediaData.MediaCollection MediaCollect
		{
			get { return (MediaData.MediaCollection)GetValue(MediaCollectProperty); }
			set { SetValue(MediaCollectProperty, value); }
		}
		public static readonly DependencyProperty MediaCollectProperty =
			DependencyProperty.Register("MediaCollect", typeof(MediaData.MediaCollection), typeof(ViewPlayer),
			  new PropertyMetadata(null, new PropertyChangedCallback(MediaCollectChanged)));

		private static void MediaCollectChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ViewPlayer p = d as ViewPlayer;
			MediaData.MediaCollection m = e.NewValue as MediaData.MediaCollection;
			if (m != null)
				p.updateView();
		}

		protected virtual void updateView() {
			if (m_rootElement != null)
			{
				init();
			}
		}


		#endregion

	}
}
