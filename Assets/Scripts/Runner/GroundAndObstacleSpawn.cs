using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GroundAndObstacleSpawn : MonoBehaviour
{
    public GameObject pfGroundTile;
    public GameObject player;
    [SerializeField] public GameObject[] keyPictures;
    GameObject currentTile;
    GameObject nextTile;
    float speed = 10f;
    float variationTimeLow = 3f;
    float variationTimeHigh = 5f;
    float timer = 0f;
    double val = 0;
    public bool pausedForDeath;
    public bool paused = false;
    Quaternion quat = new Quaternion (0,90,-90,0);

    // Start is called before the first frame update
    void OnEnable()
    {
        currentTile = Instantiate(pfGroundTile, new Vector3(12, -4, 0), Quaternion.identity);
        nextTile = Instantiate(pfGroundTile, new Vector3(114, -4, 0), Quaternion.identity);
        System.Random rand = new System.Random();
        val = variationTimeLow + (rand.NextDouble() * (variationTimeLow - variationTimeHigh));
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            System.Random rand = new System.Random();
            timer += Time.deltaTime;
            if (timer > val)
            {
                Instantiate(keyPictures[rand.Next(0, 6)], new Vector3(20, 0, 0), quat);
                val = variationTimeLow + (rand.NextDouble() * (variationTimeLow - variationTimeHigh));
                timer = 0;
            }
            if (currentTile.transform.position.x < -90)
            {
                GameObject tmp = nextTile;
                Destroy(currentTile);
                currentTile = nextTile;
                nextTile = Instantiate(pfGroundTile, new Vector3(114, -4, 0), Quaternion.identity);
            }
            moveGround();
        }
    }

    void moveGround()
    {
        if(!pausedForDeath)
        {
            Vector3 shift = new Vector3(-(speed * Time.deltaTime), 0, 0);
            currentTile.transform.position += shift;
            nextTile.transform.position += shift;
        }
    }    

    public void clearForRestart()
    {
        StartCoroutine(clear());
    }

    IEnumerator clear()
    {
        pausedForDeath = true;
        yield return new WaitForSeconds(1);
        Destroy(currentTile);
        Destroy(nextTile);
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("KeyObstacle");
        foreach (GameObject obj in allObjects)
        {
            Destroy(obj);
        }
        pausedForDeath = false;
    }
}
