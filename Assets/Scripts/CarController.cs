using UnityEngine;

public class CarController : MonoBehaviour
{
    public Transform kartModel;
    public Transform kartNormal;
    public Rigidbody sphere;

    float speed, currentSpeed;
    float rotate, currentRotate;

    [Header("Parameters")]

    public float acceleration; 
    public float steering; 
    public float gravity; 
    public LayerMask layerMask;

    [Header("Model Parts")]

    public Transform frontWheels;
    public Transform backWheels;

    void Update()
    {
        //Follow Collider & Car model stabilization
        transform.position = sphere.transform.position - new Vector3(0, 0.08f, 0);

        /*
        //Accelerate  forward
        if (Input.GetKey(KeyCode.UpArrow))
            speed = acceleration;
  
        //Accelerate  backward
        if (Input.GetKey(KeyCode.DownArrow))
            speed = acceleration/2 * -1;
        */

        //Accelerate
        if (Input.GetAxis("Vertical") != 0)
        {
            float gear = Input.GetAxis("Vertical");
            speed = acceleration * gear;
        }
        else
            speed = 0f;

        //Steer
        if (Input.GetAxis("Horizontal") != 0)
        {
            int dir = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
            float amount = Mathf.Abs((Input.GetAxis("Horizontal")));
            
            Steer(dir, amount);
        }

        currentSpeed = Mathf.SmoothStep(currentSpeed, speed, Time.deltaTime * 5f); //speed = 0f;
        currentRotate = Mathf.Lerp(currentRotate, rotate, Time.deltaTime * 4f); //rotate = 0f;
        

        //Animations    

        //a) Wheels
        frontWheels.localEulerAngles = new Vector3(0, (Input.GetAxis("Horizontal") * 10), frontWheels.localEulerAngles.z);
    
    }

    private void FixedUpdate()
    {
        //Forward Acceleration
        sphere.AddForce(kartModel.transform.forward * currentSpeed, ForceMode.Acceleration);
        
        //Gravity
        sphere.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        Debug.Log("Current NOR"+currentSpeed);
        Debug.Log("Current ABS"+Mathf.Floor(currentSpeed));
        //Steering
        // Current speed forward (0)     Current speed backward(-1) 
        if(Mathf.Floor(currentSpeed) != 0 && Mathf.Floor(currentSpeed) != -1)
        {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + currentRotate, 0), Time.deltaTime * 5f);   
        }
        
        RaycastHit hitOn;
        RaycastHit hitNear;

        Physics.Raycast(transform.position + (transform.up*.1f), Vector3.down, out hitOn, 1.1f,layerMask);
        Physics.Raycast(transform.position + (transform.up * .1f)   , Vector3.down, out hitNear, 2.0f, layerMask);

        //Normal Rotation
        kartNormal.up = Vector3.Lerp(kartNormal.up, hitNear.normal, Time.deltaTime * 8.0f);
        kartNormal.Rotate(0f, transform.eulerAngles.y, 0f);
    }

    public void Steer(int direction, float amount)
    {
        rotate = (steering * direction) * amount;
    }

    private void Speed(float x)
    {
        currentSpeed = x;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(transform.position + transform.up, transform.position - (transform.up * 2));
    //}
}
