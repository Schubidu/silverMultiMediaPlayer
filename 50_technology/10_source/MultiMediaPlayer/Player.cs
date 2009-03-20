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
	public class Player : Control
	{
		private MediaData.MediaCollection mediaCollection;
		private AppData.ApplikationKey AppKey;
		private Panel m_rootElement;

		public Player() 
		{
			DefaultStyleKey = typeof(Player);
			//AppKey = new MultiMediaPlayer.AppData.ApplikationKey();
			//AppKey.KeyValidationFailed += new MultiMediaPlayer.AppData.ApplikationKeyEventHandler(ApplikationKey_KeyValidation);
			//AppKey.KeyValidationSuccessed += new MultiMediaPlayer.AppData.ApplikationKeyEventHandler(ApplikationKey_KeyValidation);
			mediaCollection = new MultiMediaPlayer.MediaData.MediaCollection();
			mediaCollection.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(MediaCollection_PropertyChanged);
		}

		public override void OnApplyTemplate()
		{
			m_rootElement = GetTemplateChild("RootElement") as Panel;
		}

		void ApplikationKey_KeyValidation(object sender, MultiMediaPlayer.AppData.ApplikationKeyEventArgs e)
		{
			LoadPlayer();
		}

		void MediaCollection_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			LoadPlayer();
		}

		private int countCheckVars = 0;
		private bool AllVarsValide() {
			countCheckVars++;
			return (mediaCollection != null && mediaCollection.MediaItems != null && mediaCollection.MediaItems.Count > 0);// && AppKey.KeyIsValide;
		}


		private void LoadPlayer()
		{
			if (AllVarsValide()) {
				Views.ViewPlayer childPlayer = null;
				if (IsSinglePlayer)
				{
					childPlayer = new Views.SinglePlayer();
				}
				else 
				{
					childPlayer = new Views.MultiPlayer();
				}
				childPlayer.MediaCollect = mediaCollection;
				m_rootElement.Children.Add(childPlayer);
			}
		}

		private void LoadMediaCollection()
		{
			if (!MediaXmlUri.Equals(String.Empty))
			{
				mediaXmlUri = new Uri(MediaXmlUri, UriKind.RelativeOrAbsolute);
				mediaCollection.MediaXml = mediaXmlUri;
			}
		}

		private void validateKey(String key) {
			if(!key.Equals(String.Empty))
				AppKey.validateKey(key);

		}


		//#region PlayerKey (DependencyProperty)

		///// <summary>
		///// Applicationkey for Player
		///// </summary>
		//public string PlayerKey
		//{
		//    get { return (string)GetValue(PlayerKeyProperty); }
		//    set { SetValue(PlayerKeyProperty, value); }
		//}
		//public static readonly DependencyProperty PlayerKeyProperty =
		//    DependencyProperty.Register("PlayerKey", typeof(string), typeof(Player),
		//      new PropertyMetadata(String.Empty, new PropertyChangedCallback(PlayerKeyChanged)));

		//private static void PlayerKeyChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
		//{
		//    Player p = dp as Player;
		//    string s = e.NewValue as string;
		//    p.validateKey(s);
		//}
		
		//#endregion


		#region MediaXmlUri (DependencyProperty)

		private Uri mediaXmlUri;
		/// <summary>
		/// Uri to MediaXml
		/// </summary>
		public string MediaXmlUri
		{
			get { return (string)GetValue(MediaXmlUriProperty); }
			set { 
				SetValue(MediaXmlUriProperty, value); 
			}
		}
		public static readonly DependencyProperty MediaXmlUriProperty =
			DependencyProperty.Register("MediaXmlUri", typeof(string), typeof(Player),
			  new PropertyMetadata(String.Empty, new PropertyChangedCallback(MediaXmlUriChanged)));

		private static void MediaXmlUriChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
		{
			Player p = dp as Player;
			string s = e.NewValue as string;
			p.LoadMediaCollection();
		}

		#endregion

		#region IsSinglePlayer (DependencyProperty)

		/// <summary>
		/// Player is singleplayer
		/// </summary>
		public bool IsSinglePlayer
		{
			get { return (bool)GetValue(IsSinglePlayerProperty); }
			set { SetValue(IsSinglePlayerProperty, value); }
		}
		public static readonly DependencyProperty IsSinglePlayerProperty =
			DependencyProperty.Register("IsSinglePlayer", typeof(bool), typeof(Player),
			  new PropertyMetadata(false, new PropertyChangedCallback(IsSinglePlayerChanged)));

		private static void IsSinglePlayerChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
		{
			Player p = dp as Player;
			//string s = e.NewValue as string;
			p.LoadPlayer();
		}

		#endregion


	}
}
