using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GifSimulator : MonoBehaviour
{
    public float speed;
    Material bodies;
    public Texture[] sprites;
    int CurFrame = 0;
    void Start()
    {
        bodies = GetComponent<MeshRenderer>().material;
        StartCoroutine(GifChander(speed));
    }

    IEnumerator GifChander(float _t)
    {
        yield return new WaitForSeconds(_t);
        if (CurFrame > sprites.Length - 1)
            CurFrame = 0;
        else
        {
            bodies.mainTexture = sprites[CurFrame];
            CurFrame++;
        }
        StartCoroutine(GifChander(speed));
    }
}
