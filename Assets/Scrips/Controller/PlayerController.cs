using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LTAUnityBase.Base.DesignPattern;
public class SnakePoint
{
    public Vector3 Position;

    public float deltaDistance;

    public SnakePoint(Vector3 pos)
    {
        Position = pos;
    }
}
public class PlayerController : EntityController
{
    public JoyStickController joyStick;
    public List<PartController> BodyParts;
    //public Transform tail;
    float bodyDistance = 0.6f;
    public int numParts;
    List<SnakePoint> snakePoints;
    FilterTargetController filterTarget;
    //public SelectSkillsController selectSkills;
    public int LevelFireBody { get; set; }
    public int LevelPlantsBody { get; set; }
    public int LevelLightBody { get; set; }
    public int LevelDarkBody { get; set; }
    public float IncreaseAttack { get; set; }
    public float IncreaseAsdp { get; set; }
    public float IncreaseHp { get; set; }

    protected override void Awake()
    {
        base.Awake();
        Observer.Instance.AddObserver(TOPICNAME.ENEMY_DIE, OnEnemyDie);
        //Observer.Instance.AddObserver(TOPICNAME.ENEMY_SPAWNED, OnEnemySpawned);
        BodyParts = new List<PartController>();
        snakePoints = new List<SnakePoint>();
        filterTarget = GetComponent<FilterTargetController>();
        joyStick = CreateButtonManager.Instance.joyStick;
    }
    private void Start()
    {

    }
    private void OnEnable()
    {
        levelController.Level = 1;
        levelController.MaxValue = 50;
        levelController.CurrentValue = 0;

        PartController bodyPart = GetComponent<PartController>();
        BodyParts.Add(bodyPart);
        for (int i = 0; i < numParts; i++)
        {
            PushBody();
        }
        PushBody(BodyTypes.Tail);
        var snakePoint = new SnakePoint(BodyParts[BodyParts.Count - 1].transform.position);
        snakePoints.Add(snakePoint);
    }
    
