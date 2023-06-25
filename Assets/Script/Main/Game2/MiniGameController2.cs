using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity1Week_20230619.Main.Game2
{
    public class MiniGameController2 : MiniGameControllerBase, IController, IInitializa
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private FallObjectSpawner fallObjectSpawner;
        [SerializeField] private FallObjectSpawner fallObjectSpawner_1;
        [SerializeField] private FallObjectSpawner fallObjectSpawner_2;
        private bool canInstance = false;
        public void Init()
        {
            base.Init(60);
            Announce.text = "MiniGameController2 \nに、チャレンジ！";

            playerController = miniGameObject.transform.Find("Basket").GetComponent<PlayerController>();
            fallObjectSpawner = miniGameObject.transform.Find("FallObjectSpawner").GetComponent<FallObjectSpawner>();
            fallObjectSpawner_1 = miniGameObject.transform.Find("FallObjectSpawner_1").GetComponent<FallObjectSpawner>();
            fallObjectSpawner_2 = miniGameObject.transform.Find("FallObjectSpawner_2").GetComponent<FallObjectSpawner>();
            fallObjectSpawner.gameObject.SetActive(false);
            fallObjectSpawner_1.gameObject.SetActive(false);
            fallObjectSpawner_2.gameObject.SetActive(false);
        }

        public void Control()
        {
            base.TimeUpdate();
            

            if (gameState != GameState.Play) return;
            fallObjectSpawner.gameObject.SetActive(true);
            playerController.Control();

            if (timerController.ElapsedTime <= 40f)
            {
                fallObjectSpawner_1.gameObject.SetActive(true);
            }

            if(timerController.ElapsedTime <= 20f)
            {
                fallObjectSpawner_2.gameObject.SetActive(true);
            }

            // TODO
            if (!playerController.IsAlive || timerController.ElapsedTime <= 0f)
            {
                gameState = GameState.End;
            }

            if (gameState == GameState.End && drawResultCoroutine == null)
            {
                drawResultCoroutine = StartCoroutine(DrawResult(playerController.GetScore(),2));
            }
        }

    }
}

