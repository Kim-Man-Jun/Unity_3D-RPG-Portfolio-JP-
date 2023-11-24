using System;
using UnityEngine;
using static actionSystem;

public class skill4_Fire : MonoBehaviour
{
    private actionSystem actionSystem;

    private Player player;
    private Enemy enemy;

    float time;

    private void Awake()
    {
        actionSystem = GameObject.FindGameObjectWithTag("ActionSystem").GetComponent<actionSystem>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (time > 1.9f)
        {
            actionSystem.skillNum4 = false;
            actionSystem.action_skill4Start = false;
            actionSystem.playerCurrent = Enum.GetName(typeof(playerStateList), 0);

            actionSystem.enemySelect = 0;
        }
    }
}
