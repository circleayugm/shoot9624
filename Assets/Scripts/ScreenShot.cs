//==================================================================================================
//
//	ScreenShot.cs
//
//	スクリーンショット撮影用
//	GameObject ScreenShotにアタッチして使う
//
//
//==================================================================================================

using UnityEngine;
using System.Collections;

public class ScreenShot : MonoBehaviour
{
	//二重起動抑制制御.
	private static bool created;							//すでに作成されているか？(true=作成されている・false=作成されていない)

	//定数.
	static public string ApplicationName = "scr";			//スクリーンショットの接頭文字.

	//変更される変数.
	static string outputFilePath = "/";						//ファイルパス・起動時に大雑把に振り分け.
	static public string displaySaveFile = "";				//表示用ファイルパスが格納される・外部から読み出し可能.



	//-------------------------------------------------------------------
	//	void Awake()
	//		コンポーネント起動直後に呼ばれる
	//-------------------------------------------------------------------
	void Awake()
	{
//		if (CompileSW.Debug == false) return;	//デバッグモードでなければ機能しない.
		if (created == false)
		{
			DontDestroyOnLoad(this.gameObject);
			created = true;
		}
		else
		{
			Destroy(this.gameObject);
		}

		switch (Application.platform)				//プラットフォーム別でファイル保存位置を変更する.
		{
			case RuntimePlatform.WindowsPlayer:
			case RuntimePlatform.WindowsEditor:
			case RuntimePlatform.OSXPlayer:
			case RuntimePlatform.OSXEditor:
				outputFilePath = Application.dataPath;
				break;
			case RuntimePlatform.Android:
				outputFilePath = Application.persistentDataPath;
				break;
			case RuntimePlatform.IPhonePlayer:
				outputFilePath = Application.temporaryCachePath;
				break;
			default:
				outputFilePath = Application.dataPath;
				break;
		}

	}



	//-------------------------------------------------------------------
	//	void Start()
	//		ゲームオブジェクト配置後に呼ばれる
	//-------------------------------------------------------------------
	void Start()
	{

	}



	//-------------------------------------------------------------------
	//	void Update()
	//		毎フレーム呼ばれる
	//-------------------------------------------------------------------
	void Update()
	{
//		if (CompileSW.Debug == false) return;	//デバッグモードでなければ機能しない.
		if (Input.GetKeyDown(KeyCode.F1))	//スクリーンショット撮影.
		{
			Capture();
		}
	}



	//-------------------------------------------------------------------
	//	public void Capture()
	//		画面キャプチャ取得
	//		外部からも呼ばれる
	//-------------------------------------------------------------------
	static public void Capture()
	{
//		if (CompileSW.Debug == false) return;	//デバッグモードでなければ機能しない.
		System.DateTime d = System.DateTime.Now;
		ScreenCapture.CaptureScreenshot(outputFilePath + "/" + ApplicationName + d.ToString("yyyyMMdd-HHmmss-fff") + ".png");
	}


}
