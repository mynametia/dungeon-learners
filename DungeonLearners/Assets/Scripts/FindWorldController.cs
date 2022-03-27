//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using TMPro;

public class FindWorldController : MonoBehaviour
{
    public TMP_InputField searchbar;

    private string searchQuery;
    
    // Start is called before the first frame update
    async void Start()
    {
        // Get pre-loaded worlds from Firestore
        var db = FirebaseFirestore.DefaultInstance;
        Query allSubjects = db.Collection("question_bank");
        QuerySnapshot allSubjectsSnapshot = await allSubjects.GetSnapshotAsync();
        foreach (DocumentSnapshot documentSnapshot in allSubjectsSnapshot.Documents)
        {
            string subject = documentSnapshot.Id.ToString();
            Debug.Log("Document data for " + subject);
        }
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
