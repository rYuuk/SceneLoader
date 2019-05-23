using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dumper : MonoBehaviour
{
    [SerializeField] private RawImage image;
    [SerializeField] private float count = 100;

    private void Start()
    {
        for(int i=0; i < count; i++)
        {
           var a = Instantiate(image, transform);
            Texture texture = Resources.Load<Texture>("GreyMatter");
            a.texture = texture;
        }
    }
}
