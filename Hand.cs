using UnityEngine;

public class Hand : MonoBehaviour {

    [SerializeField]
    public static Art art;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(art == null)
        {
            art = GetComponentInChildren<Art>();
        }
		
	}
}
