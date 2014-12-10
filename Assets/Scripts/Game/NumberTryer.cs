using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NumberTryer : MonoBehaviour {

	[SerializeField]
	private Text _text;

	void Awake(){
		gameObject.SetActive(false);
		NumberTryer.onGo+=OnGO;
	}

	void OnDestroy(){
		NumberTryer.onGo-=OnGO;
	}

	private void OnGO(Vector3 pos, int number,bool isCorrect,System.Action onComplete){
		Map.Cell cell= NumberSelector.selectedCell;
		transform.localPosition=pos;
		gameObject.SetActive(true);
		_text.text=number.ToString();
		UAnimation.Make(gameObject)
			.MoveTo(MapRenderer.CalculateCellPosition(cell.row,cell.col))
				.Duration(0.5f)
				.Ease(UAnimation.EaseType.Linear)
				.Go(delegate() {
					if(isCorrect){
						gameObject.SetActive(false);
						onComplete();
					}else{
						UAnimation.Make(gameObject)
							.MoveTo(pos)
							.Duration(0.5f)
								.Ease(UAnimation.EaseType.Linear)
								.Go(delegate() {
									gameObject.SetActive(false);
									onComplete();
								});
					}

				});
	}
	

	private static event System.Action<Vector3,int, bool,System.Action> onGo;

	public static void Go(Vector3 pos,int number,bool correct,System.Action onComplete){
		if(onGo!=null){
			onGo(pos,number,correct,onComplete);
		}
	}
}
