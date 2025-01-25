using UnityEngine;

public class LeverOn : MonoBehaviour
{
    [SerializeField] GameObject cargoPrefab;
    [SerializeField] Transform pos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "lever")
        {
            GameObject cargo = Instantiate(cargoPrefab);
            cargo.transform.position = pos.position;
        }
    }
}
