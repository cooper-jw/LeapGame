using UnityEngine;
using System.Collections;

public class Bow : MonoBehaviour {
    public GameObject arrowPrefab;
    public Transform spawnPosition;
    public Animation anim;

    public float drawTime = 1.0f, fullDrawPerc = 1.0f;
    public float minArrowPower = 1.0f, maxArrowPower = 10.0f;
    public float blendFadeLength = 0.05f;

    private bool isDrawing = false;
    private float arrowMinMaxPowDif;
    private float curDrawPerc, curDrawTime;

    public GameObject currentArrow;
	// Use this for initialization
	void Start () {
        curDrawPerc = curDrawTime = 0.0f;
        arrowMinMaxPowDif = maxArrowPower - minArrowPower;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetMouseButton(0)) {
            if(isDrawing) {
                curDrawTime += Time.deltaTime;
                curDrawPerc = curDrawTime;
                anim.Blend("Bow_Draw", curDrawTime, blendFadeLength);
            } else {
                curDrawPerc = curDrawTime = 0.0f;
                SpawnArrow();
                isDrawing = true;
            }


        } else if(Input.GetMouseButtonUp(0)) {
            if(isDrawing) {
                Fire();
            }
        }
	}

    void Fire()
    {
        if(isDrawing && currentArrow != null)
        {
            Arrow arrow = currentArrow.GetComponent<Arrow>();
            Rigidbody rig = currentArrow.GetComponent<Rigidbody>();
            rig.isKinematic = false;
            float power = minArrowPower + (arrowMinMaxPowDif * curDrawPerc);
            rig.AddRelativeForce(Vector3.forward * power, ForceMode.Impulse);
            isDrawing = false;
            arrow.isFired = true;
            currentArrow.transform.parent = null;
            anim.Play("Bow_Idle", PlayMode.StopAll);
            currentArrow = null;
        }
    }

    void SpawnArrow()
    {
        currentArrow = (GameObject)Instantiate(arrowPrefab, spawnPosition.position, spawnPosition.rotation, spawnPosition);
    }
}
