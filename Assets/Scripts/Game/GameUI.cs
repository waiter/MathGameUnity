using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class GameUI : MonoBehaviour ,IEventSystemHandler{

	public MapRenderer mapRenderer;

	void Awake(){
		GameController.onStart+=OnGameStart;
	}

	void OnDestroy(){
		GameController.onStart-=OnGameStart;
	}

	public void OnGameStart(int index){
		Debug.Log("Maps/map_"+index);
		TextAsset ass=Resources.Load<TextAsset>("Maps/map_"+index);
		Map m=new Map();
		m.Load(ass.bytes);
		mapRenderer.SetMap(m);
	}

	public void OnBack(){
		Navigation.Pop();
	}
	// Update is called once per frame
}
