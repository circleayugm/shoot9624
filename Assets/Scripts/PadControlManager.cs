using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PadData {
	public enum BUTTON
	{
		A = 0,
		B,
		X,
		Y,
		START,
		MAX,
	}
	public enum DIRECTION           // パッド移動方向補正を加味した移動方向
	{
		UP = 0,                         // ↑が↑
		RIGHT = 1,                      // ↑が→(90度)
		DOWN = 2,                       // ↑が↓(180度)
		LEFT = 3,                       // ↑が←(270度)
	}

	public Vector3 pad { get; set; }    // パッド方向
	public PadData.DIRECTION dir { get; set; }  // パッド入力による移動方向(4方向・同時押しは後発有効・最後の入力を保持)
	public bool[] button { get; set; }  // ボタン(複数)
	public bool[] button_oneshot{ get; set; }	// ボタン(複数・押した瞬間のみ)
	public bool[] button_pressed{ get; set; }	// ボタン(押しっぱなし時true・離した場合false)

	public PadData()
	{
		Vector3 pd = new Vector3(0, 0, 0);
		pad = pd;
		dir = PadData.DIRECTION.UP;
		button = new bool[(int)BUTTON.MAX];
		button_oneshot = new bool[(int)BUTTON.MAX];
		button_pressed = new bool[(int)BUTTON.MAX];
		for (int i = 0; i < (int)BUTTON.MAX; i++)
		{
			button[i] = new bool();
			button[i] = false;
			button_oneshot[i] = new bool();
			button_oneshot[i] = false;
			button_pressed[i] = new bool();
			button_pressed[i] = false;
		}
	}
}

public class PadControlManager : MonoBehaviour {

	[SerializeField]
	Camera cam;
	[SerializeField]
	Text msg_pad_detail;



	public PadData pad;

	// Use this for initialization
	void Start()
	{
		msg_pad_detail.text = "";
		pad = new PadData();
	}

