using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using TMPro;
using System.Net;
using System.Net.Sockets;

public class NetworkUI : MonoBehaviour
{

    [SerializeField] private Button serverbutton;
    [SerializeField] private Button hostbutton;
    [SerializeField] private Button clientbutton; 
    [SerializeField] string ipAddress;   
	[SerializeField] UnityTransport transport;    
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI ipAddressText;
    private void Awake(){
        serverbutton.onClick.AddListener(() =>{
            NetworkManager.Singleton.StartServer();

        });
        hostbutton.onClick.AddListener(() =>{
            NetworkManager.Singleton.StartHost();
            GetLocalIPAddress();
        });
        clientbutton.onClick.AddListener(() =>{
            NetworkManager.Singleton.StartClient();

        });        

    }
	// ONLY FOR CLIENT SIDE
	public void SetIpAddress() {
		transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
		transport.ConnectionData.Address = ipAddress;
	}    
	public string GetLocalIPAddress() {
		var host = Dns.GetHostEntry(Dns.GetHostName());
		foreach (var ip in host.AddressList) {
			if (ip.AddressFamily == AddressFamily.InterNetwork) {
				ipAddressText.text = ip.ToString();
				ipAddress = ip.ToString();
				return ip.ToString();
			}
		}
		throw new System.Exception("No network adapters with an IPv4 address in the system!");
	}
}
