using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameScript : MonoBehaviour
{

    public double sectionWidth;
    public int maximumSectionsLoaded;

    private List<GameObject> sections = new List<GameObject>();
    private List<GameObject> sectionsLoaded = new List<GameObject>();

    private void Awake()
    {
        this.LoadSections();
    }

    private void LoadSections()
    {
        int i = 1;
        while (true)
        {
            if (!File.Exists("Assets/Resources/Prefabs/Sections/" + i + ".prefab"))
            {
                break;
            }

            GameObject section = (GameObject)Resources.Load("Prefabs/Sections/" + i, typeof(GameObject));
            this.sections.Add(section);
            i += 1;
        }
    }

    public void AddSection(int position)
    {
        Debug.Log("Holaaaa");
        int r = Random.Range(0, this.sections.Count);
        GameObject go = this.sections[r];

        float x = (float)(this.sectionWidth * position);
       
        go.transform.position = new Vector2(x, 0);
        GameObject goInstantiated = Instantiate(go, go.transform.position, Quaternion.identity);

        this.sectionsLoaded.Add(goInstantiated);

        if(this.sectionsLoaded.Count > this.maximumSectionsLoaded)
        {
            GameObject goToRemove = this.sectionsLoaded[0];
            this.sectionsLoaded.RemoveAt(0);
            Destroy(goToRemove);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //for(int i = 0; i < 10; i += 1)
        //{
            this.AddSection(0);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
