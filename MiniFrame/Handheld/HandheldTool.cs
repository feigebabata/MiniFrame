using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class HandheldTool  
{
	[DllImport ("__Internal")]
	static extern float _GetBatteryLevel ();

    public static int GetBatteryLevel()
    {
        int level=-1;
        switch(Application.platform)
        {
            case RuntimePlatform.Android:
                try    
                {    
                    string CapacityString = System.IO.File.ReadAllText("/sys/class/power_supply/battery/capacity");    
                    level = int.Parse(CapacityString);    
                }    
                catch(Exception e)
                {    
                     Debug.LogError("[HandheldTool.GetBatteryLevel]读取Android电量异常:"+e);
                }    
            break;
            case RuntimePlatform.IPhonePlayer:
                level = (int)_GetBatteryLevel();
            break;
        }
        return level;
    }

}
