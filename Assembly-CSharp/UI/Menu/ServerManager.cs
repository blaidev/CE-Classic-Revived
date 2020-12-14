using System.Collections;
using System.IO;
using UnityEngine;

public class ServerManager : DestroyableSingleton<ServerManager>
{
	private enum UpdateState
	{
		Connecting,
		Failed,
		Success
	}

	public const string DefaultOnlineServer = "24.181.130.52";

	public static readonly ServerInfo DefaultServer;

	public ServerInfo[] availableServers;

	public ServerInfo LastServer = DefaultServer;

	private string serverInfoFile;

	private UpdateState state;

	public string OnlineNetAddress => LastServer.Ip;

	public void Start()
	{
		serverInfoFile = Path.Combine(Application.persistentDataPath, "serverInfo.dat");
		LastServer = DefaultServer;
		availableServers = new ServerInfo[1]
		{
			LastServer
		};
		state = UpdateState.Success;
	}

	public IEnumerator WaitForServers()
	{
		while (state == UpdateState.Connecting)
		{
			yield return null;
		}
	}

	static ServerManager()
	{
		DefaultServer = new ServerInfo
		{
			Name = "Primary",
			Ip = "24.181.130.52",
			Default = true
		};
	}
}
