using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelSystem : MonoBehaviour
{
    public int level;
    public float currentXp;
    public float requiredXp;

    private float lerpTimer;
    private float delayTimer;
    [Header("UI")]
    public Image frontXpBar;
    public Image backXpBar;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI xpText;

    void Start()
    {
        frontXpBar.fillAmount = currentXp / requiredXp;
        backXpBar.fillAmount = currentXp / requiredXp;
        levelText.text = "level " + level;                     //level yazisi. (baslangicta 1 seviye olarak baslamasi icin)
    }

    void Update()
    {
        updateXpUI();
        if (Input.GetKeyDown(KeyCode.E))            //ONEMLI!!! //musteri yiyip icip kalktiginda exp gelecek, bunun kodlanacagi yer burasi. update fonksiyonu icinden alinip musteri kalktiginda fonksiyonuna yerlestirilecek.
            gainExperienceFlatRate(10);             //"10" gelen exp sayisi (degisken)
        if (Input.GetKeyDown(KeyCode.F))            //ONEMLI!!! //musteri sinirlenip kalktiginda exp kaybedecek, bunun kodlanacagi yer burasi. update fonksiyonu icinden alinip musteri kalktiginda fonksiyonuna yerlestirilecek.
            loseExperienceFlatRate(10);             //"10" giden exp sayisi (degisken)
        if (currentXp >= requiredXp)                //eger (suanki xp, seviye atlamak icin gereken xp'den fazla ya da esit ise (degistirilebilir) seviye atla)
            levelUp();
    }

    public void updateXpUI()                        //gelen xp'nin xp barina islemesi
    {
        float xpFraction = currentXp / requiredXp;
        float FXP = frontXpBar.fillAmount;
        if(FXP < xpFraction)
        {
            delayTimer += Time.deltaTime;
            backXpBar.fillAmount = xpFraction;
            if(delayTimer > 0.5)                                                //xp barinin ilerleme suresi buradan ayarlanacak
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 1;                          //xp barinin ilerleme suresi buradan ayarlanacak
                frontXpBar.fillAmount = Mathf.Lerp(FXP, backXpBar.fillAmount, percentComplete);
            }
        }
        if (FXP > xpFraction)                      //giden xp'nin xp barina islemesi
        {
            delayTimer += Time.deltaTime;
            backXpBar.fillAmount = xpFraction;
            if (delayTimer > 0.5)                                                //xp barinin ilerleme suresi buradan ayarlanacak
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 1;                           //xp barinin ilerleme suresi buradan ayarlanacak
                frontXpBar.fillAmount = Mathf.Lerp(FXP, backXpBar.fillAmount, percentComplete);
            }
        }
        xpText.text = currentXp + "/" + requiredXp;
    }

    public void gainExperienceFlatRate(float xpGained)      //xp kazanci
    {
        currentXp += xpGained;
        lerpTimer = 0f;
        delayTimer = 0f;
    }
    public void loseExperienceFlatRate(float xpLosed)      //xp kaybi
    {
        currentXp -= xpLosed;
        lerpTimer = 0f;
        delayTimer = 0f;
    }
    public void levelUp()     //seviye atladiginda kazanacagi ozellikler burada kodlanacak
    {
        level++;
        frontXpBar.fillAmount = 0f;
        backXpBar.fillAmount = 0f;
        currentXp = 0f;
        requiredXp = requiredXp + 50;
        levelText.text = "level " + level;          //level yazisi (Her seviye arttiginda yenilenir)
    }
}
