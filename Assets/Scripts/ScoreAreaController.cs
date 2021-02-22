using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAreaController : MonoBehaviour, IRestartableObject
{
    public GameDatabase gameDatabase;
    public float distanceBetweenColumns = 5;

    private Vector3 originalPosition;
    private GameObject scoreArea;
    private List<GameObject> scoreAreaAmount;
    public List<GameObject> ScoreAreaAmount
    {
        get { return scoreAreaAmount;  }
    }

    void Start()
    {
        scoreArea = gameDatabase.scoreArea;
        scoreAreaAmount = new List<GameObject>();
        originalPosition = scoreArea.transform.position;

        SetStartPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (ReloadScoreAreaTrigger())
        {
            RemoveFirstScoreArea();
            InstantiateScoreArea(scoreArea);
        }
    }

    private void SetStartPosition()
    {
        foreach(GameObject scoreAmount in scoreAreaAmount)
        {
            Destroy(scoreAmount.gameObject);
        }

        scoreAreaAmount.Clear();

        scoreArea.transform.position = originalPosition;

        for (int i = 0; i < 5; i++)
        {
            InstantiateScoreArea(scoreArea);
        }
    }

    private void InstantiateScoreArea(GameObject go)
    {
        GameObject newInstance = Instantiate(go);

        if (scoreAreaAmount.Count > 0)
        {
            newInstance.transform.position = new Vector3(scoreAreaAmount[scoreAreaAmount.Count - 1].transform.position.x + distanceBetweenColumns, 
                newInstance.transform.position.y, 
                0);
        }
        float scoreAreaVerticalPosition = Random.Range(-3.0f, 3.0f);
        newInstance.transform.position = new Vector3(newInstance.transform.position.x, scoreAreaVerticalPosition, 0);
        AddScoreAreaAmount(newInstance);
    }

    private void AddScoreAreaAmount(GameObject go)
    {
        scoreAreaAmount.Add(go);
    }

    private void RemoveFirstScoreArea()
    {
        Destroy(scoreAreaAmount[0].gameObject);
        scoreAreaAmount.RemoveAt(0);
    }

    private bool ReloadScoreAreaTrigger()
    {
        return scoreAreaAmount[0].transform.position.x < Camera.main.transform.position.x - 15 ? true : false;
    }

    public void DoRestart()
    {
        SetStartPosition();
    }

}
