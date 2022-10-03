using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyControllerScript : MonoBehaviour
{
    public float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;
    public float lookRateSpeed = 98f;
    private Vector2 lookInput, screenCenter, mouseDistance;
    private float rollInput;
    public float rollSpeed = 98f, rollAcceleration = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;
    }
    // Update is called once per frame
    void Update()
    {
        if(BombOff_Script.bombWentOff)
        {
            return;
        }
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;
        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;
        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);
        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);
        transform.Rotate(-mouseDistance.y * lookRateSpeed * Time.deltaTime, mouseDistance.x * lookRateSpeed * Time.deltaTime, rollInput * rollSpeed, Space.Self);
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);
        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += (transform.right * activeStrafeSpeed * Time.deltaTime) +(transform.up * activeHoverSpeed * Time.deltaTime);
    }

    //void Update()
    //{
        
        //if (gameObject!=null && gameObject.GetComponent(typeof(CharacterController)) && BombOff_Script.bombWentOff == false)
        //{
        //    Debug.Log("Running");
 
        //    CharacterController cc = (CharacterController)gameObject.GetComponent( typeof(CharacterController));
 
        //    float rotateSpeed = 10.0f;
        //    float rotationY = Input.GetAxis ("Mouse X") * rotateSpeed;
        //    transform.Rotate (0 , rotationY , 0);
           
        //    float moveSpeed = 40.0f;
        //    float dt = Time.deltaTime;
        //    float dy =  0;
        //    if(Input.GetKey(KeyCode.Q))
        //    {
        //        dy = moveSpeed * dt;
        //    }
        //    if(Input.GetKey(KeyCode.E))
        //    {
        //        dy -= moveSpeed * dt;
        //    }
        //    float dx = Input.GetAxis("Horizontal") * dt * moveSpeed;
        //    float dz= Input.GetAxis("Vertical") * dt * moveSpeed;
           
        //    cc.Move(transform.TransformDirection(new Vector3(dx, dy,dz))  );
        //}
 
    //}
}
