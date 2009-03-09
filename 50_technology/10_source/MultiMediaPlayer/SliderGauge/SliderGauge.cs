﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MultiMediaPlayer
{
	/// <summary>
	/// Represents a Slider control which allows a user to select a percentage.
	/// </summary>
	public class SliderGauge : Control
	{
		private Panel m_rootElement;
		private FrameworkElement m_highlightElement;
		private FrameworkElement m_shadowElement;
		private FrameworkElement m_downloadElement;
		private TextBlock m_percentageTextBlock;
		private bool m_guagePathMouseCaptured;

		/// <summary>
		/// Fired when the percentage is changed on the control.
		/// </summary>
		public event MultiMediaPlayer.PercentageChangedEventArgs.GaugePercentageChangedEventHandler PercentChanged;

		/// <summary>
		/// Create a new instance of the SliderGauge control.
		/// </summary>
		public SliderGauge()
		{
			DefaultStyleKey = typeof(SliderGauge);
			this.MouseLeftButtonDown += new MouseButtonEventHandler(SilverlightGauge_MouseLeftButtonDown);
			this.MouseMove += new MouseEventHandler(SilverlightGauge_MouseMove);
			this.MouseLeftButtonUp += new MouseButtonEventHandler(SilverlightGauge_MouseLeftButtonUp);
			this.LayoutUpdated += new EventHandler(SilverlightGauge_LayoutUpdated);
		}

		private void SilverlightGauge_LayoutUpdated(object sender, EventArgs e)
		{
			UpdateVisuals();
		}

		/// <summary>
		/// Builds the visual tree for the SliderGauge control when the template is applied. 
		/// </summary>
		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			m_rootElement = GetTemplateChild("RootElement") as Panel;
			m_highlightElement = GetTemplateChild("HighlightElement") as FrameworkElement;
			m_shadowElement = GetTemplateChild("ShadowElement") as FrameworkElement;
			m_downloadElement = GetTemplateChild("DownloadElement") as FrameworkElement;
			m_percentageTextBlock = GetTemplateChild("PercentageTextBlock") as TextBlock;

			UpdateVisuals();
		}

		private void SilverlightGauge_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (this.ReadOnly)
				return;

			this.Percentage = NewPercentage(e.GetPosition(this));

			m_guagePathMouseCaptured = this.CaptureMouse();
			VisualStateManager.GoToState(this, "MouseDown", true);
		}

		private void SilverlightGauge_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.ReadOnly)
				return;

			if (m_guagePathMouseCaptured)
			{
				this.Percentage = NewPercentage(e.GetPosition(this));
				FirePercentChangedEvent();
			}

			VisualStateManager.GoToState(this, "MouseOver", true);
		}

		private double NewPercentage(Point location) {
			double percentage = 0;
			if (this.Orientation == Orientation.Vertical)
				percentage = (1 - (location.Y / m_rootElement.ActualHeight));
			else
				percentage = location.X / m_rootElement.ActualWidth;

			return percentage;
		}

		private void UpdateVisuals()
		{
			if (m_highlightElement == null)
				return;

			m_highlightElement.Clip = ClippingRect(this.Percentage);
			m_downloadElement.Clip = ClippingRect(this.DownloadPercentage);

			m_percentageTextBlock.Visibility = ShowPercentageTextOnChange ? Visibility.Visible : Visibility.Collapsed;
		}

		private RectangleGeometry ClippingRect(double percentage)
		{
			RectangleGeometry clippingRect = new RectangleGeometry();
			if (this.Orientation == Orientation.Vertical)
			{
				double location = (1 - percentage) * m_rootElement.ActualHeight;
				clippingRect.Rect = new Rect(0, location, m_rootElement.ActualWidth, m_rootElement.ActualHeight - location);
			}
			else
			{
				double location = percentage * m_rootElement.ActualWidth;
				clippingRect.Rect = new Rect(0, 0, location, m_rootElement.ActualHeight);
			}
			return clippingRect;
		}

		private void SilverlightGauge_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			m_highlightElement.ReleaseMouseCapture();
			m_guagePathMouseCaptured = false;
			FirePercentChangedEvent();
			VisualStateManager.GoToState(this, "Normal", true);
		}

		private void FirePercentChangedEvent()
		{
			if (PercentChanged != null)
			{
				PercentChanged(this, new MultiMediaPlayer.PercentageChangedEventArgs.GaugePercentageChangedEventArgs(this.Percentage));
			}
		}

		#region Percentage Dependency Property

		/// <summary>
		/// Gets or sets the current percentage.
		/// </summary>
		public double Percentage
		{
			get { return (double)GetValue(PercentageProperty); }
			set { SetValue(PercentageProperty, Math.Max(0, Math.Min(1, value))); }
		}

		/// <summary>
		/// Percentage Dependency Property.
		/// </summary>
		public static readonly DependencyProperty PercentageProperty =
			DependencyProperty.Register(
			"Percentage",
			typeof(double),
			typeof(SliderGauge),
			new PropertyMetadata(new PropertyChangedCallback(PercentageChanged)));

		private static void PercentageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SliderGauge g = d as SliderGauge;
			if (g != null)
			{
				if (g.m_percentageTextBlock != null)
					g.m_percentageTextBlock.Text = g.Percentage.ToString("p0");
				g.UpdateVisuals();
			}
		}
		#endregion

		#region DownloadPercentage (DependencyProperty)

		/// <summary>
		/// DownloadPercentage Depency Property
		/// </summary>
		public double DownloadPercentage
		{
			get { return (double)GetValue(DownloadPercentageProperty); }
			set { SetValue(DownloadPercentageProperty, Math.Max(0, Math.Min(1, value))); }
		}
		public static readonly DependencyProperty DownloadPercentageProperty =
			DependencyProperty.Register("DownloadPercentage", typeof(double), typeof(SliderGauge),
			  new PropertyMetadata(new PropertyChangedCallback(DownloadPercentageChanged)));

		private static void DownloadPercentageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SliderGauge g = d as SliderGauge;
			if (g != null)
			{
				g.UpdateVisuals();
			}
		}
		#endregion

		#region Orientation Dependency Property

		/// <summary>
		/// Gets or sets the current percentage.
		/// </summary>
		public Orientation Orientation
		{
			get { return (Orientation)GetValue(OrientationProperty); }
			set { SetValue(OrientationProperty, value); }
		}

		/// <summary>
		/// Orientation Dependency Property.
		/// </summary>
		public static readonly DependencyProperty OrientationProperty =
			DependencyProperty.Register(
			"Orientation",
			typeof(Orientation),
			typeof(SliderGauge),
			new PropertyMetadata(new PropertyChangedCallback(OrientationChanged)));

		private static void OrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			SliderGauge g = d as SliderGauge;
			if (g != null)
			{
				g.UpdateVisuals();
			}
		}
		#endregion

		#region ReadOnly Dependency Property

		/// <summary>
		/// Gets or sets whether or not the user can interact with the control.
		/// </summary>
		public bool ReadOnly
		{
			get { return (bool)GetValue(ReadOnlyProperty); }
			set { SetValue(ReadOnlyProperty, value); }
		}

		/// <summary>
		/// Percentage Dependency Property.
		/// </summary>
		public static readonly DependencyProperty ReadOnlyProperty =
			DependencyProperty.Register(
			"ReadOnly",
			typeof(bool),
			typeof(SliderGauge),
			null);

		#endregion

		#region FontColor Dependency Property

		/// <summary>
		/// Gets or sets the color for the text of the control.
		/// </summary>
		public Brush FontColor
		{
			get { return (Brush)GetValue(FontColorProperty); }
			set { SetValue(FontColorProperty, value); }
		}

		/// <summary>
		/// FontColor Dependency Property.
		/// </summary>
		public static readonly DependencyProperty FontColorProperty =
			DependencyProperty.Register(
			"FontColor",
			typeof(Brush),
			typeof(SliderGauge),
			null);

		#endregion

		#region ShowPercentageTextOnChange Dependency Property

		/// <summary>
		/// Determines whether or not to show the percentage text as the 
		/// control's value changes.
		/// </summary>
		public bool ShowPercentageTextOnChange
		{
			get { return (bool)GetValue(ShowPercentageTextOnChangeProperty); }
			set { SetValue(ShowPercentageTextOnChangeProperty, value); }
		}

		/// <summary>
		/// ShowPercentageTextOnChange Dependency Property.
		/// </summary>
		public static readonly DependencyProperty ShowPercentageTextOnChangeProperty =
			DependencyProperty.Register(
			"ShowPercentageTextOnChange",
			typeof(bool),
			typeof(SliderGauge),
			null);


		#endregion
	}
}
