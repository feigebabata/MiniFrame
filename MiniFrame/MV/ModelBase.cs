using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelBase : MonoBehaviour {

	PanelBase m_panelBase;

	public virtual void Init()
	{
		if( GetComponent<ViewBase>() != null ) {
			GetComponent<ViewBase>().Init();
		}
		m_panelBase = GetComponent<PanelBase> ();
	}


	public bool IsOpen
	{
		get
		{ 
			return m_panelBase.IsOpen; 
		}
	}
}
