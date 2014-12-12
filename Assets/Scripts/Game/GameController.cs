using UnityEngine;
using System.Collections;

public class GameController  {

	public static event System.Action<int> onStart;

	public static void Start(int idx){
		onStart(idx);
	}
}
