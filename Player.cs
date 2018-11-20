using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    private CharacterController spine;
    private Camera eyes;
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private Transform lineOfSight;
    private Vector3 movePoint = Vector3.zero;
    private float speed = 25.0f;
    private static RaycastHit aim;
    [SerializeField]
    private bool isHolding = false;
    [SerializeField]
    private Hand palm;
    /*[SerializeField]
    private Art skull;*/
    private Display target;
    private Painting DaVinci;

	private int count;
	private const int TOTAL_PAIR = 1;
	public Text countText;
	public Text finishText;


    void Start () {
        spine = GetComponent<CharacterController>();
        eyes = GetComponentInChildren<Camera>();
        lineOfSight = eyes.transform;
        palm = GetComponentInChildren<Hand>();
		count = 0;
		countText.text = "Count: " + count.ToString();
		finishText.text = "";



	}

	// Update is called once per frame
	void Update () {
        float leftRight = Input.GetAxisRaw("Horizontal") * 1.5f;
        float step = Input.GetAxisRaw("Vertical") * .5f;
        float y = -Input.GetAxisRaw("Mouse Y");
        float sight = Input.GetAxisRaw("Mouse ScrollWheel") * 50f;
        Vector3 neck = eyes.transform.rotation.eulerAngles;
        bool up = neck.x < 180f && neck.x < 45f;
        bool down = neck.x < 360f && neck.x > 335f;
        bool snapX = up || down;




        /*
        if (Physics.SphereCast(lineOfSight.position, 8f, lineOfSight.forward, out aim, 10f))
        {
            if(aim.collider.gameObject.GetComponent<Display>() != null)
            {
                target = aim.collider.gameObject.GetComponent<Display>();
                if(Input.GetMouseButtonDown(0) && isHolding)
                {
                    target.isOccupied = true;
                    skull.transform.SetParent(target.transform);
                    skull.transform.position = target.transform.position;
                    target.show = skull;
                    skull = null;
                }
            }
        }
        else
        {
            if(target != null)
            {
                target = null;
            }
        }*/
        Ray target = new Ray(lineOfSight.position, lineOfSight.forward);
        if (Physics.SphereCast(target, 2f, out aim, 10f))
        {
            if(aim.collider.GetComponent<Painting>() != null)
            {
                Painting get = aim.collider.GetComponent<Painting>();
                Debug.Log(get);
                get.spotted = true;
                get.time = Time.time;
                if(Input.GetMouseButtonDown(0) && DaVinci == null && !get.IsFound)
                {
                    DaVinci = get;
                    DaVinci.IsChosen = true;
                }
                else if(Input.GetMouseButtonDown(0) && DaVinci != null && !get.IsFound && DaVinci.name != get.name)
                {
                    DaVinci.IsChosen = false;
                    if(DaVinci.spin == get.spin)
                    {
                        DaVinci.IsFound = true;
                        get.IsFound = true;
						count++;
						countText.text = "Count: " + count.ToString();
                    }
                    else
                    {
                        DaVinci.frame.material = DaVinci.defaults[5];
                    }
                    DaVinci = null;
                }
                //get.GetComponent<MeshRenderer>().material = Painting.interem;

				if (count == TOTAL_PAIR)
                {
					GameObject wall = GameObject.FindGameObjectWithTag("Wall");
					//先设置它的可用为false，就看不见它了
					Destroy(wall);
                    finishText.text = "Door open";
                }


            }
        }




        lineOfSight.Rotate(y, 0f, 0f);

        movePoint = new Vector3(0, 0, step);
        movePoint = transform.TransformDirection(movePoint);
        movePoint = movePoint * speed;
        movePoint.y -= (500f * Time.deltaTime);
        transform.Rotate(0f, leftRight, 0f);
        /*
        if(Input.GetMouseButtonDown(0) && !isHolding)
        {
            hold();
        }*/

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

        spine.Move(movePoint * Time.deltaTime);
        //holdOut();





	}
    /*
	private void OnTriggerEnter(Collider other)
    {
        if ((count == TOTAL_PAIR) && other.gameObject.CompareTag("Wall"))
        {
            other.gameObject.SetActive(false);
            finishText.text = "Door open";
        }


    }*/

    //Puts the piece of art in your hand if it is reachable
    /*
    private void hold()
    {

        if(Physics.SphereCast(lineOfSight.position,4f,lineOfSight.forward, out aim, 6f))
        {
            if(aim.collider.GetComponent<Art>() != null && (!aim.collider.GetComponent<Art>().OnDisplay
                || !aim.collider.GetComponent<Display>().isOccupied)){
                GameObject piece = aim.collider.gameObject;
                Debug.Log(piece.name);
                piece.transform.SetParent(palm.transform);
                piece.transform.position = palm.transform.position;
                Destroy(piece.GetComponent<Rigidbody>());
                skull = piece.GetComponent<Art>();

            }
        }


    }*/
    private void holdOut()
    {
        isHolding = palm.GetComponentInChildren<Art>() != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(lineOfSight.position, lineOfSight.forward * 10f);
    }

}
