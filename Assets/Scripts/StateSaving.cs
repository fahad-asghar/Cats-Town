using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using UnityEngine.UI;
using System;
public class StateSaving : MonoBehaviour
{
    public static StateSaving instance;

    [Header("DATA STORING VARIABLES")]
    public string NEW_CAT;
    public string ACTIVE_KENNELS;
    public string TOTAL_CURRENCY;
    public string INCOME_PER_SECOND;

    public string HOUR;
    public string MINUTE;
    public string SECOND;
    public string DAY;

    public string KENNEL_1_CAT;
    public string KENNEL_2_CAT;
    public string KENNEL_3_CAT;
    public string KENNEL_4_CAT;
    public string KENNEL_5_CAT;
    public string KENNEL_6_CAT;
    public string KENNEL_7_CAT;
    public string KENNEL_8_CAT;
    public string KENNEL_9_CAT;
    public string KENNEL_10_CAT;
    public string KENNEL_11_CAT;
    public string KENNEL_12_CAT;
    public string KENNEL_13_CAT;
    public string KENNEL_14_CAT;
    public string KENNEL_15_CAT;
    public string KENNEL_16_CAT;
    public string KENNEL_17_CAT;
    public string KENNEL_18_CAT;
    public string KENNEL_19_CAT;
    public string KENNEL_20_CAT;
    public string KENNEL_21_CAT;
    public string KENNEL_22_CAT;
    public string KENNEL_23_CAT;
    public string KENNEL_24_CAT;
    public string KENNEL_25_CAT;


    public string ACHIEVEMENT1;
    public string ACHIEVEMENT2;
    public string ACHIEVEMENT3;
    public string ACHIEVEMENT4;
    public string ACHIEVEMENT5;
    public string ACHIEVEMENT6;
    public string ACHIEVEMENT7;
    public string ACHIEVEMENT8;
    public string ACHIEVEMENT9;
    public string ACHIEVEMENT10;
    public string ACHIEVEMENT11;
    public string ACHIEVEMENT12;
    public string ACHIEVEMENT13;
    public string ACHIEVEMENT14;
    public string ACHIEVEMENT15;

    public string UNLOCK_CAT_LEVEL_10;
    public string BUY_50_CATS;
    public string MERGE_1200_TIMES;
    public string OPEN_40_GIFT_BOXES;
    public string COLLECT_50_LEVEL_3_CATS;
    public string COLLECT_50_LEVEL_6_CATS;
    public string COLLECT_50_LEVEL_9_CATS;
    public string COLLECT_50_LEVEL_12_CATS;
    public string COLLECT_50_LEVEL_15_CATS;
    public string COLLECT_50_LEVEL_18_CATS;
    public string COLLECT_50_LEVEL_20_CATS;
    public string COLLECT_30_LEVEL_30_CATS;
    public string COLLECT_30_LEVEL_35_CATS;
    public string COLLECT_30_LEVEL_42_CATS;
    public string COLLECT_20_LEVEL_50_CATS;

    public string TASK1;
    public string TASK2;
    public string TASK3;
    public string TASK4;
    public string TASK5;

    public string DAILY_LOGIN;
    public string BUY_10_CATS;
    public string MERGE_20_CATS;
    public string GET_30_CATS;



    [Header("DATA REQUIRED FOR LOADING")]
    [SerializeField] Transform platformsParent;
    [SerializeField] Text idleMoneyText;
    [SerializeField] List<GameObject> catsList = new List<GameObject>();


    private void Awake()
    {
        instance = this;
        LoadCurrency();
        LoadIncomeGeneratedAfterPassedTime();
        LoadKennelStates();
        LoadCatsState();
        LoadAchievements();
        LoadDailyTasks();
    }

