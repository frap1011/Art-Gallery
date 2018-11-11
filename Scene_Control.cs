using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scene_Control : MonoBehaviour {

    List<Display> displays = new List<Display>();

	void Start () {
        foreach(GameObject get in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if(get.GetComponent<Display>() != null)
            {
                displays.Add(get.GetComponent<Display>());
            }
        }
        foreach(Display set in displays)
        {
            Debug.Log(set.name);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
