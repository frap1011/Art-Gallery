using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {

    public Transform portal;
    public Transform LinkedPortal;
    private MeshCollider body;


	// Use this for initialization
	void Start () {
        body = GetComponent<MeshCollider>();
        if(!body.isTrigger)
        {
            body.isTrigger = true;
        }
		
	}
	
	// Update is called once per frame
	void Update () {

		
	}
    private void OnTriggerEnter(Collider other)
    {
        Player gamer = other.GetComponent<Player>();
        gamer.transform.position = LinkedPortal.position;
    }
}
