using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour, IRestartableObject
{
    public GameObject scoreArea;
    public float destinationTime = 1f;
    public GameDatabase gameDatabase;
    public AudioSource audioSource;

    private ScoreAreaController scoreAreaController;
    private Vector3 outPosition;
    private float activateTime;
    private float respawnRangeHorizontal;
    private float respawnRangeVertical;
    private float cameraRangeVertical;

    private bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        scoreAreaController = scoreArea.GetComponent<ScoreAreaController>();

        cameraRangeVertical = Camera.main.pixelHeight / 100;

        outPosition = new Vector3(20, 20, 0);
        transform.position = outPosition;

        isActive = false;
        activateTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ActivateBonus();
        OutOfScene();
    }

    private void SetBonusOnScene()
    {
        float lastItemPosition = scoreAreaController.ScoreAreaAmount[ scoreAreaController.ScoreAreaAmount.Count -1 ].transform.position.x;
        respawnRangeHorizontal = Random.Range(1f, scoreAreaController.distanceBetweenColumns - 1f);
        respawnRangeVertical = Random.Range(5f, cameraRangeVertical - 5f);
        transform.position = new Vector2(lastItemPosition + respawnRangeHorizontal, respawnRangeVertical);

    }

    private void ActivateBonus()
    {
        if (isActive == false)
        {
            activateTime += Time.deltaTime;

            if (activateTime >= destinationTime)
            {
                isActive = true;
                activateTime = 0f;
                SetBonusOnScene();
            }
        }
    }

    private void RemoveBonusFromScene()
    {
        isActive = false;
        transform.position = outPosition;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            audioSource.Play();
            RemoveBonusFromScene();
            GameplayManager.Instance.AddScore(10);
        }
    }

    private void OutOfScene()
    {
        if (transform.position.x  < Camera.main.transform.position.x - 15)
        {
            RemoveBonusFromScene();
        }
    }

    public void DoRestart()
    {
        RemoveBonusFromScene();
    }
}
