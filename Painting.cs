using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painting : MonoBehaviour {

    [SerializeField]
    public Material spin;
    public MeshRenderer frame;
    public bool spotted = false;
    public float time;
    public Material[] defaults = new Material[5];
    public bool IsFound = false;
    public bool IsChosen = false;
    private Light shine;
    private static float luster = 10f;
    
    

	void Start () {
        frame.material = defaults[1];
        time = Time.time;
        shine = GetComponentInChildren<Light>();
        shine.intensity = 0f;

	}
	
	// Update is called once per frame
	void Update () {

		restart();
		
		shine.color = GetComponent<MeshRenderer>().material.color;
		if(spotted && !IsFound)
		{
			frame.material = spin;
			GetComponent<MeshRenderer>().material = defaults[4];
			Shine();
		}
		else
		{
			time = Time.time;
			shine.intensity = 0f;
			GetComponent<MeshRenderer>().material = defaults[0];
			frame.material = defaults[1];
		}

		if(IsChosen && !IsFound){
			frame.material = spin;
			GetComponent<MeshRenderer>().material = defaults[2];
			Shine();
			IsChosen = true;
		}
		if(IsFound)
		{
			frame.material = spin;
			GetComponent<MeshRenderer>().material = defaults[3];
			Shine();
			ReturnToZero();
			
		}


        /*if(spotted)
        {
            frame.material = spin;
            GetComponent<MeshRenderer>().material = defaults[4];
            Shine();
        }
		else if(nope)
		{
			Shine();
			GetComponent<MeshRenderer>().material = defaults[5];
			frame.material = spin;
			spotted = false;
			IsChosen = false;
		}
        else
        {
            time = Time.time;
            shine.intensity = 0f;
            GetComponent<MeshRenderer>().material = defaults[0];
            frame.material = defaults[1];
        }
        if(IsChosen)
        {
            GetComponent<MeshRenderer>().material = defaults[2];
            frame.material = spin;
            spotted = false;
            Shine();
        }
        if(IsFound)
        {
            Shine();
            GetComponent<MeshRenderer>().material = defaults[3];
            frame.material = spin;
            spotted = false;
			IsChosen = false;
        } */




    }



    //Determines whether or not the painting is seen
    public void Shine()
    {
        shine.intensity = luster;
    }

    public void inSights()
    {
        spotted = true;
        time = Time.time;
    }


    public string toString()
    {
        return "Current Painting: " + name;
    }


	//Returns the painting to it's original form if it has not be chose and not in sights
	private void restart(){
		if(spotted && !IsChosen){
			if ((Time.time - time) > (Player.decisionTime + 1f))
				spotted = false;
		}
	}

	public void ReturnToZero()
	{
		spotted = false; IsChosen = false;
	}


}
