using System;
using UnityEngine;
using Mirror;

public class Player_controller : NetworkBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float speed;
    private Vector2 input;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Flip();
        CameraMov();
    }

    private void CameraMov()
    {
        Camera.main.transform.localPosition = new Vector3(transform.position.x, transform.position.y, -1f);
        transform.position =
            Vector2.MoveTowards(transform.position, Camera.main.transform.localPosition, Time.deltaTime);
    }

    private void Flip()
    {
        if (Input.GetAxis("Horizontal")>0)
        {
            transform.localRotation = Quaternion.Euler(0,180,0);
        }

        if (Input.GetAxis("Horizontal")<0)
        {
            transform.localRotation = Quaternion.Euler(0,0,0);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + input * speed / 100);
    }
}
