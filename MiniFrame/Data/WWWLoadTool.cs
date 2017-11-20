using UnityEngine;
using System.Collections;
using System;

public class WWWLoadTool : MonoBehaviour
{
    private WWWForm _from = null;
    private string _ip = string.Empty;
    private string _str = string.Empty;
    private Action<string> _callBack;
    public void LoadStringByPHP(string _Ip, string _Str, Action<string> _CallBack, WWWForm _From = null)
    {
        this._str = _Str;
        this._callBack = _CallBack;
        this._ip = _Ip;
        this._from = _From;

        this.StartCoroutine(this.loadStringByPHP());
    }
    IEnumerator loadStringByPHP()
    {
        yield return 0;
        WWW w = null;
        if (this._from == null)
            w = new WWW(this._ip + this._str);
        else
            w = new WWW(this._ip + this._str, this._from);
        yield return w;
        if (this._callBack != null && w != null)
        {
            if (string.IsNullOrEmpty(w.error))
                this._callBack(w.text);
            else
            {
                Debug.LogError("错误：" + w.error);
            }
        }
        yield return new WaitForEndOfFrame();
        GameObject.Destroy(this);
    }
}