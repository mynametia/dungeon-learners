using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ShareButton : MonoBehaviour
{
    public bool myWorld = false;
    // private String shareMessage;

    // public void ClickShareButton(){
    //     shareMesssage = ;

    //     StartCoroutine(TakeScreenshotAndShare());
    // }

    public void Share()
    {
        Debug.Log("Button press");
        StartCoroutine(TakeScreenshotAndShare());
    }


    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D( Screen.width, Screen.height, TextureFormat.RGB24, false );
        ss.ReadPixels( new Rect( 0, 0, Screen.width, Screen.height ), 0, 0 );
        ss.Apply();

        string filePath = Path.Combine( Application.temporaryCachePath, "shared img.png" );
        File.WriteAllBytes( filePath, ss.EncodeToPNG() );

        // To avoid memory leaks
        Destroy( ss );

        string msg;
        if (myWorld)
        {
            msg = "Hey Guys! Come join me in playing Dungeon Learners. Here's a world I created! ";
        }
        else 
        {
            msg = "Hey Guys! Come join me in playing Dungeon Learners. Here's my rank! ";
        }

        new NativeShare().AddFile( filePath )
            .SetSubject( "Dungeon Learners" ).SetText( msg ).SetUrl( "https://github.com/yasirkula/UnityNativeShare" )
            .SetCallback( ( result, shareTarget ) => Debug.Log( "Share result: " + result + ", selected app: " + shareTarget ) )
            .Share();

        
    }
}
