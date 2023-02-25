using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float sprintCooldownduration;
    public float sprintSpeed;
    public float sprintDuration;
    float sprintCooldown;
    float hAxis;
    float vAxis;
    bool sprintDown;
    bool sprintStatus =false;

    Vector3 moveVec;

    Animator anim;
    private Rigidbody charRigidbody;


    void Awake()
    {
        anim= GetComponentInChildren<Animator>();
    }

    void Start()
    {
        charRigidbody = GetComponentInChildren<Rigidbody>();
    }

    void Update()
    {
        GetInput();
        MoveCharactorByInput();
        ChangeCharactorStatus();
    }



    void GetInput()
    {
        GetPlanarMoveInput();
        GetExtraMoveInput();
    }

    void GetPlanarMoveInput() 
    {
        hAxis = Input.GetAxis("Horizontal");
        vAxis = Input.GetAxis("Vertical");
    }

    void GetExtraMoveInput()
    {
        sprintDown = Input.GetButtonDown("Sprint");
    }


    void MoveCharactorByInput()
    {
        MovePlanarrly();
        if (sprintDown && IsSprintOn())
        {
            SprintAndSetCooldown();
        }
        if (!IsSprintOn())
        {
            sprintCooldown += Time.deltaTime;
        }
    }

    void MovePlanarrly()
    {
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        charRigidbody.velocity = speed * moveVec;
        transform.LookAt(transform.position + moveVec);
    }

    void SprintAndSetCooldown()
    {
        sprintCooldown = 0;
        speed *= sprintSpeed;
        sprintStatus = true;
        Invoke(nameof(SprintOut), sprintDuration);
    }

    void SprintOut()
    {
        speed /= sprintSpeed;
        sprintStatus = false;
    }

    bool IsSprintOn()
    {
        if (sprintCooldown >= sprintCooldownduration)
        {
            return true;
        }
        else { return false; }
    }

    void ChangeCharactorStatus()
    {
        anim.SetBool("isSprint", sprintStatus == true && moveVec != Vector3.zero);
        anim.SetBool("isWalk", sprintStatus == false && moveVec != Vector3.zero);
    }
}
