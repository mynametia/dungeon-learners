
using UnityEngine;
using TMPro;

/// <summary>
/// Control ranking of data
/// </summary>
public class RankDataController : MonoBehaviour
{
    public TextMeshProUGUI rankingNumber;

    public void setRanking(int rank)
    {
        rankingNumber.text = rank.ToString();
    }
}
