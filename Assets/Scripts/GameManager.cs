using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.VFX;

public class GameManager : MonoBehaviour
{

    public Sprite defaultSprite;

    public double sectionWidth;
    public int maximumSectionsLoaded;
    public int spawnNothing = 20;
    public int spawnLoot = 60;
    public int spawnEnemy = 100;

    private CameraController cameraController;

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

        this.ManageSectionSpawns(goInstantiated);
    }

    private void ManageSectionSpawns(GameObject section) 
    {
        List<GameObject> spawns = this.GetSectionSpawns(section.transform);
        foreach (GameObject spawn in spawns)
        { 
            int r = Random.Range(0, 100);
            Debug.Log("Number: " + r);
            if (r > this.spawnNothing)
            {
                if (r <= this.spawnLoot)
                {
                    //sr.color = Color.blue;    
                    Debug.Log("Spawning Loot!!");
                    GameObject heart = (GameObject)Resources.Load("Prefabs/Loot/Heart", typeof(GameObject));
                    GameObject heartInstantiated = Instantiate(heart, spawn.transform.position, Quaternion.identity);
                    heartInstantiated.transform.parent = spawn.transform;


                }
                else
                {
                    SpriteRenderer sr = spawn.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
                    sr.sprite = this.defaultSprite;
                    sr.color = Color.red;
                    Debug.Log("Spawning Enemy!!");
                }
            }
            else
            {
                SpriteRenderer sr = spawn.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
                sr.sprite = this.defaultSprite;
                Debug.Log("Spawning Nothing!");
            }
            
        }
        
    }

    private List<GameObject> GetSectionSpawns(Transform parent)
    {
        string tag = "Spawn";
        List<GameObject> spawns = new List<GameObject>();
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.CompareTag(tag))
            {
                spawns.Add(child.gameObject);
            }
        }

        return spawns;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        this.AddSection(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
