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
	public class TestPanel : Control
	{
		private Panel m_rootElement;
		private ButtonIcon butIcon;
		private Button state1;
		private Button state2;
		private Button state3;

		public TestPanel() 
		{
			DefaultStyleKey = typeof(TestPanel);
		}

		public override void OnApplyTemplate()
		{
			butIcon = GetTemplateChild("ButIcon") as ButtonIcon;
			state1 = GetTemplateChild("State1") as Button;
			state2 = GetTemplateChild("State2") as Button;
			state3 = GetTemplateChild("State3") as Button;
			state1.Click += new RoutedEventHandler(state1_Click);
			state2.Click += new RoutedEventHandler(state2_Click);
			state3.Click += new RoutedEventHandler(state3_Click);
		}

		void state3_Click(object sender, RoutedEventArgs e)
		{
			butIcon.State = Iconstate.ThirdState;
		}

		void state2_Click(object sender, RoutedEventArgs e)
		{
			butIcon.State = Iconstate.SecondState;
		}

		void state1_Click(object sender, RoutedEventArgs e)
		{
			butIcon.State = Iconstate.FirstState;
		}
	}
}
