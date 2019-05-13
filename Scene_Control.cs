using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Scene_Control : MonoBehaviour {

    public List<Painting> picasso = new List<Painting>();
    public Text countText;
    private int count;
    public static int sceneNum = 2;
    private int ID;






    void Start () {
        ID = SceneManager.GetActiveScene().buildIndex;
        count = 0;
        foreach(GameObject pint in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if(pint.GetComponent<Painting>() != null)
            {
                picasso.Add(pint.GetComponent<Painting>());
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        countText.text = "Count: " + count;
        Max();
        if(count == picasso.Count)
        {
            if(ID == sceneNum)
            {
                Debug.Log("You beat the game!");
            }
            else
            {
                SceneManager.LoadScene(ID + 1);
            }
           
        }
      
        
    }

    private void Max()
    {
        int filler = 0;
        for(int i = 0; i < picasso.Count; i++)
        {
            if(picasso[i].IsFound)
            {
                filler++;
            }
        }
        count = filler;
    }
		
}
