using UnityEngine;

public class Player : MonoBehaviour {

    private CharacterController spine;
    private Camera eyes;
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private Transform lineOfSight;
    private Vector3 movePoint = Vector3.zero;
    public float speed = 16.0f;
    private static RaycastHit aim;
    [SerializeField]
    private bool isHolding = false;
    [SerializeField]
    private Hand palm;
    [SerializeField]
    private Art skull;
    private Display target;



    void Start () {
        spine = GetComponent<CharacterController>();
        eyes = GetComponentInChildren<Camera>();
        lineOfSight = eyes.transform;
        palm = GetComponentInChildren<Hand>();
        
        
        
        

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
        //bool right = neck.y 

            

        if (Physics.SphereCast(lineOfSight.position, 4f, lineOfSight.forward, out aim, 10f))
        {
            if(aim.collider.gameObject.GetComponent<Display>() != null)
            {
                target = aim.collider.gameObject.GetComponent<Display>();
                if(Input.GetMouseButtonDown(0) && isHolding)
                {
                    place(target.shine.transform);
                }
            }
        }
        else
        {
            if(target != null)
            {
                target = null;
            }
        }


        lineOfSight.Rotate(y, 0f, 0f);

        movePoint = new Vector3(0, 0, step);
        movePoint = transform.TransformDirection(movePoint);
        movePoint = movePoint * speed;
        movePoint.y -= (50f * Time.deltaTime);
        transform.Rotate(0f, leftRight, 0f);
        if(Input.GetMouseButtonDown(0) && !isHolding)
        {
            hold();
        }

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
        

        
        
		
	}

    //Puts the piece of art in your hand if it is reachable
    private void hold()
    {
        if(Physics.SphereCast(lineOfSight.position,4f,lineOfSight.forward, out aim, 10f))
        {
            if(aim.collider.GetComponent<Art>() != null)
            {
                GameObject piece = aim.collider.gameObject;
                Debug.Log(piece.name);
                piece.transform.SetParent(palm.transform);
                piece.transform.position = palm.transform.position;
                Destroy(piece.GetComponent<Rigidbody>());
                skull = piece.GetComponent<Art>();
                isHolding = true;
                
            }
        }
        
    }

    private void place(Transform tgt)
    {
        skull.transform.SetParent(tgt);
        skull.transform.position = tgt.position;
        if(skull.GetComponent<Rigidbody>().useGravity)
        {
            skull.GetComponent<Rigidbody>().useGravity = false;
        }
        skull = null;
        isHolding = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(lineOfSight.position, lineOfSight.forward * 10f);
    }


}
