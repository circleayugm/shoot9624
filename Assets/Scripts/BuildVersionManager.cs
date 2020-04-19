using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using UnityEngine;

public class BuildVersionManager : MonoBehaviour {
	static string FILENAME = "buildver.txt";
	static string ApplicationName = "s9624";            //アプリケーション識別名.
	static public string outputFileName = "DebugLog.utf8.txt";     //ファイルネーム.
	static public string outputFilePath = "/";

	// Use this for initialization
	void Awake () {
		switch (Application.platform)               //プラットフォーム別でファイル保存位置を変更する.
		{
			case RuntimePlatform.WindowsPlayer:
			case RuntimePlatform.WindowsEditor:
			case RuntimePlatform.OSXPlayer:
			case RuntimePlatform.OSXEditor:
			case RuntimePlatform.WebGLPlayer:
				outputFilePath = Application.dataPath;
				break;
			case RuntimePlatform.Android:
				outputFilePath = Application.persistentDataPath;
				//					outputFilePath = "/data/data/jp.gamersuniverse.mphomerun/files";	//Android本体に保存される場合の場所.
				break;
			case RuntimePlatform.IPhonePlayer:
				outputFilePath = Application.temporaryCachePath;
				break;
			default:
				outputFilePath = Application.dataPath;
				break;
		}
		ReadFile();


	}

	// Update is called once per frame
	void Update () {
		
	}




	//-------------------------------------------------------------------
	//	static public void Log(string txt)
	//		テキストの内容をログに書き出す(外部呼び出し)
	//	string txt=書き出す内容
	//-------------------------------------------------------------------
	static public void Log(string txt)
	{
		//		if (CompileSW.Debug == false) return;	//デバッグモードでなければ機能しない.
		string s = ApplicationName + " : " + DateTime.Now.ToString() + " : " + txt;     //フォーマット整えて書き出し.
		WriteFile(s);
	}



	//-------------------------------------------------------------------
	//	static void WriteFile(string txt)
	//		実際にファイルに書き出す
	//	string txt=書き出す内容
	//-------------------------------------------------------------------
	static void WriteFile(string txt)
	{
		FileInfo fi = new FileInfo(outputFilePath + "/" + outputFileName);  //ファイル作成.
		try
		{
			using (StreamWriter sw = fi.AppendText())                           //追記モード.
			{
				sw.WriteLine(txt);                                                  //ファイル出力.
			}
		}
		catch (Exception)
		{
		}
	}



	//-------------------------------------------------------------------
	//	void ReadFile()
	//		ファイルから読み込む
	//	string txt=書き出す内容
	//-------------------------------------------------------------------
	void ReadFile()
	{
		//		if (CompileSW.Debug == false) return;	//デバッグモードでなければ機能しない.
		FileInfo fi = new FileInfo(outputFilePath + "/" + outputFileName);  //ファイルあるかチェック.
		try
		{                                                                       //あった場合.
			using (StreamReader sr = new StreamReader(fi.OpenRead(), Encoding.UTF8))    //UTF8として読み込み.
			{
				string s = sr.ReadToEnd();
			}
		}
		catch (Exception)
		{                                                                       //なかった場合.
			Log("1" + Environment.NewLine);                       //新規ファイルメッセージ使ってファイル作成.
		}
	}



	//-------------------------------------------------------------------
	//	void DestroyFile()
	//		ファイルが存在したら削除する
	//-------------------------------------------------------------------
	void DestroyFile()
	{
		//		if (CompileSW.Debug == false) return;	//デバッグモードでなければ機能しない.
		FileInfo fi = new FileInfo(outputFilePath + "/" + outputFileName);  //ファイルあるかチェック.
		if (fi.Exists == true)                                              //ファイルあったら.
		{
			fi.Delete();                                                        //削除して作り直し(作り直し自体はReadFileがする).
		}
	}
}
