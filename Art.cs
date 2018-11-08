using UnityEngine;

public class Art : MonoBehaviour {

    [SerializeField]
    private Rigidbody surface;
    
    

	void Start () {
        surface = GetComponent<Rigidbody>();


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
