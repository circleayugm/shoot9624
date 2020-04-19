using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uGUI_BackGroundScrollVertical : MonoBehaviour
{
	[SerializeField]
	public ObjectManager MANAGE;




	public Image root_background;
	public int scroll_speed = -2;
	public bool stop = false;

	private Vector3 pos = new Vector3(0, 0, 0);

	// Use this for initialization
	void Start()
	{
		pos = new Vector3(0, 0, 0);
		root_background.transform.localPosition = new Vector3(0, 0, 0);
	}

	// Update is called once per frame
	void Update()
	{
		if (MANAGE.stop == false)
		{
			pos = root_background.transform.localPosition;
			pos += new Vector3(0, scroll_speed, 0);
			if ((0 - root_background.rectTransform.sizeDelta.y) >= pos.y)   // 縦-480を越えた時点で480足してスクロール位置を戻す
			{
				pos.y += root_background.rectTransform.sizeDelta.y;
			}
			root_background.transform.localPosition = pos;
		}
	}
}
