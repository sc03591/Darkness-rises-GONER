using UnityEngine;
using System.Collections.Generic;

public class DrawOrderManager : MonoBehaviour
{
    public List<string> targetTags = new List<string>(); // List of tags to check

    void Start()
    {
        foreach (string tag in targetTags)
        {
            // Find all GameObjects with the current tag
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject obj in taggedObjects)
            {
                if (obj.GetComponent<DrawOrder>() == null)
                {
                    obj.AddComponent<DrawOrder>();
                }
            }
        }
    }
}
