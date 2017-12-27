using UnityEngine;
using System.Collections;
using System;
using System.Text;
using LitJson;

public class PHPRequest : MonoBehaviour
{
	public const string ERROR = "ERROR";
	private PhPRequestData m_data;
	public void Requre(PhPRequestData _data)
    {
		m_data = _data;

        this.StartCoroutine(this.IeRequre());
    }
    IEnumerator IeRequre()
    {
        yield return 0;
        WWW w = null;
		if (m_data.Form == null)
			w = new WWW(HttpPath.PLAY_IP + m_data.Path);
        else
			w = new WWW(HttpPath.PLAY_IP + m_data.Path, m_data.Form);
        yield return w;
		if (w != null)
        {
			if( string.IsNullOrEmpty( w.error ) ) 
			{
				JsonData jd = null;
				try
				{
					jd = JsonMapper.ToObject(w.text);
					jd.ToJson();
				}
				catch 
				{
					Debug.LogErrorFormat("[PHPRequest.IeRequre] json解析异常！\n{0}\n{1}",m_data.Path,w.text);
				}

				if(jd==null)
				{
					Debug.LogErrorFormat("[PHPRequest.IeRequre] json解析异常！\n{0}\n{1}",m_data.Path,w.text);
				}
				m_data.Finsh(jd);
			}
            else
            {
				Debug.LogErrorFormat("[PHPRequest.IeRequre] {0} {1}",w.error,w.url);
            }
		}
		else
		{
			Debug.LogErrorFormat("[PHPRequest.IeRequre] 请求初始化失败！ \n{0}",w.url);
		}
        yield return new WaitForEndOfFrame();
        GameObject.Destroy(this);
    }
}
