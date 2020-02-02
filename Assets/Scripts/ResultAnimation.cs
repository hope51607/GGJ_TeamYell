using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ResultAnimation : MonoBehaviour
{
    private int[] points = {30,30};
    public GameObject cassette;
    public float xgap=3;
    public float ygap=2;
    float[] timing = { 0.1f, 1f, 0.5f };

    // Variables used to iterate points
    int lCounter = 0;
    int rCounter = 0;
    Vector3 lTempPos = new Vector3(-14, -4, 0);
    Vector3 rTempPos = new Vector3(2, -4, 0);
    List<GameObject> lPoints = new List<GameObject>();
    List<GameObject> rPoints = new List<GameObject>();

    int threshold;
    bool calculationDone = false;
    private void Awake()
    {
        cassette.transform.position = new Vector3(0, 20, 0);
        //points = GameManager.Instance.points;
        threshold = Mathf.Min(points[0], points[1])*9/10;

        for(int i=0;i<points[0];i++)
        {
            lPoints.Add(Instantiate(cassette));
        }
        for (int i = 0; i < points[1]; i++)
        {
            rPoints.Add(Instantiate(cassette));
        }
        this.StartCoroutine(CalculateLPoints());
        this.StartCoroutine(CalculateRPoints());
    }

    IEnumerator CalculateLPoints()
    {
        yield return new WaitForSeconds(1.0f);
        lCounter = 0;
        while (lCounter< lPoints.Count)
        {
            LPointsUpdate(lCounter);
            if(lCounter < threshold)
            {
                yield return new WaitForSeconds(timing[0]);
            }
            else if( lCounter == threshold )
            {
                yield return new WaitForSeconds(timing[1]);
            }
            else
            {
                yield return new WaitForSeconds(timing[2]);
            }
            lCounter++;
        }
    }
    IEnumerator CalculateRPoints()
    {
        yield return new WaitForSeconds(1.0f);
        rCounter = 0;
        while (rCounter < rPoints.Count)
        {
            RPointsUpdate(rCounter);
            if (rCounter < threshold)
            {
                yield return new WaitForSeconds(timing[0]);
            }
            else if (rCounter == threshold)
            {
                yield return new WaitForSeconds(timing[1]);
            }
            else
            {
                yield return new WaitForSeconds(timing[2]);
            }
            rCounter++;
        }
    }
    void LPointsUpdate(int i)
    {
        Transform thisTransform = lPoints[i].transform;
        thisTransform.position = new Vector3(lTempPos.x, thisTransform.position.y, thisTransform.position.z);

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(thisTransform.DOMove(lTempPos, 0.2f).SetEase(Ease.OutBounce));

        lTempPos = new Vector3(-14 + xgap * ((i+1) / 7), -4 + ygap * ((i+1) % 7), 0);
    }
    void RPointsUpdate(int i)
    {
        Transform thisTransform = rPoints[i].transform;
        thisTransform.position = new Vector3(rTempPos.x, thisTransform.position.y, thisTransform.position.z);

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(thisTransform.DOMove(rTempPos, 0.2f).SetEase(Ease.OutBounce));

        rTempPos = new Vector3(2 + xgap * ((i + 1) / 7), -4 + ygap * ((i + 1) % 7), 0);
    }

    private void Update()
    {
        if(calculationDone == false && rCounter == points[0] && lCounter == points[1] )
        {
            calculationDone = true;
        }
    }

}
