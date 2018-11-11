using UnityEngine;

public class Display : MonoBehaviour {
    [SerializeField]
    public Art show;
    [SerializeField]
    public Light shine;
    public bool reachable;
    [SerializeField]
    private string correct;
    [SerializeField]
    private bool right;
    public bool isOccupied;

	void Start () {

        shine = GetComponentInChildren<Light>();
        shine.intensity = 0f;
        reachable = false;
        right = false;
        isOccupied = false;

        
		
	}
	
	// Update is called once per frame
	void Update () {
        reachable = scan();
        
        
     
        if(isOccupied)
        {
            right = scan2();
        }
        


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
            isOccupied = true;
            Debug.Log(show.name.Equals(correct));
        }
	}

    private bool scan()
    {
        int count = 0;
        foreach(Collider worker in Physics.OverlapSphere(transform.position,10f))
        {
            if(worker.GetComponent<Player>() != null)
            {
                count++;
            }
        }
        return count > 0;
    }

    private bool scan2()
    {
        return show.name == correct; 
    }

}
