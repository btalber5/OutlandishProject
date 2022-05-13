using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour
{
    
    [SerializeField] public float typeWriterSpeed = 50f;
    private float fastTypeSpeed;
    private float tempSpeed;

    public Coroutine Run(string textToType, TMP_Text textLabel) {

       return StartCoroutine(TypeText(textToType,textLabel));
        
        
    }


    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        textLabel.text = string.Empty;
        fastTypeSpeed = typeWriterSpeed * 3f;
        
        float t = 0;
        int charIndex = 0;
        

        while (charIndex < textToType.Length) {
            if (Input.GetKey(KeyCode.Space))
            {

                tempSpeed = fastTypeSpeed;

            }
            else
            {

                tempSpeed = typeWriterSpeed;

            }
            t += Time.deltaTime * tempSpeed;
            charIndex = Mathf.FloorToInt(t);
            

            charIndex = Mathf.Clamp(charIndex, 0,textToType.Length);
            textLabel.text = textToType.Substring(0,charIndex);
            yield return null;
        }

        textLabel.text = textToType;
    
    }
}
