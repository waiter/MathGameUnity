using UnityEngine;
using System.Collections;

public class UAnimation :MonoBehaviour{

	private Vector3? _targetPos=null;
	private Vector3? _targetRotation=null;
	private Vector3? _targetScale=null;
	private float _seconds=1;
	private EaseType _type=EaseType.Linear;
	private bool _isRunning=false;

	private Vector3 _startPos;
	private Vector3 _startRotation;
	private Vector3 _startScale;

	private bool _islocal=true;
	private float _currentTime=0;
	private float _delaySeconds=0;

	private System.Action _onComplete;

	public UAnimation MoveTo(Vector3 pos){
		_targetPos=pos;
		return this;
	}

	public UAnimation RotateTo(Vector3 rotation){
		_targetRotation=rotation;
		return this;
	}

	public UAnimation ScaleTo(Vector3 scale){
		_targetScale=scale;
		return this;
	}


	public UAnimation Duration(float seconds){
		_seconds=seconds;
		return this;
	}

	public UAnimation SetLocal(bool local){
		_islocal=local;
		return this;
	}

	public UAnimation Ease(EaseType easeType){
		_type=easeType;
		return this;
	}

	public UAnimation Delay(float seconds){
		_delaySeconds=seconds;
		return this;
	}

	public UAnimation Go(System.Action onComplete=null){
		_isRunning=true;
		if(_islocal){
			_startPos=transform.localPosition;
			_startScale=transform.localScale;
			_startRotation=transform.localRotation.eulerAngles;
		}else{
			_startPos=transform.position;
			_startScale=transform.lossyScale;
			_startRotation=transform.rotation.eulerAngles;
		}
		_currentTime=0;
		_onComplete=onComplete;
		return this;
	}

	public UAnimation Pause(){
		_isRunning=false;
		return this;

	}
	public UAnimation Resume(){
		_isRunning=true;
		return this;
	}

	private float EaseValue(float t){
		switch(_type){
		case EaseType.Linear:
			return EaseLinear(t);
		case EaseType.EaseInOutCubic:
			return EaseInOutCubic(t);
		case EaseType.EaseInCubic:
			return EaseInCubic(t);
		case EaseType.EaseOutCubic:
			return EaseOutCubic(t);
		case EaseType.EaseInQuad:
			return EaseInQuad(t);
		case EaseType.EaseOutQuad:
			return EaseOutQuad(t);
		case EaseType.EaseInOutQuad:
			return EaseInOutQuad(t);
		case EaseType.EaseInElastic:
			return UAnimation.EaseInElastic(t);
		case EaseType.EaseOutElastic:
			return UAnimation.EaseOutElastic(t);
		}
		return t;
	}

	void Update(){
		if(!_isRunning){
			return;
		}
		_delaySeconds-=Time.deltaTime;
		if(_delaySeconds>0){
			return;
		}
		bool stop=false;
		if(_currentTime>_seconds){
			_currentTime=_seconds;
			stop=true;
		}
		float t=EaseValue(_currentTime/_seconds);
		if(_targetPos!=null){

			Vector3 pos=_startPos+((Vector3)_targetPos-_startPos)*t;

			if(_islocal){
				transform.localPosition=pos;
			}else{
				transform.position=pos;
			}
		}

		if(_targetRotation!=null){

			Quaternion rotation=Quaternion.Euler( _startRotation+((Vector3)_targetRotation-_startRotation)*t);
			if(_islocal){
				transform.localRotation=rotation;
			}else{
				transform.rotation=rotation;
			}
		}

		if(_targetScale!=null){
			Vector3 scale=_startScale+((Vector3)_targetScale-_startScale)*t;
			transform.localScale=scale;
		}
		_currentTime+=Time.deltaTime;

		if(stop){
			if(_onComplete!=null){
				_onComplete();
			}
			Destroy(this);
		}

	}



	private static float EaseLinear(float t){
		return t;
	}
	private static float EaseInQuad(float t){
		return t*t;
	}

	private static float EaseOutQuad(float t){
		return -t*(t-2);
	}

	private static float EaseInOutQuad(float t){
		t*=2;
		if(t<1){
			return 0.5f*t*t;
		}
		t--;
		return -0.5f*(t*(t-2)-1);
	}

	private static float EaseInCubic(float t){
		return t*t*t;
	}

	private static float EaseOutCubic(float t){
		t--;
		return t*t*t+1;
	}

	private static float EaseInOutCubic(float t){
		t*=2;
		if(t<1){
			return 0.5f*t*t*t*t;
		}
		t-=2;
		return -0.5f*(t*t*t*t-2);
	}

	private static float EaseInElastic(float t){
		if(t==0||t==1){
			return t;
		}
		float p=0.3f;
		return -Mathf.Pow(2,10*(t-=1))*Mathf.Sin(t*2*Mathf.PI/p);
	}

	private static float EaseOutElastic(float t){
		float b=0;
		float c=1;
		float d=1;
		float a=0;
		float p=0;
		float s=0;
		if (t==0) return b;  if ((t/=d)==1) return b+c;  if (p==0) p=d*.3f;
		if (a < Mathf.Abs(c)) {
			a=c;  s=p/4; }
		else {
			s = p/(2*Mathf.PI) * Mathf.Asin (c/a);
		}
		return a*Mathf.Pow(2,-10*t) * Mathf.Sin( (t*d-s)*(2*Mathf.PI)/p ) + c + b;
	}

	public static UAnimation Make(GameObject o){
		UAnimation anim=o.AddComponent<UAnimation>();
		return anim;
	}


	public static UAnimation[] GetAnimtions(GameObject o){
		return o.GetComponents<UAnimation>();
	}

	public static void RemoveAllAnimations(GameObject o){
		UAnimation[] anims=GetAnimtions(o);
		foreach(UAnimation a in anims){
			Destroy(a);
		}
	}


	public enum EaseType{
		Linear,
		EaseInQuad,
		EaseOutQuad,
		EaseInOutQuad,
		EaseInCubic,
		EaseOutCubic,
		EaseInOutCubic,
		EaseInElastic,
		EaseOutElastic,
	}
}
