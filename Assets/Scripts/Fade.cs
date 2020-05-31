using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public bool show;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (show) show = false;
            else show = true;
            GetComponent<Animator>().SetBool("show", show);
        }
    }
}
