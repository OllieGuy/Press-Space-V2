using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestionController : MonoBehaviour
{
    public AudioManager am;
    public int level = 0;
    public GameObject player;
    public GameObject lPos;
    public GameObject rPos;
    public GameObject cPos;
    public GameObject timeBoard;
    public GameObject FUBoard;
    public GameObject curiosityBoard;
    public GameObject mummyBoard;
    private bool LorR = false;
    private bool choiceActive = false;
    private bool currentSoundPlay = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && choiceActive)
        {
            LorR = !LorR;
            
        }
        if (!LorR && choiceActive)
        {
            player.transform.position = lPos.transform.position;
        }
        else if (LorR && choiceActive)
        {
            player.transform.position = rPos.transform.position;
        }
        else
        {
            player.transform.position = cPos.transform.position;
        }
        handleEvents();
    }

    void handleEvents()
    {
        switch (level)
        {
            case 0:
                StartCoroutine(playAndWaitToLevelUp("Ques_Narr_Intro"));
                break;
            case 1:
                timeBoard.SetActive(true);
                curiosityBoard.SetActive(true);
                StartCoroutine(choiceTime(5));
                break;
            case 2:
                StartCoroutine(playAndWaitToLevelUp("Ques_Narr_Second"));
                break;
            case 3:
                timeBoard.SetActive(false);
                curiosityBoard.SetActive(false);
                FUBoard.SetActive(true);
                mummyBoard.SetActive(true);
                StartCoroutine(choiceTime(5));
                break;
            case 4:
                StartCoroutine(playAndWaitToLevelUp("Ques_Narr_Outro"));
                break;
            case 5:
                SceneManager.LoadScene("Chapter 1");
                break;
            default:
                break;
        }
    }
    IEnumerator playAndWaitToLevelUp(string soundToPlay)
    {
        if (!currentSoundPlay)
        {
            currentSoundPlay = true;
            yield return new WaitForSeconds(am.play(soundToPlay));
            currentSoundPlay = false;
            level++;
        }
    }

    IEnumerator choiceTime(float time)
    {
        if (!currentSoundPlay)
        {
            currentSoundPlay = true;
            choiceActive = true;
            yield return new WaitForSeconds(time);
            choiceActive = false;
            currentSoundPlay = false;
            level++;
        }
    }
}
