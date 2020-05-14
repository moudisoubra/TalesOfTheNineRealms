using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovementController : MonoBehaviour
{
    [Header("//------ Odin main values ------")]
    public float speed;
    public float rotationSlerpSpeed;
    public bool stopMovement;
    public Animator anim;

    private Rigidbody RB; 
    private Transform mainCameraValues;

    public Vector2 movement;
    public Vector3 hitpoint;
    public float hitPointTimer = 5;
    public GameObject hitChecker;

    void Start()
    {
        anim = GetComponent<Animator>();
        mainCameraValues = Camera.main.transform;
        RB = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!stopMovement)
        {
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else
        {
            //Turn this "stopMovement" to true when you do cutscenes/dialogue.
            //movement = new Vector2(0, 0); //Sets the speed to zero so he cant move.
        }

        Vector3 cameraForward = mainCameraValues.forward; //We want to make the player move based on the camrea directions/position so we can change the camera.
        Vector3 cameraRight = mainCameraValues.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        //If we want blendtrees then we can set the value of the blend/curve using the normalized input 
        // anim.SetFloat("horizontal", Input.GetAxisRaw("Horizontal"));
        // anim.SetFloat("vertical", Input.GetAxisRaw("Vertical"));

        //Need to normalize because diagonal movement would ramp
        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 direction = (cameraForward * movement.y + cameraRight * movement.x).normalized;

        if (direction.magnitude != 0) //If there is input, then rotate object based on direction.
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSlerpSpeed);
        }

        if (direction.magnitude > 0)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        }

        var moveWithVelo = direction * speed; //Creating new varible for player movement. 
        moveWithVelo.y = RB.velocity.y; //making sure we are not messing with the Y & it stays the same.
        RB.velocity = moveWithVelo; //Making the player move using velocity rather than add force! :D
        hitPointTimer += Time.deltaTime;
        Debug.DrawRay(hitChecker.transform.position, Vector3.down);
        RaycastHit hit;
        Ray ray = new Ray(hitChecker.transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("InstantGround") && hitPointTimer > 5)
            {
                hitpoint = hit.point;
                hitPointTimer = 0;
            }
        }
    }
}
