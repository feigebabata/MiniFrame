using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;

public class HttpServerMng : MngBase 
{
	private List<PhPRequestData> m_requests = new List<PhPRequestData>();	//持续请求列表
	private float m_requestDelay=1.0f;	

	public override void Init()
	{
		EventTool.Add<PhPRequestData>(EventName.HttpServerMng.AddRequest,onAddRequest);
		EventTool.Add<string>(EventName.HttpServerMng.RemoveRequest,onRemoveRequest);
		EventTool.Add<PhPRequestData>(EventName.HttpServerMng.RequestOnly,onRequestOnly);
		EventTool.Add<string>(EventName.Loginl.LoginSuccess,onLoginSuccess);

		base.Init();
		StartCoroutine( request() );
	}

	private void onAddRequest (PhPRequestData _data)
	{
		if(m_requests.Find( (data)=>{ return data.Path.Equals(_data.Path);} ) ==null)
		{
			m_requests.Add(_data);
		}
	}

	private void onRemoveRequest (string _path)
	{
		PhPRequestData PRdata = m_requests.Find( (data)=>{ return data.Path.Equals( _path );});
		m_requests.Remove(PRdata);
	}

	private IEnumerator request()
	{
		while(true)
		{
			if(m_requests.Count>0)
			{
				foreach(PhPRequestData req in m_requests)
				{
					PHPRequest php = gameObject.AddComponent<PHPRequest>();
					php.Requre( req );
				}
			}
			yield return new WaitForSeconds( m_requestDelay );
		}
	}

	private void onRequestOnly(PhPRequestData _data)
	{
		PHPRequest php = gameObject.AddComponent<PHPRequest>();
		php.Requre( _data );
	}

	private void onLoginSuccess(string _token)
	{
		PhPRequestData.Token = _token;

		WWWForm form = new WWWForm();
		PhPRequestData reqData = new PhPRequestData(HttpPath.PersonalCenter.BaseInfo.GetBaseInfo,form,(_data)=>
			{
				if(_data["Code"].ToString() == PhPJsonData.SUCCESS)
				{
					MapMng.Self_Info.SetBaseInfo(_data["Data"]["userinfo"]);
					MapMng.Self_Info.SetPhotos(_data["Data"]["photo"]);
					MapMng.Self_Info.SetAddress(_data["Data"]["address"]);
				}
			});
		onRequestOnly (reqData);
	}

}

public class PhPRequestData
{
	public static string Token=string.Empty;
	public string Path;
	public WWWForm Form;
	public Action<JsonData> Finsh;

	public PhPRequestData(string _path,WWWForm _form,Action<JsonData> _finsh)
	{
		Path = _path;
		Form = _form;
		Finsh = _finsh;

		if(!string.IsNullOrEmpty(Token))
		{
			Form.AddField("token",Token);
		}
	}

	public PhPRequestData(string _path,Action<JsonData> _finsh)
	{
		Path = _path;
		Finsh = _finsh;
	}
}

public static class PhPJsonData
{
	public const string SUCCESS = "0";
	public const string FAILURE = "1";
	public const string NONE = "2";
}
