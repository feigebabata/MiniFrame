using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppMng : MngBase
{
	public MngBase[] mngs;
	void Awake()
	{
		main();
	}

	private void main()
	{
		for(int i = 0;i<mngs.Length;++i)
		{
			mngs[i].Init();
		}
	}

	public override void Init()
	{
		
	}
	
}
