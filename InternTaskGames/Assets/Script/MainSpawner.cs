using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSpawner : MonoBehaviour {

    public List<GameObject> objList;

	// Use this for initialization
	void Start () {
        foreach(var cx in objList)
        {
            GameObject obj = Instantiate(cx);
            obj.name = obj.name.Replace("(Clone)", "");
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
