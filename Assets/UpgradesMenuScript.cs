using TMPro;
using UnityEngine;

public class UpgradesMenuScript : MonoBehaviour
{
    [SerializeField] private string productName = "#1";
    [SerializeField] private int stockCount = 0;
    [SerializeField] private int levelPrice = 1;
    [SerializeField] private int stockUpgradePrice = 0;

    [SerializeField] private TextMeshProUGUI ProductName;
    [SerializeField] private TextMeshProUGUI StockCount;
    [SerializeField] private TextMeshProUGUI LevelPrice;
    [SerializeField] private TextMeshProUGUI StockUpgradePrice;

    public void setPanelDetails() {
        ProductName.text = productName;
        StockCount.text = stockCount.ToString();
        StockCount.text = $"${levelPrice}";
        StockCount.text = $"${stockUpgradePrice}";
    }

    public void setProductName(string str) {
        productName = str;
    }

    public void setStockCount(int str) {
        stockCount = str;
    }

    public void setLevelPrice(int str) {
        levelPrice = str;
    }

    public void setStockUpgradePrice(int str) {
        stockUpgradePrice = str;
    }
}
