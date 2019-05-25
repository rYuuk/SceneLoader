using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VRPlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform m_Camera;
    [SerializeField] private float m_InteractionTime = 2f;
    [SerializeField] private float m_RayLength = 500f;
    [SerializeField] private bool m_ShowDebugRay = true;
    [SerializeField] private float m_DebugRayLength = 5f;
    [SerializeField] private Slider m_InteractionProgressSlider;

    private bool _isInteracting = false;
    private bool _isFirstHit = false;

    private float _startTime;

    private bool _didInteract = false;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        Raycast();
    }

    private void OnInteraction(RaycastHit hit)
    {
        GameObject hitGameobject = hit.collider.gameObject;
        if (hitGameobject.name.Contains("Button"))
        {
            hitGameobject.GetComponent<Button>().onClick.Invoke();
        }
    }

    private void Raycast()
    {
        if (m_ShowDebugRay)
            Debug.DrawRay(m_Camera.position, m_Camera.forward * m_DebugRayLength, Color.green);

        Ray ray = new Ray(m_Camera.position, m_Camera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, m_RayLength))
        {
            _isInteracting = true;
            if (!_isFirstHit)
            {
                m_InteractionProgressSlider.gameObject.SetActive(true);
                _isFirstHit = true;
                _startTime = Time.time;
            }

            if(_isFirstHit)
                m_InteractionProgressSlider.value = (Time.time - _startTime)/ m_InteractionTime;

            if (Time.time - _startTime > m_InteractionTime)
            {
                if (!_didInteract)
                {
                    _didInteract = true;
                    OnInteraction(hit);
                }
            }
        }
        else
        {
            _isInteracting = false;
            _isFirstHit = false;
            _didInteract = false;
            m_InteractionProgressSlider.gameObject.SetActive(false);
            m_InteractionProgressSlider.value = 0;
        }
    }
}
