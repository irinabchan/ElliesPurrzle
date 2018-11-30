using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour {

    private static Singleton _instance;

    public static Singleton Instance 
    {
        get { return _instance; }
    }
	
	// Update is called once per frame
	private void Awake() {
        if ( (_instance != null) && (_instance != this) )
        {
            Debug.Log("Something was destroyed");
            Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("NOT destroyed");
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
	}
}
