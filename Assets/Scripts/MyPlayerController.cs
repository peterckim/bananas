using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class MyPlayerController : MonoBehaviour
{
    Rigidbody rb;
    float speed = 25.0F;
    float rotationSpeed = 50.0F;
    float jumpspeed = 20;
    bool istouching = true;
    Animator animator;

    //[SyncVar]
    //bool switched;

    //private float driftSeconds = 1.5f;
    //private float driftTimer = 0.0f;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        //Rotate();
        //animator = GetComponentInChildren<Animator>();
        //animator.SetBool("Idling", true);


        TouchListener.OnLongPressing += OnPlayerPressing;
        TouchListener.OnDoubleClick += OnPlayerDoubleClick;

    }

    void FixedUpdate()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        Quaternion turn = Quaternion.Euler(0f,rotation,0f);
        rb.MovePosition(rb.position + this.transform.forward * translation);
        rb.MoveRotation(rb.rotation * turn);

        // Animation
        //if (translation != 0)
        //{
        //    animator.SetBool("Idling", false);
        //    this.GetComponent<SetupLocalPlayerAnimation>().CmdChangeAnimState("run");
        //}
        //else
        //{
        //    animator.SetBool("Idling", true);
        //    this.GetComponent<SetupLocalPlayerAnimation>().CmdChangeAnimState("idle");
        //}

        //if (Input.GetKeyDown("space"))
        //{
        //    animator.SetTrigger("Attacking");
        //    this.GetComponent<SetupLocalPlayerAnimation>().CmdChangeAnimState("attack");
        //}

        //if (switched)
        //{
        //    this.transform.localScale = new Vector3(this.transform.localScale.x * 2, this.transform.localScale.y * 2, this.transform.localScale.z * 2);
        //}
        //Camera.main.transform.parent.position = this.transform.GetChild(1).gameObject.transform.position;
        //Camera.main.transform.parent.rotation = this.transform.GetChild(1).gameObject.transform.rotation;
        //Rotate();
    }


    //void Rotate()
    //{
    //    this.transform.rotation = Camera.main.transform.localRotation;
    //}

    void Move()
    {
        // Move forward
        //This part doesnt run
        transform.position += Camera.main.transform.forward * Time.deltaTime * 20.0f;
    }

    void Jump()
    {
        if (istouching)
        {
            Vector3 balljump = new Vector3(0.0f, 30.0f, 0.0f);
            rb.AddForce(balljump * jumpspeed);
        }
        istouching = false;

    }

    //public override void OnStartLocalPlayer()
    //{
    //    Camera.main.transform.parent.position = this.transform.GetChild(1).gameObject.transform.position;
    //    Camera.main.transform.parent.rotation = this.transform.GetChild(1).gameObject.transform.rotation;
    //}

    public void OnPlayerPressing(Touch t){
        Move();
    }

    //public void OnHiderSingleClick(Touch touch){
    //}

    public void OnPlayerDoubleClick(Touch touch) {
        Jump();
    }

    private void OnCollisionStay()
    {
        istouching = true;
    }

}