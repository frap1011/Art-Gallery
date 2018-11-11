using UnityEngine;

public class Art : MonoBehaviour {

    [SerializeField]
    private Rigidbody surface;
    public bool OnDisplay;
    public bool BeingHeld;

    

	void Start () {
        surface = GetComponent<Rigidbody>();
        OnDisplay = false;
        BeingHeld = false;


	}
	
	// Update is called once per frame
	void Update () {
        Check1();
        Check2();
		
	}
    private void Check1()
    {
        OnDisplay = GetComponentInParent<Display>() != null;
    }

    private void Check2()
    {
        BeingHeld = GetComponentInParent<Hand>() != null;
    }
    
}
