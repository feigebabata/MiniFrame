using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;


public enum PanelBaseLayer
{
	layer1,
	layer2,
	layer3,
}
public enum PanelBaseEnterAni
{
	MoveEnterUp,
	MoveEnterDown,
	MoveEnterLeft,
	MoveEnterRight,
	Rotate360,
	AlphaEnter,
	ScaleEnter,
}
public enum PanelBaseExitAni
{
	MoveExitUp,
	MoveExitDown,
	MoveExitLeft,
	MoveExitRight,
	Rotate360,
	AlphaExit,
	ScaleExit,
}


public class PanelBase : MonoBehaviour 
{
	[SerializeField]
	private PanelBaseEnterAni[] enterAni;

	[SerializeField]
	private PanelBaseExitAni[] exitAni;


	[SerializeField]
	private PanelBaseLayer layer;

	[SerializeField]
	private float aniTime=1.0f;

	public PanelBaseLayer Layer
	{
		get{return layer;}
	}

	public bool IsOpen
	{
		get;
		private set;
	}

	public void Init()
	{
		gameObject.SetActive(false);
		IsOpen=false;
		if(GetComponent<ModelBase>()!=null)
		{
			GetComponent<ModelBase>().Init();
		}
	}
	public void Open(PanelBaseEnterAni[] _enterAni=null)
	{
		if(_enterAni==null && enterAni!=null)
		{
			_enterAni = enterAni;
		}
		gameObject.SetActive(true);
		IsOpen=true;
		EventTool.Run(EventName.PanelBase.Open+gameObject.name);
		StopAllCoroutines();
		transform.localScale=Vector3.one;
		transform.localPosition = Vector3.zero;
		GetComponent<CanvasGroup>().alpha=1;
		StartCoroutine(playEnterAni(()=>
			{
				EventTool.Run(EventName.PanelBase.OpenFinsh+gameObject.name);
				},_enterAni));
	}
	public void Close(PanelBaseExitAni[] _exitAni=null)
	{
		if(_exitAni==null && exitAni!=null)
		{
			_exitAni = exitAni;
		}
		IsOpen=false;
		EventTool.Run(EventName.PanelBase.Close+gameObject.name);
		StopAllCoroutines();
		StartCoroutine(playExitAni(()=>
			{
				gameObject.SetActive(false);
				EventTool.Run(EventName.PanelBase.CloseFinsh+gameObject.name);
				},_exitAni));
	}

	
	private IEnumerator playEnterAni(Action finsh,PanelBaseEnterAni[] _enterAni)
	{

		if(aniTime>0 && _enterAni!=null && _enterAni.Length>0)
		{
			foreach(PanelBaseEnterAni pba in _enterAni)
			{
				switch(pba)
				{
					case PanelBaseEnterAni.AlphaEnter:
						StartCoroutine(alphaEnterAni());
					break;
					case PanelBaseEnterAni.MoveEnterDown:
						transform.localPosition = Vector3.up*Screen.height;
						transform.DOLocalMove(Vector3.zero,aniTime);
					break;
					case PanelBaseEnterAni.MoveEnterLeft:
						transform.localPosition = Vector3.right*Screen.width;
						transform.DOLocalMove(Vector3.zero,aniTime);
					break;
					case PanelBaseEnterAni.MoveEnterRight:
						transform.localPosition = Vector3.left*Screen.width;
						transform.DOLocalMove(Vector3.zero,aniTime);
					break;
					case PanelBaseEnterAni.MoveEnterUp:
						transform.localPosition = Vector3.down*Screen.height;
						transform.DOLocalMove(Vector3.zero,aniTime);
					break;
					case PanelBaseEnterAni.Rotate360:
						transform.localEulerAngles = Vector3.zero;
						transform.DOLocalRotate(Vector3.forward*360,aniTime,RotateMode.LocalAxisAdd);
					break;
					case PanelBaseEnterAni.ScaleEnter:
						transform.localScale = Vector3.zero;
						transform.DOScale(Vector3.one,aniTime);
					break;

				}
			}
			yield return new WaitForSeconds(aniTime);
		}
		finsh();
	}

	private IEnumerator playExitAni(Action finsh,PanelBaseExitAni[] _exitAni)
	{
		if(aniTime>0 && _exitAni!=null && _exitAni.Length>0)
		{
			foreach(PanelBaseExitAni pba in _exitAni)
			{
				switch(pba)
				{
					case PanelBaseExitAni.AlphaExit:
						StartCoroutine(alphaExitAni());
					break;
					case PanelBaseExitAni.MoveExitDown:
						transform.localPosition = Vector3.zero;
						transform.DOLocalMove(Vector3.down*Screen.height,aniTime);
					break;
					case PanelBaseExitAni.MoveExitLeft:
						transform.localPosition = Vector3.zero;
						transform.DOLocalMove(Vector3.left*Screen.width,aniTime);
					break;
					case PanelBaseExitAni.MoveExitRight:
						transform.localPosition = Vector3.zero;
						transform.DOLocalMove(Vector3.right*Screen.width,aniTime);
					break;
					case PanelBaseExitAni.MoveExitUp:
						transform.localPosition = Vector3.zero;
						transform.DOLocalMove(Vector3.up*Screen.height,aniTime);
					break;
					case PanelBaseExitAni.Rotate360:
						transform.localEulerAngles = Vector3.zero;
						transform.DOLocalRotate(Vector3.forward*360,aniTime,RotateMode.LocalAxisAdd);
					break;
					case PanelBaseExitAni.ScaleExit:
						transform.localScale = Vector3.one;
						transform.DOScale(Vector3.zero,aniTime);
					break;

				}
			}
			yield return new WaitForSeconds(aniTime);
		}
		finsh();
	}

	private IEnumerator alphaEnterAni()
	{
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha=0;
		for(float i=1;i<=100.0f;++i)
		{
			yield return new WaitForSeconds(aniTime/100);
			canvasGroup.alpha=i/100.0f;
		}
	}

	private IEnumerator alphaExitAni()
	{
		CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
		canvasGroup.alpha=1;
		for(float i=99;i>=0;--i)
		{
			yield return new WaitForSeconds(aniTime/100);
			canvasGroup.alpha=i/100.0f;
		}
	}

}
