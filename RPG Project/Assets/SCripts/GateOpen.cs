using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.SCripts
{
    class GateOpen : MonoBehaviour
    {

        [SerializeField] GameObject wall;
        [SerializeField] private float speed;
        [SerializeField] private Transform targetPos;
        [SerializeField] bool moveWallFlag;
        [SerializeField] int time;
        // Start is called before the first frame update
        void Start()
        {
            moveWallFlag = false;
        }


        private void FixedUpdate()
        {
                time++;
                wall.transform.position = Vector3.MoveTowards(wall.transform.position, targetPos.position, speed);
                if (time > 90)
                {

                    moveWallFlag = false;
                }
            
        }
    }
}
