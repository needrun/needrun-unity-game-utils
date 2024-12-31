using System;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

namespace NeedrunGameUtils
{
    public class NetworkTimer
    {
        private static TimeZoneInfo koreaTimeZone = TimeZoneInfo.CreateCustomTimeZone("UTC+09:00", TimeSpan.FromHours(9), "UTC+09:00", "UTC+09:00");
        private static string[] ntpServers = new string[] { "time.windows.com", "time.google.com" };

        public static DateTimeOffset GetNetworkDateTimeOffset()
        {
            foreach (string ntpServer in ntpServers)
            {
                try
                {
                    DateTime dateTime = GetNetworkTime(ntpServer);
                    DateTimeOffset dateTimeOffset = TimeZoneInfo.ConvertTime(dateTime, koreaTimeZone);
                    Debug.LogFormat("Success to fetch date time from : {0}", ntpServer);
                    return dateTime;
                }
                catch (Exception exception)
                {
                    Debug.LogWarningFormat("Fail to fetch date time from : {0}, error: {1}", ntpServer, exception);
                }
            }
            Debug.LogError("Fail to fetch date time from all ntpServer, so load from DateTimeOffset.Now");
            return DateTimeOffset.Now.ToOffset(TimeSpan.FromHours(9)); // 한국 시간대로 변환해서 내려줌
        }

        // from https://stackoverflow.com/questions/1193955/how-to-query-an-ntp-server-using-c
        public static DateTime GetNetworkTime(string ntpServer)
        {
            // NTP message size - 16 bytes of the digest (RFC 2030)
            var ntpData = new byte[48];

            //Setting the Leap Indicator, Version Number and Mode values
            ntpData[0] = 0x1B; //LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)

            var addresses = Dns.GetHostEntry(ntpServer).AddressList;

            //The UDP port number assigned to NTP is 123
            var ipEndPoint = new IPEndPoint(addresses[0], 123);
            //NTP uses UDP

            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                socket.Connect(ipEndPoint);

                //Stops code hang if NTP is blocked
                socket.ReceiveTimeout = 3000;

                socket.Send(ntpData);
                socket.Receive(ntpData);
                socket.Close();
            }

            //Offset to get to the "Transmit Timestamp" field (time at which the reply 
            //departed the server for the client, in 64-bit timestamp format."
            const byte serverReplyTime = 40;

            //Get the seconds part
            ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);

            //Get the seconds fraction
            ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);

            //Convert From big-endian to little-endian
            intPart = SwapEndianness(intPart);
            fractPart = SwapEndianness(fractPart);

            var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);

            //**UTC** time
            var networkDateTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);

            return networkDateTime.ToLocalTime();
        }

        // stackoverflow.com/a/3294698/162671
        static uint SwapEndianness(ulong x)
        {
            return (uint)(((x & 0x000000ff) << 24) +
                           ((x & 0x0000ff00) << 8) +
                           ((x & 0x00ff0000) >> 8) +
                           ((x & 0xff000000) >> 24));
        }
    }
}