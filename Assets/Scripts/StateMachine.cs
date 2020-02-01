using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class IState
{
    public abstract void Enter();
    public abstract void Exit();
}

public class StateMachine : MonoBehaviour
{
    public static StateMachine Instance;
    public IState currentState = new BeginningAnimation();
    private void Awake()
    {
        if(Instance ==null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.B))
        {
            ChangeState(new TypeName());
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            ChangeState(new ReadyGo());
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            ChangeState(new Game());
        }
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            ChangeState(new Result());
        }
        if (Input.GetKeyDown(KeyCode.Period))
        {
            ChangeState(new BeginningAnimation());
        }*/
    }

    public void ChangeState(IState nextState) // Just call this is enough
    {
        if(currentState!=null)
        {
            currentState.Exit();
        }
        currentState = nextState;
        currentState.Enter();
    }
}

public class BeginningAnimation : IState
{
    public override void Enter()
    {
        Debug.Log("BeginningAnimation");
        //播放開場動畫
    }
    public override void Exit()
    {
        //切換輸入名字畫面
    }
}
public class TypeName : IState
{
    public override void Enter()
    {
        Debug.Log("TypeName");
        //輸入字
    }
    public override void Exit()
    {
        //切換Game場景
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
public class ReadyGo : IState
{
    public override void Enter()
    {
        Debug.Log("ReadyGo");
        //播ReadyGo動畫、切分畫面
    }
    public override void Exit()
    {
        //開始遊戲
    }
}
public class Game : IState
{
    public override void Enter()
    {
        Debug.Log("Game");
        //遊玩
    }
    public override void Exit()
    {
        //遊戲結束，切換結算畫面
    }
}
public class Result : IState
{
    public override void Enter()
    {
        Debug.Log("Result");
        //報分數
    }
    public override void Exit()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}