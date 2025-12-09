using System.Collections.Generic;
using UnityEngine;

public class StallList : MonoBehaviour
{
    //[SerializeField] private List<string> Products;
    [Header("List of all possible products")]
    [SerializeField] private List<Product> ProductList;
    [Header("Total batch for each product")]
    [SerializeField] private int TotalBatch = 0;
    // Parents of each levels
    [SerializeField] private List<Product> A1;
    [SerializeField] private List<Product> A2;
    [SerializeField] private List<Product> A3;
    [SerializeField] private List<Product> B1;
    [SerializeField] private List<Product> B2;
    [SerializeField] private List<Product> B3;

    public List<Product> getProductList() {
        return ProductList;
    }

    public Product getRandomItem() {
        if (ProductList == null || ProductList.Count == 0) {
            Debug.LogWarning("[StallList] Products is null or empty");
            return null;
        }
        // include the last element by using Products.Count (exclusive upper bound)
        return ProductList[Random.Range(0, ProductList.Count)];
    }
}
