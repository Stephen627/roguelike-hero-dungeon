using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider Slider;
    public Color Low;
    public Color High;
    public Vector3 Offset;

    public void SetHealth (float health, float maxHealth)
    {
        this.Slider.maxValue = maxHealth;
        this.Slider.value = health;

        this.Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(
            this.Low,
            this.High,
            this.Slider.normalizedValue
        );
    }

    void Start()
    {
        this.gameObject.SetActive(false);
    }
    
    void Update()
    {
        this.Slider.transform.position = Camera.main.WorldToScreenPoint(
            this.transform.parent.position + this.Offset
        );
    }
}
