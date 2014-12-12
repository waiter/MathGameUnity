using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class LevelEntry : MonoBehaviour ,IPointerClickHandler{

	public int levelIndex=0;

	public void OnPointerClick(PointerEventData evt){
		Navigation.Push("subLevel");
	}

}
