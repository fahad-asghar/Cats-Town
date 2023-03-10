using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Navigation : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject fader;
    [SerializeField] GameObject shopPopUp;
    [SerializeField] GameObject notificationPopUp;
    [SerializeField] Text notificationText;
    [SerializeField] GameObject newCatPopUp;
    [SerializeField] Image newCatIcon;
    [SerializeField] Text newCatName;
    [SerializeField] Text newCatLevel;
    [SerializeField] GameObject achievementPopUp;
    [SerializeField] GameObject kennelPopUp;
    [SerializeField] GameObject autoMergePopUp;
    [SerializeField] GameObject doubleMoneyPopUp;
    [SerializeField] GameObject dailyTasksPopUp;
    [SerializeField] GameObject idleMoneyPopUp;
    [SerializeField] Transform quickBuyParent;


    [Header("List of buttons to disable")]
    [SerializeField] List<Button> buttons = new List<Button>();

    public static Navigation instance;
    public bool uiEnabled;

    private void Awake()
    {
        Camera.main.aspect = 1080f / 1920f;
        instance = this;
        uiEnabled = false;

    }

    private void Start()
    {
        quickBuyParent.GetChild(PlayerPrefs.GetInt(StateSaving.instance.NEW_CAT)).gameObject.SetActive(true);
    }

    #region Shop
    public void ShopPopUp()
    {
        uiEnabled = true;
        SoundManager.instance.playSound(SoundManager.instance.sound1);

        fader.SetActive(true);
        fader.GetComponent<Image>().DOFade(0.7f, 0.3f).OnComplete(delegate ()
        {
            shopPopUp.SetActive(true);
            shopPopUp.GetComponent<Animator>().Play("PopUpOpen");
        });
    }

    public void CloseShopPopUp()
    {

        SoundManager.instance.playSound(SoundManager.instance.sound1);

        shopPopUp.GetComponent<Animator>().Play("PopUpClose");
        fader.GetComponent<Image>().DOFade(0, 0.5f).OnComplete(delegate ()
        {
            uiEnabled = false;
            fader.SetActive(false);
            shopPopUp.SetActive(false);
        });
    }
    #endregion

    #region Notification
    public void NotificationPopUp(string message)
    {
        SoundManager.instance.playSound(SoundManager.instance.sound6);

        for (int i = 0; i < buttons.Count; i++)
            buttons[i].interactable = false;

        notificationPopUp.transform.DOLocalMoveY(1500, 0f).OnComplete(delegate() {
            notificationPopUp.SetActive(true);
            notificationText.text = message;
            notificationPopUp.transform.DOLocalMoveY(1314, 0.3f).OnComplete(delegate() {
                notificationPopUp.transform.DOLocalMoveY(1500, 0.15f).SetDelay(1).OnComplete(delegate ()
                {
                    notificationPopUp.SetActive(false);
                    for (int i = 0; i < buttons.Count; i++)
                        buttons[i].interactable = true;
                });
            });
        });
    }
    #endregion

    #region New Cat
    public void NewCatUnlockPopUp(Sprite catIcon, string level, string name, int index)
    {
        if (PlayerPrefs.GetInt(StateSaving.instance.NEW_CAT) < index)
        {
            SoundManager.instance.playSound(SoundManager.instance.sound7);
            uiEnabled = true;
            PlayerPrefs.SetInt(StateSaving.instance.NEW_CAT, index);

            for (int i = 0; i < quickBuyParent.childCount; i++)
                quickBuyParent.GetChild(i).gameObject.SetActive(false);
            quickBuyParent.GetChild(PlayerPrefs.GetInt(StateSaving.instance.NEW_CAT)).gameObject.SetActive(true);

            fader.SetActive(true);
            fader.GetComponent<Image>().DOFade(0.7f, 0.3f).OnComplete(delegate ()
            {
                newCatIcon.sprite = catIcon;
                newCatIcon.SetNativeSize();

                newCatName.text = name;
                newCatLevel.text = "Level: " + level;

                newCatPopUp.SetActive(true);
                newCatPopUp.GetComponent<Animator>().Play("PopUpOpen");
            });


        }
    }

    public void OKButton()
    {
        SoundManager.instance.playSound(SoundManager.instance.sound1);

        newCatPopUp.GetComponent<Animator>().Play("PopUpClose");
        fader.GetComponent<Image>().DOFade(0, 0.5f).OnComplete(delegate ()
        {
            uiEnabled = false;
            fader.SetActive(false);
            newCatPopUp.SetActive(false);
        });
    }
    #endregion

    #region Achivement
    public void AchievementPopUp()
    {
        Achievements.instance.CheckAchievements();
        uiEnabled = true;
        SoundManager.instance.playSound(SoundManager.instance.sound1);

        fader.SetActive(true);
        fader.GetComponent<Image>().DOFade(0.7f, 0.3f).OnComplete(delegate ()
        {
            achievementPopUp.SetActive(true);
            achievementPopUp.GetComponent<Animator>().Play("PopUpOpen");
        });
    }

    public void CloseAchievementPopUp()
    {

        SoundManager.instance.playSound(SoundManager.instance.sound1);

        achievementPopUp.GetComponent<Animator>().Play("PopUpClose");
        fader.GetComponent<Image>().DOFade(0, 0.5f).OnComplete(delegate ()
        {
            uiEnabled = false;
            fader.SetActive(false);
            achievementPopUp.SetActive(false);
        });
    }
    #endregion

    #region Kennel
    public void BuyKennelPopUp()
    {
        uiEnabled = true;
        SoundManager.instance.playSound(SoundManager.instance.sound1);

        fader.SetActive(true);
        fader.GetComponent<Image>().DOFade(0.7f, 0.3f).OnComplete(delegate ()
        {
            kennelPopUp.SetActive(true);
            kennelPopUp.GetComponent<Animator>().Play("PopUpOpen");
        });
    }

    public void CloseKennelPopUp()
    {
        SoundManager.instance.playSound(SoundManager.instance.sound1);

        kennelPopUp.GetComponent<Animator>().Play("PopUpClose");
        fader.GetComponent<Image>().DOFade(0, 0.5f).OnComplete(delegate ()
        {
            uiEnabled = false;
            fader.SetActive(false);
            kennelPopUp.SetActive(false);
        });
    }
    #endregion

    #region Auto Merge
    public void AutoMergePopUp()
    {
        uiEnabled = true;
        SoundManager.instance.playSound(SoundManager.instance.sound1);

        fader.SetActive(true);
        fader.GetComponent<Image>().DOFade(0.7f, 0.3f).OnComplete(delegate ()
        {
            autoMergePopUp.SetActive(true);
            autoMergePopUp.GetComponent<Animator>().Play("PopUpOpen");
        });
    }

    public void CloseAutoMergePopUp()
    {
        SoundManager.instance.playSound(SoundManager.instance.sound1);

        autoMergePopUp.GetComponent<Animator>().Play("PopUpClose");
        fader.GetComponent<Image>().DOFade(0, 0.5f).OnComplete(delegate ()
        {
            uiEnabled = false;
            fader.SetActive(false);
            autoMergePopUp.SetActive(false);
        });
    }
    #endregion

    #region Double Money
    public void DoubleMoneyPopUp()
    {
        uiEnabled = true;
        SoundManager.instance.playSound(SoundManager.instance.sound1);

        fader.SetActive(true);
        fader.GetComponent<Image>().DOFade(0.7f, 0.3f).OnComplete(delegate ()
        {
            doubleMoneyPopUp.SetActive(true);
            doubleMoneyPopUp.GetComponent<Animator>().Play("PopUpOpen");
        });
    }

    public void CloseDoubleMoneyPopUp()
    {
        SoundManager.instance.playSound(SoundManager.instance.sound1);

        doubleMoneyPopUp.GetComponent<Animator>().Play("PopUpClose");
        fader.GetComponent<Image>().DOFade(0, 0.5f).OnComplete(delegate ()
        {
            uiEnabled = false;
            fader.SetActive(false);
            doubleMoneyPopUp.SetActive(false);
        });
    }
    #endregion

    #region Daily Tasks
    public void DailyTaskPopUp()
    {
        uiEnabled = true;
        SoundManager.instance.playSound(SoundManager.instance.sound1);
        Achievements.instance.CheckDailyTasks();
        fader.SetActive(true);
        fader.GetComponent<Image>().DOFade(0.7f, 0.3f).OnComplete(delegate ()
        {
            dailyTasksPopUp.SetActive(true);
            dailyTasksPopUp.GetComponent<Animator>().Play("PopUpOpen");
        });
    }

    public void CloseDailyTasksPopUp()
    {
        SoundManager.instance.playSound(SoundManager.instance.sound1);

        dailyTasksPopUp.GetComponent<Animator>().Play("PopUpClose");
        fader.GetComponent<Image>().DOFade(0, 0.5f).OnComplete(delegate ()
        {
            uiEnabled = false;
            fader.SetActive(false);
            dailyTasksPopUp.SetActive(false);
        });
    }
    #endregion

    #region Idle Money
    public void IdleMoneyPopUp()
    {
        uiEnabled = true;

        fader.SetActive(true);
        fader.GetComponent<Image>().DOFade(0.7f, 0.3f).OnComplete(delegate ()
        {
            idleMoneyPopUp.SetActive(true);
            idleMoneyPopUp.GetComponent<Animator>().Play("PopUpOpen");
        });
    }

    public void CloseIdleMoneyPopUp()
    {
        SoundManager.instance.playSound(SoundManager.instance.sound1);

        idleMoneyPopUp.GetComponent<Animator>().Play("PopUpClose");
        fader.GetComponent<Image>().DOFade(0, 0.5f).OnComplete(delegate ()
        {
            uiEnabled = false;
            fader.SetActive(false);
            idleMoneyPopUp.SetActive(false);
        });
    }
    #endregion
}
