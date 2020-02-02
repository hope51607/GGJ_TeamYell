using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using NDream.AirConsole;
using Newtonsoft.Json.Linq;

public class ResultAnimation : MonoBehaviour
{
    private int[] points = {32,32};
    public GameObject cassette;
    public float xgap=3;
    public float ygap=2;
    float[] timing = { 0.1f, 1f, 0.5f };

    // Variables used to iterate points
    int lCounter = 0;
    int rCounter = 0;
    Vector3 lTempPos = new Vector3(-16, -4, 0);
    Vector3 rTempPos = new Vector3(3.5f, -4, 0);
    List<GameObject> lPoints = new List<GameObject>();
    List<GameObject> rPoints = new List<GameObject>();

    int threshold;
    bool calculationDone = false;
    public Animator anim;
    public Image winPlayerNum;
    public Sprite[] playerNum;
    public Button replayButton;
    private ResultScene rs;

    private void Awake()
    {
        calculationDone = false;
        cassette.transform.position = new Vector3(0, 20, 0);
        points = GameManager.Instance.points;
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
        rs = this.GetComponent<ResultScene>();
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
                if (lPoints.Count > rPoints.Count && lCounter % 4 == 1)
                {
                    AudioManager.Instance.PlaySE("1");
                }
                yield return new WaitForSeconds(timing[0]);
            }
            else if( lCounter == threshold )
            {
                yield return new WaitForSeconds(timing[1]);
            }
            else
            {
                if (lPoints.Count > rPoints.Count)
                {
                    AudioManager.Instance.PlaySE("1");
                }
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
                if (rPoints.Count >= lPoints.Count && rCounter % 4 == 1)
                {
                    AudioManager.Instance.PlaySE("1");
                }
                yield return new WaitForSeconds(timing[0]);
            }
            else if (rCounter == threshold)
            {
                yield return new WaitForSeconds(timing[1]);
            }
            else
            {
                if (rPoints.Count >= lPoints.Count)
                {
                    AudioManager.Instance.PlaySE("1");
                }
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

        lTempPos = new Vector3(-16 + xgap * ((i+1) / 7), -4 + ygap * ((i+1) % 7), 0);
    }
    void RPointsUpdate(int i)
    {
        Transform thisTransform = rPoints[i].transform;
        thisTransform.position = new Vector3(rTempPos.x, thisTransform.position.y, thisTransform.position.z);

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(thisTransform.DOMove(rTempPos, 0.2f).SetEase(Ease.OutBounce));

        rTempPos = new Vector3(3.5f + xgap * ((i + 1) / 7), -4 + ygap * ((i + 1) % 7), 0);
    }
    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }
    private void Update()
    {
        if (calculationDone == false && lCounter == points[0] && rCounter == points[1] )
        {
            Wait(1);
            if (rCounter ==lCounter)
            {
                anim.Play("Draw");
            }
            else
            {
                if(rCounter > lCounter)
                {
                    winPlayerNum.sprite = playerNum[1];
                }
                else
                {
                    winPlayerNum.sprite = playerNum[0];
                }
                anim.Play("PlayerWin");
            }
            calculationDone = true;
            AirConsole.instance.onMessage += OnMessage;
            replayButton.onClick.AddListener(delegate () { rs.Replay(); });
        }
    }

    void OnMessage(int from, JToken data)
    {
        int playerNumber = AirConsole.instance.ConvertDeviceIdToPlayerNumber(from);
        if (playerNumber >= 2) {
            return;
        }

        if (data["blow"] != null) {
            rs.Replay();
        }
    }

    private void OnDestroy()
    {
        AirConsole.instance.onMessage -= OnMessage;
    }

}
