using UnityEngine;

[RequireComponent(typeof(PlanetaryCoordinates))]
public class TrailRendererColliderGenerator : MonoBehaviour
{
    public float colliderRadius = 0.25f;

    PlanetaryCoordinates pc;
    Vector3 refPosition;

    void Start()
    {
        refPosition = transform.position;
        pc = GetComponent<PlanetaryCoordinates>();
    }

    void Update()
    {
        var dist = Vector3.Distance(refPosition, transform.position);
        if (dist >= 2 * colliderRadius)
        {
            CreateGameObject();
        }
    }

    void CreateGameObject()
    {
        GameObject go = new GameObject();
        SphereCollider collider = go.AddComponent<SphereCollider>();
        TrailRendererCollider trc = go.AddComponent<TrailRendererCollider>();

        collider.radius = colliderRadius;
        go.transform.position = transform.position;
        trc.ttl = (float) (pc.T_orb / KeplerEquations.SECONDS_PER_DAY / pc.timeScale);
        trc.name = name;
    }
}
