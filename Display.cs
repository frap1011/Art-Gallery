using UnityEngine;

public class Display : MonoBehaviour {
    [SerializeField]
    private Art show;
    [SerializeField]
    public Light shine;
    public bool reachable;

	void Start () {

        shine = GetComponentInChildren<Light>();
        shine.intensity = 0f;
		
	}
	
	// Update is called once per frame
	void Update () {
        if(reachable && show == null)
        {
            shine.intensity = 100f;
        }
        else
        {
            shine.intensity = 0f;
        }

        if(show == null)
        {
            show = GetComponentInChildren<Art>();
        }
		else
        {
            show.transform.position = shine.transform.position;
            if(show.GetComponent<Rigidbody>() != null)
            {
                Destroy(show.GetComponent<Rigidbody>());
            }


        }
	}

}
