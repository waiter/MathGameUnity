using UnityEngine;
using System.Collections;
using UnityEditor;

public class MapGeneratorEditor : Editor {



	[MenuItem("Sodu/Generate")]
	public static void Generate(){
		for(int i=0;i<12;i++){
			Map m=new Map();
			m.Generate(40,i/3+1);
			byte[] binary=m.Save();
			System.IO.File.WriteAllBytes("Assets/Resources/Maps/map_"+i+".txt",binary);
		}
	}
}
