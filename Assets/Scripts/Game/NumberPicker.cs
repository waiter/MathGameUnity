using UnityEngine;
using System.Collections;

public class NumberPicker : MonoBehaviour {

	public GameObject PrefabNumberCard;

	void Awake(){
		for(int i=1;i<10;i++){
			NumberItemView view=InstantiateNumberCard(i);
			view.number=i;
		}
	}

	private NumberItemView InstantiateNumberCard(int idx){
		GameObject cd=Instantiate(PrefabNumberCard) as GameObject;
		NumberItemView item=cd.GetComponent<NumberItemView>();
		item.transform.parent=transform;
		item.transform.localPosition=new Vector3(-250+idx*50,-330,0);
		item.transform.localScale=Vector3.one;
		return item;
	}
}
