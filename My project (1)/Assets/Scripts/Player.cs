using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    float hAxis;
    float vAxis;
    bool sprintDown;

    Vector3 moveVec;
    Vector3 sprintVec;

    Animator anim;

    void Awake()
    {
        anim= GetComponentInChildren<Animator>();
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
        moveVec = new Vector3(hAxis, 0, vAxis).normalized;
        transform.position += speed * Time.deltaTime * moveVec;
        transform.LookAt(transform.position + moveVec);
    }

    void ChangeCharactorStatus()
    {
        anim.SetBool("isWalk", moveVec != Vector3.zero);
    }
}
