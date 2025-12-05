using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private PaymentHandler PH;
    [SerializeField] private PathList Entrance;
    [SerializeField] private PathList StallStations;
    [SerializeField] private PathList Exit;
    [SerializeField] private Transform PaymentArea;
    [SerializeField] private WaitingLineHandler waitingLineHandler;

    [SerializeField] private float PositioningOffset = 10;
    [SerializeField] private float speed = 10;
    [SerializeField] private int BuyTimeMax = 10;
    [SerializeField] private int NpcCount = 10;
    [SerializeField] private GameObject NPCModel;
    [SerializeField] private Transform Parent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(spawn());
    }

    IEnumerator spawn() {
        for (int i = 0; i < NpcCount; i++) {
            GameObject NPC = Instantiate(NPCModel, transform.position, Quaternion.identity, Parent);
            NPC.GetComponent<PathHandler>().PH = PH;
            NPC.GetComponent<PathHandler>().Entrance = Entrance;
            NPC.GetComponent<PathHandler>().StallStations = StallStations;
            NPC.GetComponent<PathHandler>().Exit = Exit;
            NPC.GetComponent<PathHandler>().PaymentArea = PaymentArea;
            NPC.GetComponent<PathHandler>().waitingLineHandler = waitingLineHandler;

            NPC.GetComponent<PathHandler>().PositioningOffset = PositioningOffset;
            NPC.GetComponent<PathHandler>().speed = speed;
            NPC.GetComponent<PathHandler>().BuyTimeMax = BuyTimeMax;

            yield return new WaitForSeconds(UnityEngine.Random.Range(1, 5));
        }
    }
}
