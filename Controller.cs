using System;
using RPC;

[System.Serializable]
public event EventHandler<EventArgs> DiscordJoinEvent; //merci jeuxjeux20

[System.Serializable]
public event EventHandler<EventArgs> DiscordSpectateEvent; 

[System.Serializable]
public event EventHandler<DiscordRpc.JoinRequest> DiscordJoinRequestEvent;  //Marche STP.

public class Controller : MonoBehaviour
{
    public RichPresence presence;
    public string applicationId;
    public string optionalSteamId;
    public event EventHandler<EventArgs> onConnect;
    public event EventHandler<EventArgs> onDisconnect;
    public DiscordJoinEvent onJoin;
    public DiscordJoinEvent onSpectate;
    public DiscordJoinRequestEvent onJoinRequest;

    EventHandlers handlers;

    public void ReadyCallback()
    {
        onConnect.Invoke();
    }

    public void DisconnectedCallback(int errorCode, string message)
    {
        onDisconnect.Invoke();
    }

    public void ErrorCallback(int errorCode, string message)
    {
    }

    public void JoinCallback(string secret)
    {
        onJoin.Invoke(secret);
    }

    public void SpectateCallback(string secret)
    {
        onSpectate.Invoke(secret);
    }

    public void RequestCallback(DiscordRpc.JoinRequest request)
    {
      
        onJoinRequest.Invoke(request);
    }

    void Start()
    {
    }

    void Update()
    {
        RunCallbacks();
    }

    void OnEnable()
    {
        //Modifie
        callbackCalls = 0;

        handlers = new DiscordRpc.EventHandlers();
        handlers.readyCallback = ReadyCallback;
        handlers.disconnectedCallback += DisconnectedCallback;
        handlers.errorCallback += ErrorCallback;
        handlers.joinCallback += JoinCallback;
        handlers.spectateCallback += SpectateCallback;
        handlers.requestCallback += RequestCallback;
        DiscordRpc.Initialize(applicationId, ref handlers, true, optionalSteamId);
    }

    void OnDisable()
    {
        //Modifie
        DiscordRpc.Shutdown();
    }

    void OnDestroy()
    {

    }
}
