using System.Collections;
using System.Collections.Generic;

public class SocketUnPack
{
    public static void Init()
    {
        EventTool.Add<byte[]>(EventName.SocketClient.ReceiveData,receivData);
    }

    private static void receivData(byte[] rData)
    {

    }
}