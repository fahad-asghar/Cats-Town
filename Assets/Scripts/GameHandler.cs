using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;

    [Header("UI")]
    [SerializeField] Text totalCurrencyText;
    [SerializeField] Transform platformsParent;
    [SerializeField] GameObject kennelButton;
    [SerializeField] Text autoMergetimerText;
    [SerializeField] Button autoMergeButton;
    [SerializeField] Text balloonTimerText;
    [SerializeField] Text doubleMoneyTimerText;
    [SerializeField] Button doubleMoneyButton;


    [Header("CATS LIST")]
    [SerializeField] List<GameObject> catsList = new List<GameObject>();

    [Header("EFFECTS")]
    [SerializeField] GameObject catsSpawnEffect;
    [SerializeField] GameObject balloonEffect;

    [Header("BALLOON")]
    [SerializeField] GameObject balloon;

  
    //Variables
    public BigInteger totalCurrency;
    private BigInteger temp;
    private int autoMergeTimer = 180;
    private int doubleMoneyTimer = 180;
    private int balloonTimer = 120;
    private int balloonSpawnInterval = 240;
    [HideInInspector] public bool doubleMoneyEnabled;
    [HideInInspector] public bool balloonEnabled;

    #region Currency Units
    string million = "1000000";
    string billion = "1000000000";
    string trillion = "1000000000000";
    string quadrillion = "1000000000000000";
    string quintillion = "1000000000000000000";
    string sextillion = "1000000000000000000000";
    string septillion = "1000000000000000000000000";
    string octillion = "1000000000000000000000000000";
    string nonillion = "1000000000000000000000000000000";
    string decillion = "1000000000000000000000000000000000";

    string thresholdMillion = "999999";
    string thresholdBillion = "999999999";
    string thresholdTrillion = "999999999999";
    string thresholdQuadrillion = "999999999999999";
    string thresholdQuintillion = "999999999999999999";
    string thresholdSextillion = "999999999999999999999";
    string thresholdSeptillion = "999999999999999999999999";
    string thresholdOctillion = "999999999999999999999999999";
    string thresholdNonillion = "999999999999999999999999999999";
    string thresholdDecillion = "999999999999999999999999999999999";

    #endregion

    private void Awake()
    {
        instance = this;
        temp = totalCurrency;
    }

    private void Start()
    {
        SpawnBalloon();
    }

    #region Currency Calculations
    public void UpdateCurrency(BigInteger currencyToAdd)
    {      
        totalCurrency += currencyToAdd;
        totalCurrencyText.text = totalCurrency.ToString();


     /*   if (totalCurrency > BigInteger.Parse(thresholdMillion) && totalCurrency <= BigInteger.Parse(thresholdBillion))
            totalCurrencyText.text = totalCurrency / BigInteger.Parse(million) + "MILLION";

        else if (totalCurrency > BigInteger.Parse(thresholdBillion) && totalCurrency <= BigInteger.Parse(thresholdTrillion))
            totalCurrencyText.text = totalCurrency / BigInteger.Parse(billion) + "BILLION";

        else if (totalCurrency > BigInteger.Parse(thresholdTrillion) && totalCurrency <= BigInteger.Parse(thresholdQuadrillion))
            totalCurrencyText.text = totalCurrency / BigInteger.Parse(trillion) + "TRILLION";

        else if (totalCurrency > BigInteger.Parse(thresholdQuadrillion) && totalCurrency <= BigInteger.Parse(thresholdQuintillion))
            totalCurrencyText.text = totalCurrency / BigInteger.Parse(quadrillion) + "QUADRILLION";

        else if (totalCurrency > BigInteger.Parse(thresholdQuintillion) && totalCurrency <= BigInteger.Parse(thresholdSextillion))
            totalCurrencyText.text = totalCurrency / BigInteger.Parse(quintillion) + "QUINTILLION";

        else if (totalCurrency > BigInteger.Parse(thresholdSextillion) && totalCurrency <= BigInteger.Parse(thresholdSeptillion))
            totalCurrencyText.text = totalCurrency / BigInteger.Parse(sextillion) + "SEXTILLION";

        else if (totalCurrency > BigInteger.Parse(thresholdSeptillion) && totalCurrency <= BigInteger.Parse(thresholdOctillion))
            totalCurrencyText.text = totalCurrency / BigInteger.Parse(septillion) + "SEPTILLION";

        else if (totalCurrency > BigInteger.Parse(thresholdOctillion) && totalCurrency <= BigInteger.Parse(thresholdNonillion))
            totalCurrencyText.text = totalCurrency / BigInteger.Parse(octillion) + "OCTILLION";

        else if (totalCurrency > BigInteger.Parse(thresholdNonillion) && totalCurrency <= BigInteger.Parse(thresholdDecillion))
            totalCurrencyText.text = totalCurrency / BigInteger.Parse(nonillion) + "NONILLION";

        else if (totalCurrency > BigInteger.Parse(thresholdDecillion))
            totalCurrencyText.text = totalCurrency / BigInteger.Parse(decillion) + "DECILLION";

        else
            totalCurrencyText.text = totalCurrency.ToString();*/
    }  
    #endregion


    #region Buy Cat
    public void BuyCat()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            string name = EventSystem.current.currentSelectedGameObject.name;
            for (int i = 0; i < catsList.Count; i++)
            {
                if (name.Equals(catsList[i].name))
                {
                    if (totalCurrency > BigInteger.Parse(catsList[i].GetComponent<CurrencyGenerator>().buyingPrice))
                    {
                        for (int j = 0; j < platformsParent.childCount; j++)
                        {
                            if (platformsParent.GetChild(j).childCount == 0 && platformsParent.GetChild(j).gameObject.activeSelf)
                            {
                                if (Achievements.instance.buy50Cats != 50)
                                    Achievements.instance.buy50Cats++;
                                if (Achievements.instance.buy10Cats != 10)
                                    Achievements.instance.buy10Cats++;

                                SoundManager.instance.playSound(SoundManager.instance.sound4);
                                totalCurrency -= BigInteger.Parse(catsList[i].GetComponent<CurrencyGenerator>().buyingPrice);
                                temp = totalCurrency;

                                GameObject obj = Instantiate(catsList[i], new UnityEngine.Vector3(platformsParent.GetChild(j).transform.position.x, platformsParent.GetChild(j).transform.position.y, 0), UnityEngine.Quaternion.identity);
                                obj.name = catsList[i].name;
                                obj.transform.parent = platformsParent.GetChild(j);
                                obj.transform.localPosition = new UnityEngine.Vector2(0, 0);
                                obj.GetComponent<DragObject>().startPosition = obj.transform.localPosition;
                                obj.GetComponent<DragObject>().startScale = obj.transform.localScale;
                                return;
                            }
                        }
                        Navigation.instance.NotificationPopUp("KENNY NOT AVAILABLE");
                        return;
                    }
                    else
                    {
                        Navigation.instance.NotificationPopUp("NOT ENOUGH MONEY");
                        return;
                    }
                }
            }
        }
    }
    #endregion


    #region Kennels  
    public void NewKennel()
    {
        if (totalCurrency >= 5000)
        {
            totalCurrency -= 5000;
            SoundManager.instance.playSound(SoundManager.instance.sound4);
            Navigation.instance.CloseKennelPopUp();

            for (int i = 0; i < platformsParent.childCount; i++)
            {
                if (!platformsParent.GetChild(i).gameObject.activeSelf)
                {
                    platformsParent.GetChild(i).gameObject.SetActive(true);
                    break;
                }
            }
            CheckKennelLayout();

            for (int i = 0; i < platformsParent.childCount; i++)
            {
                if (!platformsParent.GetChild(i).gameObject.activeSelf)
                    return;
            }
            kennelButton.SetActive(false);
        }
        else
            Navigation.instance.NotificationPopUp("NOT ENOUGH MONEY");
    }

    public void CheckKennelLayout()
    {
        int countActiveChilds = 0;
        for (int i = 0; i < platformsParent.childCount; i++)
        {
            if (platformsParent.GetChild(i).gameObject.activeSelf)
                countActiveChilds++;
        }
        if (countActiveChilds > 15 && countActiveChilds <= 20)        
            AdjustGrid(4, 15, 0, 628, 0, 51.02f, 140f, 188, 75);
   
        if (countActiveChilds > 20)        
            AdjustGrid(5, 15, 0, 628, 0, 5f, 140f, 180, 75);               
    }

    private void AdjustGrid(int coloumn, int paddingLeft, int paddingRight, int paddingTop, int paddingBottom, float spacingX, float spacingY, float sizeX, float sizeY)
    {
        platformsParent.GetComponent<GridLayoutGroup>().constraintCount = coloumn;
        platformsParent.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.Flexible;
        platformsParent.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedColumnCount;

        platformsParent.GetComponent<GridLayoutGroup>().padding.left = paddingLeft;
        platformsParent.GetComponent<GridLayoutGroup>().padding.right = paddingRight;
        platformsParent.GetComponent<GridLayoutGroup>().padding.top = paddingTop;
        platformsParent.GetComponent<GridLayoutGroup>().padding.bottom = paddingBottom;

        platformsParent.GetComponent<GridLayoutGroup>().cellSize = new UnityEngine.Vector2(sizeX, sizeY);
        platformsParent.GetComponent<GridLayoutGroup>().spacing = new UnityEngine.Vector2(spacingX, spacingY);
    }

   

    #endregion


    #region AutoMerge
    public void StartMerge()
    {
        autoMergeButton.interactable = false;
        autoMergetimerText.transform.parent.gameObject.SetActive(true);
        Navigation.instance.CloseAutoMergePopUp();
        AutoMerge();
    }

    public void AutoMerge()
    {
        if (autoMergeTimer > 0)
        {
            autoMergeTimer--;
            int minutes = autoMergeTimer / 60;
            float seconds = Mathf.Round((((float)autoMergeTimer / 60) - (autoMergeTimer / 60)) * 60);

            if (minutes < 10 && seconds > 10)
                autoMergetimerText.text = "0" + minutes + " : " + seconds;
            else if (seconds < 10 && minutes > 10)
                autoMergetimerText.text = minutes + " : " + "0" + seconds;
            else if (minutes < 10 && seconds < 10)
                autoMergetimerText.text = "0" + minutes + " : " + "0" + seconds;
            else
                autoMergetimerText.text = minutes + " : " + seconds;

            for (int i = 0; i < platformsParent.childCount - 1; i++)
            {
                if (platformsParent.GetChild(i).childCount > 0)
                {
                    for (int j = 0; j < platformsParent.childCount - 1; j++)
                    {
                        if (platformsParent.GetChild(j).childCount > 0 && j != i)
                        {
                            if (platformsParent.GetChild(i).GetChild(0).CompareTag(platformsParent.GetChild(j).GetChild(0).tag)
                                && platformsParent.GetChild(i).GetChild(0).name != "52" && platformsParent.GetChild(j).GetChild(0).name != "52")
                            {
                                int index = 0;
                                for (int k = 0; k < catsList.Count; k++)
                                {
                                    if (catsList[k].name == platformsParent.GetChild(i).GetChild(0).name)
                                    {
                                        index = k;
                                        break;
                                    }
                                }

                                SoundManager.instance.playSound(SoundManager.instance.sound3);

                                GameObject obj = Instantiate(catsList[++index], new UnityEngine.Vector3(platformsParent.GetChild(i).transform.position.x, platformsParent.GetChild(i).transform.position.y, 0), UnityEngine.Quaternion.identity);
                                obj.name = catsList[index].name;
                                obj.transform.parent = platformsParent.GetChild(i);
                                obj.transform.localPosition = new UnityEngine.Vector2(0, 0);
                                obj.GetComponent<DragObject>().startPosition = obj.transform.localPosition;
                                obj.GetComponent<DragObject>().startScale = obj.transform.localScale;
                                Instantiate(catsSpawnEffect, new UnityEngine.Vector3(platformsParent.GetChild(i).transform.position.x, platformsParent.GetChild(i).transform.position.y + 0.35f, 0), UnityEngine.Quaternion.identity);

                                DOTween.Kill(platformsParent.GetChild(i).GetChild(0));
                                DOTween.Kill(platformsParent.GetChild(j).GetChild(0));

                                UpdateAchievements(index);
                                UpdateDailyTasks();
                                Navigation.instance.NewCatUnlockPopUp(obj.GetComponent<SpriteRenderer>().sprite, obj.name, obj.tag, index);

                               
                                Destroy(platformsParent.GetChild(i).GetChild(0).gameObject);
                                Destroy(platformsParent.GetChild(j).GetChild(0).gameObject);

                                if (autoMergeTimer > 0)
                                    Invoke("AutoMerge", 1);
                                else
                                {
                                    autoMergetimerText.transform.parent.gameObject.SetActive(false);
                                    autoMergeTimer = 180;
                                    autoMergeButton.interactable = true;
                                }

                                return;
                            }
                        }
                    }
                }

            }
            Invoke("AutoMerge", 1);
        }

        else
        {
            autoMergetimerText.transform.parent.gameObject.SetActive(false);
            autoMergeTimer = 180;
            autoMergeButton.interactable = true;
        }

    }

    #endregion


    #region Double Money
    public void StartDoubleMoney()
    {
        doubleMoneyButton.interactable = false;
        doubleMoneyTimerText.transform.parent.gameObject.SetActive(true);
        Navigation.instance.CloseDoubleMoneyPopUp();
        DoubleMoney();
    }

    public void DoubleMoney()
    {
        if (doubleMoneyTimer > 0)
        {
            doubleMoneyEnabled = true;
            doubleMoneyTimer--;
            int minutes = doubleMoneyTimer / 60;
            float seconds = Mathf.Round((((float)doubleMoneyTimer / 60) - (doubleMoneyTimer / 60)) * 60);

            if (minutes < 10 && seconds > 10)
                doubleMoneyTimerText.text = "0" + minutes + " : " + seconds;
            else if (seconds < 10 && minutes > 10)
                doubleMoneyTimerText.text = minutes + " : " + "0" + seconds;
            else if (minutes < 10 && seconds < 10)
                doubleMoneyTimerText.text = "0" + minutes + " : " + "0" + seconds;
            else
                doubleMoneyTimerText.text = minutes + " : " + seconds;

           
            Invoke("DoubleMoney", 1);
        }

        else
        {
            doubleMoneyEnabled = false;
            doubleMoneyTimerText.transform.parent.gameObject.SetActive(false);
            doubleMoneyTimer = 180;
            doubleMoneyButton.interactable = true;
        }

    }
    #endregion


    #region Achivements & Daily Tasks
    public void GetReward()
    {
        string parentName = EventSystem.current.currentSelectedGameObject.transform.parent.name;

        if (parentName.Equals(StateSaving.instance.ACHIEVEMENT1))
            PlayerPrefs.SetInt(StateSaving.instance.ACHIEVEMENT1, 1);
        if (parentName.Equals(StateSaving.instance.ACHIEVEMENT2))
            PlayerPrefs.SetInt(StateSaving.instance.ACHIEVEMENT2, 1);
        if (parentName.Equals(StateSaving.instance.ACHIEVEMENT3))
            PlayerPrefs.SetInt(StateSaving.instance.ACHIEVEMENT3, 1);
        if (parentName.Equals(StateSaving.instance.ACHIEVEMENT4))
            PlayerPrefs.SetInt(StateSaving.instance.ACHIEVEMENT4, 1);
        if (parentName.Equals(StateSaving.instance.ACHIEVEMENT5))
            PlayerPrefs.SetInt(StateSaving.instance.ACHIEVEMENT5, 1);
        if (parentName.Equals(StateSaving.instance.ACHIEVEMENT6))
            PlayerPrefs.SetInt(StateSaving.instance.ACHIEVEMENT6, 1);
        if (parentName.Equals(StateSaving.instance.ACHIEVEMENT7))
            PlayerPrefs.SetInt(StateSaving.instance.ACHIEVEMENT7, 1);
        if (parentName.Equals(StateSaving.instance.ACHIEVEMENT8))
            PlayerPrefs.SetInt(StateSaving.instance.ACHIEVEMENT8, 1);
        if (parentName.Equals(StateSaving.instance.ACHIEVEMENT9))
            PlayerPrefs.SetInt(StateSaving.instance.ACHIEVEMENT9, 1);
        if (parentName.Equals(StateSaving.instance.ACHIEVEMENT10))
            PlayerPrefs.SetInt(StateSaving.instance.ACHIEVEMENT10, 1);
        if (parentName.Equals(StateSaving.instance.ACHIEVEMENT11))
            PlayerPrefs.SetInt(StateSaving.instance.ACHIEVEMENT11, 1);
        if (parentName.Equals(StateSaving.instance.ACHIEVEMENT12))
            PlayerPrefs.SetInt(StateSaving.instance.ACHIEVEMENT12, 1);
        if (parentName.Equals(StateSaving.instance.ACHIEVEMENT13))
            PlayerPrefs.SetInt(StateSaving.instance.ACHIEVEMENT13, 1);
        if (parentName.Equals(StateSaving.instance.ACHIEVEMENT14))
            PlayerPrefs.SetInt(StateSaving.instance.ACHIEVEMENT14, 1);
        if (parentName.Equals(StateSaving.instance.ACHIEVEMENT15))
            PlayerPrefs.SetInt(StateSaving.instance.ACHIEVEMENT15, 1);

        if (parentName.Equals(StateSaving.instance.TASK1))
            PlayerPrefs.SetInt(StateSaving.instance.TASK1, 1);
        if (parentName.Equals(StateSaving.instance.TASK2))
            PlayerPrefs.SetInt(StateSaving.instance.TASK2, 1);
        if (parentName.Equals(StateSaving.instance.TASK3))
            PlayerPrefs.SetInt(StateSaving.instance.TASK3, 1);
        if (parentName.Equals(StateSaving.instance.TASK4))
            PlayerPrefs.SetInt(StateSaving.instance.TASK4, 1);

        SoundManager.instance.playSound(SoundManager.instance.sound4);
        string name = EventSystem.current.currentSelectedGameObject.name;
        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().interactable = false;
        UpdateCurrency(BigInteger.Parse(name));
    }

    public void UpdateDailyTasks()
    {
        if (Achievements.instance.merge20Cats != 20)
            Achievements.instance.merge20Cats++;

        if (Achievements.instance.get30Cats != 30)
            Achievements.instance.get30Cats++;
    }

    public void UpdateAchievements(int index)
    {
        index++;
    
        if (Achievements.instance.merge1200Times != 1200)
            Achievements.instance.merge1200Times++;

        if (index == 10)
        {
            if (Achievements.instance.unlockCatLevel10 != 1)
                Achievements.instance.unlockCatLevel10++;
        }

        if (index == 3)
        {
            if (Achievements.instance.collect50Level3Cats != 50)
                Achievements.instance.collect50Level3Cats++;
        }
        if (index == 6)
        {
            if (Achievements.instance.collect50Level6Cats != 50)
                Achievements.instance.collect50Level6Cats++;
        }
        if (index == 9)
        {
            if (Achievements.instance.collect50Level9Cats != 50)
                Achievements.instance.collect50Level9Cats++;
        }
        if (index == 12)
        {
            if (Achievements.instance.collect50Level12Cats != 50)
                Achievements.instance.collect50Level12Cats++;
        }
        if (index == 15)
        {
            if (Achievements.instance.collect50Level15Cats != 50)
                Achievements.instance.collect50Level15Cats++;
        }
        if (index == 18)
        {
            if (Achievements.instance.collect50Level18Cats != 50)
                Achievements.instance.collect50Level18Cats++;
        }
        if (index == 20)
        {
            if (Achievements.instance.collect50Level20Cats != 50)
                Achievements.instance.collect50Level20Cats++;
        }
        if (index == 30)
        {
            if (Achievements.instance.collect30Level30Cats != 30)
                Achievements.instance.collect30Level30Cats++;
        }
        if (index == 35)
        {
            if (Achievements.instance.collect30Level35Cats != 30)
                Achievements.instance.collect30Level35Cats++;
        }
        if (index == 42)
        {
            if (Achievements.instance.collect30Level42Cats != 30)
                Achievements.instance.collect30Level42Cats++;
        }
        if (index == 50)
        {
            if (Achievements.instance.collect20Level50Cats != 20)
                Achievements.instance.collect20Level50Cats++;
        }
    }

    #endregion

    #region Balloon

    private void SpawnBalloon()
    {
        if (balloonSpawnInterval == 0)
        {
            balloon.SetActive(true);
            balloon.GetComponent<Animator>().Play("BalloonAnimation", -1, 0);
            balloonSpawnInterval = 240;
        }

        else
            balloonSpawnInterval--;

        Invoke("SpawnBalloon", 1);
    }

    public void BalloonActivated()
    {
        balloonTimerText.gameObject.SetActive(true);
        StartTimer();
    }

    public void StartTimer()
    {
        if (balloonTimer > 0)
        {
            balloonEnabled = true;
            balloonTimer--;
            int minutes = balloonTimer / 60;
            float seconds = Mathf.Round((((float)balloonTimer / 60) - (balloonTimer / 60)) * 60);

            if (minutes < 10 && seconds > 10)
                balloonTimerText.text = "0" + minutes + " : " + seconds;
            else if (seconds < 10 && minutes > 10)
                balloonTimerText.text = minutes + " : " + "0" + seconds;
            else if (minutes < 10 && seconds < 10)
                balloonTimerText.text = "0" + minutes + " : " + "0" + seconds;
            else
                balloonTimerText.text = minutes + " : " + seconds;


            Invoke("StartTimer", 1);
        }

        else
        {
            balloonEnabled = false;
            balloonTimerText.gameObject.SetActive(false);
            Spawner.instance.BalloonDeActivated();
            balloonTimer = 120;
        }

    }
    #endregion
}
