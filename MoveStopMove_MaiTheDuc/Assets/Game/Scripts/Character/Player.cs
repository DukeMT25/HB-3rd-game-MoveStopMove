using UnityEngine;

public class Player : Character
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] FloatingJoystick _floatingJoystick;
    //[SerializeField] Transform Ray;

    public Vector3 MoveDirection { get; set; }

    public P_Idle IdleState { get; set; }
    public P_Run RunState { get; set; }

    //Atk
    public P_Attack AtkState { get; set; }

    //Ulti
    public P_Ulti UltiState { get; set; }

    //Dead
    public P_Dead DeadState { get; set; }


    public override void Start()
    {
        base.Start();

        AI.onAnyAIDead += AI_onAnyAIDead;
    }

    private void AI_onAnyAIDead(object sender, AI.OnAnyAIDeadArgs e)
    {
        if (targetController.listEnemy.Contains(e._ai))
        {
            targetController.listEnemy.Remove(e._ai);
        }
    }

    public override void OnInit()
    {
        base.OnInit();

        UpdateWeapon();

        IdleState = new P_Idle(this, _anim, Constraint.idleName, this);
        RunState = new P_Run(this, _anim, Constraint.runName, this);
        AtkState = new P_Attack(this, _anim, Constraint.atkName, this);
        DeadState = new P_Dead(this, _anim, Constraint.deadName);

        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
        Moving();
    }

    public void UpdateWeapon()
    {
        weaponIndex = PlayerPrefs.GetInt("SelectedWeapon");
        Debug.Log(weaponIndex);
        ShowWeaponInHand();

        ObjectPool objpool = gameManager.WeaponObjectPool[weaponIndex];
        ReleaseWeapon();
        for (int i = 0; i < 2; i++)
        {
            Weapon weapon = gameManager.Weaponspawner.SpawnWeapon(gameManager.WeaponHolder, objpool);
            _listWeaponatk.Add(weapon);
        }
    }
    private void ReleaseWeapon()
    {
        for (int i = 0; i < _listWeaponatk.Count; i++)
        {
            _listWeaponatk[i].GetComponent<PooledObj>().Release();
        }
        _listWeaponatk.Clear();
    }    
    public override void ShowWeaponInHand()
    {
        HideAllWeapon();
        ListWeaponsInHand[weaponIndex].SetActive(true);
    }

    private void Moving()
    {
        MoveDirection = (Vector3.right * _floatingJoystick.Horizontal + Vector3.forward * _floatingJoystick.Vertical) * moveSpeed * Time.deltaTime;
        transform.position += MoveDirection;
        if (MoveDirection != Vector3.zero)
        {
            RotateTowards(gameObject, MoveDirection);
        }
    }

    private void RotateTowards(GameObject gameObject, Vector3 direction)
    {
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public override void Attack()
    {
        base.Attack();
    }

    protected override void OnDead()
    {
        base.OnDead();

        MoveDirection = Vector3.zero;
        StateMachine.ChangeState(DeadState);

        //UIManager.Instance.SwitchToRevivePanel();
        //GameManager.Instance.GameOver();
    }

    public override void OnHit(float damage)
    {
        base.OnHit(damage);
    }
}
