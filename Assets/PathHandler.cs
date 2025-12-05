using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathHandler : MonoBehaviour
{
    [Header("References")]
    public PaymentHandler PH;
    public PathList Entrance;
    public PathList StallStations;
    public PathList Exit;
    public Transform PaymentArea;
    public WaitingLineHandler waitingLineHandler;

    [Header("Movement Settings")]
    public float PositioningOffset = 0.3f;
    public float speed = 5f;

    [Header("Buying Settings")]
    public float BuyTimeMax = 4f;
    [SerializeField] private int BuyIteration = 0;
    [SerializeField] private List<Product> SelectedProducts = new List<Product>();
    [SerializeField] private GameObject selectedStall;
    private Transform currentLinePosition;

    public int getBuyIteration() => BuyIteration;

    private void OnTriggerEnter(Collider other) {
        selectedStall = other.gameObject;
    }

    public List<Product> getProducts() {
        return SelectedProducts;
    }

    void Start() {
        BuyIteration = Random.Range(1, 4);
        StartCoroutine(MoveToEntrance());
    }

    public void MoveToLinePosition(Transform targetPosition) {
        currentLinePosition = targetPosition;
        StartCoroutine(MoveToTarget(currentLinePosition.position));
    }

    private IEnumerator MoveToTarget(Vector3 target, System.Action onComplete = null) {
        while (Vector3.Distance(transform.position, target) > PositioningOffset) {
            transform.position = Vector3.MoveTowards(
                transform.position, 
                target, 
                speed * Time.deltaTime
            );
            yield return null;
        }

        onComplete?.Invoke();
    }

    private IEnumerator MoveToEntrance() {
        foreach (Transform next in Entrance.getPathList()) {
            yield return MoveToTargetCoroutine(next.position);
        }

        StartCoroutine(GoToStationArea());
    }

    private IEnumerator GoToStationArea() {
        for (int i = 0; i < BuyIteration; i++) {
            int stationIndex = Random.Range(1, StallStations.getPathList().Count);
            PathList stationSubList = StallStations.getPathList()[stationIndex].GetComponent<PathList>();
            int subIndex = Random.Range(1, stationSubList.getPathList().Count);

            // Move to stall tp sublist to offset
            yield return MoveToTargetCoroutine(StallStations.getPathList()[stationIndex].position);
            yield return MoveToTargetCoroutine(stationSubList.getPathList()[subIndex].position);

            Transform XOffset = stationSubList.getPathList()[subIndex];
            float x = Random.Range(
                -XOffset.GetComponent<NPCPlacementOffset>().npcXOffset,
                XOffset.GetComponent<NPCPlacementOffset>().npcXOffset
            );

            int lastIndex = XOffset.childCount - 1;
            Vector3 subPos = XOffset.GetChild(lastIndex).position;
            Vector3 finalPos = new Vector3(subPos.x + x, subPos.y, subPos.z);

            yield return MoveToTargetCoroutine(finalPos);

            // Buy products
            if (selectedStall != null) {
                StallList sl = selectedStall.GetComponent<StallList>();
                SelectedProducts.Add(sl.getRandomItem());
                Debug.LogWarning($"[SelectedProducts.Add(sl.getRandomItem())] : {SelectedProducts.Count}");
            }

            float buyTime = Random.Range(BuyTimeMax / 2, BuyTimeMax);
            yield return new WaitForSeconds(buyTime);

            // Backtrack
            yield return MoveToTargetCoroutine(stationSubList.getPathList()[subIndex].position);
            yield return MoveToTargetCoroutine(StallStations.getPathList()[stationIndex].position);
        }
        StartCoroutine(GoToWaitingLine());
    }

    private IEnumerator GoToWaitingLine() {
        // Wait until there is space in the line
        while (!waitingLineHandler.TryAddToLine(this)) {
            yield return new WaitForSeconds(0.5f);
        }
    }

    public IEnumerator ExitFromEntrance() {
        //waitingLineHandler.getPayerList().Remove(gameObject);

        foreach (Transform next in Exit.getPathList()) {
            yield return MoveToTargetCoroutine(next.position);
        }

        Destroy(gameObject);
    }

    private IEnumerator MoveToTargetCoroutine(Vector3 target) {
        while (Vector3.Distance(transform.position, target) > PositioningOffset) {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return null;
        }
    }
}
