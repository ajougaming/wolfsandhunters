using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
    private float startingHealth;
    private float currentHealth;
    private PlayerCtrl playerCtrl;
    public Slider healthSlider;
    public Text healthText;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    private bool isDamage = false;
    private bool isDie = false;
	// Use this for initialization
	void Awake () {
        playerCtrl = GetComponent<PlayerCtrl>();
        startingHealth = playerCtrl.playerHP;
	}
	
	// Update is called once per frame
	void Update () {
        isDamage = playerCtrl.isDamage;
        currentHealth = playerCtrl.playerHP;
        healthSlider.value = currentHealth;
        healthText.text = currentHealth + "/" + startingHealth;
        if (currentHealth > 0)
        {


            if (isDamage)
            {
                damageImage.color = flashColor;
            }
            else
            {
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }
        }
        playerCtrl.isDamage = false;
	}
}
