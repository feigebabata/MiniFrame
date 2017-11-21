using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LocalData 
{
	public static string GetLocalPath()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer:
            case RuntimePlatform.WindowsEditor:
                return Application.streamingAssetsPath;
            case RuntimePlatform.Android:
			case RuntimePlatform.IPhonePlayer:
                return Application.persistentDataPath;
            default:
                return null;
        }

    }

	public static string GetWWWPath()
	{
		return "file:///"+GetLocalPath();
	}

    public static string GetDateNow()
    {
        return DateTime.Now.ToString("yyyy-MM-dd HH:mm");
    }

    public static UnityEngine.Object ResLoad(string path)
    {
        try
        {
           var go = Resources.Load(path);
           if(go!=null)
           {
               return go;
           }else
           {
               Debug.LogError("[LocalData.ResLoad]加载资源为空:"+path);
               return null;
           }
        }
        catch(Exception e)
        {
            Debug.LogError("[LocalData.ResLoad]Resources.Load异常:"+e);
            return null;
        }
    }
}
