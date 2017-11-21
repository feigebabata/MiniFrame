using System.Collections;
using System.Collections.Generic;
public class EventName 
{
	public class PanelBase
	{
		public static readonly string Init = "PanelBase.Init";
		public static readonly string Open = "PanelBase.Open";
		public static readonly string Close = "PanelBase.Close";
		public static readonly string InitFinsh = "PanelBase.InitFinsh";
		public static readonly string OpenFinsh = "PanelBase.OpenFinsh";
		public static readonly string CloseFinsh = "PanelBase.CloseFinsh";
	}

	public class Audio
	{
		public static readonly string PlayBG = "Audio.PlayBG";
		public static readonly string PlayOnly = "Audio.PlayOnly";
	}
}
