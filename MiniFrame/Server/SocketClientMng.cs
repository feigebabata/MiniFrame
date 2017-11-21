using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;

public class SocketClientMng : MngBase
{

	private Socket socket;
	private byte[] buffer = new byte[m_BufferLength];
    private const int m_BufferLength = 1024*1024;
    private int m_BufferSize=0; //数组的实际占用的长度

	public override void Init()
	{
		SocketUnPack.Init();
		EventTool.Add<byte[]>(EventName.SocketClient.Send,send);
		EventTool.Add<string,int>(EventName.SocketClient.ConnectServer,connectServer);
		EventTool.Add(EventName.SocketClient.CloseConnect,closeConnect);
	}

	private void send(byte[] sendData)
	{
		if(socket!=null)
		{
			try
			{
				socket.BeginSend(sendData,0,sendData.Length,0,new AsyncCallback((ar)=>
					{
						try
						{
							Socket handler = (Socket)ar.AsyncState;
							handler.EndSend(ar);
						}
						catch(Exception e)
						{
							socket = null;
							Debug.LogError("[ServerMng.Send]信息发送失败:"+e);
						}
					}),socket);
			}
			catch(Exception e)
			{
				socket = null;
				Debug.LogError("[ServerMng.Send]信息发送失败:"+e);
			}
			
		}
		else
		{
			Debug.LogError("[ServerMng.Send]socket为空！");
		}
	}

	private void connectServer(string ipStr,int port)
	{
		if(socket!=null)
		{
			socket.Close();
		}
		socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
		try
		{
			socket.BeginConnect(IPAddress.Parse(ipStr),port,new AsyncCallback((ar)=>
				{
					try
					{
						Socket handler = (Socket)ar.AsyncState;
						handler.EndConnect(ar);
						Debug.Log("[ServerMng.ConnectServer]连接服务器成功");
						receiveData();
					}
					catch(Exception e)
					{
						socket = null;
					}
				}),socket);

		}
		catch(Exception e)
		{
			Debug.LogError("[ServerMng.ConnectServer]连接服务器失败:"+e);
		}
	}

	private void receiveData()
	{
		try
		{
			socket.BeginReceive(buffer,m_BufferSize, m_BufferLength-m_BufferSize,SocketFlags.None,new AsyncCallback((ar)=>
			{
				try
				{
					Socket handler = (Socket)ar.AsyncState;
					int rLength = handler.EndReceive(ar);
					if(rLength>0)
					{
						byte[] rData = unPackHead();
						EventTool.Run<byte[]>(EventName.SocketClient.ReceiveData,rData);
					}
					receiveData();
				}
				catch(Exception e)
				{
					socket = null;
					Debug.LogError("[ServerMng.receiveData]信息接受失败:"+e);
				}
				
			}),socket);
		}
		catch(Exception e)
		{
			socket = null;
			Debug.LogError("[ServerMng.receiveData]信息接受失败:"+e);
		}
	}

	private void closeConnect()
	{
		socket.Close();
		socket=null;
	}

	private byte[] unPackHead()
	{
		byte[] rData=null;
		//接受消息注意粘包问题
		return rData;
	}
}
