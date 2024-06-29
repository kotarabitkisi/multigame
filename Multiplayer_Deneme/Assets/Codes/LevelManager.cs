using Photon.Pun;
using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] Players;
    public SceneChanger changer;
    IEnumerator Start()
    {
        PhotonNetwork.Instantiate("Karakter", Vector3.zero, Quaternion.identity, 0, null);
        yield return new WaitForSecondsRealtime(0.5f);

        Players = GameObject.FindGameObjectsWithTag("Player");
        changer.setplayer();
        


    }

}
