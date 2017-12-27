using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PanelBaseMng : MngBase
{
	private Dictionary<string,PanelBase> panelBases = new Dictionary<string, PanelBase>();

	private const string PBDIR = "PanelBases/";
	public override void Init()
	{
		EventTool.Add<string>(EventName.PanelBase.Init,initPanelBase);
		EventTool.Add<string>(EventName.PanelBase.Open,(panelBaseName)=>{openPanelBase(panelBaseName,null);});
		EventTool.Add<string>(EventName.PanelBase.Close,(panelBaseName)=>{closePanelBase(panelBaseName,null);});
		EventTool.Add<string,PanelBaseEnterAni[]>(EventName.PanelBase.Open2,openPanelBase);
		EventTool.Add<string,PanelBaseExitAni[]>(EventName.PanelBase.Close2,closePanelBase);
		
	}

	private void initPanelBase(string panelBaseName)
	{
		if(panelBases.ContainsKey(panelBaseName))
		{
			Debug.LogWarning("[PanelBaseMng.initPanelBase]"+panelBaseName+"已初始化过！");
			return;
		}
		GameObject go = LocalDataTool.ResLoad(PBDIR+panelBaseName) as GameObject;
		go = Instantiate<GameObject>(go);
		go.name=panelBaseName;
		PanelBase pb = go.GetComponent<PanelBase>();
		go.transform.SetParent(transform.GetChild((int)pb.Layer),false);
		panelBases.Add(panelBaseName,pb);
		pb.Init();
	}

	private void openPanelBase(string panelBaseName,PanelBaseEnterAni[] enterAni)
	{
		if(!panelBases.ContainsKey(panelBaseName))
		{
			initPanelBase(panelBaseName);
		}
		Transform t = panelBases[panelBaseName].transform;
		t.SetSiblingIndex(t.parent.childCount-1);
		panelBases[panelBaseName].Open(enterAni);
	}

	private void closePanelBase(string panelBaseName,PanelBaseExitAni[] exitAni)
	{
		if(!panelBases.ContainsKey(panelBaseName))
		{
//			Debug.LogWarning("[PanelBaseMng.closePanelBase]"+panelBaseName+"尚未加载！");
			return;
		}
		if(!panelBases[panelBaseName].IsOpen)
		{
//			Debug.LogWarning("[PanelBaseMng.closePanelBase]"+panelBaseName+"尚未打开！");
			return;
		}
		panelBases[panelBaseName].Close(exitAni);
	}
}
