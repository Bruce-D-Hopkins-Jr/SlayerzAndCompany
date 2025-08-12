using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image icon;
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI hpText;

    private int maxHP;

    private void LateUpdate()
    {        
        var cam = Camera.main;
        if (cam == null) return;

        // Make HUD face the camera
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);        
    }

    public void Setup(HeroType heroName, Sprite portrait, int maxHP)
    {
        nameText.text = heroName.ToString();
        icon.sprite = portrait;
        this.maxHP = maxHP;
    }

    public void UpdateHealth(int currentHP)
    {
        healthBar.value = (float)currentHP / maxHP;
        hpText.text = currentHP.ToString() + " / " + maxHP.ToString();
    }
}