	// Update is called once per frame
	void Update()
	{

		msg_pad_detail.text = "Pad Detail: ";
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		if (x < 0.00f)
		{
			pad.dir = PadData.DIRECTION.LEFT;
		}
		else if (x > 0.00f)
		{
			pad.dir = PadData.DIRECTION.RIGHT;
		}
		if (y < 0.00f)
		{
			pad.dir = PadData.DIRECTION.UP;
		}
		else if (y > 0.00f)
		{
			pad.dir = PadData.DIRECTION.DOWN;
		}
		switch(pad.dir) {
			case PadData.DIRECTION.UP:
				msg_pad_detail.text += "↑";
				break;
			case PadData.DIRECTION.RIGHT:
				msg_pad_detail.text += "→";
				break;
			case PadData.DIRECTION.DOWN:
				msg_pad_detail.text += "↓";
				break;
			case PadData.DIRECTION.LEFT:
				msg_pad_detail.text += "←";
				break;
		}
		msg_pad_detail.text += "\n";

		if (x != 0.00f)
		{
			msg_pad_detail.text += "<color=#ff0000ff>";
		}
		msg_pad_detail.text += "X=" + x.ToString() + "\n";
		if (x != 0.00f)
		{
			msg_pad_detail.text += "</color>";
		}
		if (y != 0.00f)
		{
			msg_pad_detail.text += "<color=#ff0000ff>";
		}
		msg_pad_detail.text += "Y=" + y.ToString() + "\n\n";
		if (y != 0.00f)
		{
			msg_pad_detail.text += "</color>";
		}
		pad.pad = new Vector3(x, y, 0);
		//pad_dir = new Vector3(x, y, 0);

		for(int i=0;i<(int)PadData.BUTTON.MAX;i++)
		{
			pad.button_oneshot[i] = false;
		}
		//pad.button_oneshot[(int)PadData.BUTTON.A] = Input.GetButtonDown("Btn_A");
		if ((pad.button[(int)PadData.BUTTON.A] = Input.GetButton("Btn_A")) == true)
		{
			if (pad.button_pressed[(int)PadData.BUTTON.A] == false)
			{
				pad.button_oneshot[(int)PadData.BUTTON.A] = true;
				pad.button_pressed[(int)PadData.BUTTON.A] = true;
			}
			else
			{
				pad.button_oneshot[(int)PadData.BUTTON.A] = false;
			}
			msg_pad_detail.text += "<color=#ff0000ff>→Btn_A</color> ";
		}
		else
		{
			msg_pad_detail.text += "　Btn_A ";
			pad.button_oneshot[(int)PadData.BUTTON.A] = false;
			pad.button_pressed[(int)PadData.BUTTON.A] = false;
		}

		//pad.button_oneshot[(int)PadData.BUTTON.B] = Input.GetButtonDown("Btn_B");
		if (pad.button[(int)PadData.BUTTON.B] = Input.GetButton("Btn_B") == true)
		{
			if (pad.button_pressed[(int)PadData.BUTTON.B] == false)
			{
				pad.button_oneshot[(int)PadData.BUTTON.B] = true;
				pad.button_pressed[(int)PadData.BUTTON.B] = true;
			}
			else
			{
				pad.button_oneshot[(int)PadData.BUTTON.B] = false;
			}
			msg_pad_detail.text += "<color=#ff0000ff>→Btn_B</color>\n";
		}
		else
		{
			msg_pad_detail.text += "　Btn_B\n";
			pad.button_oneshot[(int)PadData.BUTTON.B] = false;
			pad.button_pressed[(int)PadData.BUTTON.B] = false;
		}

		//pad.button_oneshot[(int)PadData.BUTTON.X] = Input.GetButtonDown("Btn_X");
		if (pad.button[(int)PadData.BUTTON.X] = Input.GetButton("Btn_X") == true)
		{
			if (pad.button_pressed[(int)PadData.BUTTON.X] == false)
			{
				pad.button_oneshot[(int)PadData.BUTTON.X] = true;
				pad.button_pressed[(int)PadData.BUTTON.X] = true;
			}
			else
			{
				pad.button_oneshot[(int)PadData.BUTTON.X] = false;
			}
			msg_pad_detail.text += "<color=#ff0000ff>→Btn_X</color> ";
		}
		else
		{
			msg_pad_detail.text += "　Btn_X ";
			pad.button_oneshot[(int)PadData.BUTTON.X] = false;
			pad.button_pressed[(int)PadData.BUTTON.X] = false;
		}

		//pad.button_oneshot[(int)PadData.BUTTON.Y] = Input.GetButtonDown("Btn_Y");
		if (pad.button[(int)PadData.BUTTON.Y] = Input.GetButton("Btn_Y") == true)
		{
			if (pad.button_pressed[(int)PadData.BUTTON.Y] == false)
			{
				pad.button_oneshot[(int)PadData.BUTTON.Y] = true;
				pad.button_pressed[(int)PadData.BUTTON.Y] = true;
			}
			else
			{
				pad.button_oneshot[(int)PadData.BUTTON.Y] = false;
			}
			msg_pad_detail.text += "<color=#ff0000ff>→Btn_Y</color>\n";
		}
		else
		{
			msg_pad_detail.text += "　Btn_Y\n";
			pad.button_oneshot[(int)PadData.BUTTON.Y] = false;
			pad.button_pressed[(int)PadData.BUTTON.Y] = false;
		}

		pad.button_oneshot[(int)PadData.BUTTON.START] = Input.GetButtonDown("Btn_Start");
		if (pad.button[(int)PadData.BUTTON.START] = Input.GetButton("Btn_Start") == true)
		{
			if (pad.button_pressed[(int)PadData.BUTTON.START] == false)
			{
				pad.button_oneshot[(int)PadData.BUTTON.START] = true;
				pad.button_pressed[(int)PadData.BUTTON.START] = true;
			}
			else
			{
				pad.button_oneshot[(int)PadData.BUTTON.START] = false;
			}
			msg_pad_detail.text += "<color=#ff0000ff>→Btn_Start</color> ";
		}
		else
		{
			msg_pad_detail.text += "　Btn_Start ";
			pad.button_oneshot[(int)PadData.BUTTON.START] = false;
			pad.button_pressed[(int)PadData.BUTTON.START] = false;
		}



		msg_pad_detail.text += "\n\n pad.pad=" + pad.pad + "\n pad.button(true)=\n";
		for (int i=0;i<(int)PadData.BUTTON.MAX;i++)
		{
			if (pad.button[i]==true)
			{
				msg_pad_detail.text += "[" + i + "]\n";
			}
		}
		msg_pad_detail.text += "\n pad.button_oneshot(true)=\n";
		for (int i = 0; i < (int)PadData.BUTTON.MAX; i++)
		{
			if (pad.button_oneshot[i] == true)
			{
				msg_pad_detail.text += "[" + i + "]\n";
			}
		}
		msg_pad_detail.text += "\n pad.button_pressed(true)=\n";
		for (int i = 0; i < (int)PadData.BUTTON.MAX; i++)
		{
			if (pad.button_pressed[i] == true)
			{
				msg_pad_detail.text += "[" + i + "]\n";
			}
		}




	}
}
