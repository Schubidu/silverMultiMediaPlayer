﻿using System;
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
	[TemplateVisualState(Name = "FirstState", GroupName = "IconStates")]
	[TemplateVisualState(Name = "SecondState", GroupName = "IconStates")]
	[TemplateVisualState(Name = "ThirdState", GroupName = "IconStates")]
	public class ButtonIcon : Control
	{
		private Panel m_rootElement;
		private FrameworkElement m_firstStateElement;
		private FrameworkElement m_secondStateElement;
		private FrameworkElement m_thirdStateElement;

		public ButtonIcon() 
		{
			DefaultStyleKey = typeof(ButtonIcon);
			this.LayoutUpdated += new EventHandler(ButtonIcon_LayoutUpdated);
			this.Loaded += new RoutedEventHandler(ButtonIcon_Loaded);
		}

		void ButtonIcon_Loaded(object sender, RoutedEventArgs e)
		{
			UpdateVisuals();
		}

		void ButtonIcon_LayoutUpdated(object sender, EventArgs e)
		{
			UpdateVisuals();
		}

		public override void OnApplyTemplate()
		{
			m_rootElement = GetTemplateChild("RootElement") as Panel;
			m_firstStateElement = GetTemplateChild("FirstStateElement") as FrameworkElement;
			m_secondStateElement = GetTemplateChild("SecondStateElement") as FrameworkElement;
			m_thirdStateElement = GetTemplateChild("ThirdStateElement") as FrameworkElement;
			UpdateVisuals();
		}

		protected virtual void UpdateVisuals()
		{
			SetState(State);
		}

		private void SetState(Iconstate state)
		{
			VisualStateManager.GoToState(this, state+"", true);
			//switch (state)
			//{ 
			//    case Iconstate.FirstState:
			//        VisualStateManager.GoToState(this, "FirstState", true);
			//        break;
			//    case Iconstate.SecondState:
			//        VisualStateManager.GoToState(this, "SecondState", true);
			//        break;
			//    case Iconstate.ThirdState:
			//        VisualStateManager.GoToState(this, "ThirdState", true);
			//        break;
			//}

		}


		#region State (DependencyProperty)

		/// <summary>
		/// A description of the property.
		/// </summary>
		public Iconstate State
		{
			get { return (Iconstate)GetValue(StateProperty); }
			set {
				SetState(value);
				SetValue(StateProperty, value); 
			}
		}
		public static readonly DependencyProperty StateProperty =
			DependencyProperty.Register("State", typeof(Iconstate), typeof(ButtonIcon),
			  new PropertyMetadata(Iconstate.FirstState));

		#endregion


	}
}
