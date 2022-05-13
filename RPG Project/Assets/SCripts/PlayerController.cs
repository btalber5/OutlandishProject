using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Scripts
{
    [RequireComponent(typeof(PlayerMotor))]
    public class PlayerController : MonoBehaviour
    {

        Camera cam;
        public LayerMask movementMask;
        PlayerMotor motor;
        

        //double click variables
        private int clicked = 0;
        private float clickTime = 0;
        private float clickDelay = 0.4f;
        private bool doubleClick;
        private bool canMove = false;
        //checks if talking to an npc
        internal bool dialoguing;
        internal bool hasInteracted;
        Animator animator;
        InventoryUI UIManager;

        //for checking dash 
        Vector3 returnVal;
        string direction;


        public Interactable focus;
        // Start is called before the first frame update
        void Start()
        {
            cam = Camera.main;
            motor = GetComponent<PlayerMotor>();
            animator = GetComponentInChildren<Animator>();
            UIManager = GameObject.Find("Canvas").GetComponent<InventoryUI>();
            dialoguing = false;
            
        }

        // Update is called once per frame
        void Update()
        {

            if (dialoguing == false) {
                if (Input.GetMouseButtonDown(0))
                {


                    Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                    RaycastHit hit;



                    if (Physics.Raycast(ray, out hit, 100, movementMask))
                    {

                        //move player to what is hit
                        motor.MoveToPoint(hit.point);
                        //Debug.Log(hit.point);
                        //stop focusing objects

                        RemoveFocus();

                    }
                }




                if (Input.GetMouseButtonDown(1))
                {

                    Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 100))
                    {

                        Interactable interactable = hit.collider.GetComponent<Interactable>();
                        
                        if (interactable != null && !interactable.hasInteracted)
                        {

                            SetFocus(interactable);

                        }

                        else {

                            RemoveFocus();
                        
                        }
                    }

                }
                if (Input.GetMouseButtonDown(2)) {

                    cam.GetComponent<CameraController>().orbitPlayer = !cam.GetComponent<CameraController>().orbitPlayer;


                }

                if (Input.GetKeyDown(KeyCode.W)) {

                    Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100, movementMask))
                    {
                        direction = "up";
                        //move player to what is hit

                        motor.Dash(direction);
                        animator.SetBool("ifDash", true);

                        motor.stopMoving(motor.getAgent().transform.position);
                        //stop focusing objects

                        RemoveFocus();
                        doubleClick = false;
                    }



                }

                if (Input.GetKeyDown(KeyCode.S))
                {

                    Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100, movementMask))
                    {
                        direction = "down";
                        //move player to what is hit

                        motor.Dash(direction);
                        animator.SetBool("ifDash", true);

                        motor.stopMoving(motor.getAgent().transform.position);
                        //stop focusing objects

                        RemoveFocus();
                        doubleClick = false;
                    }



                }

                if (Input.GetKeyDown(KeyCode.A))
                {

                    Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100, movementMask))
                    {
                        direction = "left";
                        //move player to what is hit

                        motor.Dash(direction);
                        animator.SetBool("ifDash", true);

                        motor.stopMoving(motor.getAgent().transform.position);
                        //stop focusing objects

                        RemoveFocus();
                        doubleClick = false;
                    }



                }

                if (Input.GetKeyDown(KeyCode.D))
                {

                    Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 100, movementMask))
                    {
                        direction = "right";
                        //move player to what is hit

                        motor.Dash(direction);
                        animator.SetBool("ifDash", true);

                        motor.stopMoving(motor.getAgent().transform.position);
                        //stop focusing objects

                        RemoveFocus();
                        doubleClick = false;
                    }



                }

                if (Input.GetKeyDown(KeyCode.I)) {

                    UIManager.toggleCanvas = !UIManager.toggleCanvas;
                    Debug.Log(UIManager.toggleCanvas);
                }
            }



        }

        public Vector3 getMouseDirection(Vector3 point) {
            
            if (point.x < 0 && point.z < 0) {

                returnVal = new Vector3(-2f,0f,-2f); 
            
            }
            else if (point.x < 0 && point.z > 0) {

                returnVal = new Vector3(-2f, 0f, 2f);

            }
            else if (point.x > 0 && point.z > 0)
            {

                returnVal = new Vector3(2f, 0f, 2f);

            }
            else if (point.x > 0 && point.z < 0) {

                returnVal = new Vector3(2f, 0f, -2f);


            }

            else if (point.x  == 0 && point.z < 0)
            {

                returnVal = new Vector3(0f, 0f, -2f);


            }
            else if (point.x == 0 && point.z > 0)
            {

                returnVal = new Vector3(0f, 0f, 2f);


            }
            else if (point.x > 0 && point.z == 0)
            {

                returnVal = new Vector3(2f, 0f, 0f);


            }

            else if (point.x < 0 && point.z == 0)
            {

                returnVal = new Vector3(-2f, 0f, 0f);


            }




            return returnVal;
        }

        void checkDoubleClick() {


            clicked++;
            if (clicked == 1)
            {

                clickTime = Time.time;

                if (clicked == 1 && Time.time > clickTime + clickDelay)
                {

                    doubleClick = false;
                    clicked = 0;

                }


            }



            if (clicked > 1 && Time.time - clickTime < clickDelay)
            {
                doubleClick = true;
                clicked = 0;
                clickTime = 0;
                Debug.Log("Double clicked");


            }

            else if (clicked > 2 || Time.time - clickTime > 1)
            {

                clicked = 0;
            }

        }
        void SetFocus(Interactable interactable)
        {
            if (interactable != focus)
            {
                if (focus != null) {

                    focus.OnDeFocused();
                
                }
                focus = interactable;
                motor.FollowTarget(focus);

            }
            
            interactable.OnFocused(transform);
        }

        void RemoveFocus() {
            if (focus != null)
            {
                focus.OnDeFocused();
            }
            focus = null;
            motor.StopFollowingTarget();
        }


    }
}