    private void OnDisable()
    {
        Observer.Instance.RemoveObserver(TOPICNAME.ENEMY_DIE, OnEnemyDie);
        BodyParts.Clear();
        snakePoints.Clear();
    }
    protected override void OnLevelUp(int level)
    {
        base.OnLevelUp(level);
        if (level > 1)
        {
            PushNormalBody();
        }
    }
    protected override void OnDie()
    {
        CreateGameController.Instance.LosePanel();
    }
    void Update()
    {
        if (isPause)
        {
            return;
        }
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");
        //Vector3 direction = new Vector3(horizontal, vertical);
        //Move(direction);
        if (joyStick.Direction != Vector3.zero)
        {
            speed = maxSpeed;
            body.up = joyStick.Direction;
            UpdateMovement(body.up);
        }
        else
        {
            speed = 0;
            UpdateMovement(body.up);
        }
        int num = isClosedShape();
        if (num > 0 && lastShootTime <= Time.time)
        {
            Vector2[] points = new Vector2[num + 1];
            for (int i = 0; i <= num; i++)
            {
                points[i] = BodyParts[i].transform.position;
            }
            Vector2 center = CenterPolygon(points);
            BulletController bullet = Create.Instance.CreateBulletCircle(center);
            bullet.Damage = Damage * Constants.CIRCLE_DAMAGE_MULTIPLIER;
            for (int i = 0; i < points.Length; i++)
            {
                points[i] -= center;
            }
            bullet.GetComponent<PolygonCollider2D>().points = points;
            MeshFilter meshFilter = bullet.GetComponent<MeshFilter>();
            PolygonCollider2D polygon = bullet.GetComponent<PolygonCollider2D>();
            Utils.GenerateMeshPolygon2D(meshFilter, polygon);
            lastShootTime = Time.time + 2f;
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (body.up != Vector3.zero)
            {
                UpdateDirectionOfBody(body.up);
            }
            Shoot();
        }

    }
    void UpdateMovement(Vector3 direction)
    {
        if (BodyParts[0] == null)
        {
            return;
        }
        Vector3 curPos = transform.position;
        Vector3 nextPos = curPos + direction * speed * Time.deltaTime;
        transform.position = nextPos;
        if (direction != Vector3.zero)
        {
            UpdatePath();
            UpdateBodies();
            //UpdateTail();
        }
    }
    void UpdatePath()
    {
        if (BodyParts[0] == null)
        {
            return;
        }
        var curPoint = new SnakePoint(BodyParts[0].transform.position);
        
        if (snakePoints.Count > 0)
        {
            var lastPoint = snakePoints[snakePoints.Count - 1];
            curPoint.deltaDistance = Vector3.Distance(curPoint.Position, lastPoint.Position);
        }
        if (curPoint.deltaDistance != 0)
        {
            snakePoints.Add(curPoint);
        }    }
    void UpdateBodies()
    {
        if (BodyParts.Count <= 1)
        {
            return;
        }
        for (int i = 1; i < BodyParts.Count; ++i)
        {
            float remainDistance = bodyDistance * i;
            for (int j = snakePoints.Count - 1; j > 0; j--)
            {
                if (remainDistance <= snakePoints[j].deltaDistance)
                {
                    float LerpProgress = 0;
                    if (snakePoints[j].deltaDistance > 0)
                    {
                        LerpProgress = remainDistance / snakePoints[j].deltaDistance;
                    }
                    
                    Vector3 pos = Vector3.Lerp(snakePoints[j].Position, snakePoints[j - 1].Position, LerpProgress);
                    Vector3 dir = pos - BodyParts[i].transform.position;
                    if (dir != Vector3.zero)
                    {
                        BodyParts[i].body.up = dir;
                    }
                    BodyParts[i].transform.position = pos;
                    if (i == BodyParts.Count - 1)
                    {
                        snakePoints.RemoveRange(0, j - 1);
                    }
                    break;
                }
                remainDistance -= snakePoints[j].deltaDistance;
            }
        }
    }
    //void UpdateTail()
    //{
    //    float remainDistance = bodyDistance * BodyParts.Count;
    //    for (int j = snakePoints.Count - 1; j > 0; j--)
    //    {
    //        if (remainDistance <= snakePoints[j].deltaDistance)
    //        {
    //            float LerpProgress = 0;
    //            if (snakePoints[j].deltaDistance > 0)
    //            {
    //                LerpProgress = remainDistance / snakePoints[j].deltaDistance;
    //            }
    //            Vector3 pos = Vector3.Lerp(snakePoints[j].Position, snakePoints[j - 1].Position, LerpProgress);
    //            Vector3 dir = pos - tail.position;
    //            if (dir != Vector3.zero)
    //            {
    //                tail.up = pos - tail.position;
    //            }
    //            tail.position = pos;
    //            snakePoints.RemoveRange(0, j - 1);
    //            break;
    //        }
    //        remainDistance -= snakePoints[j].deltaDistance;
    //    }
    //}
    void UpdateDirectionOfBody(Vector3 direction)
    {
        for (int i = 0; i<BodyParts.Count; i++)
        {
            BodyParts[i].RotateGun(direction);
}
    }
    void ShootCircle(Vector2 pos)
    {
        BulletController bullet = Create.Instance.CreateBulletCircle(pos);
        bullet.Damage = Damage * Constants.CIRCLE_DAMAGE_MULTIPLIER;
    }
    void Shoot()
    {
        for (int i = 0; i < BodyParts.Count; i++)
        {
            BodyParts[i].Shoot();
        }
    }
    void CreateGunTurret(Vector3 pos)
    {
        PartController bodyPart = Create.Instance.CreateGunTurret(pos);
        bodyPart.Damage = Damage;
        bodyPart.Asdp = Asdp;
    }
    public void PushBody(BodyTypes bodyTypes = BodyTypes.Normal)
    {
        int indexFinal = BodyParts.Count - 1;
        PartController bodyFinal = BodyParts[indexFinal];
        Vector3 pos = bodyFinal.transform.position - bodyFinal.body.up * bodyDistance;
        PartController bodyPart = null;
        switch (bodyTypes)
        {
            case BodyTypes.Normal:
                bodyPart = Create.Instance.CreateBodyPartNormal(pos, bodyFinal.transform.rotation);
                break;
            case BodyTypes.Light:
                bodyPart = Create.Instance.CreateBodyPartLight(pos, bodyFinal.transform.rotation);
                break;
            case BodyTypes.Fire:
                bodyPart = Create.Instance.CreateBodyPartFire(pos, bodyFinal.transform.rotation);
                break;
            case BodyTypes.Ice:
                bodyPart = Create.Instance.CreateBodyPartIce(pos, bodyFinal.transform.rotation);
                break;
            case BodyTypes.Plants:
                bodyPart = Create.Instance.CreateBodyPartPlants(pos, bodyFinal.transform.rotation);
                break;
            case BodyTypes.Dark:
                bodyPart = Create.Instance.CreateBodyPartDark(pos, bodyFinal.transform.rotation);
                break;
            case BodyTypes.Tail:
                bodyPart = Create.Instance.CreateTailPart(pos, bodyFinal.transform.rotation);
                break;
            default:
                break;
        }
        //bodyPart.setBodyTypes(bodyTypes);
        bodyPart.Damage = Damage;
        bodyPart.Asdp = Asdp;
        BodyParts.Add(bodyPart);
        //BodyParts.Insert(randomIndex, bodyPart);
    }
    void OnEnemyDie(object data)
    {
        EnemyController enemy = (EnemyController)data;
        //Transform transEnemy = enemy.transform;
        //transEnemies.Remove(transEnemy);
        levelController.CurrentValue += enemy.expRewards;
    }

