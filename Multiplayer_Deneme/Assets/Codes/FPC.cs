using Photon.Pun;
using UnityEngine;

public class FPC : MonoBehaviour
{
    public GameObject Player;
    public GameObject Camera;
    public float Xrot, Yrot,sensivity;
    public PhotonView pw;
    private void Update()
    {
        if (pw.IsMine)
        {
            Xrot -= Input.GetAxis("Mouse Y")*Time.deltaTime*sensivity;
            Yrot += Input.GetAxis("Mouse X")*Time.deltaTime*sensivity;

            Xrot=Mathf.Clamp(Xrot, -89,89);

            transform.localRotation = Quaternion.Euler(Xrot, 0, 0);
            Player.transform.rotation = Quaternion.Euler(0, Yrot, 0);
            transform.localPosition = new Vector3(0,2.7f,0.9f);
        }

    }
}
