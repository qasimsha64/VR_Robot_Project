using UnityEngine;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class VRCommandListener : MonoBehaviour
{
    private UdpClient udpServer;
    private Thread receiveThread;
    public int listenPort = 5000;

    public string latestCommand = "";
    private bool hasNewCommand = false;
    private volatile bool isRunning = true; // Safe thread flag

    [Header("Movement & Turn Speeds")]
    public float moveSpeed = 5.0f;
    public float turnSpeed = 100.0f;

    void Start()
    {
        receiveThread = new Thread(new ThreadStart(ListenForData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
        Debug.Log("✅ VR Command Listener started on port: " + listenPort);
    }

    private void ListenForData()
    {
        try
        {
            udpServer = new UdpClient(listenPort);
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

            while (isRunning)
            {
                byte[] data = udpServer.Receive(ref remoteEndPoint);
                string message = Encoding.UTF8.GetString(data).Trim();

                lock (this)
                {
                    latestCommand = message;
                    hasNewCommand = true;
                }

                Debug.Log("📡 Received: " + message);
            }
        }
        catch (SocketException)
        {
            // Socket closed normally on application exit
        }
        catch (ThreadAbortException)
        {
            // Catch thread abort during Unity Play Mode stop to prevent red error console
            Thread.ResetAbort();
        }
        catch (System.Exception e)
        {
            if (isRunning) Debug.LogError("❌ UDP Exception: " + e.Message);
        }
    }

    void Update()
    {
        string cmd = "";
        lock (this)
        {
            if (hasNewCommand)
            {
                cmd = latestCommand;
            }
        }

        if (cmd == "MOVE_FORWARD")
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
        }
        else if (cmd == "MOVE_BACKWARD")
        {
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.Self);
        }
        else if (cmd == "MOVE_LEFT")
        {
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime, Space.Self);
        }
        else if (cmd == "MOVE_RIGHT")
        {
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime, Space.Self);
        }
    }

    private void CleanupThread()
    {
        isRunning = false;

        if (udpServer != null)
        {
            udpServer.Close();
            udpServer = null;
        }

        if (receiveThread != null && receiveThread.IsAlive)
        {
            receiveThread.Abort(); // Abort safely wrapped in catch block
            receiveThread = null;
        }
    }

    void OnDisable()
    {
        CleanupThread();
    }

    void OnDestroy()
    {
        CleanupThread();
    }

    void OnApplicationQuit()
    {
        CleanupThread();
    }
}