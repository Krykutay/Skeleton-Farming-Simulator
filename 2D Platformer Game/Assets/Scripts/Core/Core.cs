using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement movement { get; private set; }
    public CollusionSenses collusionSenses { get; private set; }

    void Awake()
    {
        movement = GetComponentInChildren<Movement>();
        collusionSenses = GetComponentInChildren<CollusionSenses>();

        if (!movement || !collusionSenses)
            Debug.LogError("Missing Core component(s)");
    }

    public void LogicUpdate()
    {
        movement.LogicUpdate();
    }

}
