using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppyExplode : MonoBehaviour
{
    public GameObject ExplosionPlane;
    public Canvas canvas;
    public GameObject amGO;
    public AudioManager am;

    public void boom()
    {
        amGO = GameObject.Find("AudioManager");
        am = amGO.GetComponent<AudioManager>();
        canvas = GetComponentInParent<Canvas>();
        MeshRenderer MR = GetComponent<MeshRenderer>();
        MR.enabled = false;
        GameObject EP = Instantiate(ExplosionPlane, canvas.transform);
        StartCoroutine(Explosion(EP, MR));
    }

    IEnumerator Explosion(GameObject EP, MeshRenderer MR)
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(EP);
        yield return new WaitForSeconds(1f);
        MR.enabled = true;
        am.play("C1_SFX_Bark");
        yield return null;
    }
}
