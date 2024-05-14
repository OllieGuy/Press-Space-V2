using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chapter2Controller : MonoBehaviour
{
    public AudioManager am;
    public int level = 0;
    public GameObject player;
    public GameObject gardenTriggerBox;
    public Camera platformCamera;
    public Camera walledCamera;
    public Camera fpCamera;
    private Rigidbody rb;
    private Player3rdPersonController p3c;
    private Player1stPersonMovement p1m;
    private GardenEnter ge;
    private bool spaceOnFrame = false;
    private bool currentSoundPlay = false;
    private IEnumerator currentCoroutine;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        walledCamera.enabled = false;
        fpCamera.enabled = false;
        platformCamera.enabled = true;
        rb = player.GetComponent<Rigidbody>();
        ge = player.GetComponent<GardenEnter>();
        p3c = player.GetComponent<Player3rdPersonController>();
        p1m = player.GetComponent<Player1stPersonMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceOnFrame = true;
        }
        else
        {
            spaceOnFrame = false;
        }
        handleEvents();
    }

    void handleEvents()
    {
        if (p3c.jumpCount >= 25 && level <= 1)
        {
            StartCoroutine(preparingQuestion()); // REPLACE WITH SCENE
        }
        switch (level)
        {
            case 0:
                StartCoroutine(playAndWaitToLevelUp("C2_Narr_First_Island_Intro"));
                break;
            case 1:
                if (player.transform.position.x != 0 || player.transform.position.z != 0)
                {
                    am.play("C2_Narr_First_Island_Moves");
                    level++;
                }
                break;
            case 2:
                if (player.transform.position.y < 80f)
                {
                    StartCoroutine(playAndWaitToLevelUp("C2_Narr_Falls_Off_First_Island"));
                }
                break;
            case 3:
                SpawnOnWalledPlatform();
                currentCoroutine = preparingPrison();
                StartCoroutine(currentCoroutine);
                level++;
                break;
            case 4:
                if (player.transform.position.y < 50f)
                {
                    StopCoroutine(currentCoroutine);
                    StartCoroutine(playAndWaitToLevelUp("C2_Narr_Falls_Off_Second_Island"));
                }
                break;
            case 5:
                p3c.enabled = false;
                p1m.enabled = true;
                SpawnInRoom();
                StartCoroutine(preparingGarden());
                level++;
                break;
            case 6:
                if (player.transform.position.y < 0f)
                {
                    StartCoroutine(playAndWaitToLevelUp("C2_Narr_Player_Hidden_Exit"));
                }
                if (ge.inRangeOfGarden) //&& (player.transform.rotation.y > 0.9 || player.transform.position.y < 0.6))
                {
                    Debug.Log("prep garden");
                    if (spaceOnFrame)
                    {
                        SceneManager.LoadScene("Garden");
                    }
                }
                break;
            case 7:
                SceneManager.LoadScene("Chapter 3");
                break;
            default:
                break;
        }
    }
    private void SpawnOnWalledPlatform()
    {
        platformCamera.enabled = false;
        platformCamera.GetComponent<AudioListener>().enabled = false;
        walledCamera.enabled = true;
        walledCamera.GetComponent<AudioListener>().enabled = true;
        player.transform.position = new Vector3(0f, 61.5f, 0f);
        rb.velocity = Vector3.zero;
    }
    private void SpawnInRoom()
    {
        walledCamera.enabled = false;
        walledCamera.GetComponent<AudioListener>().enabled = false;
        fpCamera.enabled = true;
        fpCamera.GetComponent<AudioListener>().enabled = true;
        fpCamera.GetComponent<FirstPersonCamera>().enabled = true;
        player.transform.position = new Vector3(-0.15f, 2f, -5.5f);
        fpCamera.transform.rotation = new Quaternion(0,0,0,0);
        player.transform.rotation = new Quaternion(0, 0, 0, 0);
        rb.velocity = Vector3.zero;
    }
    IEnumerator playAndWaitToLevelUp(string soundToPlay)
    {
        if (!currentSoundPlay)
        {
            p3c.enabled = false;
            currentSoundPlay = true;
            yield return new WaitForSeconds(am.play(soundToPlay));
            p3c.enabled = true;
            currentSoundPlay = false;
            level++;
        }
    }
    IEnumerator preparingQuestion()
    {
        p3c.enabled = false;
        player.GetComponent<Rigidbody>().useGravity = false;
        yield return new WaitForSeconds(am.play("C2_Narr_First_Island_Jumps25"));
        SceneManager.LoadScene("Question");
    }
    IEnumerator preparingPrison()
    {
        am.play("C2_Narr_Second_Island_Intro");
        yield return new WaitForSeconds(20);
        am.play("C2_Narr_20_Seconds_Elapsed");
        yield return new WaitForSeconds(10);
        am.play("C2_Narr_Taking_To_Prison");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Prison");
    }
    IEnumerator preparingGarden()
    {
        yield return new WaitForSeconds(am.play("C2_Narr_Room_Intro"));
        yield return new WaitForSeconds(15);
        am.play("C2_Narr_Nearly_Ready");
        yield return new WaitForSeconds(5);
        am.play("C2_Narr_Invitation");
        gardenTriggerBox.SetActive(true);
    }
}