    private void LoadCurrency()
    {
        if (PlayerPrefs.HasKey(TOTAL_CURRENCY))
        {
            string temp = PlayerPrefs.GetString(TOTAL_CURRENCY);
            GameHandler.instance.UpdateCurrency(BigInteger.Parse(temp));
        }
    }
    private void LoadIncomeGeneratedAfterPassedTime()
    {
        if (PlayerPrefs.HasKey(HOUR) && PlayerPrefs.HasKey(MINUTE)
            && PlayerPrefs.HasKey(SECOND) && PlayerPrefs.HasKey(DAY))
        {
            int differenceInSeconds = 0;
            int currentTimeInSeconds = (DateTime.Now.Hour * 60 * 60) + (DateTime.Now.Minute * 60) + DateTime.Now.Second;
            int savedTimeInSeconds = (PlayerPrefs.GetInt(HOUR) * 60 * 60) + (PlayerPrefs.GetInt(MINUTE) * 60) + PlayerPrefs.GetInt(SECOND);

            if (DateTime.Now.Day == PlayerPrefs.GetInt(DAY))
                differenceInSeconds = currentTimeInSeconds - savedTimeInSeconds;
            else
                differenceInSeconds = 86400;


            string incomePerSecond = PlayerPrefs.GetString(INCOME_PER_SECOND);
            string difference = differenceInSeconds.ToString();
            BigInteger tempIncomePerSecond = BigInteger.Parse(incomePerSecond);
            BigInteger tempDifferenceInSecond = BigInteger.Parse(difference);

            GameHandler.instance.UpdateCurrency(tempDifferenceInSecond * tempIncomePerSecond);
            idleMoneyText.text = tempIncomePerSecond * tempDifferenceInSecond + "";

            if(tempIncomePerSecond * tempDifferenceInSecond > 0)
                Navigation.instance.IdleMoneyPopUp();

        }
    }
    private void LoadKennelStates()
    {
        if (PlayerPrefs.HasKey(ACTIVE_KENNELS))
        {
            for (int i = 0; i < PlayerPrefs.GetInt(ACTIVE_KENNELS); i++)
            {
                platformsParent.GetChild(i).gameObject.SetActive(true);
            }
            GameHandler.instance.CheckKennelLayout();
        }
    }
    private void LoadCatsState()
    {
        if (PlayerPrefs.HasKey(KENNEL_1_CAT))
        {
            int index = 0;
            for(int i = 0; i < catsList.Count; i++)
            {
                if (PlayerPrefs.GetString(KENNEL_1_CAT) == catsList[i].name)
                {
                    index = i;
                    break;
                }
            }
            SpawnCat(index, platformsParent.GetChild(0));
        }

        if (PlayerPrefs.HasKey(KENNEL_2_CAT))
        {
            int index = 0;
            for (int i = 0; i < catsList.Count; i++)
            {
                if (PlayerPrefs.GetString(KENNEL_2_CAT) == catsList[i].name)
                {
                    index = i;
                    break;
                }
            }
            SpawnCat(index, platformsParent.GetChild(1));
        }

        if (PlayerPrefs.HasKey(KENNEL_3_CAT))
        {
            int index = 0;
            for (int i = 0; i < catsList.Count; i++)
            {
                if (PlayerPrefs.GetString(KENNEL_3_CAT) == catsList[i].name)
                {
                    index = i;
                    break;
                }
            }
            SpawnCat(index, platformsParent.GetChild(2));
        }

        if (PlayerPrefs.HasKey(KENNEL_4_CAT))
        {
            int index = 0;
            for (int i = 0; i < catsList.Count; i++)
            {
                if (PlayerPrefs.GetString(KENNEL_4_CAT) == catsList[i].name)
                {
                    index = i;
                    break;
                }
            }
            SpawnCat(index, platformsParent.GetChild(3));
        }

        if (PlayerPrefs.HasKey(KENNEL_5_CAT))
        {
            int index = 0;
            for (int i = 0; i < catsList.Count; i++)
            {
                if (PlayerPrefs.GetString(KENNEL_5_CAT) == catsList[i].name)
                {
                    index = i;
                    break;
                }
            }
            SpawnCat(index, platformsParent.GetChild(4));
        }

        if (PlayerPrefs.HasKey(KENNEL_6_CAT))
        {
            int index = 0;
            for (int i = 0; i < catsList.Count; i++)
            {
                if (PlayerPrefs.GetString(KENNEL_6_CAT) == catsList[i].name)
                {
                    index = i;
                    break;
                }
            }
            SpawnCat(index, platformsParent.GetChild(5));
        }

        if (PlayerPrefs.HasKey(KENNEL_7_CAT))
        {
            int index = 0;
            for (int i = 0; i < catsList.Count; i++)
            {
                if (PlayerPrefs.GetString(KENNEL_7_CAT) == catsList[i].name)
                {
                    index = i;
                    break;
                }
            }
            SpawnCat(index, platformsParent.GetChild(6));
        }

        if (PlayerPrefs.HasKey(KENNEL_8_CAT))
        {
            int index = 0;
            for (int i = 0; i < catsList.Count; i++)
            {
                if (PlayerPrefs.GetString(KENNEL_8_CAT) == catsList[i].name)
                {
                    index = i;
                    break;
                }
            }
            SpawnCat(index, platformsParent.GetChild(7));
        }

        if (PlayerPrefs.HasKey(KENNEL_9_CAT))
        {
            int index = 0;
            for (int i = 0; i < catsList.Count; i++)
            {
                if (PlayerPrefs.GetString(KENNEL_9_CAT) == catsList[i].name)
                {
                    index = i;
                    break;
                }
            }
            SpawnCat(index, platformsParent.GetChild(8));
        }

        if (PlayerPrefs.HasKey(KENNEL_10_CAT))
        {
            int index = 0;
            for (int i = 0; i < catsList.Count; i++)
            {
                if (PlayerPrefs.GetString(KENNEL_10_CAT) == catsList[i].name)
                {
                    index = i;
                    break;
                }
            }
            SpawnCat(index, platformsParent.GetChild(9));
        }

        if (PlayerPrefs.HasKey(KENNEL_11_CAT))
        {
            int index = 0;
            for (int i = 0; i < catsList.Count; i++)
            {
                if (PlayerPrefs.GetString(KENNEL_11_CAT) == catsList[i].name)
                {
                    index = i;
                    break;
                }
            }
            SpawnCat(index, platformsParent.GetChild(10));
        }

        if (PlayerPrefs.HasKey(KENNEL_12_CAT))
        {
            int index = 0;
            for (int i = 0; i < catsList.Count; i++)
            {
                if (PlayerPrefs.GetString(KENNEL_12_CAT) == catsList[i].name)
                {
                    index = i;
                    break;
                }
            }
            SpawnCat(index, platformsParent.GetChild(11));
        }

        if (PlayerPrefs.HasKey(KENNEL_13_CAT))
        {
            int index = 0;
            for (int i = 0; i < catsList.Count; i++)
            {
                if (PlayerPrefs.GetString(KENNEL_13_CAT) == catsList[i].name)
                {
                    index = i;
                    break;
                }
            }
            SpawnCat(index, platformsParent.GetChild(12));
        }

        if (PlayerPrefs.HasKey(KENNEL_14_CAT))
        {
            int index = 0;
            for (int i = 0; i < catsList.Count; i++)
            {
                if (PlayerPrefs.GetString(KENNEL_14_CAT) == catsList[i].name)
                {
                    index = i;
                    break;
                }
            }
            SpawnCat(index, platformsParent.GetChild(13));
        }

        if (PlayerPrefs.HasKey(KENNEL_15_CAT))
        {
            int index = 0;
            for (int i = 0; i < catsList.Count; i++)
            {
                if (PlayerPrefs.GetString(KENNEL_15_CAT) == catsList[i].name)
                {
                    index = i;
                    break;
                }
            }
            SpawnCat(index, platformsParent.GetChild(14));
        }

        if (PlayerPrefs.HasKey(KENNEL_16_CAT))
        {
            int index = 0;
            for (int i = 0; i < catsList.Count; i++)
            {
                if (PlayerPrefs.GetString(KENNEL_16_CAT) == catsList[i].name)
                {
                    index = i;
                    break;
                }
            }
            SpawnCat(index, platformsParent.GetChild(15));
        }

        if (PlayerPrefs.HasKey(KENNEL_17_CAT))
        {
            int index = 0;
            for (int i = 0; i < catsList.Count; i++)
            {
                if (PlayerPrefs.GetString(KENNEL_17_CAT) == catsList[i].name)
                {
                    index = i;
                    break;
                }
            }
            SpawnCat(index, platformsParent.GetChild(16));
        }

        if (PlayerPrefs.HasKey(KENNEL_18_CAT))
        {
            int index = 0;
            for (int i = 0; i < catsList.Count; i++)
            {
                if (PlayerPrefs.GetString(KENNEL_18_CAT) == catsList[i].name)
                {
                    index = i;
                    break;
                }
            }
            SpawnCat(index, platformsParent.GetChild(17));
        }

        if (PlayerPrefs.HasKey(KENNEL_19_CAT))
        {
            int index = 0;
            for (int i = 0; i < catsList.Count; i++)
            {
                if (PlayerPrefs.GetString(KENNEL_19_CAT) == catsList[i].name)
                {
                    index = i;
                    break;
                }
            }
            SpawnCat(index, platformsParent.GetChild(18));
        }

        if (PlayerPrefs.HasKey(KENNEL_20_CAT))
        {
            int index = 0;
            for (int i = 0; i < catsList.Count; i++)
            {
                if (PlayerPrefs.GetString(KENNEL_20_CAT) == catsList[i].name)
                {
                    index = i;
                    break;
                }
            }
            SpawnCat(index, platformsParent.GetChild(19));
        }

        if (PlayerPrefs.HasKey(KENNEL_21_CAT))
        {
            int index = 0;
            for (int i = 0; i < catsList.Count; i++)
            {
                if (PlayerPrefs.GetString(KENNEL_21_CAT) == catsList[i].name)
                {
                    index = i;
                    break;
                }
            }
            SpawnCat(index, platformsParent.GetChild(20));
        }

        if (PlayerPrefs.HasKey(KENNEL_22_CAT))
        {
            int index = 0;
            for (int i = 0; i < catsList.Count; i++)
            {
                if (PlayerPrefs.GetString(KENNEL_22_CAT) == catsList[i].name)
                {
                    index = i;
                    break;
                }
            }
            SpawnCat(index, platformsParent.GetChild(21));
        }

        if (PlayerPrefs.HasKey(KENNEL_23_CAT))
        {
            int index = 0;
            for (int i = 0; i < catsList.Count; i++)
            {
                if (PlayerPrefs.GetString(KENNEL_23_CAT) == catsList[i].name)
                {
                    index = i;
                    break;
                }
            }
            SpawnCat(index, platformsParent.GetChild(22));
        }

        if (PlayerPrefs.HasKey(KENNEL_24_CAT))
        {
            int index = 0;
            for (int i = 0; i < catsList.Count; i++)
            {
                if (PlayerPrefs.GetString(KENNEL_24_CAT) == catsList[i].name)
                {
                    index = i;
                    break;
                }
            }
            SpawnCat(index, platformsParent.GetChild(23));
        }

        if (PlayerPrefs.HasKey(KENNEL_25_CAT))
        {
            int index = 0;
            for (int i = 0; i < catsList.Count; i++)
            {
                if (PlayerPrefs.GetString(KENNEL_25_CAT) == catsList[i].name)
                {
                    index = i;
                    break;
                }
            }
            SpawnCat(index, platformsParent.GetChild(24));
        }
    }
    private void SpawnCat(int index, Transform trans)
    {
        GameObject obj = Instantiate(catsList[index], new UnityEngine.Vector3(trans.position.x, trans.transform.position.y, 0), UnityEngine.Quaternion.identity);
        obj.name = catsList[index].name;
        obj.transform.parent = trans;
        obj.transform.localPosition = new UnityEngine.Vector2(0, 0);
        obj.GetComponent<DragObject>().startPosition = obj.transform.localPosition;
        obj.GetComponent<DragObject>().startScale = obj.transform.localScale;
    }
    private void LoadAchievements()
    {
        Achievements.instance.unlockCatLevel10 = PlayerPrefs.GetInt(UNLOCK_CAT_LEVEL_10);
        Achievements.instance.merge1200Times = PlayerPrefs.GetInt(MERGE_1200_TIMES);
        Achievements.instance.buy50Cats = PlayerPrefs.GetInt(BUY_50_CATS);
        Achievements.instance.open40GiftBoxes = PlayerPrefs.GetInt(OPEN_40_GIFT_BOXES);
        Achievements.instance.collect50Level3Cats = PlayerPrefs.GetInt(COLLECT_50_LEVEL_3_CATS);
        Achievements.instance.collect50Level6Cats = PlayerPrefs.GetInt(COLLECT_50_LEVEL_6_CATS);
        Achievements.instance.collect50Level9Cats = PlayerPrefs.GetInt(COLLECT_50_LEVEL_9_CATS);
        Achievements.instance.collect50Level12Cats = PlayerPrefs.GetInt(COLLECT_50_LEVEL_12_CATS);
        Achievements.instance.collect50Level15Cats = PlayerPrefs.GetInt(COLLECT_50_LEVEL_15_CATS);
        Achievements.instance.collect50Level18Cats = PlayerPrefs.GetInt(COLLECT_50_LEVEL_18_CATS);
        Achievements.instance.collect50Level20Cats = PlayerPrefs.GetInt(COLLECT_50_LEVEL_20_CATS);
        Achievements.instance.collect30Level30Cats = PlayerPrefs.GetInt(COLLECT_30_LEVEL_30_CATS);
        Achievements.instance.collect30Level35Cats = PlayerPrefs.GetInt(COLLECT_30_LEVEL_35_CATS);
        Achievements.instance.collect30Level42Cats = PlayerPrefs.GetInt(COLLECT_30_LEVEL_42_CATS);
        Achievements.instance.collect20Level50Cats = PlayerPrefs.GetInt(COLLECT_20_LEVEL_50_CATS);

        Achievements.instance.CheckAchievements();
    }
    private void LoadDailyTasks()
    {
        if (PlayerPrefs.GetInt(DAY) == DateTime.Now.Day)
        {
            Achievements.instance.dailyLogin = PlayerPrefs.GetInt(DAILY_LOGIN);
            Achievements.instance.buy10Cats = PlayerPrefs.GetInt(BUY_10_CATS);
            Achievements.instance.merge20Cats = PlayerPrefs.GetInt(MERGE_20_CATS);
            Achievements.instance.get30Cats = PlayerPrefs.GetInt(GET_30_CATS);
        }
        else
        {
            PlayerPrefs.DeleteKey(DAILY_LOGIN);
            PlayerPrefs.DeleteKey(BUY_10_CATS);
            PlayerPrefs.DeleteKey(MERGE_20_CATS);
            PlayerPrefs.DeleteKey(GET_30_CATS);

            PlayerPrefs.DeleteKey(TASK1);
            PlayerPrefs.DeleteKey(TASK2);
            PlayerPrefs.DeleteKey(TASK3);
            PlayerPrefs.DeleteKey(TASK4);

        }
    }

