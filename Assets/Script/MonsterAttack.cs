using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject AttackPrefab; //공격 프리펩
    [SerializeField]
    private Transform spawnPoint;//발사체 생성위치

    private float attackSpeed;//공격속도
    private float attackRange;//공격범위
    private WeaponState weaponState = WeaponState.SearchTarget;// 무기 상태
    private Transform attackTarget = null;//공격대상
    private PlayerSpawner playerSpawner;
    private Monster monster;

    private MonsterStats.Stats monsterStats;//몬스터 스텟정보
    private PlayerStats.Stats playerStats; //플레이어 스텟 정보
    
    //private MonsterSpawner monsterSpawner;//게임에 존재하는 적정보 획득용

    public void Setup(Monster monster, PlayerSpawner playerSpawner)
    {
        this.playerSpawner = playerSpawner;
        this.monster = monster;
        this.monsterStats = monster.monsterSpawner.startMonsterSpawners[0].monsterStats.stats[0];
        this.playerStats = playerSpawner.playerStats.stats;
        this.attackSpeed = monster.monsterSpawner.startMonsterSpawners[0].monsterStats.stats[0].attackSpeed;//공격속도
        this.attackRange = monster.monsterSpawner.startMonsterSpawners[0].monsterStats.stats[0].attackRange;//공격범위

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

    private IEnumerator SearchTarget()
    {
        while (true)
        {
            //현재 타워에 가장 가까이 있는 공격 대상(적) 탐색
            attackTarget = FindClosestAttackTarget();

            if (attackTarget != null)
            {
                ChangeState(WeaponState.AttackToTarget);
            }
            yield return null;
        }
    }
    private Transform FindClosestAttackTarget()
    {
        //제일 가까이 있는 적을 찾기 위해 최초 거리를 최대한 크게 설정
        float closestDistSqr = Mathf.Infinity;
        
        if (playerSpawner.player!=null) // Player 가 생성되면 
        {
            float distance = Vector3.Distance(playerSpawner.player.transform.position, transform.position);
            //현재 검사중인 적과의 거리가 공격범위 내에 있고, 현재까지 검사한 적보다 거리가 가까우면
            if (distance <= attackRange && distance <= closestDistSqr)
            {
                closestDistSqr = distance;
                attackTarget = playerSpawner.player.transform;
            }
            return attackTarget;
        }
        else
        {
            return null;
        }
    }

    private IEnumerator AttackToTarget()
    {
        while (true)
        {
            //1.target이 있는지 검사(다른 발사체에 의해 제거, Goal 지점까지 이동해 삭제 등)
            if (attackTarget == null)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            //2. target이 공격 범위 안에 있는지 검사 (공격 범위를 벗어나면 새로운 적 탐색)
            float distance = Vector3.Distance(attackTarget.position, transform.position);

            if (distance > attackRange)
            {
                attackTarget = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            //3. Player와 Monster HP가 0이 아닌지 확인
            if (playerSpawner.player.CurrentHP > 0 && monster.CurrentHP > 0)
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

        int demage = Random.Range(monsterStats.attackDamageMin, monsterStats.attackDamageMax + 1); //몬스터 데미지
        bool criticalFlag = false;

        if (Random.Range(1f, 100f) < monsterStats.criticalChance) // 몬스터 크리티컬 확율 (0-100)
        {
            demage = (int)((float)demage * monsterStats.criticalDamagema);//몬스터 데미지 * 크리티컬 데미지(배)
            criticalFlag = true;
        }

        demage -= playerStats.defense;//방어력 제외하고 데미지

        if (demage <= 0) // 만약 방어력이 더 높다면 데미지는 1
            demage = 1;


        //회피 성공
        if (Random.Range(1f, 100f) > monsterStats.hit - playerStats.evasion) //몬스터의 명중률 - 플레이어의 회피률
        {
            demage = 0;
        }

        clone.GetComponent<MonsterProjectile>().Setup(attackTarget.transform, demage, criticalFlag);

        if (monsterStats.vampire > 0 && demage > 0)//체력 흡혈 %
        {
            monster.vampire((int)((float)demage * monsterStats.vampire));
        }
    }
}
