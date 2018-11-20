using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour {

    [SerializeField]
    public Material spin;
    public MeshRenderer frame;
    public bool spotted = false;
    public float time;
    public Material[] defaults = new Material[6];
    public bool IsFound = false;
    public bool IsChosen = false;
    private Light shine;
    private static float luster = 10f;
    public bool nope = false;
    
    

	void Start () {
        frame.material = spin;
        time = Time.time;
        shine = GetComponentInChildren<Light>();

}
	
	// Update is called once per frame
	void Update () {
        shine.color = GetComponent<MeshRenderer>().material.color;
        if(IsChosen || nope)
        {
            spotted = false;
        }

        if (!IsFound)
        {
            if ((Time.time - time) > .1f)
            {
                spotted = false;
                nope = false;

            }

            if (spotted/* && !IsChosen*/)
            {
                GetComponent<MeshRenderer>().material = defaults[4];
                frame.material = spin;
                shine.intensity = luster;
            }
            else if(IsChosen)
            {
                GetComponent<MeshRenderer>().material = defaults[2];
                frame.material = spin;
                shine.intensity = luster;
            }
            else if(!spotted && nope)
            {
                GetComponent<MeshRenderer>().material = defaults[5];
                frame.material = spin;
                shine.intensity = luster;
            }
            else
            {
                GetComponent<MeshRenderer>().material = defaults[0];
                frame.material = defaults[1];
                shine.intensity = 0f;
            }
            
        }
        else
        {
            frame.material = spin;
            GetComponent<MeshRenderer>().material = defaults[3];
            shine.intensity = luster;

        }

	}



}
