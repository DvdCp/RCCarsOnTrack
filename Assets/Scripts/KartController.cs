using UnityEngine;

public class KartController : MonoBehaviour
{
    private float speedToReach, currentSpeed;
    private float rotate, currentRotate;
    public Transform kartModel;
    public Transform respawnPoint;
    public Rigidbody _rb;
    
    [Header("Parameters")]
    public float acceleration = 30f;
    public float steering = 50f;
    public float gravity = 9f;

    private void FixedUpdate()
    {
        //Acceleration
        _rb.AddForce(kartModel.transform.forward * currentSpeed, ForceMode.Acceleration);

        //Gravity
        _rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        //Steering
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + currentRotate, 0), Time.deltaTime * 5f);

        Throttle();
        Steer();
       
    }
    
    public void Steer()
    {   float direction = 0; // Dx or Sx ? 

        if(Input.GetKeyDown(KeyCode.LeftArrow))
            direction = 1;
        else if(Input.GetKeyDown(KeyCode.RightAlt))
            direction = -1;

        rotate = steering  * direction;
    
        //int direction = input > 0 ? 1 : -1;  // Dx or Sx ?
        //float amount = Mathf.Abs((input));

       // rotate = (steering * direction) * amount;
    }

    public void Throttle() // Method for AI actions
    {   

        if(Input.GetKeyDown(KeyCode.UpArrow))
            speedToReach = acceleration;
        else if(Input.GetKeyDown(KeyCode.DownArrow))
            speedToReach = acceleration *-1;
        //speed = acceleration * input;
        currentSpeed = Mathf.SmoothStep(currentSpeed, speedToReach, Time.deltaTime * 12f); 
        speedToReach = 0f;
        currentRotate = Mathf.Lerp(currentRotate, rotate, Time.deltaTime * 4f); 
        rotate = 0f;
    }

    public void Respawn()
    {
        _rb.transform.position = respawnPoint.position;
    }
    
}
