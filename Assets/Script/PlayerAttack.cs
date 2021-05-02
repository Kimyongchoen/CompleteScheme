using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject AttackPrefab; //공격 프리펩
    [SerializeField]
    private Transform spawnPoint;//발사체 생성위치
    
    private float attackSpeed;//공격속도
    private float attackRange;//공격범위
    
    private WeaponState weaponState = WeaponState.SearchTarget;// 무기 상태
    private Monster attackTarget = null;//공격대상
    private MonsterSpawner monsterSpawner;//게임에 존재하는 적정보 획득용
    private Player player;//공격할때 플레이어 정지

    private PlayerStats.Stats playerStats; //플레이어 스텟 정보
    private bool GoldExperience = false; //골드 경험치 획득여부
    public void Setup(Player player , MonsterSpawner monsterSpawner)
    {
        this.monsterSpawner = monsterSpawner;
        this.player = player;
        this.playerStats = player.playerSpawner.playerStats;
        this.attackSpeed = playerStats.attackSpeed;//공격속도
        this.attackRange = playerStats.attackRange;//공격범위

        //최초 상태를 waponState.SearchTarget으로 설정
        ChangeState(WeaponState.SearchTarget);
    }
    
    public void ChangeState(WeaponState newState)
    {
        //이전에 재생중이던 상태 종료
        StopCoroutine(weaponState.ToString());
        //상태변경
        weaponState = newState;
        //새로운 상태 재생
        StartCoroutine(weaponState.ToString());
    }

    public void Update()
    {

    }


    private IEnumerator SearchTarget()
    {
        while (true)
        {
            //현재 타워에 가장 가까이 있는 공격 대상(적) 탐색
            attackTarget = FindClosestAttackTarget();

            //플레이어 체력이 없으면 정지
            if (player.CurrentHP <= 0)
            {
                this.player.MoveStop();//이동 정지
                break;
            }

            if (attackTarget != null)
            {
                ChangeState(WeaponState.AttackToTarget);
            }
            yield return null;
        }
    }
    private Monster FindClosestAttackTarget()
    {
        //제일 가까이 있는 적을 찾기 위해 최초 거리를 최대한 크게 설정
        float closestDistSqr = Mathf.Infinity;
        
        for (int i = 0; i < monsterSpawner.MonsterList.Count; ++i)
        {
            float distance = Vector3.Distance(monsterSpawner.MonsterList[i].transform.position, transform.position);
            //현재 검사중인 적과의 거리가 공격범위 내에 있고, 현재까지 검사한 적보다 거리가 가까우면
            if (distance <= attackRange && distance <= closestDistSqr)
            {
                closestDistSqr = distance;
                attackTarget = monsterSpawner.MonsterList[i];
            }
        }
        return attackTarget;

    }

    private IEnumerator AttackToTarget()
    {
        while(true){
            if (attackTarget.CurrentHP <= 0)
            {
                //몬스터 hp가 0이면 골드 경험치 획득
                if (GoldExperience == false)//중복 획득 제어
                {
                    player.GoldExperience(attackTarget.gold, attackTarget.experience);
                    GoldExperience = true;
                }
            }
            
            //플레이어 체력이 없으면 정지
            if (player.CurrentHP <= 0)
            {
                this.player.MoveStop();//이동 정지
                break;
            }

            //1.target이 있는지 검사
            if (attackTarget == null )
            {
                ChangeState(WeaponState.SearchTarget);
                this.player.MoveStart();//다시 이동
                GoldExperience = false;
                break;
            }
            
            //2. target이 공격 범위 안에 있는지 검사 (공격 범위를 벗어나면 새로운 적 탐색)
            float distance = Vector3.Distance(attackTarget.transform.position, transform.position);

            if (distance > attackRange)
            {
                attackTarget = null;
                ChangeState(WeaponState.SearchTarget);
                this.player.MoveStart();//다시 이동
                GoldExperience = false;
                break;
            }

            this.player.MoveStop();//이동 정지

            //3. Player와 Monster HP가 0이 아닌지 확인
            if (attackTarget.CurrentHP > 0 && player.CurrentHP > 0)
            {
                //공격
                SpawnAttack();
            }
            //attackRate 만큼 대기
            yield return new WaitForSeconds(attackSpeed);
        }
    }

    private void SpawnAttack()
    {
        GameObject clone = Instantiate(AttackPrefab, spawnPoint.position, Quaternion.identity);

        int demage = Random.Range(playerStats.attackDamageMin, playerStats.attackDamageMax + 1); //플레이어의 데미지
        bool criticalFlag = false;

        if (Random.Range(1f, 100f) < playerStats.criticalChance) // 크리티컬 확율 (0-100)
        {
            demage = (int)((float)demage * playerStats.criticalDamagema);//데미지 * 크리티컬 데미지(배)
            criticalFlag = true;
        }

        demage -= attackTarget.monsterStats.defense;//방어력 제외하고 데미지

        if (demage <= 0) // 만약 방어력이 더 높다면 데미지는 1
            demage = 1;

        
        //회피 성공
        if (Random.Range(1f, 100f) > playerStats.hit - attackTarget.monsterStats.evasion) //플레이어의 명중률 - 몬스터의 회피률
        {
            demage = 0;
        }
        
        clone.GetComponent<PlayerProjectile>().Setup(attackTarget.transform, demage, criticalFlag);
        
        if (playerStats.vampire > 0 && demage > 0)//체력 흡혈 %
        {
            player.vampire((int)((float)demage * playerStats.vampire));
        }
    }
}
