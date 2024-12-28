using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public LayerMask GroundLayers;
    private bool grounded = false;
    private float jumpforce = 5;
    private Rigidbody body;
    private static float maxSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundedCheck();
        JumpAndGravity();
        if (grounded) Move(false);
        else Move(true);
        ClampSpeed();
    }

    //Wall Jump disabled
    private void GroundedCheck()
    {
        //Sphere is set so it only checks feet
        grounded = Physics.CheckSphere(transform.position + Vector3.down * .2f, .4f, GroundLayers);
    }

    private void JumpAndGravity()
    {
        if (grounded && Input.GetKeyDown("space"))
        {
            body.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        }
    }

    private void Move(bool air = false)
    {
        float horizontalRotation = transform.GetChild(0).gameObject.GetComponent<AlignCamera>().horizontalRotation;
        Vector3 force = Vector3.zero;
        switch (Direction())
        {
            case 0:
                force = Vector3.forward;
                break;
            case 1:
                force = (Vector3.forward + Vector3.right).normalized;
                break;
            case 2:
                force = Vector3.right;
                break;
            case 3:
                force = (Vector3.right + Vector3.back).normalized;
                break;
            case 4:
                force = Vector3.back;
                break;
            case 5:
                force = (Vector3.back + Vector3.left).normalized;
                break;
            case 6:
                force = Vector3.left;
                break;
            case 7:
                force = (Vector3.left + Vector3.forward).normalized;
                break;
            default:
                if (!air) body.velocity = new Vector3(body.velocity.x/1.01f, body.velocity.y, body.velocity.z/1.01f);
                break;
        }
        Vector3 rotatedForce = Quaternion.AngleAxis(horizontalRotation, Vector3.up) * force;
        float horizontalVelocity = new Vector2(body.velocity.x, body.velocity.z).magnitude;
        if (horizontalVelocity < 15) horizontalVelocity *= 4f;
        float currentSpeed = Vector3.ProjectOnPlane(body.velocity, Vector3.up).magnitude;
        float relativeSpeed = Vector3.Dot(rotatedForce, body.velocity);
        if (force != Vector3.zero)
        {
            body.velocity = Vector3.ClampMagnitude(rotatedForce.normalized * (currentSpeed + relativeSpeed) / 2, 150) + body.velocity.y * Vector3.up;
            if (!air) ApplyForce(rotatedForce * 12f);
            else ApplyForce(rotatedForce * 3f);
        }
    }

    public void ApplyForce(Vector3 force)
    {
        body.AddForce(force);
    }

    /*  
     *  -1: none
     *  0: forward
     *  1: diagonal
     *  2: right
     *  3: diagonal
     *  4: back
     *  5: diagonal
     *  6: left
     *  7: diagonal
    */
    private int Direction()
    {
        bool forward = (Input.GetKey("w") || Input.GetKey("u") || Input.GetKey("up"));
        bool right = (Input.GetKey("d") || Input.GetKey("k") || Input.GetKey("right"));
        bool back = (Input.GetKey("s") || Input.GetKey("j") || Input.GetKey("down"));
        bool left = (Input.GetKey("a") || Input.GetKey("h") || Input.GetKey("left"));

        if (forward)
        {
            if (right && !left) return 1;
            if (!right && left) return 7;
            if (!back) return 0;
        }
        if (back)
        {
            if (right && !left) return 3;
            if (!right && left) return 5;
            if (!forward) return 4;
        }
        if (right && !left) return 2;
        if (!right && left) return 6;
        return -1;
    }

    private void ClampSpeed()
    {
        Vector3 velocity = body.velocity;
        Vector3 modifiedVelocity = Vector3.ProjectOnPlane(velocity, Vector3.up);
        //Horizontal
        float ratio = modifiedVelocity.magnitude / maxSpeed;
        if (ratio > 1) modifiedVelocity /= ratio;
        modifiedVelocity = new Vector3(modifiedVelocity.x, velocity.y, modifiedVelocity.z);
        //Vertical
        ratio = velocity.y / (maxSpeed * 4f);
        if (ratio > 1) modifiedVelocity.y /= ratio;
        body.velocity = modifiedVelocity;
    }

}
