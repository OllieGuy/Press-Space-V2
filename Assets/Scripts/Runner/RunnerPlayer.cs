using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerPlayer : MonoBehaviour
{
    private GroundAndObstacleSpawn gos;
    private Rigidbody rb;
    private Vector3 jumpHeight = new Vector3(0f,1000f,0f);
    private bool isGrounded = true;
    private bool pointWhenReachGround = false;
    private float fallMultiplier = 6f;
    private float animTimer = 0f;
    public int score = 0;
    public AudioManager am;
    public GameObject rc;
    public RunnerController runnerController;
    [SerializeField] public GameObject[] playerAnimFrames;
    private GameObject curFrame;

    void Start()
    {
        gos = rc.GetComponent<GroundAndObstacleSpawn>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!gos.paused)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            if (transform.position.y > 0.15)
            {
                isGrounded = false;
            }
            else
            {
                isGrounded = true;
            }
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !gos.pausedForDeath)
            {
                am.play("Game_SFX_Jump");
                rb.AddForce(jumpHeight);
            }
            if (isGrounded && pointWhenReachGround)
            {
                score++;
                pointWhenReachGround = false;
            }
            handleAnim();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "KeyCollider")
        {
            die();
            pointWhenReachGround = false;
        }
        else if (col.gameObject.tag == "PointCollider" && !isGrounded)
        {
            am.play("Game_SFX_Point");
            pointWhenReachGround = true;
        }
    }

    public void die()
    {
        am.play("Game_SFX_Lose");
        runnerController.addToPastScores(score);
        score = 0;
        StartCoroutine(died());
    }

    public void handleAnim()
    {
        animTimer += Time.deltaTime;
        if (animTimer > 0.2f && isGrounded)
        {
            if (curFrame == playerAnimFrames[1])
            {
                curFrame = playerAnimFrames[2];
            }
            else
            {
                curFrame = playerAnimFrames[1];
            }
            animTimer = 0f;
        }
        else if (!isGrounded)
        {
            curFrame = playerAnimFrames[3];
        }
        if (gos.pausedForDeath)
        {
            curFrame = playerAnimFrames[4];
        }
        foreach (GameObject obj in playerAnimFrames)
        {
            if (obj != curFrame)
            {
                obj.SetActive(false);
            }
            else
            {
                obj.SetActive(true);
            }
        }
    }
    public void handleAnim(int frameToShow)
    {
        curFrame = playerAnimFrames[frameToShow];
        foreach (GameObject obj in playerAnimFrames)
        {
            if (obj != curFrame)
            {
                obj.SetActive(false);
            }
            else
            {
                obj.SetActive(true);
            }
        }
    }

    IEnumerator died()
    {
        gos.paused = true;
        if (!runnerController.explainedFirstFail && !runnerController.diedForFirstTimeOnKey)
        {
            yield return new WaitForSeconds(am.returnLength("Game_Narr_First_Fail"));
            runnerController.explainedFirstFail = true;
        }
        else if (!runnerController.explainedRandomDie && runnerController.diedForFirstTimeOnKey)
        {
            yield return new WaitForSeconds(am.returnLength("Game_Narr_Forbidden_Key_First_Death"));
            runnerController.explainedRandomDie = true;
        }
        gos.paused = false;
        gos.clearForRestart();
        yield return new WaitForSeconds(1f);
        gos.enabled = false;
        gos.enabled = true;
        curFrame = playerAnimFrames[1];
        am.play("Game_SFX_Start");
    }
}
