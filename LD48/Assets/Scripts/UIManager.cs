using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public ProgressBar health_bar;
    public ProgressBar mana_bar;
    public ProgressBar stamina_bar;
    public ProgressBar xp_bar;
    public Canvas death_canvas;
    public Canvas level_up_canvas;


    public Text agi_text;
    public Text int_text;
    public Text str_text;

    public Text health_text;
    public Text mana_text;
    public Text stamina_text;

    public Text level_text;

    public Text stat_points_text;


    public Text death_level_text;
    public Text death_floor_text;


    private void Awake()
    {
        ServiceLocator.SetUIManager(this);

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthUI(int current, int max)
    {
        health_bar.current = current;
        health_bar.max = max;
    }


    public void UpdateManaUI(int current, int max)
    {
        mana_bar.current = current;
        mana_bar.max = max;
    }

    public void UpdateStaminaUI(int current, int max)
    {
        stamina_bar.current = current;
        stamina_bar.max = max;
    }

    public void UpdateXPUI(int current, int min, int max)
    {
        xp_bar.current = current;
        xp_bar.min = min;
        xp_bar.max = max;
    }

    public void LevelUp()
    {
        if(ServiceLocator.GetPlayerController().levelled)
        {
            ToggleLevelUpCanvas();
            ServiceLocator.GetPlayerController().levelled = false;


        }
    }

    public void ToggleLevelUpCanvas()
    {
        UpdateStats();
        level_up_canvas.gameObject.SetActive(!level_up_canvas.gameObject.activeSelf);
        

    }


    private void UpdateStats()
    {
        PlayerController p = ServiceLocator.GetPlayerController();
        p.ModifyHealth(p.GetMaxHealth());
        p.ModifyMana(p.GetMaxMana());
        p.ModifyStamina(p.GetMaxStamina());
        agi_text.text = p.stats.GetAgility().ToString();
        str_text.text = p.stats.GetStrength().ToString();
        int_text.text = p.stats.GetIntellect().ToString();

        health_text.text = p.GetMaxHealth().ToString();
        mana_text.text = p.GetMaxMana().ToString();
        stamina_text.text = p.GetMaxStamina().ToString();

        level_text.text = p.GetLevel().ToString();
        stat_points_text.text = p.GetStatPoints().ToString();

    }

    public void ShowDeathScreen()
    {
        death_canvas.gameObject.SetActive(true);
        death_floor_text.text = ServiceLocator.GetLevelGeneration().floor_num.ToString();
        death_level_text.text = ServiceLocator.GetPlayerController().GetLevel().ToString();
    }

    public void UpgradeStrength()
    {
        PlayerController p = ServiceLocator.GetPlayerController();
        if (p.GetStatPoints() > 0)
        {
            p.stats.ModifyStrength(1);
            UpdateStats();
            p.SpendStatPoint();
        }

    }

    public void UpgradeIntelligenceh()
    {
        PlayerController p = ServiceLocator.GetPlayerController();
        if (p.GetStatPoints() > 0)
        {
            p.stats.ModifyIntellect(1);
            UpdateStats();
            p.SpendStatPoint();
        }
    }

    public void UpgradeAgility()
    {
        PlayerController p = ServiceLocator.GetPlayerController();
        if (p.GetStatPoints() > 0)
        {
            p.stats.ModifyAgility(1);
            UpdateStats();
            p.SpendStatPoint();
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
