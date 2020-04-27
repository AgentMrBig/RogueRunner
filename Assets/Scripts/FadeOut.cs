using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();

        startFading();
    }

    IEnumerator FadeOutEnum()
    {
        for(float f = 1; f >= -0.05f; f -= 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.25f);
        }
    }

    public void startFading()
    {
        StartCoroutine("FadeOutEnum");
    }

    // Update is called once per frame
    void Update()
    {
        if(rend.material.color.a <= 0.1)
        {
            Destroy(gameObject);
        }
    }
}
