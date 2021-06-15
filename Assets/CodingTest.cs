using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodingTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] array = { 1, 5, 2, 6, 3, 7, 4 };
        int[,] commands = { { 2, 5, 3 }, { 4, 4, 1 }, { 1, 7, 3 } };
        Debug.Log(solution(array, commands)) ;
    }

    public int[] solution(int[] array, int[,] commands)
    {
        int[] answer = new int[] { };

        answer = new int[commands.GetLongLength(0)];

        for (int n = 0; n < commands.GetLongLength(0); n++)
        {
            int start = commands[n, 0];
            int len = commands[n, 1];
            int pos = commands[n, 2];

            int[] t = new int[len - start + 1];
            for (int j = 0; j < t.Length; j++)
            {
                t[j] = array[j + start - 1];
            }
            Ord(ref t);

            answer[n] = t[pos - 1];
        }

        return answer;
    }

    public void Ord(ref int[] arr)
    {
        int i = 0, j = 0, temp = 0;

        for (; i < arr.Length - 1; i++)
        {
            for (j = i + 1; j < arr.Length; j++)
            {
                if (arr[i] > arr[j])
                {
                    temp = arr[i]; // 큰 값을 잠시 임시변수에 저장
                    arr[i] = arr[j]; // 작은 값을 앞으로 옮김
                    arr[j] = temp; // 임시변수에 넣어둔 것을 뒤로 옮김
                }
            }
        }
    }
}
