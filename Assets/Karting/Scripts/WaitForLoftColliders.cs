using KartGame.KartSystems;
using System.Collections;
using UnityEngine;

public class WaitForLoftColliders : MonoBehaviour
{
    public ArcadeKart kart;
    public GameObject roadParent;
    public float delay = 1.0f;

    private IEnumerator Start()
    {
        if (kart != null)
            kart.enabled = false;

        // Wait for road mesh colliders
        yield return new WaitUntil(() => HasMeshCollider(roadParent));

        // Wait for LoftRoadReady flag if available
        yield return new WaitUntil(() => LoftRoadReady.IsReady);

        // Safety delay
        yield return new WaitForSeconds(delay);

        // Enable the kart and reset position if needed
        if (kart != null)
        {
            kart.GetComponent<Rigidbody>().isKinematic = false;
            kart.enabled = true;
        }
    }

    private bool HasMeshCollider(GameObject parent)
    {
        if (parent == null) return false;
        return parent.GetComponentsInChildren<MeshCollider>().Length > 0;
    }
}