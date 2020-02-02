using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIName : MonoBehaviour
{
    [SerializeField]
    private Text m_text;

    [SerializeField]
    private int m_playerIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_text = GetComponent<Text>();
    }

    void GetName()
    {
        if (GameManager.Instance.PlayerNames.Length <= m_playerIndex)
            return;

        m_text.text = GameManager.Instance.PlayerNames[m_playerIndex];
    }

}
