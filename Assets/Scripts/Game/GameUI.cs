using UnityEngine;
using System.Collections;

public class GameUI : MonoBehaviour {

	public MapRenderer mapRenderer;

	void Start () {
		Map m=new Map();
		m.Generate(40);
		mapRenderer.SetMap(m);
	}

	public void OnBack(){
		Navigation.Pop();
	}
	// Update is called once per frame
	void Update () {
	}
}
