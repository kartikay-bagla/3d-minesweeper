using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isMine=false;
    public int gridValue=0;

    private bool valueShown=false;

    void OnTriggerEnter(Collider col)
    {
        if (col.tag=="Player")
        {
            if (isMine)
            {

            }
            else
                DisplayValue();
        }
    }

    void DisplayValue ()
    {
        if (valueShown)
            return;
        valueShown=true;
        Text value = GetComponentInChildren(typeof(Text)) as Text;
        value.text=gridValue.ToString();
    }
}
