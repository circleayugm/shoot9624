using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoLoop : MonoBehaviour {

	[SerializeField]
	public PadControlManager PADCTRL;       // パッドコントロールマネージャ
	[SerializeField]
	public ObjectManager OBJMANAGE;         // オブジェクトマネージャ
	[SerializeField]
	public Image[] SPR_PICTURE;             // 絵を入れておく場所(nullの場合絵が存在しない)
	[Space(20)]
	[SerializeField]
	public RectTransform[] ROOT_MESSAGES;   // メッセージ類の呼び出し
	[Space(20)]
	[SerializeField]
	public Text[] MSG_CURSOR;               // 各画面のカーソル(nullの場合カーソルが存在しない)
	[Space(20)]
	[SerializeField]
	public Text MSG_PRESS_START;			// スタートボタンで開始のテキスト(点滅)
	[SerializeField]
	public Text MSG_STANDBY_TEXT;           // ゲーム開始前デモのテキスト(1文字づつ表示)
	[SerializeField]
	public Text MSG_VOLUME_BGM;             // セットアップ画面の音量(BGM・可変)
	[SerializeField]
	public Text MSG_VOLUME_SE;              // セットアップ画面の音量(SE・可変)
	[Space(20)]
	[SerializeField]
	public Text MSG_SCORE;					// スコア数値部(1234567890)(右詰め)
	[SerializeField]
	public Text MSG_TIME;                   // タイム数値部(00.0)(右詰め)
	[SerializeField]
	public MainGameLoop GAME;				// ゲームオブジェクト

	public enum MODE	// ゲームモード
	{
		INIT = -1,			// 初期化中・初期化完了後にタイトルに移行
		TITLE,				// タイトル
		SETUP,				// セットアップ(音量など設定)
		BRIEFING_NORMAL,	// ゲーム開始前デモ(ノーマル)
		BRIEFING_EXPERT,	// ゲーム開始前デモ(エキスパート)
		GAME_NORMAL,		// ゲームクリア(ノーマル)
		GAME_EXPERT,		// ゲームクリア(エキスパート)
		GAMEOVER,			// ゲームオーバー
		EXIT,				// アプリ終了
		MAX
	}

	const string TEXT_STANDBY = "CONSOLE START\n  ENGINE OK\n  ENERGY OK\n  STAB OK\n  CONTROL OK\n\n  ALL SYSTEM IS\n	COMPLETE\n\n  HELLO, MY MASTER!";

	const int CURSOR_SELECTION_MAX = 4;		// カーソル選択項目数
	readonly int[] CURSOR_POSITION = new int[CURSOR_SELECTION_MAX] { -32, -64, -96, -128 };	// それぞれの項目のカーソルY座標

	const int CURSOR_MOVE_FIRST_NEXT = 50;	// スティック入力後1回目のリピート時間
	const int CURSOR_MOVE_SECOND_NEXT = 10; // 入力後2回目以降のリピート時間

	const float VOLUME_BGM_DEFAULT = 0.71f;	// 標準設定の音量(BGM)
	const float VOLUME_SE_DEFAULT = 0.71f;  // 標準設定の音量(SE)

	int volume_cnt = -1;
	bool volume_first_clear = false;
	float volume_bgm = 0.71f;			// 音量(BGM)
	float volume_se = 0.71f;            // 音量(SE)

	int cursor_cnt = -1;				// カーソルカウンタ(-1の場合は入力ナシ)
	bool cursor_first_clear = false;    // 入力後1回目を突破した？(true=突破・false=していない)
	int cursor_pos = 0;					// カーソル初期位置(基本0)

	private MODE mode = MODE.INIT;		// ゲームモード
	public MODE Mode
	{
		get
		{
			return mode;
		}
		set
		{
			mode = value;
		}
	}
	int count = -1;		// 基本カウンタ


	// Use this for initialization
	void Start () {
		mode = MODE.TITLE;
#if UNITY_WEBGL
		Application.targetFrameRate = 58;
#else
		Application.targetFrameRate = 60;
#endif
		volume_bgm = SoundManager.Instance.volume.BGM;
		volume_se = SoundManager.Instance.volume.SE;
	}
	
	// Update is called once per frame
	void Update () {
		if (OBJMANAGE.boot == false)    // ObjectManagerの初期化を待つ
		{
			return;
		}
		switch (mode){
			case MODE.TITLE:
				{
					int sel = TitleLoop();
					if (sel == 0)
					{
						mode = MODE.BRIEFING_NORMAL;
						count = -1;
						SoundManager.Instance.PlaySE((int)SoundHeader.SE.DECIDE);
						MSG_STANDBY_TEXT.text = "";
						SPR_PICTURE[(int)MODE.TITLE].enabled = false;
						ROOT_MESSAGES[(int)MODE.TITLE].gameObject.SetActive(false);
						SPR_PICTURE[(int)MODE.BRIEFING_NORMAL].enabled = true;
						ROOT_MESSAGES[(int)MODE.BRIEFING_NORMAL].gameObject.SetActive(true);
					}
					else if (sel == 1)
					{
						mode = MODE.BRIEFING_EXPERT;
						count = -1;
						SoundManager.Instance.PlaySE((int)SoundHeader.SE.DECIDE);
						MSG_STANDBY_TEXT.text = "";
						SPR_PICTURE[(int)MODE.TITLE].enabled = false;
						ROOT_MESSAGES[(int)MODE.TITLE].gameObject.SetActive(false);
						SPR_PICTURE[(int)MODE.BRIEFING_EXPERT].enabled = true;
						ROOT_MESSAGES[(int)MODE.BRIEFING_EXPERT].gameObject.SetActive(true);
					}
					else if (sel == 2)
					{
						mode = MODE.SETUP;
						SoundManager.Instance.PlayBGM((int)SoundHeader.BGM.SETUP_LOOP);
						SoundManager.Instance.PlaySE((int)SoundHeader.SE.DECIDE);
						cursor_pos = 0; // カーソル位置を音量(BGM)にしておく
						MSG_VOLUME_BGM.text = "      BGM  " + (int)(volume_bgm * 100.0f) + "%";
						MSG_VOLUME_SE.text = "      SE    " + (int)(volume_se * 100.0f) + "%";
						MSG_CURSOR[(int)MODE.SETUP].transform.localPosition = new Vector3(0, CURSOR_POSITION[cursor_pos], 0);
						ROOT_MESSAGES[(int)MODE.TITLE].gameObject.SetActive(false);
						ROOT_MESSAGES[(int)MODE.SETUP].gameObject.SetActive(true);
					}
					else if (sel == 3)
					{
						mode = MODE.EXIT;
						SoundManager.Instance.PlaySE((int)SoundHeader.SE.DECIDE);
						SPR_PICTURE[(int)MODE.TITLE].enabled = false;
						ROOT_MESSAGES[(int)MODE.TITLE].gameObject.SetActive(false);
						SPR_PICTURE[(int)MODE.EXIT].enabled = true;
						ROOT_MESSAGES[(int)MODE.EXIT].gameObject.SetActive(true);
					}
				}
				break;
			case MODE.SETUP:
				{
					int sel = SetupLoop();
					if (sel == 3)
					{
						mode = MODE.TITLE;
						SoundManager.Instance.PlaySE((int)SoundHeader.SE.DECIDE);
						SoundManager.Instance.StopBGM();
						SoundManager.Instance.SaveVolume(SoundManager.Instance.volume.BGM, SoundManager.Instance.volume.SE, SoundManager.Instance.volume.Voice);
						cursor_pos = 2;     // カーソル位置をセットアップにしておく
						ROOT_MESSAGES[(int)MODE.SETUP].gameObject.SetActive(false);
						ROOT_MESSAGES[(int)MODE.TITLE].gameObject.SetActive(true);
						MSG_CURSOR[(int)MODE.TITLE].transform.localPosition = new Vector3(0, CURSOR_POSITION[cursor_pos], 0);
					}
					else if (sel == 2)
					{
						SoundManager.Instance.PlaySE((int)SoundHeader.SE.DECIDE);
						volume_bgm = VOLUME_BGM_DEFAULT;
						SoundManager.Instance.volume.BGM = volume_bgm;
						volume_se = VOLUME_SE_DEFAULT;
						SoundManager.Instance.volume.SE = volume_se;
						MSG_VOLUME_BGM.text = "      BGM  " + (int)(volume_bgm * 100.0f) + "%";
						MSG_VOLUME_SE.text = "      SE    " + (int)(volume_se * 100.0f) + "%";
					}
				}
				break;
			case MODE.BRIEFING_NORMAL:
				{
					int sel = BriefingLoop();
				}
				break;
			case MODE.BRIEFING_EXPERT:
				{
					int sel = BriefingLoop();
				}
				break;
			case MODE.GAME_NORMAL:
			case MODE.GAME_EXPERT:
				count = -1;
				break;
			case MODE.EXIT:
				if (PADCTRL.pad.button_oneshot[(int)PadData.BUTTON.A]==true)
				{
					Application.Quit();
				}
				break;
			default:
				break;
		}
		count++;
	}



	int TitleLoop(){
		int ret = -1;   // 項目が選ばれた場合に戻る数値



		if (PADCTRL.pad.pad.y != 0)
		{
			if (cursor_cnt == -1)
			{
				cursor_cnt = 0;
			}
			if (0 < PADCTRL.pad.pad.y)  // 上に入っている
			{
				if (cursor_cnt == 0)	// 入力直後・リピート直後
				{
					SoundManager.Instance.PlaySE((int)SoundHeader.SE.CURSOR_MOVE);
					cursor_pos--;
					if (cursor_pos == -1)
					{
						cursor_pos = 3;
					}
					MSG_CURSOR[(int)mode].transform.localPosition = new Vector3(0, CURSOR_POSITION[cursor_pos], 0);
				}
				cursor_cnt++;
				if (cursor_first_clear == true)
				{
					if (cursor_cnt == CURSOR_MOVE_SECOND_NEXT)
					{
						cursor_cnt = 0;
					}
				}
				else
				{
					if (cursor_cnt == CURSOR_MOVE_FIRST_NEXT)
					{
						cursor_cnt = 0;
						cursor_first_clear = true;
					}
				}
			}
			else
			{                       // 下に入っている
				if (cursor_cnt == 0)    // 入力直後・リピート直後
				{
					SoundManager.Instance.PlaySE((int)SoundHeader.SE.CURSOR_MOVE);
					cursor_pos++;
					if (cursor_pos == 4)
					{
						cursor_pos = 0;
					}
					MSG_CURSOR[(int)mode].transform.localPosition = new Vector3(0, CURSOR_POSITION[cursor_pos], 0);
				}
				cursor_cnt++;
				if (cursor_first_clear == true)
				{
					if (cursor_cnt == CURSOR_MOVE_SECOND_NEXT)
					{
						cursor_cnt = 0;
					}
				}
				else
				{
					if (cursor_cnt == CURSOR_MOVE_FIRST_NEXT)
					{
						cursor_cnt = 0;
						cursor_first_clear = true;
					}
				}
			}
		}
		else
		{                           // 入力されていない
			cursor_cnt = -1;			// カーソルリピートを初期化
			cursor_first_clear = false;
		}

		if (PADCTRL.pad.button_oneshot[(int)PadData.BUTTON.A] == true)	// ボタン入力があった時に状況を返す
		{
			ret = cursor_pos;
		}
		return ret;
	}



	int SetupLoop()
	{
		int ret = -1;   // 項目が選ばれた場合に戻る数値
		bool isVolume = false;  // ボリューム調整中？(true=調整中・false=そうではない)

		if (PADCTRL.pad.pad.x != 0)
		{
			if (volume_cnt == -1)
			{
				volume_cnt = 0;
			}
			if (0 < PADCTRL.pad.pad.x)  // 右に入っている
			{
				isVolume = true;
				if (volume_cnt == 0)    // 入力直後・リピート直後
				{
					switch (cursor_pos)
					{
						case 0:
							volume_bgm += 0.01f;
							if (1.00f < volume_bgm)
							{
								volume_bgm = 1.00f;
							}
							else
							{
								SoundManager.Instance.PlaySE((int)SoundHeader.SE.CURSOR_MOVE);
							}
							SoundManager.Instance.volume.BGM = volume_bgm;
							MSG_VOLUME_BGM.text = "      BGM  " + (int)(volume_bgm * 100.0f) + "%";
							break;
						case 1:
							volume_se += 0.01f;
							if (1.00f < volume_se)
							{
								volume_se = 1.00f;
							}
							else
							{
								SoundManager.Instance.PlaySE((int)SoundHeader.SE.CURSOR_MOVE);
							}
							SoundManager.Instance.volume.SE = volume_se;
							MSG_VOLUME_SE.text = "      SE    " + (int)(volume_se * 100.0f) + "%";
							break;
						default:
							break;
					}
				}
				volume_cnt++;
				if (volume_first_clear == true)
				{
					if (volume_cnt == CURSOR_MOVE_SECOND_NEXT)
					{
						volume_cnt = 0;
					}
				}
				else
				{
					if (volume_cnt == CURSOR_MOVE_FIRST_NEXT)
					{
						volume_cnt = 0;
						volume_first_clear = true;
					}
				}
			}
			else
			{                       // 左に入っている
				isVolume = true;
				if (volume_cnt == 0)    // 入力直後・リピート直後
				{
					switch (cursor_pos)
					{
						case 0:
							volume_bgm -= 0.01f;
							if (0.00f > volume_bgm)
							{
								volume_bgm = 0.00f;
							}
							else
							{
								SoundManager.Instance.PlaySE((int)SoundHeader.SE.CURSOR_MOVE);
							}
							SoundManager.Instance.volume.BGM = volume_bgm;
							MSG_VOLUME_BGM.text = "      BGM  " + (int)(volume_bgm * 100.0f) + "%";
							break;
						case 1:
							volume_se -= 0.01f;
							if (0.00f > volume_se)
							{
								volume_se = 0.00f;
							}
							else
							{
								SoundManager.Instance.PlaySE((int)SoundHeader.SE.CURSOR_MOVE);
							}
							SoundManager.Instance.volume.SE = volume_se;
							MSG_VOLUME_SE.text = "      SE    " + (int)(volume_se * 100.0f) + "%";
							break;
						default:
							break;
					}
				}
				volume_cnt++;
				if (volume_first_clear == true)
				{
					if (volume_cnt == CURSOR_MOVE_SECOND_NEXT)
					{
						volume_cnt = 0;
					}
				}
				else
				{
					if (volume_cnt == CURSOR_MOVE_FIRST_NEXT)
					{
						volume_cnt = 0;
						volume_first_clear = true;
					}
				}
			}
		}
		else
		{                           // 入力されていない
			volume_cnt = -1;            // カーソルリピートを初期化
			volume_first_clear = false;
			isVolume = false;
		}

		if (PADCTRL.pad.pad.y != 0)
		{
			if (isVolume == false)
			{
				if (cursor_cnt == -1)
				{
					cursor_cnt = 0;
				}
				if (0 < PADCTRL.pad.pad.y)  // 上に入っている
				{
					if (cursor_cnt == 0)    // 入力直後・リピート直後
					{
						SoundManager.Instance.PlaySE((int)SoundHeader.SE.CURSOR_MOVE);
						cursor_pos--;
						if (cursor_pos == -1)
						{
							cursor_pos = 3;
						}
						MSG_CURSOR[(int)mode].transform.localPosition = new Vector3(0, CURSOR_POSITION[cursor_pos], 0);
					}
					cursor_cnt++;
					if (cursor_first_clear == true)
					{
						if (cursor_cnt == CURSOR_MOVE_SECOND_NEXT)
						{
							cursor_cnt = 0;
						}
					}
					else
					{
						if (cursor_cnt == CURSOR_MOVE_FIRST_NEXT)
						{
							cursor_cnt = 0;
							cursor_first_clear = true;
						}
					}
				}
				else
				{                       // 下に入っている
					if (cursor_cnt == 0)    // 入力直後・リピート直後
					{
						SoundManager.Instance.PlaySE((int)SoundHeader.SE.CURSOR_MOVE);
						cursor_pos++;
						if (cursor_pos == 4)
						{
							cursor_pos = 0;
						}
						MSG_CURSOR[(int)mode].transform.localPosition = new Vector3(0, CURSOR_POSITION[cursor_pos], 0);
					}
					cursor_cnt++;
					if (cursor_first_clear == true)
					{
						if (cursor_cnt == CURSOR_MOVE_SECOND_NEXT)
						{
							cursor_cnt = 0;
						}
					}
					else
					{
						if (cursor_cnt == CURSOR_MOVE_FIRST_NEXT)
						{
							cursor_cnt = 0;
							cursor_first_clear = true;
						}
					}
				}
			}
		}
		else
		{                           // 入力されていない
			cursor_cnt = -1;            // カーソルリピートを初期化
			cursor_first_clear = false;
		}

		if (PADCTRL.pad.button_oneshot[(int)PadData.BUTTON.A] == true)  // ボタン入力があった時に状況を返す
		{
			ret = cursor_pos;
		}
		return ret;

	}




	int BriefingLoop()
	{
		int ret = -1;
		string str = "";
		if (count == 0)
		{
			SoundManager.Instance.PlayBGM((int)SoundHeader.BGM.BRIEFING);
			MSG_STANDBY_TEXT.text = "";
			MSG_STANDBY_TEXT.enabled = true;
			MSG_SCORE.text = "0";
			MSG_TIME.text = "60.0";
		}
		if (((count / 2) + 1) < TEXT_STANDBY.Length)
		{
			SoundManager.Instance.PlaySE((int)SoundHeader.SE.MESSAGE);
			for (int i = 0; i < ((count / 2) + 1); i++)
			{
				str += TEXT_STANDBY[i];
			}
		}
		else
		{
			str = TEXT_STANDBY;
		}
		MSG_STANDBY_TEXT.text = str;
		if (((count >> 6) & 1) == 0)
		{
			MSG_PRESS_START.enabled = false;
		}
		else
		{
			MSG_PRESS_START.enabled = true;
		}
		if (count== 600)
		{
			SoundManager.Instance.StopBGM();
		}
		if (PADCTRL.pad.button_oneshot[(int)PadData.BUTTON.A] == true)
		{
			switch (mode)
			{
				case MODE.BRIEFING_NORMAL:
					mode = MODE.GAME_NORMAL;
					count = 0;
					SPR_PICTURE[(int)MODE.BRIEFING_NORMAL].enabled = false;
					MSG_PRESS_START.enabled = false;
					MSG_STANDBY_TEXT.enabled = false;
					GAME.playing = true;
					SoundManager.Instance.PlayBGM((int)SoundHeader.BGM.GAME_NORMAL);
					break;
				case MODE.BRIEFING_EXPERT:
					mode = MODE.GAME_EXPERT;
					SPR_PICTURE[(int)MODE.BRIEFING_EXPERT].enabled = false;
					MSG_PRESS_START.enabled = false;
					MSG_STANDBY_TEXT.enabled = false;
					count = 0;
					GAME.playing = true;
					SoundManager.Instance.PlayBGM((int)SoundHeader.BGM.GAME_EXPERT);
					break;
			}
		}
		return ret;
	}



	public void ReturnTitle()
	{
		mode = MODE.TITLE;
		//cursor_pos = 0;     // カーソル位置をノーマルに戻す
		SPR_PICTURE[(int)MODE.TITLE].enabled = true;
		ROOT_MESSAGES[(int)MODE.TITLE].gameObject.SetActive(true);
		MSG_CURSOR[(int)MODE.TITLE].transform.localPosition = new Vector3(0, CURSOR_POSITION[cursor_pos], 0);

	}
}
