using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SubLevel : MonoBehaviour ,IPointerClickHandler{

	public Image[] stars;
	public Image lockImg;
	public Text txt;

	public void OnPointerClick(PointerEventData evt){
		GameController.Start(int.Parse(txt.text));
		Navigation.Push("game");
	}

	public int starCount{
		set{
			for(int i=0;i<3;i++){
				stars[i].gameObject.SetActive(value<i);
			}
		}
	}

	public bool isLocked{
		set{
			lockImg.gameObject.SetActive(value);
			txt.gameObject.SetActive(!value);
		}
	}

	public int levelIndex{
		set{
			txt.text=value.ToString();
		}
	}
}
