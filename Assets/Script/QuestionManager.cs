using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    private string StageName;
    public void setStageName(string StageName)
    {
        this.StageName = StageName;
    }
    public string getStageName()
    {
        return StageName;
    }
}
