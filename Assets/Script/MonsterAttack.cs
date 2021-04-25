using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject AttackPrefab; //공격 프리펩
    [SerializeField]
    private Transform spawnPoint;//발사체 생성위치
    [SerializeField]
    private float attackRate;//공격속도
    [SerializeField]
    private float attackRange;//공격범위
    private WeaponState weaponState = WeaponState.SearchTarget;// 무기 상태
    private Transform attackTarget = null;//공격대상
    private PlayerSpawner playerSpawner;
    //private MonsterSpawner monsterSpawner;//게임에 존재하는 적정보 획득용
    private int damage = 1;

    public void Setup(Monster monster, PlayerSpawner playerSpawner)
    {
        this.playerSpawner = playerSpawner;
        
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

            //공격
            SpawnAttack();

            //attackRate 만큼 대기
            yield return new WaitForSeconds(attackRate);

        }


    }

    private void SpawnAttack()
    {
        GameObject clone = Instantiate(AttackPrefab, spawnPoint.position, Quaternion.identity);
        clone.GetComponent<MonsterProjectile>().Setup(attackTarget, damage);
    }
}
