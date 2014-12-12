using UnityEngine;
using System.Collections;

public class SubLevelPanel : MonoBehaviour {

	public GameObject PrefabSubLevel;


	void Awake(){
		for(int i=0;i<12;i++){
			InstantiateSubLevel(i);
		}
	}

	void InstantiateSubLevel(int idx){
		GameObject o=Instantiate(PrefabSubLevel) as GameObject;
		o.transform.parent=transform;
		SubLevel subLv=o.GetComponent<SubLevel>();
		subLv.levelIndex=idx;
		subLv.isLocked=idx>1;
	}
}
