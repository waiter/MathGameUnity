using UnityEngine;
using System.Collections;

public class NumberSelector {

	public static event System.Action<Map.Cell> onCellSelected;

	private static Map.Cell _cell;

	public static Map.Cell selectedCell{
		get{
			return _cell;
		}set{
			_cell=value;
			if(onCellSelected!=null){
				onCellSelected(value);
			}
			AudioController.Play("Select");
			Debug.Log("Select "+value.number);
		}
	}
}
