using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{

    public float speed;
    public float turnSpeed;
    public float horizontalInput;
    public float VerticalInput;
    Vector3 start_pos;
    Vector3 first_pos;
    Vector3 dest;
    Vector3 dest_rot;
    public float distance_parcouru;
    public float distance_parcouru_ancienne;
    int cpt;
    int last_speed_upgrade;
    float turn;
    Rigidbody RB;
    float rot;
    public bool can_move;

    public score new_score;

    private void Start()
    {
        start_pos = GameObject.Find("Ground_road_start").transform.position;
        first_pos = transform.position;
        last_speed_upgrade = 0;
        RB = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        //VerticalInput = Input.GetAxis("Vertical");

        //transform.Translate(Vector3.forward * Time.deltaTime * speed * VerticalInput);

        distance_parcouru = first_pos.z + transform.position.z;

        cpt = (int)(distance_parcouru / 100);

        if (cpt > last_speed_upgrade)
        {
            speed += 0.01f;
            last_speed_upgrade = cpt;
        }

        new_score.update_score((int)distance_parcouru);
    }

    private void FixedUpdate()
    {
        if (can_move)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            VerticalInput = Mathf.Abs(Input.GetAxis("Vertical"));


            if (VerticalInput != 0)
            {
                transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed * horizontalInput);
                //turn = 0.3f * horizontalInput;
            }

            rot = transform.rotation.y * 100;

            //Debug.Log(rot);
            if (rot > 10 || rot < -10)
                turn = 0.15f * rot / 10;
            else
                turn = 0;

            dest = RB.transform.position + new Vector3(turn, 0, speed / 10) * VerticalInput;
            RB.MovePosition(dest);

            //RB.MoveRotation(Quaternion.Euler(0, turnSpeed * horizontalInput, 0));

            //if (VerticalInput != 0)
            //    RB.MoveRotation(Quaternion.Euler(0, Time.fixedDeltaTime * turnSpeed * horizontalInput, 0));
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("road"))
            can_move = true;
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("road"))
            can_move = false;
        else
            can_move = true;
    }
}
