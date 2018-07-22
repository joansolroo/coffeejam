using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTask : ScriptableObject {

    [SerializeField] string taskName;
    [SerializeField] string code;

    public enum TaskType
    {
        prototype, debug, deliverable, deadline
    }
    [SerializeField] TaskType taskType;
}
