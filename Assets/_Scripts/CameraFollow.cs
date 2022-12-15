using UnityEngine;

namespace _Scripts
{
    public class CameraFollow : Singleton<CameraFollow>
    {
        //Public Variables
    	
        public Transform target;
        public float distance = 6.4f;
        public float height = 1.4f;
        public float rotationDamping = 3.0f;
        public float heightDamping = 2.0f;
        public float zoomRatio = 0.5f;
        public float defaultFOV = 60f;
    
        //Private Variables
        Vector3 rotationVector;
    
        void LateUpdate(){
            if(target != null){
                float wantedAngle = rotationVector.y;
                float wantedHeight = target.position.y + height; //
                float myAngle = transform.eulerAngles.y;
                float myHeight = transform.position.y;
    
                myAngle = Mathf.LerpAngle(myAngle, wantedAngle, rotationDamping * Time.deltaTime);
                myHeight = Mathf.Lerp(myHeight, wantedHeight, heightDamping * Time.deltaTime);
    
                Quaternion currentRotation = Quaternion.Euler(0, myAngle, 0);
                transform.position = target.position;
                transform.position -= currentRotation * Vector3.forward * distance;
                Vector3 temporary = transform.position;
                temporary.y = myHeight;
                transform.position = temporary;
                transform.LookAt(target);
            }
        }
    
        void FixedUpdate(){
            if(target != null){
                Vector3 localVelocity = target.InverseTransformDirection(target.GetComponent<Rigidbody>().velocity);
                if(localVelocity.z < -10.5f){
                    Vector3 temporary = rotationVector;
                    temporary.y = target.eulerAngles.y + 180;
                    rotationVector = temporary;
                }else{
                    Vector3 temporary = rotationVector;
                    temporary.y = target.eulerAngles.y;
                    rotationVector = temporary;
                }
                float acc = target.GetComponent<Rigidbody>().velocity.magnitude;
                GetComponent<Camera>().fieldOfView = defaultFOV + acc * zoomRatio * Time.deltaTime;
            }
        }
    }
}