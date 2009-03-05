using System;
namespace MultiMediaPlayer.MediaData
{
	interface IMediaItem
	{
		ContentType Content { get; set; }
		TimeSpan Duration { get; set; }
		bool IsFirst { get; set; }
		bool IsLast { get; set; }
		bool IsSkippable { get; set; }
		MediaType ItemType { get; set; }
		System.Collections.Generic.List<Marker> Markers { get; set; }
		Meta Metadata { get; set; }
		TimeSpan SkipAfter { get; set; }
		string ThumbUrl { get; set; }
		Tracking TrackingData { get; set; }
	}
}
