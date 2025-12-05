using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaymentHandler : MonoBehaviour
{
    [SerializeField] private WaitingLineHandler waitingLineHandler;
    private int currentNPCProductIndex = 0;
    private float currentMoney = 0;

    void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            // Get the first NPC in the waiting line
            PathHandler currentPayer = waitingLineHandler.PeekFirstInLine();
            if (currentPayer == null) return;

            // Check if the list if empty, if less than or equal to 0, terminate the process
            if (waitingLineHandler.getPayers().Count <= 0) {
                Debug.LogWarning("Line Is Empty, no customers to pay! :>");
                return;
            }

            Product payerSelectedProducts = currentPayer.getProducts()[currentNPCProductIndex];
            currentMoney += payerSelectedProducts.price;
            Debug.Log($"Paid: {payerSelectedProducts.productName} with price of {payerSelectedProducts.price} :> Updated Current Money to: {currentMoney}");
            
            currentNPCProductIndex++;

            if (currentNPCProductIndex >= currentPayer.getBuyIteration()) {
                currentNPCProductIndex = 0;

                // Remove from waiting line & start exit
                waitingLineHandler.RemoveFromLine(currentPayer);
                StartCoroutine(currentPayer.ExitFromEntrance());
            }
        }
    }
}
