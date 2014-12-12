using UnityEngine;
using System.Collections;

public class ApplicationView : MonoBehaviour {

	public GameObject menuUI;
	public GameObject gameUI;
	public GameObject levelUI;
	public GameObject sublevelUI;

	void Awake(){
		Navigation.Register("menu",menuUI);
		Navigation.Register("game",gameUI);
		Navigation.Register("level",levelUI);
		Navigation.Register("subLevel",sublevelUI);
	}
	void Start(){
		Navigation.HideAll();
		Navigation.Push("menu");
	}
}
