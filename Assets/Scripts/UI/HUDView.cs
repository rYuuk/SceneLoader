using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDView : MonoBehaviour
{
    [SerializeField] private GameObject m_Player;
    [SerializeField] private float m_Delay = 0.5f;

    private void Update()
    {
        transform.position = m_Player.transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, m_Player.transform.rotation, m_Delay * Time.deltaTime) ;
    }
}
