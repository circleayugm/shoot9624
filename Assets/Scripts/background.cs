/*
============================================================================================================================
	background.cs
	背景のスクロール
		Circle-AY.Info / U.G.M.
============================================================================================================================
*/

using UnityEngine;
using System.Collections;

public class background : MonoBehaviour {

	public GameObject bg1;				//Inspector;スクロールしたい画像を貼る.
	public float scrollSpeed1 = 0.1f;	//Inspector;速度を決める.



	//------------------------------------------------
	//	void Update()
	//	毎フレーム呼び出される処理
	//------------------------------------------------
	void Update () {
		bg1.GetComponent<Renderer>().material.mainTextureOffset = new Vector2 (0,bg1.GetComponent<Renderer>().material.mainTextureOffset.y - Time.deltaTime * scrollSpeed1);
	}
}
