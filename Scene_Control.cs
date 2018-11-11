using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Scene_Control : MonoBehaviour {

    List<Display> displays = new List<Display>();
    int cond;

	void Start () {
        foreach(GameObject get in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if(get.GetComponent<Display>() != null)
            {
                displays.Add(get.GetComponent<Display>());
                cond++;
            }
        }
        foreach(Display set in displays)
        {
            Debug.Log(set.name);
        }
	}
	
	// Update is called once per frame
	void Update () {
		if(complete())
        {
            Debug.Log("You completed the level");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
	}

    private bool complete()
    {
        int pick = 0;
        foreach(Display check in displays)
        {
            if(check.right)
            {
                pick++;
            }
        }
        return cond == pick;
    }
}
