using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using IoneVectronConverter.Ione.Orders.Models;
using IoneVectronConverter.Vectron.MasterData;
using IoneVectronConverter.Vectron.Models;
using Microsoft.AspNetCore.DataProtection;
using Newtonsoft.Json;

namespace IoneVectronConverter.Vectron.Client;

public class VectronClient : IVectronClient
{
    public const string SendDelimiter = "\0";
    const int port = 1050;
    const string secret = "*6H@6TF7bDrCbU-V1.0";
    
    //Todo: implement client
    public VPosResponse Send(OrderListData order)
    {
        throw new NotImplementedException();
    }
    
    public async Task<VPosResponse> SendReceipt(Receipt receipt)
    {
        return await Task.Run(() =>
            {
                Socket socket = GetVPosSocket();
                SendBase64String(ref socket, receipt.ToJson());

                string jsonText = Encoding.UTF8.GetString(Convert.FromBase64String(Encoding.UTF8.GetString(GetResponse(socket))));

                socket.Close();

                return JsonConvert.DeserializeObject<VPosResponse>(jsonText);
            }
        );
    }
    
    static void SendBase64String(ref Socket socket, string text)
    {
        socket.Send(Encoding.UTF8.GetBytes(Convert.ToBase64String(Encoding.UTF8.GetBytes(text)) + SendDelimiter));
    }
    
    static byte[] GetResponse(Socket socket)
    {
        var buffer = new byte[256];
        int bytesRead;
        List<byte> responseBytes = new List<byte>();
        while ((bytesRead = socket.Receive(buffer)) > 0)
        {
            responseBytes.AddRange(buffer.Take(bytesRead));
        }
        return responseBytes.ToArray();
    }

    public MasterDataResponse GetMasterData()
    {
        throw new NotImplementedException();
    }
    
    static Socket GetVPosSocket()
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect(new IPEndPoint(IPAddress.Parse("000.000.00.00"), port));
        byte[] bytes = new byte[6];
        socket.Receive(bytes);
        string salt = Encoding.UTF8.GetString(bytes);
        var md5 = MD5.Create();
        socket.Send(md5.ComputeHash(Encoding.UTF8.GetBytes(secret + salt)));
        return socket;
    }
}