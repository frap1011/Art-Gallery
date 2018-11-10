using UnityEngine.SceneManagement;
using UnityEngine;

public class Scene_Control : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log(SceneManager.GetActiveScene().name);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
