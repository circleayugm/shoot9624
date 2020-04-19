/* ==========================================================================================================
 * 
 *		SoundHeader.cs
 *		Circle-AY.Info / U.G.M.
 *		
 *		音周りの定義ファイル
 *		番号で呼ぶと見通しが悪いので名前を定義する
 *		SoundManagerの再生関数と同時に使われることを想定している
 * 
 *		20150321	とりあえず作成
 * 
 * ========================================================================================================== 
 */

using UnityEngine;
using System.Collections;

public class SoundHeader
{

	//曲列挙.
	public enum BGM
	{
		BRIEFING,
		GAME_NORMAL,
		GAME_EXPERT,
		GAME_CLEAR,
		GAME_OVER,
		SETUP_LOOP,
		Count
	}

	//SE列挙.
	public enum SE
	{
		DECIDE,
		SHIP_SHOT,
		SHIP_MISS,
		ENEMY_DEAD,
		CURSOR_MOVE,
		MESSAGE,
		Count
	}
		
	//音声列挙.
	public enum VOICE
	{
		Count
	}

}
