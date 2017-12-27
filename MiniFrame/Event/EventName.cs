using System.Collections;
using System.Collections.Generic;
public static class EventName 
{
	public static class PanelBase
	{
		public const string Init = "PanelBase.Init";
		public const string Open = "PanelBase.Open";
		public const string Close = "PanelBase.Close";
		public const string Open2 = "PanelBase.Open2";
		public const string Close2 = "PanelBase.Close2";
		public const string OpenFinsh = "PanelBase.OpenFinsh";
		public const string CloseFinsh = "PanelBase.CloseFinsh";
	}

	public static class Audio
	{
		public const string PlayBG = "Audio.PlayBG";
		public const string PlayOnly = "Audio.PlayOnly";
	}

	public static class SocketClient
	{
		public const string Send = "SocketClient.Send";
		public const string ConnectServer = "SocketClient.ConnectServer";
		public const string CloseConnect = "SocketClient.CloseConnect";
		public const string ReceiveData = "SocketClient.ReceiveData";
	}

	public static class HttpServerMng
	{
		public const string AddRequest = "HttpServerMng.AddRequest";
		public const string RemoveRequest = "HttpServerMng.RemoveRequest";
		public const string RequestOnly = "HttpServerMng.RequestOnly";
	}

	public static class Map
	{
		public const string Open = "Map.Open";
		public const string Close = "Map.Close";
		public const string ResetMap = "Map.ResetMap";
		public const string DragMap = "Map.DragMap";
		public const string SetMiniMapPos = "Map.SetMiniMapPos";
	}

	public static class Manor
	{
		public const string Open = "Manor.Open";
		public const string Close = "Manor.Close";
		public const string ShowManor = "Manor.ShowManor";
	}

	public static class Loading
	{
		public const string Open = "Loading.Open";
		public const string Close = "Loading.Close";
		public const string Reset = "Loading.Reset";
	}

	public static class SystemTip
	{
		public const string ShowTip = "SystemTip.ShowTip";
	}

	public static class Loginl
	{
		public const string LoginSuccess = "Loginl.LoginSuccess";
	}

	public static class GreensShop
	{
		
	}
}
