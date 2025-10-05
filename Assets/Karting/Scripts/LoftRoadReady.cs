using UnityEngine;

public class LoftRoadReady : MonoBehaviour
{
    public static bool IsReady { get; private set; } = false;

    // Call this from your loft generator once the mesh and colliders are done.
    public static void SetReady(bool ready)
    {
        IsReady = ready;
    }

    void Start()
    {
        // Optional fallback if you don't modify LoftRoadBehavior directly:
        // Assume road is ready after 2 seconds
        StartCoroutine(AutoReady());
    }

    System.Collections.IEnumerator AutoReady()
    {
        yield return new WaitForSeconds(2f);
        IsReady = true;
    }
}