//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FindWorldController : MonoBehaviour
{
    public TMP_InputField searchbar;

    private string searchQuery;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSearchQuery()
    {
        searchQuery = searchbar.text;
        Debug.Log(searchQuery);
    }
}
