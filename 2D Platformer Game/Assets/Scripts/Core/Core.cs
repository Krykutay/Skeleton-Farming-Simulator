using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour
{
    public Movement movement { get; private set; }
    public CollisionSenses collisionSenses { get; private set; }

    void Awake()
    {
        movement = GetComponentInChildren<Movement>();
        collisionSenses = GetComponentInChildren<CollisionSenses>();

        if (!movement || !collisionSenses)
            Debug.LogError("Missing Core component(s)");
    }

    public void LogicUpdate()
    {
        movement.LogicUpdate();
    }

}
