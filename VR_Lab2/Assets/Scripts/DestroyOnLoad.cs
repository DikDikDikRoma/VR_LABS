using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLoad : MonoBehaviour
{
    // Start is called before the first frame update

    void Awake()
    {
        UnityEngine.Object.Destroy(this);
    }

    //-------------------------------------------------


}
