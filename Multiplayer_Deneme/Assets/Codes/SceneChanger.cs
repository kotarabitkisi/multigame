using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public TextMeshProUGUI playerText;
    public int playerCount;
    public GameObject[] player;
    public float time, timespeed;
    public void setplayer()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        playerText.text = playerCount + "/" + player.Length;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject player_ = other.gameObject;
            playerCount++;
            playerText.text = playerCount + "/" + player.Length;
            player_.GetComponent<hareket>().spectatormode = true;
            player_.GetComponent<CapsuleCollider>().enabled = false;
            player_.GetComponent<Rigidbody>().useGravity = false;
            if (playerCount == player.Length) timespeed = 1;
        }
    }
    private void Update()
    {
        time -= timespeed * Time.deltaTime;
        if (timespeed != 0)
        {
            playerText.text = "next level is loading: " + time.ToString("0") + " second left";
        }

        if (time <= 0)
        {
            for (int i = 0; i < player.Length; i++)
            {
                player[i].GetComponent<hareket>().spectatormode = false;
                player[i].GetComponent<Rigidbody>().useGravity = true;
                player[i].GetComponent<CapsuleCollider>().enabled = true;
            }
            PhotonNetwork.AutomaticallySyncScene = false;
            PhotonNetwork.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
