using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace Assets.SCripts
{
    class FlameSwitch : Interactable
    {

        List<Color> colorList;
        Light pointLight;
        int startingPoint;
        ParticleSystem particleEffect;
        void Start() {
            colorList.Add(new Color(248,0,233));
            colorList.Add(new Color(0,179,255));
            colorList.Add(new Color(24,212,0));
            colorList.Add(new Color(255,51,0));
            startingPoint = 0;
            pointLight = GetComponentInChildren<Light>();

            particleEffect = GetComponentInChildren<ParticleSystem>();
        }

        public override void Interact()
        {
            base.Interact();
            
            if (startingPoint == colorList.Count - 1)
            {
                particleEffect.startColor = colorList[startingPoint];
                pointLight.color = colorList[startingPoint];
                startingPoint = 0;


            }

            else {
                startingPoint++;
                particleEffect.startColor = colorList[startingPoint];
                pointLight.color = colorList[startingPoint];
            }
            



        }


    }
}
