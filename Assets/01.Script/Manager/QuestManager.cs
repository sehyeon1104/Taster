using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoSingleton<QuestManager>
{
    public bool doQuest { private set; get; } = false;
    public bool isClear { private set; get; } = false;

    public int killedNpcCount = 0;
    [SerializeField]
    private GameObject researcherPrefab = null;
    [SerializeField]
    private GameObject soldierPrefab = null;
    [SerializeField]
    private GameObject nursePrefab = null;
    [SerializeField]
    private int summonResearcherCount = 0;
    [SerializeField]
    private int summonSoldierCount = 0;
    [SerializeField]
    private int summonNurseCount = 0;

    [SerializeField]
    private Transform[] kitchenMobSpawnPos;
    [SerializeField]
    private Transform[] LaboratorMobSpawnPos;

    private void Start()
    {
        doQuest = false;
        isClear = false;
    }

    private void EnterKitchen()
    {
        Debug.Log("Enter Kitchen");
        UIManager.Instance.questTitle.text = "Tasty People";
        UIManager.Instance.questContent.text = "Defeat all people.";

        doQuest = true;
        UIManager.Instance.ToggleQuestUI(doQuest);

        StartCoroutine(KitchenQuest());
    }

    IEnumerator KitchenQuest()
    {
        summonResearcherCount = 12;
        summonNurseCount = 8;

        InstantiateResearcher(summonResearcherCount, kitchenMobSpawnPos);
        InstantiateNurse(summonNurseCount, kitchenMobSpawnPos);

        while (!isClear)
        {
            for(int i = 0; i < kitchenMobSpawnPos.Length; ++i)
            {
                if(kitchenMobSpawnPos[i].childCount != 0)
                {
                    break;
                }

                if(i == kitchenMobSpawnPos.Length - 1)
                {
                    isClear = true;
                    doQuest = false;
                }
            }

            yield return new WaitForEndOfFrame();
        }

        SaveManager.Instance.SaveToJson();
        UIManager.Instance.ToggleQuestUI(doQuest);
        yield break;
    }

    private void EnterLaborator()
    {
        Debug.Log("Enter Laborator");
        UIManager.Instance.questTitle.text = "New Skill..?";
        UIManager.Instance.questContent.text = "Defeat all people \n who guard a laborator.";

        doQuest = true;

        StartCoroutine(LaboratorQuest());
    }

    IEnumerator LaboratorQuest()
    {
        summonResearcherCount = 15;
        summonSoldierCount = 5;

        InstantiateResearcher(summonResearcherCount, LaboratorMobSpawnPos);
        InstantiateSoldier(summonSoldierCount, LaboratorMobSpawnPos);

        for (int i = 0; i < LaboratorMobSpawnPos.Length; ++i)
        {
            if (LaboratorMobSpawnPos[i].childCount != 0)
            {
                break;
            }

            if (i == LaboratorMobSpawnPos.Length - 1)
            {
                isClear = true;
                doQuest = false;
            }
        }
        
        SaveManager.Instance.SaveToJson();
        UIManager.Instance.ToggleQuestUI(doQuest);
        yield break;
    }

    void ServerRoomQuest()
    {
        Debug.Log("Interfere Communication");
        UIManager.Instance.questTitle.text = "Interfere with communication";
        UIManager.Instance.questContent.text = "A person is communicating to the outside. \nGet in the way!";
    }

    void InstantiateResearcher(int researcherCount, Transform[] spawnPos)
    {
        int pivot = 0;

        while (spawnPos[pivot].childCount != 0 && pivot < spawnPos.Length)
        {
            pivot++;
        }

        for (int i = pivot; i < pivot + researcherCount; ++i)
        {
            Instantiate(researcherPrefab, spawnPos[i]);
        }
    }

    void InstantiateSoldier(int soldierCount, Transform[] spawnPos)
    {
        int pivot = 0;

        while(spawnPos[pivot].childCount != 0 && pivot < spawnPos.Length)
        {
            pivot++;
        }

        for (int i = pivot; i < pivot + soldierCount; ++i)
        {
            Instantiate(soldierPrefab, spawnPos[i]);
        }
    }

    void InstantiateNurse(int nurseCount, Transform[] spawnPos)
    {
        int pivot = 0;

        Debug.Log("InstantiateNurse");

        while (spawnPos[pivot].childCount != 0 && pivot < spawnPos.Length)
        {
            Debug.Log(pivot);
            pivot++;
        }

        for (int i = pivot; i < pivot + nurseCount; ++i)
        {
            Debug.Log("Instantiate");
            Instantiate(nursePrefab, spawnPos[i]);
        }
    }

}
