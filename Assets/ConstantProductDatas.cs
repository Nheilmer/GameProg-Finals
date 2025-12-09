using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    [SerializeField] private List<Product> islandFridgeR1;
    [SerializeField] private List<Product> islandFridgeR2;
    [SerializeField] private List<Product> refR1;

    [SerializeField] private List<Product> islandFridgeL1;
    [SerializeField] private List<Product> refL1;
    [SerializeField] private List<Product> refL2;
    [SerializeField] private List<Product> refL3;

    [SerializeField] private List<Product> snack1;
    [SerializeField] private List<Product> snack2;
    
    [SerializeField] private List<Product> breads;
    [SerializeField] private List<Product> cannedFoods;

    [SerializeField] private List<Product> biscuit;
    [SerializeField] private List<Product> hygiene;
    
    [SerializeField] private List<Product> foodDressings;
    [SerializeField] private List<Product> bodyWash;

    [SerializeField] private List<Product> schoolSupplies;
    [SerializeField] private List<Product> tools;

    [SerializeField] private List<Product> electronics1;
    [SerializeField] private List<Product> electronics2;

    private void initStalls() {
        string[] lines = File.ReadAllLines("file.txt");

        // Loop through the list
        foreach (string category in lines) {
            // Set a current list to enter all
            List<Product> currentList;
            switch (category) {
                case "Snack 1": currentList = snack1;
                    break;
                default:
                    break;
            }
        }
    }
}
