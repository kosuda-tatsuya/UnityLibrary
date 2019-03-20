﻿using UnityEngine;
using UnityOSC;

namespace Kosu.UnityLibrary
{
    public class TCPOSCSender : BaseTCPSender
    {

        public TCPOSCSender(string host, int port) : base(host, port) { }

        protected override void Constructed(string host, int port)
        {
            Debug.Log("[TCPOSCSender] Invoke Contructor Method");
            _host = host;
            _port = port;
            Connect();
        }

        public static OSCMessage CreateOSCMessage(string address, params object[] objects)
        {
            var msg = new OSCMessage(address);

            foreach (object msgvalue in objects)
            {
                msg.Append(msgvalue);
            }

            return msg;
        }

        public static OSCBundle CreateOSCBundle(string address, params object[] objects)
        {
            return null;
        }

        protected override byte[] Serialize<T>(T data)
        {
            if (!(data is OSCPacket))
            {
                Debug.LogError($"data format is invalid : datatype = {typeof(T)}");
                return null;
            }

            return (data as OSCPacket).BinaryData;
        }

    }

    public class TCPOSCReciever : BaseTCPReciever<OSCPacket>
    {

        public TCPOSCReciever(string host, int port) : base(host, port) { }

        protected override void Constructed(string host, int port)
        {
            Debug.Log("[TCPOSCReciever] Invoke Constructor Method");
            _host = host;
            _port = port;
            StartServer();
        }

        protected override OSCPacket DeSerialize(byte[] data)
        {
            return OSCPacket.Unpack(data);
        }

    }
}
