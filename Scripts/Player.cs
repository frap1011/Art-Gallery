using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    private CharacterController spine;
	[SerializeField]
    private float ownTime;
    private Camera eyes;
    private bool isLooking;
    [SerializeField]
    private Transform lineOfSight;
    private Vector3 movePoint = Vector3.zero;
    private float speed = 25.0f;
    private static RaycastHit aim;
	public const float decisionTime = 4.5f;
	[SerializeField]
    private Painting DaVinci;
	[SerializeField]
	private Painting Mikey;
	[SerializeField]
	private Text rejection;
    

	private int count;
	private const int TOTAL_PAIR = 4;
	public Text countText;
	private Ray target;


    void Start () {
        spine = GetComponent<CharacterController>();
        eyes = GetComponentInChildren<Camera>();
        lineOfSight = eyes.transform;
		count = 0;
		countText.text = "Count: " + count.ToString();
		rejection.gameObject.SetActive(false);
		target = new Ray(lineOfSight.position, lineOfSight.forward);
        

	}
	
	// Update is called once per frame
	void Update () {
        float leftRight = Input.GetAxisRaw("Horizontal") * 1.5f;
        float step = Input.GetAxisRaw("Vertical") * 1.5f;
        float y = -Input.GetAxisRaw("Mouse Y");
        
        target = new Ray(lineOfSight.position, lineOfSight.forward);
		bool Notselected = DaVinci == null;
		Art();
        
        if(isLooking)
        {
            ownTime = Time.time;
        }

        if (Physics.SphereCast(target, 2f, out aim, 50f))
        {
			//The painting that was spotted if it was spotted
			Painting goal = aim.collider.gameObject.GetComponent<Painting>();
			if(goal != null && !goal.IsChosen)
			{
				isLooking = true;
				Debug.Log(goal.name + " is in sights");
				Paint(goal);
					
			}
			else
			{
				isLooking = false;
			}
          
        }

		SnapBack();
		Evaluate();
    


        lineOfSight.Rotate(y, 0f, 0f);

        movePoint = new Vector3(0, 0, step);
        movePoint = transform.TransformDirection(movePoint);
        movePoint = movePoint * speed;
        movePoint.y -= (500f * Time.deltaTime);
        transform.Rotate(0f, leftRight, 0f);
        

        spine.Move(movePoint * Time.deltaTime);

	}
	private void Paint(Painting brush){
		brush.spotted = true;
		float scene = brush.time;
		//Debugging
		Debug.Log(scene + ": Happy :" + ownTime);
		if ((ownTime - scene) > decisionTime)
		{
			brush.IsChosen = true;
			if (DaVinci == null)
				DaVinci = brush;
			else if (DaVinci != null && Mikey == null)
				Mikey = brush;
		}


	}

	private void Evaluate()
	{
		if(DaVinci != null && Mikey != null)
		{
			if(DaVinci.spin == Mikey.spin)
			{
				DaVinci.IsFound = true; Mikey.IsFound = true;
			}
			else
			{
				DaVinci.ReturnToZero(); Mikey.ReturnToZero();
				StartCoroutine("Reject");
			}
			DaVinci = null; Mikey = null;
		}
		
	}
	//Deplays a rejection if the answers are wrong
	private IEnumerator Reject()
	{
		rejection.gameObject.SetActive(true);
		yield return new WaitForSeconds(2.0f);
		rejection.gameObject.SetActive(false);
	}

	//A debugging method
	private void Art()
	{
		if(DaVinci == null)
		{
			Debug.Log("A painting has not been chosen");
		}
		else
		{
			Debug.Log("A painting has been chosen");
		}
	}
    private void SnapBack()
    {
        Vector3 neck = eyes.transform.rotation.eulerAngles;
        bool up = neck.x < 180f && neck.x < 45f;
        bool down = neck.x < 360f && neck.x > 335f;
        bool snapX = up || down;
        if (!snapX)
        {
            if (neck.x >= 45f && neck.x < 180f)
            {
                eyes.transform.Rotate(-2f, 0, 0);
            }
            else if (neck.x < 360f && neck.x <= 335f)
            {
                eyes.transform.Rotate(2f, 0, 0);
            }
        }

    }

	private void OnDrawGizmos()
	{
		Gizmos.DrawRay(target);
	}
}
