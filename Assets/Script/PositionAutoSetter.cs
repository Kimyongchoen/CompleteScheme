using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PositionAutoSetter : MonoBehaviour
{

	[SerializeField]
	private Vector3 distance = Vector3.up * 20.0f;

	private Transform targetTransform;
	private RectTransform rectTransform;
	private int type = 0;
	private float position;

	public void Setup(Transform target, int type)
	{
		// Slider UI가 쫓아다닐 target 설정
		targetTransform = target;
		// RectTransform 컴포넌트 정보 얻어오기
		rectTransform = GetComponent<RectTransform>();
		//Type 0 Slider 1 VectorText
		this.type = type;
	}
	private void LateUpdate()
	{
		//오브젝트의 위치가 갱신된 이후에 slider UI도 함께 위치를 설정하도록 하기위해
		// LateUpdate()에서 호출한다.

		// 적이 파괴되어 쫓아다닐 대사이 사라지면 slider UI도 삭제
		if (targetTransform == null)
		{
			Destroy(gameObject);
			return;
		}

		//종류에 따라 위치 변경
		if (type == 0)
		{
			position = 0.45f;
		}
		else if (type == 1)
		{
			position = 0.55f;
		}
		else if (type == 2)
		{
			position = -0.2f;
		}

		//오브젝트의 월드 좌표를 기준으로 화면에서의 좌표 값을 구함
		//Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);

		//화면내에서 좌표 + distance만큼 떨어진 위치를 Slider UI의 위치로 설정
		//rectTransform.position = screenPosition + distance;

		rectTransform.position = targetTransform.position + (Vector3.up * position);


	}
}
