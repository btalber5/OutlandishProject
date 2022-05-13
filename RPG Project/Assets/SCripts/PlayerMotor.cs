using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



namespace Scripts
{

    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMotor : MonoBehaviour
    {

        NavMeshAgent agent;
        Transform target;
        internal bool dialoguing;

        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            dialoguing = false;
        }

        private void Update()
        {
            if(target != null)
            {
                agent.SetDestination(target.position);
                FaceTarget();
            }
        }


        public void MoveToPoint(Vector3 point) {

            agent.SetDestination(point);
            
        
        }

        public void stopMoving(Vector3 currentPos) {


            agent.stoppingDistance = 0f;
            agent.SetDestination(currentPos);
        
        
        }

        public void Dash(string direction) {

            if (direction.Equals("up"))
            {
                Vector3 offSet = new Vector3(0f,0f,2f);
                agent.Move(offSet);
                
            }
            if (direction.Equals("down"))
            {
                Vector3 offSet = new Vector3(0f, 0f, -2f);
                agent.Move(offSet);
            }
            if (direction.Equals("left"))
            {
                Vector3 offSet = new Vector3(-2f, 0f, 0f);
                agent.Move(offSet);
            }
            if (direction.Equals("right"))
            {
                Vector3 offSet = new Vector3(2f, 0f, 0f);
                agent.Move(offSet);
            }




        }

        public void Teleport(Vector3 location) {

            agent.Move(location);


        }

        public void FollowTarget(Interactable newTarget) {

            agent.stoppingDistance = newTarget.radius * .8f;
            agent.updateRotation = false;
            target = newTarget.interactionTransform;
        
            
        }

        public void StopFollowingTarget() {

            agent.stoppingDistance = 0f;
            agent.updateRotation = true;
            target = null;

        }

        public NavMeshAgent getAgent() {

            return agent;
        
        }

        public void FaceTarget() {


            Vector3 Direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(Direction.x,0f,Direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}
