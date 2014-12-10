using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioController  {

	private static AudioSource _source;
	private static Dictionary<string,AudioClip> _clipMap=new Dictionary<string,AudioClip>();
	public static AudioSource source{
		get{
			if(_source==null){
				_source=new GameObject("AudioSource").AddComponent<AudioSource>();
			}
			return _source;
		}
	}
	public static void Play(string name){
		if(!_clipMap.ContainsKey(name)){
			AudioClip clip=Resources.Load<AudioClip>("Audios/"+name);
			if(clip!=null){
				_clipMap.Add(name,clip);
			}
		}
		source.PlayOneShot(_clipMap[name]);
	}

}
