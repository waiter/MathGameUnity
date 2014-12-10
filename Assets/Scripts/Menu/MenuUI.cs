using UnityEngine;
using System.Collections;

public class MenuUI : MonoBehaviour {


	public void OnGameStart(){
		Navigation.Push("level");
	}
}
