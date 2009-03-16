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
	[TemplatePart(Name = "Core", Type = typeof(FrameworkElement))]
	[TemplateVisualState(Name = "Normal", GroupName = "CommonStates")]
	[TemplateVisualState(Name = "MouseDown", GroupName = "CommonStates")]
	[TemplateVisualState(Name = "MouseOver", GroupName = "CommonStates")]
	[TemplateVisualState(Name = "Download", GroupName = "DownloadStates")]
	[TemplateVisualState(Name = "Intermediate", GroupName = "DownloadStates")]

	public class SliderDownloadGauge : SliderGauge
	{
		private FrameworkElement m_downloadElement;
		private bool isIntermediate = false;

		public SliderDownloadGauge() {
			DefaultStyleKey = typeof(SliderDownloadGauge);
		}

		/// <summary>
		/// Builds the visual tree for the SliderGauge control when the template is applied. 
		/// </summary>
		public override void OnApplyTemplate()
		{
			m_downloadElement = GetTemplateChild("DownloadElement") as FrameworkElement;
			base.OnApplyTemplate();

			UpdateVisuals();
		}

		protected override void UpdateVisuals() {
			if (m_downloadElement == null)
			{
				return;
			}
			else
			{
				m_downloadElement.Clip = ClippingRect(this.DownloadPercentage);
			}
			base.UpdateVisuals();

		}

		protected override void GoToState(bool useTransitions)
		{
			base.GoToState(useTransitions);
			if (isIntermediate)
				VisualStateManager.GoToState(this, "Intermediate", useTransitions);
			else
				VisualStateManager.GoToState(this, "Download", useTransitions);
		}

		protected override double NewPercentage(Point location)
		{
			return ValidatePercentage(base.NewPercentage(location));
		}

		protected override double ValidatePercentage(double percentage)
		{
			return (percentage < this.DownloadPercentage) ? percentage : this.DownloadPercentage;
		}

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
			SliderDownloadGauge g = d as SliderDownloadGauge;
			if (g != null)
			{
				g.UpdateVisuals();
			}
		}
		#endregion


		#region IsIntermediate (DependencyProperty)

		/// <summary>
		/// IsIntermediate Depency Property
		/// </summary>
		public bool IsIntermediate
		{
			get { return (bool)GetValue(IsIntermediateProperty); }
			set {
				isIntermediate = value;
				GoToState(true);
				SetValue(IsIntermediateProperty, value); 
			}
		}
		public static readonly DependencyProperty IsIntermediateProperty =
			DependencyProperty.Register("IsIntermediate", typeof(bool), typeof(SliderDownloadGauge),
			  new PropertyMetadata(false));

		#endregion


	}
}
