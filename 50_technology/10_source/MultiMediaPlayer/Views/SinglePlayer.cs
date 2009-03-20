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
	public class SinglePlayer : ViewPlayer
	{
		public SinglePlayer()
		{
			DefaultStyleKey = typeof(SinglePlayer);
		}

		//public override void OnApplyTemplate()
		//{
		//    //m_rootElement = GetTemplateChild("RootElement") as Panel;
		//}

		protected override void init() 
		{
			MediaData.MediaItem m = MediaCollect.MediaItems[0];
			switch (m.ItemType)
			{
				case MultiMediaPlayer.MediaData.MediaType.Video:
					VideoControl v = new VideoControl();
					v.SetData(m);
					m_rootElement.Children.Add(v);
					break;
			}
		}

		//protected override void updateView()
		//{
		//}
	}
}
