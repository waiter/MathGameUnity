using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NumberItemView : MonoBehaviour {


	void Awake(){
		gameObject.SetActive(true);
	}
	[SerializeField]
	private Text _text;
	public void OnNumberClick(){
		Map.Cell cell=NumberSelector.selectedCell;
		bool isVaild=cell.CheckVaild(number);
		if(!isVaild){
			gameObject.SetActive(false);
			NumberTryer.Go(transform.localPosition,number,false,delegate() {
				gameObject.SetActive(true);
			});
		}else{
			gameObject.SetActive(false);
			NumberTryer.Go(transform.localPosition,number,true,delegate() {
				gameObject.SetActive(true);
				cell.isShow=true;
				cell.userValue=number;
			});
		}
	}

	public int number{
		set{
			_text.text=value.ToString();
		}get{
			return int.Parse(_text.text);
		}
	}
}