    protected override EntityInfo GetEntityInfo(int level)
    {
        return DataManager.Instance.playerVO.GetEntityInfo(level);
    }
    int isClosedShape()
    {
        if (BodyParts.Count < 4)
        {
            return 0;
        }
        SpriteRenderer spriteRenderer = BodyParts[0].transform.Find("Body").GetComponent<SpriteRenderer>();
        float distance = spriteRenderer.bounds.size.y;
        Vector3 A = BodyParts[0].transform.position + BodyParts[0].body.up.normalized * distance / 2;
        Vector3 B = BodyParts[0].transform.position;
        for (int i = 3; i < BodyParts.Count; i++)
        {
            Vector3 C = BodyParts[i].transform.position;
            Vector3 D = BodyParts[i - 1].transform.position;
            if (IsIntersection(A, B, C, D))
            {
                return i;
            }
        }
        return 0;
    }
    bool IsIntersection(Vector3 A, Vector3 B, Vector3 C, Vector3 D)
    {
        float denominator = ((D.y - C.y) * (B.x - A.x)) - ((D.x - C.x) * (B.y - A.y));
        float numerator1 = ((D.x - C.x) * (A.y - C.y)) - ((D.y - C.y) * (A.x - C.x));
        float numerator2 = ((B.x - A.x) * (A.y - C.y)) - ((B.y - A.y) * (A.x - C.x));

        if (denominator == 0)
        {
            return false;
        }

        float r = numerator1 / denominator;
        float s = numerator2 / denominator;

        return (r >= 0 && r <= 1) && (s >= 0 && s <= 1);
    }
    Vector3 GetIntersection(Vector3 A, Vector3 B, Vector3 C, Vector3 D)
    {
        float denominator = ((D.y - C.y) * (B.x - A.x)) - ((D.x - C.x) * (B.y - A.y));
        float numerator1 = ((D.x - C.x) * (A.y - C.y)) - ((D.y - C.y) * (A.x - C.x));
        float numerator2 = ((B.x - A.x) * (A.y - C.y)) - ((B.y - A.y) * (A.x - C.x));
        float r = numerator1 / denominator;
        float s = numerator2 / denominator;
        float x_intersection = A.x + r * (B.x - A.x);
        float y_intersection = A.y + r * (B.y - A.y);
        return new Vector3(x_intersection, y_intersection, 0);
    }
    Vector2 CenterPolygon(Vector2[] _points)
    {
        Vector2 center = Vector2.zero;
        Vector2 sum = Vector2.zero;
        for (int i = 0; i < _points.Length; i++)
        {
            sum += _points[i];
        }
        center = sum / _points.Length;
        return center;
    }
    float FurthestPolygon(Vector2[] _points)
    {
        Vector2 center = CenterPolygon(_points);
        float distance = Vector2.Distance(center, _points[0]);

        for (int i = 0; i < _points.Length; i++)
        {
            float dis = Vector2.Distance(center, _points[i]);
            if (distance < dis)
            {
                distance = dis;
            }
        }
        return distance;
    }
    //public void SelectSkills()
    //{
    //    selectSkills.gameObject.SetActive(true);
    //}
    #region Upgrade
    public void UpgradeFireBody()
    {
        LevelFireBody++;
        foreach (PartController part in BodyParts)
        {
            if (part.bodyTypes == BodyTypes.Fire)
            {
                part.Upgrade(LevelFireBody);
            }
        }
    }
    public void UpgradePlantsBody()
    {
        LevelPlantsBody++;
        foreach (PartController part in BodyParts)
        {
            if (part.bodyTypes == BodyTypes.Plants)
            {
                part.Upgrade(LevelPlantsBody);
            }
        }
    }
    public void UpgradeLightBody()
    {
        LevelLightBody++;
        foreach (PartController part in BodyParts)
        {
            if (part.bodyTypes == BodyTypes.Light)
            {
                part.Upgrade(LevelLightBody);
            }
        }
    }
    public void UpgradeDarkBody()
    {
        LevelDarkBody++;
        foreach (PartController part in BodyParts)
        {
            if (part.bodyTypes == BodyTypes.Dark)
            {
                part.Upgrade(LevelDarkBody);
            }
        }
    }
    public void PushNormalBody()
    {
        int randomIndex = UnityEngine.Random.Range(1, BodyParts.Count - 1);
        PartController bodyPartIndex = BodyParts[randomIndex];
        PartController bodyPart = Create.Instance.CreateBodyPartNormal(bodyPartIndex.transform.position, bodyPartIndex.transform.rotation);
        bodyPart.Damage = Damage;
        bodyPart.Asdp = Asdp;
        BodyParts.Insert(randomIndex, bodyPart);
    }
    public void PushFireBody()
    {
        int randomIndex = UnityEngine.Random.Range(1, BodyParts.Count - 1);
        PartController bodyPartIndex = BodyParts[randomIndex];
        PartController bodyPart = Create.Instance.CreateBodyPartFire(bodyPartIndex.transform.position, bodyPartIndex.transform.rotation);
        bodyPart.Damage = Damage;
        bodyPart.Asdp = Asdp;
        bodyPart.Upgrade(LevelFireBody);
        BodyParts.Insert(randomIndex, bodyPart);
    }
    public void PushPlantsBody()
    {
        int randomIndex = UnityEngine.Random.Range(1, BodyParts.Count - 1);
        PartController bodyPartIndex = BodyParts[randomIndex];
        PartController bodyPart = Create.Instance.CreateBodyPartPlants(bodyPartIndex.transform.position, bodyPartIndex.transform.rotation);
        bodyPart.Damage = Damage;
        bodyPart.Asdp = Asdp;
        bodyPart.Upgrade(LevelPlantsBody);
        BodyParts.Insert(randomIndex, bodyPart);
    }
    public void PushLightBody()
    {
        int randomIndex = UnityEngine.Random.Range(1, BodyParts.Count - 1);
        PartController bodyPartIndex = BodyParts[randomIndex];
        PartController bodyPart = Create.Instance.CreateBodyPartLight(bodyPartIndex.transform.position, bodyPartIndex.transform.rotation);
        bodyPart.Damage = Damage;
        bodyPart.Asdp = Asdp;
        bodyPart.Upgrade(LevelLightBody);
        BodyParts.Insert(randomIndex, bodyPart);
    }
    public void PushDarkBody()
    {
        int randomIndex = UnityEngine.Random.Range(1, BodyParts.Count - 1);
        PartController bodyPartIndex = BodyParts[randomIndex];
        PartController bodyPart = Create.Instance.CreateBodyPartDark(bodyPartIndex.transform.position, bodyPartIndex.transform.rotation);
        bodyPart.Damage = Damage;
        bodyPart.Asdp = Asdp;
        bodyPart.Upgrade(LevelDarkBody);
        BodyParts.Insert(randomIndex, bodyPart);
    }
    #endregion
}

public class CreatePlayer : SingletonMonoBehaviour<PlayerController>
{
}