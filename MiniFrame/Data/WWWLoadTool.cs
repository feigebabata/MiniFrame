using UnityEngine;
using System.Collections;
using System;

public class WWWLoadTool : MonoBehaviour
{
	static GameObject wwwLoadTool_GO;
	public static void Create(string _path,Action<WWW> _finsh)
	{
		if(wwwLoadTool_GO == null)
		{
			wwwLoadTool_GO = new GameObject ("WWWLoadTool");
		}
		wwwLoadTool_GO.AddComponent<WWWLoadTool> ().loadFile (_path, _finsh);
	}

	void loadFile(string _path,Action<WWW> _finsh)
    {
		this.StartCoroutine(this.ieLoadFile(_path,_finsh));
    }
	IEnumerator ieLoadFile(string _path,Action<WWW> _finsh)
    {
        yield return 0;
		WWW w = new WWW(_path);
		yield return w;
		if(w!=null && string.IsNullOrEmpty(w.error))
		{
			_finsh(w);
		}
		else
		{
			_finsh( null );
		}
        yield return new WaitForEndOfFrame();
        GameObject.Destroy(this);
    }
}