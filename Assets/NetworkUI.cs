using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;


public class NetworkUI : MonoBehaviour
{

    [SerializeField] private Button serverbutton;
    [SerializeField] private Button hostbutton;
    [SerializeField] private Button clientbutton;    
    // Start is called before the first frame update

    private void Awake(){
        serverbutton.onClick.AddListener(() =>{
            NetworkManager.Singleton.StartServer();

        });
        hostbutton.onClick.AddListener(() =>{
            NetworkManager.Singleton.StartHost();

        });
        clientbutton.onClick.AddListener(() =>{
            NetworkManager.Singleton.StartClient();

        });        

    }

}
