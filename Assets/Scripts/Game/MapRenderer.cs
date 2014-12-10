using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MapRenderer : MonoBehaviour {


	public GameObject PrefabCell;

	public Image cellHighLight;

	private CellView[,] _views=new CellView[9,9];
	void Awake(){
		NumberSelector.onCellSelected+=OnCellSelected;
		for(int i=0;i<9;i++){
			for(int j=0;j<9;j++){
				InstantiateCell(i,j);
			}
		}
		cellHighLight.gameObject.SetActive(false);
	}

	void OnDestroy(){
		NumberSelector.onCellSelected-=OnCellSelected;
	}

	private void OnCellSelected(Map.Cell cell){
		cellHighLight.gameObject.SetActive(true);
		int i=cell.row;
		int j=cell.col;
		Debug.LogError(i+":"+j);
		cellHighLight.rectTransform.localPosition=CalculateCellPosition(i,j);
	}

	private CellView InstantiateCell(int i,int j){
		GameObject o=Instantiate(PrefabCell) as GameObject;
		CellView view=o.GetComponent<CellView>();
		view.transform.parent=transform;

		view.transform.localPosition=CalculateCellPosition(i,j);
		view.GetComponent<RectTransform>().localScale=Vector3.one;
		_views[i,j]=view;
		return view;
	}



	public void SetMap(Map map){
		for(int i=0;i<map.sizeX;i++){
			for(int j=0;j<map.sizeY;j++){
				_views[i,j].Bind(map[i,j]);
			}
		}
	}

	public static Vector3 CalculateCellPosition(int i,int j){
		int size=51;
		return new Vector3(-204+size*j,204-size*i,0);
	}


}
