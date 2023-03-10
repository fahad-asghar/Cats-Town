using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievements : MonoBehaviour
{
    public int unlockCatLevel10;
    public Text unlockCatLevel10CounterText;
    public Button unlockCatLevel10Button;

    public int buy50Cats;
    public Text buy50CatsText;
    public Button buy50CatsButton;

    public int merge1200Times;
    public Text merge1200TimesText;
    public Button merge1200TimesButton;

    public int open40GiftBoxes;
    public Text open40GiftBoxesText;
    public Button open40GiftBoxesButton;

    public int collect50Level3Cats;
    public Text collect50Level3CatsText;
    public Button collect50Level3CatsButton;

    public int collect50Level6Cats;
    public Text collect50Level6CatsText;
    public Button collect50Level6CatsButton;

    public int collect50Level9Cats;
    public Text collect50Level9CatsText;
    public Button collect50Level9CatsButton;

    public int collect50Level12Cats;
    public Text collect50Level12CatsText;
    public Button collect50Level12CatsButton;

    public int collect50Level15Cats;
    public Text collect50Level15CatsText;
    public Button collect50Level15CatsButton;

    public int collect50Level18Cats;
    public Text collect50Level18CatsText;
    public Button collect50Level18CatsButton;

    public int collect50Level20Cats;
    public Text collect50Level20CatsText;
    public Button collect50Level20CatsButton;

    public int collect30Level30Cats;
    public Text collect30Level30CatsText;
    public Button collect30Level30CatsButton;

    public int collect30Level35Cats;
    public Text collect30Level35CatsText;
    public Button collect30Level35CatsButton;

    public int collect30Level42Cats;
    public Text collect30Level42CatsText;
    public Button collect30Level42CatsButton;

    public int collect20Level50Cats;
    public Text collect20Level50CatsText;
    public Button collect20Level50CatsButton;


    public int buy10Cats;
    public Text buy10CatsText;
    public Button buy10CatsButton;

    public int merge20Cats;
    public Text merge20CatsText;
    public Button merge20CatsButton;

    public int get30Cats;
    public Text get30CatsText;
    public Button get30CatsButton;

    public int dailyLogin;
    public Text dailyLoginText;
    public Button dailyLoginButton;

    public static Achievements instance;



    private void Awake()
    {
        instance = this;

      

        CheckDailyTasks();
        CheckAchievements();
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt(StateSaving.instance.DAILY_LOGIN) == 0)
            dailyLogin++;
    }

    public void CheckDailyTasks()
    {
        dailyLoginText.text = dailyLogin + "/1";
        buy10CatsText.text = buy10Cats + "/10";
        merge20CatsText.text = merge20Cats + "/20";
        get30CatsText.text = get30Cats + "/30";

        if (dailyLogin == 1 && PlayerPrefs.GetInt(StateSaving.instance.TASK1) == 0)
            dailyLoginButton.interactable = true;
        if (buy10Cats == 10 && PlayerPrefs.GetInt(StateSaving.instance.TASK2) == 0)
            buy10CatsButton.interactable = true;
        if (merge20Cats == 20 && PlayerPrefs.GetInt(StateSaving.instance.TASK3) == 0)
            merge20CatsButton.interactable = true;
        if (get30Cats == 30 && PlayerPrefs.GetInt(StateSaving.instance.TASK4) == 0)
            get30CatsButton.interactable = true;
    }

    public void CheckAchievements()
    {
        unlockCatLevel10CounterText.text = unlockCatLevel10 + "/1";
        buy50CatsText.text = buy50Cats + "/50";
        merge1200TimesText.text = merge1200Times + "/1200";
        open40GiftBoxesText.text = open40GiftBoxes + "/40";
        collect50Level3CatsText.text = collect50Level3Cats + "/50";
        collect50Level6CatsText.text = collect50Level6Cats + "/50";
        collect50Level9CatsText.text = collect50Level9Cats + "/50";
        collect50Level12CatsText.text = collect50Level12Cats + "/50";
        collect50Level15CatsText.text = collect50Level15Cats + "/50";
        collect50Level18CatsText.text = collect50Level18Cats + "/50";
        collect50Level20CatsText.text = collect50Level20Cats + "/50";
        collect30Level30CatsText.text = collect30Level30Cats + "/30";
        collect30Level35CatsText.text = collect30Level35Cats + "/30";
        collect30Level42CatsText.text = collect30Level42Cats + "/30";
        collect20Level50CatsText.text = collect20Level50Cats + "/20";


        if (unlockCatLevel10 == 1 && PlayerPrefs.GetInt(StateSaving.instance.ACHIEVEMENT1) == 0)
            unlockCatLevel10Button.interactable = true;
        if (buy50Cats == 50 && PlayerPrefs.GetInt(StateSaving.instance.ACHIEVEMENT2) == 0)
            buy50CatsButton.interactable = true;
        if (merge1200Times == 1200 && PlayerPrefs.GetInt(StateSaving.instance.ACHIEVEMENT3) == 0)
            merge1200TimesButton.interactable = true;
        if (open40GiftBoxes == 40 && PlayerPrefs.GetInt(StateSaving.instance.ACHIEVEMENT4) == 0)
            open40GiftBoxesButton.interactable = true;
        if (collect50Level3Cats == 50 && PlayerPrefs.GetInt(StateSaving.instance.ACHIEVEMENT5) == 0) 
            collect50Level3CatsButton.interactable = true;
        if (collect50Level6Cats == 50 && PlayerPrefs.GetInt(StateSaving.instance.ACHIEVEMENT6) == 0)
            collect50Level6CatsButton.interactable = true;
        if (collect50Level9Cats == 50 && PlayerPrefs.GetInt(StateSaving.instance.ACHIEVEMENT7) == 0)
            collect50Level9CatsButton.interactable = true;
        if (collect50Level12Cats == 50 && PlayerPrefs.GetInt(StateSaving.instance.ACHIEVEMENT8) == 0)
            collect50Level12CatsButton.interactable = true;
        if (collect50Level15Cats == 50 && PlayerPrefs.GetInt(StateSaving.instance.ACHIEVEMENT9) == 0)
            collect50Level15CatsButton.interactable = true;
        if (collect50Level18Cats == 50 && PlayerPrefs.GetInt(StateSaving.instance.ACHIEVEMENT10) == 0)
            collect50Level18CatsButton.interactable = true;
        if (collect50Level20Cats == 50 && PlayerPrefs.GetInt(StateSaving.instance.ACHIEVEMENT11) == 0)
            collect50Level20CatsButton.interactable = true;
        if (collect30Level30Cats == 30 && PlayerPrefs.GetInt(StateSaving.instance.ACHIEVEMENT12) == 0)
            collect30Level30CatsButton.interactable = true;
        if (collect30Level35Cats == 30 && PlayerPrefs.GetInt(StateSaving.instance.ACHIEVEMENT13) == 0)
            collect30Level35CatsButton.interactable = true;
        if (collect30Level42Cats == 30 && PlayerPrefs.GetInt(StateSaving.instance.ACHIEVEMENT14) == 0)
            collect30Level42CatsButton.interactable = true;
        if (collect20Level50Cats == 20 && PlayerPrefs.GetInt(StateSaving.instance.ACHIEVEMENT15) == 0)
            collect20Level50CatsButton.interactable = true;
    }
}
