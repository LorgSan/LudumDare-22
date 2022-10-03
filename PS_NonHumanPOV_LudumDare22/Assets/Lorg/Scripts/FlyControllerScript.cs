using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyControllerScript : MonoBehaviour
{
    void Update()
    {
        if(gameObject!=null && gameObject.GetComponent(typeof(CharacterController)) && BombOff_Script.bombWentOff == false)
        {
            Debug.Log("Running");
 
            CharacterController cc = (CharacterController)gameObject.GetComponent( typeof(CharacterController));
 
            float rotateSpeed = 10.0f;
            float rotationY = Input.GetAxis ("Mouse X") * rotateSpeed;
            transform.Rotate (0 , rotationY , 0);
           
            float moveSpeed = 40.0f;
            float dt = Time.deltaTime;
            float dy =  0;
            if(Input.GetKey(KeyCode.Q))
            {
                dy = moveSpeed * dt;
            }
            if(Input.GetKey(KeyCode.E))
            {
                dy -= moveSpeed * dt;
            }
            float dx = Input.GetAxis("Horizontal") * dt * moveSpeed;
            float dz= Input.GetAxis("Vertical") * dt * moveSpeed;
           
            cc.Move(transform.TransformDirection(new Vector3(dx, dy,dz))  );
        }
 
    }
}
