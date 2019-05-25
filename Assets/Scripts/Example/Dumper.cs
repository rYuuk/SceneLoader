using UnityEngine;
using UnityEngine.UI;

public class Dumper : MonoBehaviour
{
    [SerializeField] private RawImage m_RawImage;
    [SerializeField] private float m_Count = 100;

    private void Start()
    {
        for (int i = 0; i < m_Count; i++)
        {
            RawImage fakeRawImage = Instantiate(m_RawImage, transform);
            Texture texture = Resources.Load<Texture>("Textures/GreyMatter");
            fakeRawImage.texture = texture;
        }
    }
}
