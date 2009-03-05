using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml.Linq;

namespace MultiMediaPlayer
{
	public class ApplikationKey
	{
		private bool isValideKey = false;

		/// <summary>
		/// Fired when current sender proof is successed
		/// </summary>
		public event ApplikationKeyEventHandler KeyValidationSuccessed;

		/// <summary>
		/// Fired when current sender cannot proof
		/// </summary>
		public event ApplikationKeyEventHandler KeyValidationFailed;

		/// <summary>
		/// Class for proofing Appkey
		/// </summary>
		public ApplikationKey()
		{
		}

		///// <summary>
		///// current sender is valide
		///// </summary>
		//public bool IsValideKey
		//{
		//    get
		//    {
		//        return isValideKey;
		//    }
		//}

		/// <summary>
		/// validate current sender
		/// </summary>
		/// <param name="sender">current sender</param>
		public void validateKey(string appkey)
		{
			WebClient wc = new WebClient();
			wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
			wc.DownloadStringAsync(Settings.ApplicationKeyUrl);
		}

		void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
		{
			if (e.Error != null)
			{
				OnKeyValidationFailed();
			}
			else
			{
				var doc = XDocument.Parse(e.Result);
				try
				{
					isValideKey = (doc.Descendants("isvalidekey").First().Value.ToLower() == "true");
				}
				catch (Exception ex)
				{
				}
				OnKeyValidationSuccessed();
			}
		}


	
		
		private void OnKeyValidationFailed()
		{
			if (KeyValidationFailed != null)
				KeyValidationFailed(this, new ApplikationKeyEventArgs { IsValideKey = isValideKey, });
		}

		private void OnKeyValidationSuccessed()
		{
			if (KeyValidationSuccessed != null)
				KeyValidationSuccessed(this, new ApplikationKeyEventArgs { IsValideKey = isValideKey, });
		}
	}

	public delegate void ApplikationKeyEventHandler(object sender, ApplikationKeyEventArgs e);
}
