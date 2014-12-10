using UnityEngine;
using System.Collections;

public class PointerConvert  {

	private static Canvas _rootCanvas;

	public static Canvas rootCanvas{
		get{
			if(_rootCanvas==null){
				_rootCanvas=GameObject.FindWithTag("RootCanvas").GetComponent<Canvas>();
			}
			return _rootCanvas;
		}
	}

	public static Vector3 ScreenToCanvasSpace(Vector3 pos){
		return rootCanvas.transform.InverseTransformPoint(pos);
	}
}
