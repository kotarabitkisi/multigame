using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] GameObject[] ObjtoMat;
    public GameObject[] Players;
    PhotonView pw;

    private IEnumerator Start()
    {
        pw = GetComponent<PhotonView>();
        if (pw.IsMine)
        {
            yield return new WaitForSecondsRealtime(0.5f);

            if (PhotonNetwork.IsMasterClient)
            {
                int randomSeed = Random.Range(0, int.MaxValue);
                pw.RPC("SetRandomSeed", RpcTarget.AllBuffered, randomSeed);
            }
        }
    }

    [PunRPC]
    public void SetRandomSeed(int seed)
    {
        Random.InitState(seed);
        SetType();
    }
    [PunRPC]
    public  void ChangePosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
    public void MoveToNewPosition(Vector3 newPosition)
    {
        pw.RPC("ChangePosition", RpcTarget.AllBuffered, newPosition);
    }
    public void SetType()
    {
        for (int j = 0; j < 4; j++)
        {
            ObjtoMat[j].GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag=="Enemy")
        {
            print("a");
            Players = GameObject.FindGameObjectsWithTag("Player");
            foreach (var player in Players) { player.GetComponent<Stats>().MoveToNewPosition(Vector3.zero); }
        }
    }
}
