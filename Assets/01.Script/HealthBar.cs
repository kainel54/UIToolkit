using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform _barTrm;

    private void Awake()
    {
        _barTrm = transform.Find("Bar");
    }
    public void SetBarScale(float nomalizedScale)
    {
        Vector3 scale = _barTrm.localScale;
        scale.x *= Mathf.Clamp(nomalizedScale, 0f, 1f);
        _barTrm.localScale = scale;
    }
}
