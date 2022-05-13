using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
namespace Assets.SCripts
{
    public class GateTimer : MonoBehaviour
    {

        public System.Timers.Timer aTimer;
        private Material material;
        bool timerOff;
        float dissolveVal;
        bool gateCanDissolve;
        GameObject player;
        [SerializeField] Transform gameObject;
        float distanceSqr;
        // Start is called before the first frame update
        void Start()
        {

            gateCanDissolve = true;
            dissolveVal = 0;
            timerOff = true;
            material = GetComponentInChildren<MeshRenderer>().sharedMaterial;
            Debug.Log(material);
            material.SetFloat("_dissolved", dissolveVal);

            ;
        }

        // Update is called once per frame
        void Update()
        {

            if (PlayerManager.instance.enemiesKilled == 3)
            {

                if (timerOff == false)
                {
                    if (dissolveVal < 1)
                    {
                        dissolveVal = dissolveVal + 0.0008f;
                        Debug.Log(dissolveVal);
                    }
                    material.SetFloat("_dissolved", dissolveVal);
                }

            }




        }

        IEnumerator Open()
        {

            SetTimer();
            
            yield return new WaitForSeconds(7);




        }
        private void SetTimer()
        {
            timerOff = false;
            aTimer = new System.Timers.Timer(7000);

            aTimer.Elapsed += OnTimedEvent;

            aTimer.AutoReset = false;
            aTimer.Enabled = true;

        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            timerOff = true;
            Destroy(gameObject);

        }

    }
}

