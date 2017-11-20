using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
