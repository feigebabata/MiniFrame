using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelBase : MonoBehaviour {

	public virtual void Init()
	{
		GetComponent<ViewBase>().Init();
	}
}
