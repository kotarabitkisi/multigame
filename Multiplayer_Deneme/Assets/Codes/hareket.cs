using Photon.Pun;
using System.Collections;
using UnityEngine;

public class hareket : MonoBehaviour
{
    public GameObject[] Players;
    [SerializeField] float Rotx, Roty, speed;

    public Rigidbody rb, CMRB;
    PhotonView pw;
    public GameObject CameraHandler, Camera;
    public Animator animator;
    public GameObject levelmanager;
    public bool spectatormode;
    private void Awake()
    {
        levelmanager = GameObject.FindGameObjectWithTag("LM");
        pw = GetComponent<PhotonView>();
    }
    IEnumerator Start()
    {
        if (pw.IsMine)
        {
            Camera.SetActive(true);
        }
        yield return new WaitForSecondsRealtime(0.5f);
        Players = GameObject.FindGameObjectsWithTag("Player");
       
    }
    private void Update()
    {
        if (pw.IsMine)
        {
            if (spectatormode)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rb.velocity = Vector3.up * 10;
                }
                else if (Input.GetKeyDown(KeyCode.LeftShift)|| Input.GetKeyDown(KeyCode.RightShift))
                {
                    rb.velocity = Vector3.down * 10;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    RaycastHit hit;
                    if (Physics.Raycast(transform.position, Vector3.down, out hit, 4))
                    {
                        rb.velocity = Vector3.up * 10;
                    }
                }
            }
            if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            {
                animator.SetBool("Running", true);
            }
            else
            {
                animator.SetBool("Running", false);
            }
        }
    }
    private void FixedUpdate()
    {
        if (pw.IsMine)
        {

            rb.velocity = (Input.GetAxisRaw("Vertical") * transform.forward + Input.GetAxisRaw("Horizontal") * transform.right) * speed + rb.velocity.y * Vector3.up;


        }
    }



}
