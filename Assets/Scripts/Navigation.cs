using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Navigation  {

	private static Dictionary<string,GameObject> _scene=new Dictionary<string, GameObject>();

	private static Stack<string> _sceneStack=new Stack<string>();

	public static event System.Action onPushStart;
	public static event System.Action onPushEnd;

	public static void Register(string sceneName,GameObject scene){
		_scene.Add(sceneName,scene);
	}

	public static void HideAll(){
		foreach(string key in _scene.Keys){
			_scene[key].gameObject.SetActive(false);
		}
	}
	public static void Push(string sceneName){
		if(_sceneStack.Count>0){
			GameObject o=_scene[_sceneStack.Peek()];

			UAnimation.Make(o)
					.MoveTo(new Vector3(-480,0,0))
					.Duration(0.5f)
					.Ease(UAnimation.EaseType.Linear)
					.Go(delegate() {
					o.SetActive(false);
				});
		}
		_scene[sceneName].SetActive(true);
		_scene[sceneName].transform.localPosition=new Vector3(480,0,0);
		UAnimation.Make(_scene[sceneName])
			.MoveTo(Vector3.zero)
				.Ease(UAnimation.EaseType.Linear)
				.Duration(0.5f)
				.Go();

		_sceneStack.Push(sceneName);
	}

	public static void Pop(){
		if(_sceneStack.Count>0){
			GameObject o=_scene[_sceneStack.Pop()];
			UAnimation.Make(o)
				.MoveTo(new Vector3(480,0,0))
					.Duration(0.5f)
					.Ease(UAnimation.EaseType.Linear)
					.Go(delegate() {
						o.SetActive(false);
					});

		}

		if(_sceneStack.Count>0){
			GameObject o=_scene[_sceneStack.Peek()];
			o.gameObject.SetActive(true);
			UAnimation.Make(o)
				.MoveTo(Vector3.zero)
					.Ease(UAnimation.EaseType.Linear)
					.Duration(0.5f)
					.Go();
		}

	}
}
