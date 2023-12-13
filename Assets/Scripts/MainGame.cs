using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using JetBrains.Annotations;
public class MainGame : MonoBehaviour
{
    #region champs de donnees
    [SerializeField] private List<MonsterInfosPerStage> MonsterInfosPerStages;
    private List<MonsterInfos> CurrentMonstersList;
    public List<Upgrade> Upgrades = new List<Upgrade>();
    public List<Upgrade> _unlockedUpgrades = new List<Upgrade>();
    public float TimeDuration = 20;
    public float Timer;
    public MonsterUI My_MonsterUI;
    public GameObject PrefabHitPoint;
    public GameObject PrefabUpgradeUI;
    public GameObject ParentUpgrades;
    int _currentMonster;
    public int stageMax;
    public int kills;
    public int killsMaxToNextStage;

    private bool IsTimerActive = false;
    public TMP_Text timerText;
    public float _timerAutoDamage;
    public int currentStageID;
    public TMP_Text textStage;
    public TMP_Text textcoins;
    public Button _retour;
    public Button _avance;
    public TMP_Text killsText;
    public int coins;
    
    #endregion
    #region Singleton
    private static MainGame instance;
    public static MainGame Instance { get; private set; }
    

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);
        instance = this;
    }
    #endregion

    void Start()
    {
        
        currentStageID = 0;
        stageMax = currentStageID;
        CurrentMonstersList = MonsterInfosPerStages[currentStageID].InfosList;
        My_MonsterUI.Coins = coins;

        //int LifeMonster = Monster._lifeMax;
        killsMaxToNextStage = CurrentMonstersList.Count;
        timerText.gameObject.SetActive(false);

        foreach (var upgrade in Upgrades)
        {
            GameObject go = GameObject.Instantiate(PrefabUpgradeUI, ParentUpgrades.transform, false);
            //go.transform.localPosition = Vector3.zero;
            go.GetComponent<UpgradeUI>().Initialize(upgrade);
        }
        My_MonsterUI.SetMonster(CurrentMonstersList[_currentMonster]);
    }

    void Update()
    {


        textStage.text = "stage " + (currentStageID + 1);
        if (currentStageID > 0)
        {
            _retour.gameObject.SetActive(true);
        }
        else
        {
            _retour.gameObject.SetActive(false);
        }

        if (currentStageID != stageMax)
        {
            _avance.gameObject.SetActive(true);
        }
        else
        {
            _avance.gameObject.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(world, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);

                MonsterUI monster = hit.collider.GetComponent<MonsterUI>();
                monster.Hit(1);


                GameObject go = GameObject.Instantiate(PrefabHitPoint, monster.Canvas.transform, false);
                go.transform.localPosition = new Vector3(world.x, world.y, 0);
                go.transform.DOLocalMoveY(150, 0.8f);
                go.GetComponent<Text>().DOFade(0, 0.8f);
                GameObject.Destroy(go, 0.8f);
                go.transform.localPosition = UnityEngine.Random.insideUnitCircle * 100;

                if (monster.IsAlive() == false)
                {

                    NextMonster();
                }


            }
        }





        UpdateTimer();
        _timerAutoDamage += Time.deltaTime;

        if (_timerAutoDamage >= 1.0f)
        {
            _timerAutoDamage = 0;
            foreach (var upgrade in _unlockedUpgrades)
            {
                My_MonsterUI.Hit(upgrade.DPS);

                GameObject go = GameObject.Instantiate(PrefabHitPoint, My_MonsterUI.Canvas.transform, false);
                go.transform.localPosition = new Vector3(0, 0, 0);
                go.transform.DOLocalMoveY(150, 0.8f);
                go.GetComponent<Text>().DOFade(0, 0.8f);
                GameObject.Destroy(go, 0.8f);
                go.transform.localPosition = UnityEngine.Random.insideUnitCircle * 100;

                if (My_MonsterUI.IsAlive() == false)
                {
                    NextMonster();

                }

            }

        }

    }

    public void Retour()
    {
        if (currentStageID > 0)
        {

            CurrentMonstersList = MonsterInfosPerStages[currentStageID].InfosList;
            currentStageID -= 1;

        }
    }

    public void Avance()
    {
        if (currentStageID < stageMax)
        {
            currentStageID += 1;
            CurrentMonstersList = MonsterInfosPerStages[currentStageID].InfosList;
        }
    }




    public void AddUpgrade(Upgrade upgrade)
    {
        _unlockedUpgrades.Add(upgrade);
    }

    private void NextMonster()
    {

        EarnCoins();
        _currentMonster = (_currentMonster + 1) % CurrentMonstersList.Count;
        My_MonsterUI.SetMonster(CurrentMonstersList[_currentMonster]);
        if (currentStageID == stageMax)
        {
            
            kills += 1;
            if (kills >= killsMaxToNextStage)
            {
                kills = 0;
                currentStageID += 1;
                stageMax += 1;
                CurrentMonstersList = MonsterInfosPerStages[currentStageID].InfosList;
            }
        }
        if (_currentMonster == 10)
        {
            StartTimer();
        }
        else
        {
            StopTimer();
        }
    }

    private void StartTimer()
    {
        timerText.gameObject.SetActive(true);
        IsTimerActive = true;
        Timer = TimeDuration;
    }

    private void UpdateTimer()
    {
        if (IsTimerActive)
        {
            Timer -= Time.deltaTime;
            timerText.text = "" + Timer.ToString();
            if (Timer <= 0f)
            {
                currentStageID -= 1;
                timerText.gameObject.SetActive(false);
            }
            //else
            //{
            //    //timerText.gameObject.SetActive(false);
            //    //IsTimerActive = false;
            //}
        }


    }

    private void StopTimer()
    {
        timerText.gameObject.SetActive(false);
        IsTimerActive = false;
    }

    private void ResetMonster()
    {
        My_MonsterUI.SetMonster(CurrentMonstersList[_currentMonster]);
        IsTimerActive = false;
        ResetTimer();
    }

    private void ResetTimer()
    {
        Timer = TimeDuration;
    }

    public void EarnCoins()
    {
        textcoins.text = "" + My_MonsterUI.Coins;
        if(My_MonsterUI.Life <= 0)
        {
            //coins += currentMonstersList.coins ;
        }
    }






}

