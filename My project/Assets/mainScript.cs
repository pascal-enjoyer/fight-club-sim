using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using static mainScript;
using static System.Net.Mime.MediaTypeNames;

public class mainScript : MonoBehaviour
{
    private static System.Random random = new System.Random();

    public GameObject RedFighterPrefab;
    public GameObject BlueFighterPrefab;

    public Dropdown redPriorityDropdown;
    public Dropdown bluePriorityDropdown;

    public UnityEngine.UI.Text redText;
    public UnityEngine.UI.Text blueText;
    public UnityEngine.UI.Text endText;

    public InputField iterationTime, maxCountToAdd, onStartCount, fightDuration;

    private int xCoordForFirstTeam = -45;
    private int zCoordForFirstTeam = -45;

    private int xCoordForSecondTeam = -45;
    private int zCoordForSecondTeam = 45;




    public System.Threading.ThreadPriority redPriority;
    public System.Threading.ThreadPriority bluePriority;

    private List<Team> teams;
    private ActionQueue actionQueue;


    IEnumerator EndSimulationAfterDuration(float duration)
    {
        Debug.Log("Starting simulation...");
        Debug.Log(duration);
        yield return new WaitForSeconds(duration);

        foreach (var team in teams)
        {
            Debug.Log($"Stopping {team.Name}'s fight");
            team.StopFighting();
        }

    }

    void Update()
    {
        if (teams != null)
            actionQueue.ExecuteActions();
    }

    public void StartSimulation()
    {
        if (teams != null)
        {
            foreach (var team in GameObject.FindGameObjectsWithTag("Player")) {
                Destroy(team);
            }
            teams.Clear();
            endText.text = "";

        }
        redPriority = GetPriorityFromDropdown(redPriorityDropdown);
        bluePriority = GetPriorityFromDropdown(bluePriorityDropdown);
        actionQueue = new ActionQueue();
        teams = new List<Team>
        {
            new Team("Красные", int.Parse(onStartCount.text), RedFighterPrefab, xCoordForFirstTeam, zCoordForFirstTeam, int.Parse(maxCountToAdd.text), actionQueue, redText, endText, int.Parse(iterationTime.text)),
            new Team("Синие", int.Parse(onStartCount.text), BlueFighterPrefab, xCoordForSecondTeam, zCoordForSecondTeam, int.Parse(maxCountToAdd.text), actionQueue, blueText, endText,int.Parse(iterationTime.text))
        };

        System.Threading.Thread redTeamThread = new Thread(() => teams[0].Fight(teams));
        redTeamThread.Priority = redPriority;
        redTeamThread.Start();
        System.Threading.Thread blueTeamThread = new Thread(() => teams[1].Fight(teams));
        blueTeamThread.Priority = bluePriority;
        blueTeamThread.Start();

        StartCoroutine(EndSimulationAfterDuration(Math.Max(int.Parse(fightDuration.text), 1)));
    }
    public class Team
    {
        public string Name { get; }
        public int FighterCount { get; private set; }
        private bool fighting = true;
        public List<GameObject> fighters = new List<GameObject>();
        public GameObject fighterPrefab;
        private ActionQueue actionQueue;
        public int maxCountToAdd;
        public int xCoord, zCoord;
        public UnityEngine.UI.Text text;
        public UnityEngine.UI.Text endText;
        public int iterationTime;

        public Team(string name, int initialFighters, GameObject fighterPrefab, int xCoord, int zCoord, int maxCountToAdd, ActionQueue actionQueue, UnityEngine.UI.Text text, UnityEngine.UI.Text endText, int iterationTime)
        {
            Name = name;
            FighterCount = initialFighters;
            this.fighterPrefab = fighterPrefab;
            this.xCoord = xCoord;
            this.zCoord = zCoord;
            this.actionQueue = actionQueue;
            this.maxCountToAdd = maxCountToAdd;
            this.text = text;
            this.endText = endText;
            this.iterationTime = iterationTime;
        }




        public void Fight(List<Team> teams)
        {
            SpawnFighters(this, FighterCount);
            while (fighting)
            {
                Thread.Sleep(random.Next(10, Math.Max(iterationTime, 11)));

                int newFighters = random.Next(1, maxCountToAdd);

                SpawnFighters(this, newFighters);
                FighterCount = fighters.Count + newFighters;

                Team opponent = teams[0] == this ? teams[1] : teams[0];
                int fightersLost = random.Next(1, Mathf.Min(opponent.fighters.Count + 1, maxCountToAdd));

                actionQueue.EnqueueAction(() =>
                {
                    for (int i = 0; i < fightersLost && opponent.fighters.Count >= fightersLost; i++)
                    {
                        opponent.fighters[i].GetComponent<fighter>().agent.SetDestination(new Vector3(opponent.fighters[i].GetComponent<fighter>().agent.transform.position.x, 1, 0));
                        opponent.fighters[i].GetComponent<Animator>().SetBool("run", true);
                        opponent.fighters.Remove(opponent.fighters[i]);
                    }
                });
                opponent.FighterCount = Mathf.Max(0, opponent.fighters.Count - fightersLost);
                actionQueue.EnqueueAction(() =>
                {
                    text.text = $"К команде {Name} присоединилось {newFighters} бойцов.\n" +
                    $"{Name} атакует {opponent.Name}, убивает {fightersLost} бойцов.";
                });


            }

            actionQueue.EnqueueAction(() =>
            {
                endText.text = $"В команде {teams[0].Name} осталось {teams[0].FighterCount} бойцов.\n" +
                $"В команде {teams[1].Name} осталось {teams[1].FighterCount} бойцов.";
            });
        }




        private void SpawnFighters(Team team, int count)
        {
            actionQueue.EnqueueAction(() =>
            {
                for (int i = 0; i < count; i++)
                {
                    Vector3 spawnPosition = new Vector3(random.Next(-Math.Abs(xCoord), Math.Abs(xCoord)), 1, random.Next(zCoord < 0 ? zCoord : 5, zCoord < 0 ? -5 : zCoord));
                    GameObject fighterPrefab2 = UnityEngine.Object.Instantiate(fighterPrefab, spawnPosition, Quaternion.identity);
                    fighterPrefab2.transform.LookAt(new Vector3(fighterPrefab2.transform.position.x, 1, 0));
                    fighters.Add(fighterPrefab2);
                }
            });
        }

        public void StopFighting()
        {
            Debug.Log($"{Name} is stopping fighting.");
            fighting = false;
        }
    }

    public class ActionQueue
    {
        private readonly Queue<Action> actions = new Queue<Action>();
       
        public void EnqueueAction(Action action)
        {
            lock (this)
                actions.Enqueue(action);
        }

        public void ExecuteActions()
        {
            lock (this)
            {
                while (actions.Count > 0)
                {
                    var action = actions.Dequeue();
                    action();
                }
            }
        }
    }
    private System.Threading.ThreadPriority GetPriorityFromDropdown(Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            case 0: return System.Threading.ThreadPriority.Lowest;
            case 1: return System.Threading.ThreadPriority.BelowNormal;
            case 2: return System.Threading.ThreadPriority.Normal;
            case 3: return System.Threading.ThreadPriority.AboveNormal;
            case 4: return System.Threading.ThreadPriority.Highest;
            default: return System.Threading.ThreadPriority.Normal;
        }
    }

}

