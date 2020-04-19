using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameLoop : MonoBehaviour {

	[SerializeField]
	public Text MSG_DEBUG;
	[SerializeField]
	public Text MSG_SCORE;
	[SerializeField]
	public Text MSG_TIMER;
	[SerializeField]
	public Text MSG_GAMEOVER;
	[SerializeField]
	public Image SPR_GAMEOVER;
	[SerializeField]
	public Text MSG_CLEAR_NORMAL;
	[SerializeField]
	public Image SPR_CLEAR_NORMAL;
	[SerializeField]
	public Text MSG_CLEAR_EXPERT;
	[SerializeField]
	public Image SPR_CLEAR_EXPERT;
	[SerializeField]
	public Text MSG_PRESS_BUTTON;
	[SerializeField]
	public ObjectManager OBJMANAGE;
	[SerializeField]
	public DemoLoop DEMO;
	[SerializeField]
	public PadControlManager PAD;


	public static readonly int[] SCORETABLE = new int[4] { 29337, 175451, 85362, 208 };


	public bool playing = false;
	public int score = 0;
	public int timer = 600;
	int count = 0;
	int gocount = 0;

	// Use this for initialization
	void Start () {
		MSG_SCORE.text = "0";
		MSG_TIMER.text = "60.0";
		count = 0;
		gocount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (playing == false)
		{
			return;
		}

		if (OBJMANAGE.objectUsed.Count == 0)	// 自機生成
		{
			gocount = 0;
			count = 0;
			score = 0;
			timer = 600;
			OBJMANAGE.score = 0;
			if (DEMO.Mode==DemoLoop.MODE.GAME_EXPERT)
			{
				OBJMANAGE.expert = true;
			}
			else
			{
				OBJMANAGE.expert = false;
			}
			OBJMANAGE.Set(ObjectManager.TYPE.MYSHIP, 0, new Vector3(0, -3.4f, 0), 0, 1);
		}



/*
		else if (OBJMANAGE.objectUsed.Count > 0)	// 敵生成(暫定)
		{
			if (gocount == 0)
			{
				if ((count & 7) == 0)
				{
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY2, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(1, 4));
				}
			}
		}
*/
	if (gocount==0)
	{
			switch (count)
			{
				case 60:
				case 70:
				case 80:
				case 90:
				case 100:
				case 110:
				case 120:
				case 130:
				case 140:
				case 150:
				case 160:
				case 170:
				case 180:
				case 190:
				case 200:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, 5);
					break;
				case 210:
				case 220:
				case 230:
				case 240:
				case 250:
				case 260:
				case 270:
				case 280:
				case 290:
				case 300:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, 7);
					break;
				case 310:
				case 315:
				case 320:
				case 325:
				case 330:
				case 335:
				case 340:
				case 345:
				case 350:
				case 355:
				case 360:
				case 365:
				case 370:
				case 375:
				case 380:
				case 385:
				case 390:
				case 395:
				case 400:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, 4);
					break;

				case 410:
				case 420:
				case 430:
				case 440:
				case 450:
				case 460:
				case 470:
				case 480:
				case 490:
				case 500:
				case 510:
				case 520:
				case 530:
				case 540:
				case 550:
				case 560:
				case 570:
				case 580:
				case 590:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, 8);
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, 7);
					break;

				case 650:
				case 655:
				case 660:
				case 665:
				case 670:
				case 675:
				case 680:
				case 685:
				case 690:
				case 695:
				case 700:
				case 705:
				case 710:
				case 715:
				case 720:
				case 725:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY2, 0, new Vector3(-2.4f, 5.45f, 0), 0, Random.Range(2, 6));
					break;

				case 730:
				case 735:
				case 740:
				case 745:
				case 750:
				case 755:
				case 760:
				case 765:
				case 770:
				case 775:
				case 780:
				case 785:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY2, 0, new Vector3(2.4f, 5.45f, 0), 0, Random.Range(2, 6));
					break;

				case 800:
				case 810:
				case 820:
				case 830:
				case 840:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(0.0f, 2.4f), 5.45f, 0), 0, 4);
					break;
				case 850:
				case 860:
				case 870:
				case 880:
				case 890:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-2.4f,0.0f), 5.45f, 0), 0, 4);
					break;
				case 900:
				case 910:
				case 920:
				case 930:
				case 940:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(0.0f, 2.4f), 5.45f, 0), 0, 6);
					break;
				case 950:
				case 960:
				case 970:
				case 980:
				case 990:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-2.4f, 0.0f), 5.45f, 0), 0, 6);
					break;

				case 825:
				case 845:
				case 865:
				case 885:
				case 905:
				case 925:
				case 945:
				case 965:
				case 985:
				case 1005:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY2, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(2, 6));
					break;

				case 1050:
				case 1060:
				case 1070:
				case 1080:
				case 1090:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(0, 5.45f, 0), 0, 7);
					break;

				case 1100:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-0.1f, 0.1f), 5.45f, 0), 0, 7);
					break;
				case 1110:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-0.3f, 0.3f), 5.45f, 0), 0, 7);
					break;
				case 1120:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-0.5f, 0.5f), 5.45f, 0), 0, 7);
					break;
				case 1130:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-0.7f, 0.7f), 5.45f, 0), 0, 7);
					break;
				case 1140:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-0.9f, 0.9f), 5.45f, 0), 0, 7);
					break;
				case 1150:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-1.1f, 1.1f), 5.45f, 0), 0, 7);
					break;
				case 1160:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-1.3f, 1.3f), 5.45f, 0), 0, 7);
					break;
				case 1170:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-1.5f, 1.5f), 5.45f, 0), 0, 7);
					break;
				case 1180:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-1.7f, 1.7f), 5.45f, 0), 0, 7);
					break;
				case 1190:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-1.9f, 1.9f), 5.45f, 0), 0, 7);
					break;
				case 1200:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-2.1f, 2.1f), 5.45f, 0), 0, 7);
					break;
				case 1210:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-2.3f, 2.3f), 5.45f, 0), 0, 7);
					break;

				case 1250:
				case 1260:
				case 1270:
				case 1280:
				case 1290:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(Random.Range(2.0f, 2.4f), 5.45f, 0), 0, Random.Range(4, 7));
					break;
				case 1300:
				case 1310:
				case 1320:
				case 1330:
				case 1340:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(Random.Range(-2.4f, -2.0f), 5.45f, 0), 0, Random.Range(4, 7));
					break;
				case 1350:
				case 1360:
				case 1370:
				case 1380:
				case 1390:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(Random.Range(2.0f, 2.4f), 5.45f, 0), 0, Random.Range(4, 7));
					break;
				case 1400:
				case 1410:
				case 1420:
				case 1430:
				case 1440:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(Random.Range(-2.4f, -2.0f), 5.45f, 0), 0, Random.Range(4, 7));
					break;

				case 1460:
				case 1465:
				case 1470:
				case 1475:
				case 1480:
				case 1485:
				case 1490:
				case 1495:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(7, 10));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(6, 9));
					break;



				case 1500:
				case 1520:
				case 1540:
				case 1560:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(6, 8));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY2, 0, new Vector3(2.4f, 5.45f, 0), 0, Random.Range(4, 7));
					break;
				case 1580:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(7, 9));
					break;
				case 1600:
				case 1620:
				case 1640:
				case 1660:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(6, 8));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY2, 0, new Vector3(-2.4f, 5.45f, 0), 0, Random.Range(4, 7));
					break;
				case 1680:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(7, 9));
					break;

				case 1700:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY2, 0, new Vector3(Random.Range(-2.4f,2.4f), 5.45f, 0), 0, Random.Range(5, 8));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY2, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(5, 8));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY2, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(5, 8));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY2, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(5, 8));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY2, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(5, 8));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY2, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(5, 8));
					break;

				case 1720:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-2.4f, 5.45f, 0), 0, 8);
					break;
				case 1725:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-2.3f, 5.45f, 0), 0, 8);
					break;
				case 1730:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-2.2f, 5.45f, 0), 0, 8);
					break;
				case 1735:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-2.1f, 5.45f, 0), 0, 8);
					break;
				case 1740:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-2.0f, 5.45f, 0), 0, 8);
					break;
				case 1745:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-1.9f, 5.45f, 0), 0, 8);
					break;
				case 1750:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-1.9f, 5.45f, 0), 0, 8);
					break;
				case 1755:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-1.8f, 5.45f, 0), 0, 8);
					break;
				case 1760:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-1.7f, 5.45f, 0), 0, 8);
					break;
				case 1765:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-1.6f, 5.45f, 0), 0, 8);
					break;
				case 1770:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-1.5f, 5.45f, 0), 0, 8);
					break;
				case 1775:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-1.4f, 5.45f, 0), 0, 8);
					break;
				case 1780:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-1.3f, 5.45f, 0), 0, 8);
					break;
				case 1785:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-1.2f, 5.45f, 0), 0, 8);
					break;
				case 1790:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-1.1f, 5.45f, 0), 0, 8);
					break;
				case 1795:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-1.0f, 5.45f, 0), 0, 8);
					break;
				case 1800:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-0.9f, 5.45f, 0), 0, 8);
					break;
				case 1805:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-0.8f, 5.45f, 0), 0, 8);
					break;
				case 1810:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-0.7f, 5.45f, 0), 0, 8);
					break;
				case 1815:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-0.6f, 5.45f, 0), 0, 8);
					break;
				case 1820:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-0.5f, 5.45f, 0), 0, 8);
					break;
				case 1825:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-0.4f, 5.45f, 0), 0, 8);
					break;
				case 1830:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-0.3f, 5.45f, 0), 0, 8);
					break;
				case 1835:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-0.2f, 5.45f, 0), 0, 8);
					break;
				case 1840:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-0.1f, 5.45f, 0), 0, 8);
					break;
				case 1845:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(0.0f, 5.45f, 0), 0, 8);
					break;
				case 1850:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(0.1f, 5.45f, 0), 0, 8);
					break;
				case 1855:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(0.2f, 5.45f, 0), 0, 8);
					break;
				case 1860:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(0.3f, 5.45f, 0), 0, 8);
					break;
				case 1865:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(0.4f, 5.45f, 0), 0, 8);
					break;
				case 1870:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(0.5f, 5.45f, 0), 0, 8);
					break;
				case 1875:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(0.6f, 5.45f, 0), 0, 8);
					break;
				case 1880:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(0.7f, 5.45f, 0), 0, 8);
					break;
				case 1885:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(0.8f, 5.45f, 0), 0, 8);
					break;
				case 1890:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(0.9f, 5.45f, 0), 0, 8);
					break;
				case 1895:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(1.0f, 5.45f, 0), 0, 8);
					break;
				case 1900:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(1.1f, 5.45f, 0), 0, 8);
					break;
				case 1905:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(1.2f, 5.45f, 0), 0, 8);
					break;
				case 1910:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(1.3f, 5.45f, 0), 0, 8);
					break;
				case 1915:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(1.4f, 5.45f, 0), 0, 8);
					break;
				case 1920:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(1.5f, 5.45f, 0), 0, 8);
					break;
				case 1925:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(1.6f, 5.45f, 0), 0, 8);
					break;
				case 1930:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(1.7f, 5.45f, 0), 0, 8);
					break;
				case 1935:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(1.8f, 5.45f, 0), 0, 8);
					break;
				case 1940:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(1.9f, 5.45f, 0), 0, 8);
					break;
				case 1945:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(2.0f, 5.45f, 0), 0, 8);
					break;
				case 1950:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(2.1f, 5.45f, 0), 0, 8);
					break;
				case 1955:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(2.2f, 5.45f, 0), 0, 8);
					break;
				case 1960:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(2.3f, 5.45f, 0), 0, 8);
					break;
				case 1965:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(2.4f, 5.45f, 0), 0, 8);
					break;
				case 1970:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(2.3f, 5.45f, 0), 0, 9);
					break;
				case 1975:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(2.2f, 5.45f, 0), 0, 9);
					break;
				case 1980:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(2.1f, 5.45f, 0), 0, 9);
					break;
				case 1985:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(2.0f, 5.45f, 0), 0, 9);
					break;
				case 1990:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(1.9f, 5.45f, 0), 0, 9);
					break;
				case 1995:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(1.8f, 5.45f, 0), 0, 9);
					break;
				case 2000:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(1.7f, 5.45f, 0), 0, 9);
					break;
				case 2005:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(1.6f, 5.45f, 0), 0, 9);
					break;
				case 2010:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(1.5f, 5.45f, 0), 0, 9);
					break;
				case 2015:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(1.4f, 5.45f, 0), 0, 9);
					break;
				case 2020:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(1.3f, 5.45f, 0), 0, 9);
					break;
				case 2025:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(1.2f, 5.45f, 0), 0, 9);
					break;
				case 2030:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(1.1f, 5.45f, 0), 0, 9);
					break;
				case 2035:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(1.0f, 5.45f, 0), 0, 9);
					break;
				case 2040:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(0.9f, 5.45f, 0), 0, 9);
					break;
				case 2045:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(0.8f, 5.45f, 0), 0, 9);
					break;
				case 2050:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(0.7f, 5.45f, 0), 0, 9);
					break;
				case 2055:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(0.6f, 5.45f, 0), 0, 9);
					break;
				case 2060:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(0.5f, 5.45f, 0), 0, 9);
					break;
				case 2065:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(0.4f, 5.45f, 0), 0, 9);
					break;
				case 2070:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(0.3f, 5.45f, 0), 0, 9);
					break;
				case 2075:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(0.2f, 5.45f, 0), 0, 9);
					break;
				case 2080:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(0.1f, 5.45f, 0), 0, 9);
					break;
				case 2085:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(0.0f, 5.45f, 0), 0, 9);
					break;
				case 2090:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-0.1f, 5.45f, 0), 0, 9);
					break;
				case 2095:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-0.2f, 5.45f, 0), 0, 9);
					break;
				case 2100:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-0.3f, 5.45f, 0), 0, 9);
					break;
				case 2105:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-0.4f, 5.45f, 0), 0, 9);
					break;
				case 2110:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-0.5f, 5.45f, 0), 0, 9);
					break;
				case 2115:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-0.6f, 5.45f, 0), 0, 9);
					break;
				case 2120:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-0.7f, 5.45f, 0), 0, 9);
					break;
				case 2125:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-0.8f, 5.45f, 0), 0, 9);
					break;
				case 2130:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-0.9f, 5.45f, 0), 0, 9);
					break;
				case 2135:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-1.0f, 5.45f, 0), 0, 9);
					break;
				case 2140:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-1.1f, 5.45f, 0), 0, 9);
					break;
				case 2145:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-1.2f, 5.45f, 0), 0, 9);
					break;
				case 2150:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-1.3f, 5.45f, 0), 0, 9);
					break;
				case 2155:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-1.4f, 5.45f, 0), 0, 9);
					break;
				case 2160:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-1.5f, 5.45f, 0), 0, 9);
					break;
				case 2165:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-1.6f, 5.45f, 0), 0, 9);
					break;
				case 2170:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-1.7f, 5.45f, 0), 0, 9);
					break;
				case 2175:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-1.8f, 5.45f, 0), 0, 9);
					break;
				case 2180:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-1.9f, 5.45f, 0), 0, 9);
					break;
				case 2185:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-2.0f, 5.45f, 0), 0, 9);
					break;
				case 2190:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-2.1f, 5.45f, 0), 0, 9);
					break;
				case 2195:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-2.2f, 5.45f, 0), 0, 9);
					break;
				case 2200:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-2.3f, 5.45f, 0), 0, 9);
					break;
				case 2205:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(-2.4f, 5.45f, 0), 0, 9);
					break;


				case 2210:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(7, 10));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(7, 10));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(7, 10));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(7, 10));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(7, 10));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(7, 10));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(7, 10));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(7, 10));
					break;
				case 2250:
				case 2255:
				case 2260:
				case 2265:
				case 2270:
				case 2275:
				case 2280:
				case 2285:
				case 2290:
				case 2295:
				case 2300:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY2, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(2, 6));
					break;

				case 2350:
				case 2450:
				case 2550:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(-2.2f, 5.45f, 0), 0, 3);
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3( 2.2f, 5.45f, 0), 0, 3);
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(-1.8f, 5.45f, 0), 0, 4);
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(1.8f, 5.45f, 0), 0, 4);
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(-1.4f, 5.45f, 0), 0, 5);
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(1.4f, 5.45f, 0), 0, 5);
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(-1.0f, 5.45f, 0), 0, 6);
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(1.0f, 5.45f, 0), 0, 6);
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(-0.6f, 5.45f, 0), 0, 7);
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(0.6f, 5.45f, 0), 0, 7);
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(-0.2f, 5.45f, 0), 0, 8);
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(0.2f, 5.45f, 0), 0, 8);
					break;
				case 2400:
				case 2425:
				case 2500:
				case 2525:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(5, 8));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(5, 8));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(5, 8));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(5, 8));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(5, 8));
					break;

				case 2600:
				case 2605:
				case 2610:
				case 2615:
				case 2620:
				case 2625:
				case 2630:
				case 2635:
				case 2640:
				case 2645:
				case 2650:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(6, 9));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY2, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(3, 7));
					break;

				case 2700:
				case 2705:
				case 2710:
				case 2715:
				case 2720:
				case 2725:
				case 2730:
				case 2735:
				case 2740:
				case 2745:
				case 2750:
				case 2755:
				case 2760:
				case 2765:
				case 2770:
				case 2775:
				case 2780:
				case 2785:
				case 2790:
				case 2795:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(2, 8));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY2, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(2, 6));
					break;

				case 2850:
				case 2860:
				case 2870:
				case 2880:
				case 2890:
				case 2900:
				case 2910:
				case 2920:
				case 2930:
				case 2940:
				case 2950:
				case 2960:
				case 2970:
				case 2980:
				case 2990:
				case 3000:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(2, 9));
					break;

				case 3010:
				case 3015:
				case 3020:
				case 3025:
				case 3030:
				case 3035:
				case 3040:
				case 3045:
				case 3050:
				case 3055:
				case 3060:
				case 3065:
				case 3070:
				case 3075:
				case 3080:
				case 3085:
				case 3090:
				case 3095:
				case 3100:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-2.3f, -1.3f), 5.45f, 0), 0, Random.Range(11, 15));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(1.3f, 2.3f), 5.45f, 0), 0, Random.Range(11, 15));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(Random.Range(-1.0f, 1.0f), 5.45f, 0), 0, Random.Range(3, 7));
					break;

				case 3105:
				case 3110:
				case 3115:
				case 3120:
				case 3125:
				case 3130:
				case 3135:
				case 3140:
				case 3145:
				case 3150:
				case 3155:
				case 3160:
				case 3165:
				case 3170:
				case 3175:
				case 3180:
				case 3185:
				case 3190:
				case 3195:
				case 3200:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-2.3f, -1.3f), 5.45f, 0), 0, Random.Range(11, 15));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(1.3f, 2.3f), 5.45f, 0), 0, Random.Range(11, 15));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY2, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(2, 6));
					break;

				case 3250:
				case 3260:
				case 3270:
				case 3280:
				case 3290:
				case 3300:
				case 3310:
				case 3320:
				case 3330:
				case 3340:
				case 3350:
				case 3360:
				case 3370:
				case 3380:
				case 3390:
				case 3400:
				case 3410:
				case 3420:
				case 3430:
				case 3440:
				case 3450:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY1, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, 7);
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(4, 7));
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY3, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(4, 7));
					break;


				case 3500:
				case 3505:
				case 3510:
				case 3515:
				case 3520:
				case 3525:
				case 3530:
				case 3535:
				case 3540:
				case 3545:
				case 3550:
				case 3555:
				case 3560:
				case 3565:
				case 3570:
				case 3575:
				case 3580:
				case 3585:
				case 3590:
				case 3595:
					OBJMANAGE.Set(ObjectManager.TYPE.ENEMY2, 0, new Vector3(Random.Range(-2.4f, 2.4f), 5.45f, 0), 0, Random.Range(2, 6));
					break;
			}

		}

		score = OBJMANAGE.score;
		MSG_SCORE.text = "" + score;
		MSG_TIMER.text = "" + timer / 10 + "." + timer % 10;
		count++;
		if (OBJMANAGE.stop == true)
		{
			count--;
			switch (gocount)
			{
				case 0:
					break;
				case 160:
					SoundManager.Instance.PlayBGM((int)SoundHeader.BGM.GAME_OVER);
					SPR_GAMEOVER.enabled = true;
					MSG_GAMEOVER.enabled = true;
					MSG_PRESS_BUTTON.gameObject.SetActive(true);
					break;
				case 1000:
					SoundManager.Instance.StopBGM();
					break;
				case 1200:
					SoundManager.Instance.StopBGM();
					OBJMANAGE.ResetAll();
					SPR_CLEAR_NORMAL.enabled = false;
					SPR_CLEAR_EXPERT.enabled = false;
					MSG_CLEAR_NORMAL.enabled = false;
					MSG_CLEAR_EXPERT.enabled = false;
					SPR_GAMEOVER.enabled = false;
					MSG_GAMEOVER.enabled = false;
					MSG_PRESS_BUTTON.enabled = false;
					MSG_PRESS_BUTTON.gameObject.SetActive(false);
					OBJMANAGE.stop = false;
					OBJMANAGE.destroy = false;
					playing = false;
					DEMO.ReturnTitle();
					break;
			}
			if (gocount > 160)
			{
				if (gocount > 1180)
				{
					gocount -= 128;
				}
				if (PAD.pad.button_oneshot[(int)PadData.BUTTON.A] == true)
				{
					gocount = 1199;
					SoundManager.Instance.PlaySE((int)SoundHeader.SE.DECIDE);
				}
				if ((gocount >> 6) % 2 == 0)
				{
					MSG_PRESS_BUTTON.enabled = true;
				}
				else
				{
					MSG_PRESS_BUTTON.enabled = false;
				}
			}
			gocount++;
		}
		else
		{
			if (count % 6 == 0)
			{
				timer--;
				if (timer < 0)
				{
					timer = 0;
				}
			}
			if (count == (600 * 6))
			{
				// ゲーム終了(タイムアップ)
				OBJMANAGE.destroy = true;
			}
			else if (count == ((600 * 6) + 60))
			{
				if (DEMO.Mode == DemoLoop.MODE.GAME_EXPERT)
				{
					SPR_CLEAR_EXPERT.enabled = true;
				}
				else
				{
					SPR_CLEAR_NORMAL.enabled = true;
				}
			}
			else if (count == ((600 * 6) + 160))
			{
				MSG_PRESS_BUTTON.gameObject.SetActive(true);
				if (DEMO.Mode == DemoLoop.MODE.GAME_EXPERT)
				{
					MSG_CLEAR_EXPERT.enabled = true;
				}
				else
				{
					MSG_CLEAR_NORMAL.enabled = true;
				}
				SoundManager.Instance.PlayBGM((int)SoundHeader.BGM.GAME_CLEAR);
			}
			else if (count == ((600 * 6) + 1000))
			{
				SoundManager.Instance.StopBGM();
			}
			else if (count == ((600 * 6) + 260))
			{
				//SoundManager.Instance.StopBGM();
			}
			else if (count == ((600 * 6) + 1200))
			{
				OBJMANAGE.ResetAll();
				SoundManager.Instance.StopBGM();
				SPR_CLEAR_NORMAL.enabled = false;
				MSG_CLEAR_NORMAL.enabled = false;
				SPR_CLEAR_EXPERT.enabled = false;
				MSG_CLEAR_EXPERT.enabled = false;
				SPR_GAMEOVER.enabled = false;
				MSG_GAMEOVER.enabled = false;
				MSG_PRESS_BUTTON.enabled = false;
				MSG_PRESS_BUTTON.gameObject.SetActive(false);
				OBJMANAGE.stop = false;
				OBJMANAGE.destroy = false;
				playing = false;
				DEMO.ReturnTitle();
			}
		}
		if (count > ((600 * 6) + 1190))
		{
			count -= 128;
		}
		if (count > ((600 * 6) + 160))
		{
			if (((count >> 6) % 2) == 0)
			{
				MSG_PRESS_BUTTON.enabled = true;
			}
			else
			{
				MSG_PRESS_BUTTON.enabled = false;
			}
			if (PAD.pad.button_oneshot[(int)PadData.BUTTON.A] == true)
			{
				SoundManager.Instance.PlaySE((int)SoundHeader.SE.DECIDE);
				count = (600 * 6) + 1199;
			}
		}
		


		MSG_DEBUG.text += "\n\nGame Count=" + count;
		MSG_DEBUG.text += "\nOver Count=" + gocount;
	}
}
