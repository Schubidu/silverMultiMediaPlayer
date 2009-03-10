using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MultiMediaPlayer.App
{
	public partial class Page : UserControl
	{
		public Page()
		{
			InitializeComponent();
			//SGauge.PercentChanged += new PercentageChangedEventArgs.GaugePercentageChangedEventHandler(SGauge_PercentChanged);
			Submit.Click += new RoutedEventHandler(Submit_Click);
		}

		void Submit_Click(object sender, RoutedEventArgs e)
		{
			Gauge.Percentage += 0.05;
			SGauge.Percentage += 0.05;
			Gauge_Copy.Percentage += 0.05;
			SGauge_Copy.Percentage += 0.05;
		}

		void SGauge_PercentChanged(object sender, PercentageChangedEventArgs.GaugePercentageChangedEventArgs e)
		{
		//	MessageBox.Show(e.Percentage.ToString());
		}

		private void SliderGauge_PercentChanged(object sender, PercentageChangedEventArgs.GaugePercentageChangedEventArgs e)
		{
			//Gauge.Percentage = (e.Percentage < Gauge.DownloadPercentage) ? e.Percentage : Gauge.DownloadPercentage;
		}
	}
}
