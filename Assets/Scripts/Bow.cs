using UnityEngine;
using System.Collections;

public class Bow : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform spawnPosition;
    public Animation anim;

    public Transform pivotTransform;
    public Transform handleTransform;
    public Transform nockTransform;
    public Transform nockRestTransform;

    private const float minPull = 0.05f;
    private const float maxPull = 0.5f;
    private const float drawTime = 1.0f;

    public float arrowMinVelocity = 3f;
    public float arrowMaxVelocity = 30f;
    private float arrowVelocity = 30f;

    public AudioClip drawSound;
    public AudioClip arrowSlideSound;
    public AudioClip releaseSound;
    public AudioClip nockSound;


    private bool isDrawing = false;
    private float arrowMinMaxPowDif;
    private float curDrawPerc, curDrawTime;

    public GameObject currentArrow;
    // Use this for initialization
    void Start()
    {
        curDrawPerc = curDrawTime = 0.0f;
        arrowMinMaxPowDif = arrowMaxVelocity - arrowMinVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (isDrawing)
            {

            }
            else
            {
                curDrawPerc = curDrawTime = 0.0f;
                SpawnArrow();
                isDrawing = true;
            }


        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isDrawing)
            {
                Fire();
            }
        }
    }

    void Fire()
    {
        if (isDrawing && currentArrow != null)
        {
            Arrow arrow = currentArrow.GetComponent<Arrow>();
            Rigidbody rig = currentArrow.GetComponent<Rigidbody>();
            rig.isKinematic = false;
            float power = arrowMinVelocity + (arrowMinMaxPowDif * curDrawPerc);
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
