
using UnityEngine;
using TMPro;

public class RankDataController : MonoBehaviour
{
    public TextMeshProUGUI rankingNumber;

    public void setRanking(int rank)
    {
        rankingNumber.text = rank.ToString();
    }
}
