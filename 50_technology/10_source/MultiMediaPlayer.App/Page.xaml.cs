﻿using System;
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
		}

		private void SliderGauge_PercentChanged(object sender, PercentageChangedEventArgs.GaugePercentageChangedEventArgs e)
		{
			//Gauge.Percentage = (e.Percentage < Gauge.DownloadPercentage) ? e.Percentage : Gauge.DownloadPercentage;
		}
	}
}
