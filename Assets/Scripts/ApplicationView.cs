using UnityEngine;
using System.Collections;

public class ApplicationView : MonoBehaviour {

	public GameObject menuUI;
	public GameObject gameUI;
	public GameObject levelUI;

	void Awake(){
		Navigation.Register("menu",menuUI);
		Navigation.Register("game",gameUI);
		Navigation.Register("level",levelUI);

	}
	void Start(){
		Navigation.HideAll();
		Navigation.Push("menu");
	}
}
