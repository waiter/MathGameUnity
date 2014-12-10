using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CellView: MonoBehaviour {

	public Sprite selectedCell;

	[SerializeField]
	private Text _number;
	private Map.Cell _cell;

	public void OnSelected(){
		NumberSelector.selectedCell=_cell;
	}

	public void Bind(Map.Cell cell){
		cell.onCellUpdate=delegate(Map.Cell obj) {
			number=cell.userValue;
			isNumberVisible=cell.isShow;
		};
		cell.FireUpdate();
		_cell=cell;
	}

	public Vector2 localPosition{
		set{
			GetComponent<RectTransform>().localPosition=value;
		}
	}

	public int number{
		set{
			_number.text=value.ToString();
		}
	}

	public bool isNumberVisible{
		set{
			_number.gameObject.SetActive(value);
		}
	}

}
