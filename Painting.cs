using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour {

    [SerializeField]
    private float sight = 8f;
    [SerializeField]
    private bool inView = false;
    [SerializeField]
    public Material spin;
    public MeshRenderer frame;
    [SerializeField]
    private Material dark;
    [SerializeField]
    private Material orange;
    public static Material interem;
    public static Material blank;
    public bool spotted = false;
    

	void Start () {
        frame.material = spin;
        interem = orange;
        blank = dark;

    }
	
	// Update is called once per frame
	void Update () {
        reveal();
        spotted = false;
        if(inView)
        {
            frame.material = spin;
        }
        else
        {
            frame.material = blank;
        }
        if(spotted)
        {
            GetComponent<MeshRenderer>().material = interem;
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
        }

	}

    public void reveal()
    {
        Collider[] inter = Physics.OverlapSphere(transform.position, sight);
        foreach(Collider space in inter)
        {
            if(space.GetComponent<Player>() != null)
            {
                inView = true;
            }
            else
            {
                inView = false;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sight);

    }
}
