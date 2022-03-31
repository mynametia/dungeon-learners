//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using TMPro;
using System.Collections.Generic;

/// <summary>
/// Controls finding of worlds
/// </summary>
public class FindWorldController : MonoBehaviour
{
    public TMP_InputField searchbar;

    public TMP_Text worldName0;
    public TMP_Text worldName1;
    public TMP_Text worldName2;
    public GameObject disable1;
    public GameObject disable2;
    private string searchQuery;
    
    // Start is called before the first frame update
    async void Start()
    {
        List<string> worldsList = new List<string>();
        
        // Get pre-loaded worlds from Firestore
        var db = FirebaseFirestore.DefaultInstance;
        Query allSubjects = db.Collection("question_bank");
        QuerySnapshot allSubjectsSnapshot = await allSubjects.GetSnapshotAsync();
        foreach (DocumentSnapshot documentSnapshot in allSubjectsSnapshot.Documents)
        {
            string subject = documentSnapshot.Id.ToString();
            Debug.Log("Document data for " + subject);
            worldsList.Add(subject);
        }

        ChangeWorldNameText(worldsList);
    }

    // Update is called once per frame
    void Update()
    {
        searchQuery = searchbar.text;
        if (searchQuery == "")
        {
            disable1.SetActive(true);
            disable2.SetActive(true);
        }
    }

    public void ChangeWorldNameText(List<string> worldNames)
    {
        worldName0.text = worldNames[0];
        worldName1.text = worldNames[1];
        worldName2.text = worldNames[2];
    }

    public void UpdateSearchQuery()
    {
        searchQuery = searchbar.text;
        if (searchQuery == "Computing" || searchQuery == "computing")
        {
            disable1.SetActive(false);
            disable2.SetActive(false);
        }
        Debug.Log(searchQuery);
    }
}