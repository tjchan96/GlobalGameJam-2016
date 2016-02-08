using UnityEngine;
using System.Collections;

public class Minimap : MonoBehaviour {
	public int xPosInverted = 0;
	public int yPosInverted = 0;
	public int width = 0;
	public int height = 0;
	public int borderDim = 0;
	public Texture2D texture;

	private Camera cam;
	private Rect camRect;
	private Rect borderRectLeft;
	private Rect borderRectRight;
	private Rect borderRectTop;
	private Rect borderRectBottom;

	void Start () {
		cam = GetComponent<Camera> ();

		camRect = new Rect (Screen.width - xPosInverted - borderDim, Screen.height - yPosInverted - borderDim, width, height);
		cam.pixelRect = camRect;
	}

	void OnGUI () {
		borderRectLeft = new Rect (Screen.width - xPosInverted - borderDim * 2, 0, borderDim, height + borderDim * 2);
		borderRectRight = new Rect (Screen.width - xPosInverted - borderDim + width, 0, borderDim, height + borderDim * 2);
		borderRectTop = new Rect (Screen.width - xPosInverted - borderDim * 2, 0, width + borderDim * 2, borderDim);
		borderRectBottom = new Rect (Screen.width - xPosInverted - borderDim * 2, yPosInverted + borderDim, width + borderDim * 2, borderDim);

		camRect = new Rect (Screen.width - xPosInverted - borderDim, Screen.height - yPosInverted - borderDim, width, height);
		cam.pixelRect = camRect;

		GUI.skin.box.normal.background = texture;
		GUI.Box(borderRectLeft, GUIContent.none);
		GUI.Box(borderRectRight, GUIContent.none);
		GUI.Box(borderRectTop, GUIContent.none);
		GUI.Box(borderRectBottom, GUIContent.none);
	}
}
