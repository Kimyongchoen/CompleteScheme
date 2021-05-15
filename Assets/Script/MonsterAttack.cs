using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject AttackPrefab; //공격 프리펩
    [SerializeField]
    private Transform spawnPoint;//발사체 생성위치

    private WeaponState weaponState = WeaponState.SearchTarget;// 무기 상태
    private Transform attackTarget = null;//공격대상
    private PlayerSpawner playerSpawner;
    private Monster monster;

    private MonsterStats.Stats monsterStats;//몬스터 스텟정보
    private PlayerStats.Stats playerStats; //플레이어 스텟 정보

    public float addedDamage;//추가 데미지
    //private MonsterSpawner monsterSpawner;//게임에 존재하는 적정보 획득용

    public void Setup(Monster monster, PlayerSpawner playerSpawner)
    {
        this.playerSpawner = playerSpawner;
        this.monster = monster;
        this.monsterStats = monster.monsterStats;
        this.playerStats = playerSpawner.playerStats;
    }

    public void StartAttack()
    {
        //최초 상태를 waponState.SearchTarget으로 설정
        ChangeState(WeaponState.SearchTarget);
        BuffToTarget(); //버프
        StartCoroutine("RecoveryToTarget"); //회복
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
            if (distance <= monsterStats.attackRange && distance <= closestDistSqr)
            {
                closestDistSqr = distance;
                attackTarget = playerSpawner.player.transform;
            }
            else
            {
                attackTarget = null;
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
        
        //플레이어가 공격 범위에 들어오면 버프 & 회복 정지
        StopCoroutine("RecoveryToTarget"); //회복 정지

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
            if (distance > monsterStats.attackRange)
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

            //attackSpeed 만큼 대기
            yield return new WaitForSeconds(monsterStats.attackSpeed);
        }
    }

    private void SpawnAttack()
    {
        GameObject clone = Instantiate(AttackPrefab, spawnPoint.position, Quaternion.identity);

        int demage = Random.Range(monsterStats.attackDamageMin + (int)addedDamage, monsterStats.attackDamageMax + (int)addedDamage + 1); //몬스터 데미지 + addedDamage 버프 데미지
        bool criticalFlag = false;
        int defense = playerStats.defense;
        float evasion = playerStats.evasion;


        if (Random.Range(1f, 100f) < monsterStats.criticalChance) // 몬스터 크리티컬 확율 (0-100)
        {
            demage = (int)((float)demage * monsterStats.criticalDamagema);//몬스터 데미지 * 크리티컬 데미지(배)
            criticalFlag = true;
        }

        if (playerStats.Armor >= 0)//갑옷 강화 따른 방어력 UP 갑옷이 없으면 -1
        {
            defense += playerSpawner.player.ItemStats.Armor[playerStats.Armor];
        }

        //버프에 의한 방어력 UP
        defense += playerSpawner.player.ItemStats.DefenseUp;

        demage -= defense;//방어력 제외하고 데미지

        if (demage <= 0) // 만약 방어력이 더 높다면 데미지는 1
            demage = 1;

        if (playerStats.Boots >= 0)//부츠 강화 따른 회피력 UP 부츠가 없으면 -1
        {
            evasion += playerSpawner.player.ItemStats.Boots[playerStats.Boots];
        }

        //회피 성공
        if (Random.Range(1f, 100f) > monsterStats.hit - evasion) //몬스터의 명중률 - 플레이어의 회피률
        {
            demage = 0;
        }

        clone.GetComponent<MonsterProjectile>().Setup(attackTarget.transform, demage, criticalFlag, (int)addedDamage);

        if (monsterStats.vampire > 0 && demage > 0)//체력 흡혈 %
        {
            monster.vampire((int)((float)demage * monsterStats.vampire));
        }
    }

    public void BuffToTarget()//float buff;//버프 공격력 추가 %
    {
        if (monsterStats.buff > 0) //버프 가능 몬스터 일경우
        {
            //현재 맵에 배치된 모든 monster 태그를 가진 오브젝트 탐색
            GameObject[] monster = GameObject.FindGameObjectsWithTag("monster");

            for (int i=0;i<monster.Length;++i)
            {
                MonsterAttack attack = monster[i].GetComponent<MonsterAttack>();

                //이미 버프를 받고 있고, 현재 버프데미지 보다 높으면 패스
                if(attack.addedDamage > monsterStats.attackDamageMax * monsterStats.buff)
                {
                    continue;
                }

                if (Vector3.Distance(attack.transform.position,transform.position) <= 0.9f)//버프 범위 고정 0.9
                {
                    attack.addedDamage = attack.monsterStats.attackDamageMax * monsterStats.buff;
                }
            }
        }
    }


    private IEnumerator RecoveryToTarget()//float recovery;//힐 추가 초당 +hp
    {
        while (true)
        {
            if (monsterStats.recovery > 0) //버프 가능 몬스터 일경우
            {
                //현재 맵에 배치된 모든 monster 태그를 가진 오브젝트 탐색
                GameObject[] monster = GameObject.FindGameObjectsWithTag("monster");

                for (int i = 0; i < monster.Length; ++i)
                {
                    MonsterAttack attack = monster[i].GetComponent<MonsterAttack>();

                    //HP가 가득 차있으면 pass
                    if (attack.monster.MaxHP == attack.monster.CurrentHP)
                    {
                        continue;
                    }

                    if (Vector3.Distance(attack.transform.position, transform.position) <= 0.9f)//버프 범위 고정 0.9
                    {
                        if (attack.monster.CurrentHP > 0)
                        {
                            attack.monster.currentHP += monsterStats.recovery;

                            attack.monster.RecoveryText((int)monsterStats.recovery);
                            if (attack.monster.currentHP > attack.monster.MaxHP)
                                attack.monster.currentHP = attack.monster.MaxHP;
                        }
                    }
                }
            }


            //1초 만큼 대기
            yield return new WaitForSeconds(1f);
        }
    }

}