    private void OnApplicationQuit()
    {
        SaveCurrency();
        SaveIncomePerSecond();
        SaveKennelState();
        SaveCatsState();
        SaveAchievements();
        SaveDailyTasks();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveCurrency();
            SaveIncomePerSecond();
            SaveKennelState();
            SaveCatsState();
            SaveAchievements();
            SaveDailyTasks();
        }
    }

    private void SaveCurrency()
    {
        PlayerPrefs.SetString(TOTAL_CURRENCY, GameHandler.instance.totalCurrency.ToString());
    }
    private void SaveIncomePerSecond()
    {
        PlayerPrefs.DeleteKey(INCOME_PER_SECOND);
        BigInteger incomePerSecond = 0;
        for(int i = 0; i < platformsParent.childCount; i++)
        {
            if (platformsParent.GetChild(i).childCount > 0
                && platformsParent.GetChild(i).tag == "Platform")
            {
                string temp = platformsParent.GetChild(i).GetChild(0).GetComponent<CurrencyGenerator>().currencyPerSecond;
                incomePerSecond += BigInteger.Parse(temp);           
            }
        }
        PlayerPrefs.SetString(INCOME_PER_SECOND, incomePerSecond.ToString());
        PlayerPrefs.SetInt(HOUR, DateTime.Now.Hour);
        PlayerPrefs.SetInt(MINUTE, DateTime.Now.Minute);
        PlayerPrefs.SetInt(SECOND, DateTime.Now.Second);
        PlayerPrefs.SetInt(DAY, DateTime.Now.Day);
    }
    private void SaveKennelState()
    {
        int counter = 0;
        for (int i = 0; i < platformsParent.childCount; i++)
        {
            if (platformsParent.GetChild(i).gameObject.activeSelf 
                && platformsParent.GetChild(i).tag == "Platform")
                counter++;
        }
        PlayerPrefs.SetInt(ACTIVE_KENNELS, counter);                
    }
    private void SaveCatsState()
    {
        for (int i = 0; i < platformsParent.childCount; i++)
        {
            if (platformsParent.GetChild(i).childCount > 0
                && platformsParent.GetChild(i).gameObject.tag == "Platform")
            {
                if (i == 0)
                    PlayerPrefs.SetString(KENNEL_1_CAT, platformsParent.GetChild(i).GetChild(0).name);
                if (i == 1)
                    PlayerPrefs.SetString(KENNEL_2_CAT, platformsParent.GetChild(i).GetChild(0).name);
                if (i == 2)
                    PlayerPrefs.SetString(KENNEL_3_CAT, platformsParent.GetChild(i).GetChild(0).name);
                if (i == 3)
                    PlayerPrefs.SetString(KENNEL_4_CAT, platformsParent.GetChild(i).GetChild(0).name);
                if (i == 4)
                    PlayerPrefs.SetString(KENNEL_5_CAT, platformsParent.GetChild(i).GetChild(0).name);
                if (i == 5)
                    PlayerPrefs.SetString(KENNEL_6_CAT, platformsParent.GetChild(i).GetChild(0).name);
                if (i == 6)
                    PlayerPrefs.SetString(KENNEL_7_CAT, platformsParent.GetChild(i).GetChild(0).name);
                if (i == 7)
                    PlayerPrefs.SetString(KENNEL_8_CAT, platformsParent.GetChild(i).GetChild(0).name);
                if (i == 8)
                    PlayerPrefs.SetString(KENNEL_9_CAT, platformsParent.GetChild(i).GetChild(0).name);
                if (i == 9)
                    PlayerPrefs.SetString(KENNEL_10_CAT, platformsParent.GetChild(i).GetChild(0).name);
                if (i == 10)
                    PlayerPrefs.SetString(KENNEL_11_CAT, platformsParent.GetChild(i).GetChild(0).name);
                if (i == 11)
                    PlayerPrefs.SetString(KENNEL_12_CAT, platformsParent.GetChild(i).GetChild(0).name);
                if (i == 12)
                    PlayerPrefs.SetString(KENNEL_13_CAT, platformsParent.GetChild(i).GetChild(0).name);
                if (i == 13)
                    PlayerPrefs.SetString(KENNEL_14_CAT, platformsParent.GetChild(i).GetChild(0).name);
                if (i == 14)
                    PlayerPrefs.SetString(KENNEL_15_CAT, platformsParent.GetChild(i).GetChild(0).name);
                if (i == 15)
                    PlayerPrefs.SetString(KENNEL_16_CAT, platformsParent.GetChild(i).GetChild(0).name);
                if (i == 16)
                    PlayerPrefs.SetString(KENNEL_17_CAT, platformsParent.GetChild(i).GetChild(0).name);
                if (i == 17)
                    PlayerPrefs.SetString(KENNEL_18_CAT, platformsParent.GetChild(i).GetChild(0).name);
                if (i == 18)
                    PlayerPrefs.SetString(KENNEL_19_CAT, platformsParent.GetChild(i).GetChild(0).name);
                if (i == 19)
                    PlayerPrefs.SetString(KENNEL_20_CAT, platformsParent.GetChild(i).GetChild(0).name);
                if (i == 20)
                    PlayerPrefs.SetString(KENNEL_21_CAT, platformsParent.GetChild(i).GetChild(0).name);
                if (i == 21)
                    PlayerPrefs.SetString(KENNEL_22_CAT, platformsParent.GetChild(i).GetChild(0).name);
                if (i == 22)
                    PlayerPrefs.SetString(KENNEL_23_CAT, platformsParent.GetChild(i).GetChild(0).name);
                if (i == 23)
                    PlayerPrefs.SetString(KENNEL_24_CAT, platformsParent.GetChild(i).GetChild(0).name);
                if (i == 24)
                    PlayerPrefs.SetString(KENNEL_25_CAT, platformsParent.GetChild(i).GetChild(0).name);
            }

            else
            {
                if (i == 0)
                    PlayerPrefs.DeleteKey(KENNEL_1_CAT);
                if (i == 1)
                    PlayerPrefs.DeleteKey(KENNEL_2_CAT);
                if (i == 2)
                    PlayerPrefs.DeleteKey(KENNEL_3_CAT);
                if (i == 3)
                    PlayerPrefs.DeleteKey(KENNEL_4_CAT);
                if (i == 4)
                    PlayerPrefs.DeleteKey(KENNEL_5_CAT);
                if (i == 5)
                    PlayerPrefs.DeleteKey(KENNEL_6_CAT);
                if (i == 6)
                    PlayerPrefs.DeleteKey(KENNEL_7_CAT);
                if (i == 7)
                    PlayerPrefs.DeleteKey(KENNEL_8_CAT);
                if (i == 8)
                    PlayerPrefs.DeleteKey(KENNEL_9_CAT);
                if (i == 9)
                    PlayerPrefs.DeleteKey(KENNEL_10_CAT);
                if (i == 10)
                    PlayerPrefs.DeleteKey(KENNEL_11_CAT);
                if (i == 11)
                    PlayerPrefs.DeleteKey(KENNEL_12_CAT);
                if (i == 12)
                    PlayerPrefs.DeleteKey(KENNEL_13_CAT);
                if (i == 13)
                    PlayerPrefs.DeleteKey(KENNEL_14_CAT);
                if (i == 14)
                    PlayerPrefs.DeleteKey(KENNEL_15_CAT);
                if (i == 15)
                    PlayerPrefs.DeleteKey(KENNEL_16_CAT);
                if (i == 16)
                    PlayerPrefs.DeleteKey(KENNEL_17_CAT);
                if (i == 17)
                    PlayerPrefs.DeleteKey(KENNEL_18_CAT);
                if (i == 18)
                    PlayerPrefs.DeleteKey(KENNEL_19_CAT);
                if (i == 19)
                    PlayerPrefs.DeleteKey(KENNEL_20_CAT);
                if (i == 20)
                    PlayerPrefs.DeleteKey(KENNEL_21_CAT);
                if (i == 21)
                    PlayerPrefs.DeleteKey(KENNEL_22_CAT);
                if (i == 22)
                    PlayerPrefs.DeleteKey(KENNEL_23_CAT);
                if (i == 23)
                    PlayerPrefs.DeleteKey(KENNEL_24_CAT);
                if (i == 24)
                    PlayerPrefs.DeleteKey(KENNEL_25_CAT);             
            }
        }


    }
    private void SaveAchievements()
    {
        PlayerPrefs.SetInt(UNLOCK_CAT_LEVEL_10, Achievements.instance.unlockCatLevel10);
        PlayerPrefs.SetInt(MERGE_1200_TIMES, Achievements.instance.merge1200Times);
        PlayerPrefs.SetInt(BUY_50_CATS, Achievements.instance.buy50Cats);
        PlayerPrefs.SetInt(OPEN_40_GIFT_BOXES, Achievements.instance.open40GiftBoxes);
        PlayerPrefs.SetInt(COLLECT_50_LEVEL_3_CATS, Achievements.instance.collect50Level3Cats);
        PlayerPrefs.SetInt(COLLECT_50_LEVEL_6_CATS, Achievements.instance.collect50Level6Cats);
        PlayerPrefs.SetInt(COLLECT_50_LEVEL_9_CATS, Achievements.instance.collect50Level9Cats);
        PlayerPrefs.SetInt(COLLECT_50_LEVEL_12_CATS, Achievements.instance.collect50Level12Cats);
        PlayerPrefs.SetInt(COLLECT_50_LEVEL_15_CATS, Achievements.instance.collect50Level15Cats);
        PlayerPrefs.SetInt(COLLECT_50_LEVEL_18_CATS, Achievements.instance.collect50Level18Cats);
        PlayerPrefs.SetInt(COLLECT_50_LEVEL_20_CATS, Achievements.instance.collect50Level20Cats);
        PlayerPrefs.SetInt(COLLECT_30_LEVEL_30_CATS, Achievements.instance.collect30Level30Cats);
        PlayerPrefs.SetInt(COLLECT_30_LEVEL_35_CATS, Achievements.instance.collect30Level35Cats);
        PlayerPrefs.SetInt(COLLECT_30_LEVEL_42_CATS, Achievements.instance.collect30Level42Cats);
        PlayerPrefs.SetInt(COLLECT_20_LEVEL_50_CATS, Achievements.instance.collect20Level50Cats);
    }
    private void SaveDailyTasks()
    {
        PlayerPrefs.SetInt(DAILY_LOGIN, Achievements.instance.dailyLogin);
        PlayerPrefs.SetInt(BUY_10_CATS, Achievements.instance.buy10Cats);
        PlayerPrefs.SetInt(MERGE_20_CATS, Achievements.instance.merge20Cats);
        PlayerPrefs.SetInt(GET_30_CATS, Achievements.instance.get30Cats);
    }

}
