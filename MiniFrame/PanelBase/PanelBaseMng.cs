using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PanelBaseMng : MonoBehaviour 
{
	private Dictionary<string,PanelBase> panelBases = new Dictionary<string, PanelBase>();

	private const string PBDIR = "PanelBases/";
	public void Init()
	{
		EventMng.Add<PanelBaseName>(EventName.PanelBase.Init,initPanelBase);
		EventMng.Add<PanelBaseName>(EventName.PanelBase.Open,openPanelBase);
		EventMng.Add<PanelBaseName>(EventName.PanelBase.Close,closePanelBase);

	}

	private void initPanelBase(PanelBaseName panelBaseName)
	{
		if(panelBases.ContainsKey(panelBaseName.ToString()))
		{
			Debug.LogWarning("[PanelBaseMng.initPanelBase]"+panelBaseName+"已初始化过！");
			return;
		}
		GameObject go = Resources.Load<GameObject>(PBDIR+panelBaseName);
		go = Instantiate<GameObject>(go);
		go.name=panelBaseName.ToString();
		PanelBase pb = go.GetComponent<PanelBase>();
		go.transform.SetParent(transform.GetChild((int)pb.Layer),false);
		panelBases.Add(panelBaseName.ToString(),pb);
		pb.Init();
	}

	private void openPanelBase(PanelBaseName panelBaseName)
	{
		if(!panelBases.ContainsKey(panelBaseName.ToString()))
		{
			initPanelBase(panelBaseName);
		}
		Transform t = panelBases[panelBaseName.ToString()].transform;
		t.SetSiblingIndex(t.parent.childCount-1);
		panelBases[panelBaseName.ToString()].Open();
	}

	private void closePanelBase(PanelBaseName panelBaseName)
	{
		if(!panelBases.ContainsKey(panelBaseName.ToString()))
		{
			Debug.LogWarning("[PanelBaseMng.closePanelBase]"+panelBaseName+"尚未加载！");
			return;
		}
		if(!panelBases[panelBaseName.ToString()].IsOpen)
		{
			Debug.LogWarning("[PanelBaseMng.closePanelBase]"+panelBaseName+"尚未打开！");
			return;
		}
		panelBases[panelBaseName.ToString()].Close();
	}
}
