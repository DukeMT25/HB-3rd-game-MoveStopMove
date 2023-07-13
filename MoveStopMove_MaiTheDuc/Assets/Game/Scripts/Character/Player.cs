using System;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private int coin;
    [SerializeField] private CameraFollow cam;
    public Vector3 MoveDirection { get; set; }

    public P_Idle IdleState { get; set; }
    public P_Run RunState { get; set; }

    //Atk
    public P_Attack AtkState { get; set; }
    //Ulti
    public P_Ulti UltiState { get; set; }
    //Dead
    public P_Dead DeadState { get; set; }
    //Dance
    public P_Dance DanceState { get; set; }

    public int Coin { get => coin; set => coin = value; }

    private void Awake()
    {
        Coin = PlayerPrefs.GetInt(Constraint.COIN, 10000);
    }

    public override void Start()
    {
        base.Start();
    }

    public override void OnInit()
    {
        base.OnInit();

        Indicator.SetName("You");

        attackTime = 0.3f; 

        UpdateWeapon();
        hp = 1;
        Exp = 0;

        transform.localScale = Vector3.one;
        cam.transform.position = new Vector3(0, 10, -9.6f);

        IdleState = new P_Idle(this, _anim, Constraint.idleName, this);
        RunState = new P_Run(this, _anim, Constraint.runName, this);
        AtkState = new P_Attack(this, _anim, Constraint.atkName, this);
        DeadState = new P_Dead(this, _anim, Constraint.deadName);
        DanceState = new P_Dance(this, _anim, Constraint.danceName);

        StateMachine.Initialize(IdleState);

        AI.onAnyAIDead += AI_onAnyAIDead;
    }

    private void AI_onAnyAIDead(object sender, AI.OnAnyAIDeadArgs e)
    {
        if (targetController.listEnemy.Contains(e._ai))
        {
            targetController.listEnemy.Remove(e._ai);
        }
    }

    protected override void Update()
    {
        if (GameManager.Instance.IsState(GameState.Gameplay))
        {
            base.Update();
            Moving();
        }
    }

    public override void LevelUp()
    {
        base.LevelUp();

        if (Exp == 2)
        {
            cam.Higher(3f);
        }
        else if (Exp == 7)
        {
            cam.Higher(4f);
        }
        else if (Exp == 12)
        {
            cam.Higher(5f);
        }

        if (Exp >= 18)
        {
            levelManager.OnFinish();

            StateMachine.ChangeState(DanceState);
            UIManager.Instance.OpenUI<Win>();
        }
    }

    public void UpdateWeapon()
    {
        //weaponIndex = PlayerPrefs.GetInt("SelectedWeapon");
        weaponIndex = PlayerPrefs.GetInt(Constraint.SELECTED_WEAPON);
        //Debug.Log(weaponIndex);
        ShowWeaponInHand();
    }

    private void Moving()
    {
        MoveDirection = (Vector3.right * FloatingJoystick.Horizontal + Vector3.forward * FloatingJoystick.Vertical) * moveSpeed * Time.deltaTime;

        if (!Constraint.isWall(this.gameObject, LayerMask.GetMask(Constraint.LAYOUT_WALL)) && !IsDead)
        {
            transform.position += MoveDirection;
        }

        if (MoveDirection != Vector3.zero)
        {
            RotateTowards(gameObject, MoveDirection);
        }
    }

    private void RotateTowards(GameObject gameObject, Vector3 direction)
    {
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public override void OnDespawn()
    {
        MoveDirection = Vector3.zero;
        StateMachine.ChangeState(DeadState);

        UIManager.Instance.CloseUI<InGame>();
        UIManager.Instance.OpenUI<Revive>();
    }

    public void SetTransformPosition(Transform transform)
    {
        gameObject.transform.position = transform.position;
    }
}
