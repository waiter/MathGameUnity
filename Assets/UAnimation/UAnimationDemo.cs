using UnityEngine;
using System.Collections;

public class UAnimationDemo : MonoBehaviour {


	void Update(){

		if(Input.GetKeyDown(KeyCode.Space)){
			transform.localPosition=new Vector3(0,-400,0);
			transform.localScale=new Vector3(1,1,1);
			transform.localRotation=Quaternion.Euler(0,0,0);


			UAnimation.Make(gameObject)
				.MoveTo(new Vector3(0,0,0))
					.ScaleTo(new Vector3(5,5,5))
					.RotateTo(new Vector3(0,0,90))
					.Duration(2f)
					.Ease(UAnimation.EaseType.EaseOutElastic)
					.Go();
		}
	}


	[ContextMenu("test")]
	void Test(){
	}
	
}
