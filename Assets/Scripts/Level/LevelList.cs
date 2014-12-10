using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelList : MonoBehaviour {

	public Image image;

	public void OnDrag(BaseEventData pointerEventData) {
		PointerEventData evt=pointerEventData as PointerEventData;
		Vector3 pos=PointerConvert.ScreenToCanvasSpace(evt.position);
		image.rectTransform.localPosition=new Vector3(0,pos.y,0);
	}

	public void OnClick(BaseEventData evt){
		Navigation.Push("game");
	}


}
