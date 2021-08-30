using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Iviz.Tools;

namespace Iviz.Ntp
{
    public class NtpQuery
    {
        static readonly DateTime EpochTime = new(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static async ValueTask<TimeSpan> GetNetworkTimeOffsetAsync(string ntpServer, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(ntpServer))
            {
                throw new ArgumentNullException(nameof(ntpServer));
            }

            const int maxFailures = 3;
            const int minSuccess = 10;
            
            var offsets = new List<TimeSpan>();
            int numFailures = 0;
            
            while (offsets.Count < minSuccess)
            {
                try
                {
                    TimeSpan offset = await GetNetworkTimeOffsetOneShotAsync(ntpServer, token);
                    //Console.WriteLine(offset.TotalMilliseconds);
                    offsets.Add(offset);
                    numFailures = 0;
                }
                catch (Exception e) when (e is not OperationCanceledException)
                {
                    Logger.LogDebugFormat("[NtpQuery]: {0}", e);
                    numFailures++;
                    if (numFailures == maxFailures)
                    {
                        throw new IOException($"Failed to contact host '{ntpServer}'");
                    }
                }

                await Task.Delay(1000, token);
            }

            offsets.Sort(); // TODO: implement fancier filtering
            
            long mean = 0L;
            long remainder = 0L;
            int minIndex = offsets.Count / 3;
            int maxIndex = offsets.Count * 2 / 3;
            int n = maxIndex - minIndex;
            for (int i = minIndex; i < maxIndex; i++) // workaround to avoid overflow (don't think it can happen)
            {
                long ticks = offsets[i].Ticks;
                mean += ticks / n;
                remainder += ticks % n;
                mean += remainder / n;
                remainder %= n;
            }
            
            return new TimeSpan(mean); 
        }
        
        public static async ValueTask<TimeSpan> GetNetworkTimeOffsetOneShotAsync(string ntpServer, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(ntpServer))
            {
                throw new ArgumentNullException(nameof(ntpServer));
            }
            
            using var ntpDataRent = new Rent<byte>(48);
            byte[] ntpData = ntpDataRent.Array;
            var ntpDataSegment = new ArraySegment<byte>(ntpData, 0, 48);

            Array.Clear(ntpData, 0, ntpData.Length);
            ntpData[0] = 0x1B; //LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)

            Task timerTask = Task.Delay(3000, token);

            var hostEntry = await AwaitWithTimeout(Dns.GetHostEntryAsync(ntpServer), timerTask);
            var addresses = hostEntry?.AddressList;
            if (addresses == null || addresses.Length == 0)
            {
                throw new IOException($"Failed to resolve address for '{ntpServer}'");
            }

            DateTime t0, t3;
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                await AwaitWithTimeout(socket.ConnectAsync(ntpServer, 123), timerTask);
                t0 = DateTime.UtcNow;
                await AwaitWithTimeout(socket.SendAsync(ntpDataSegment, SocketFlags.None), timerTask);
                await AwaitWithTimeout(socket.ReceiveAsync(ntpDataSegment, SocketFlags.None), timerTask);
                t3 = DateTime.UtcNow;
            }
            
            var t1 = ToDateTime(BitConverter.ToUInt32(ntpData, 32), BitConverter.ToUInt32(ntpData, 36));
            var t2 = ToDateTime(BitConverter.ToUInt32(ntpData, 40), BitConverter.ToUInt32(ntpData, 44));

            var offsetTimes2 = (t1 - t0) + (t2 - t3);
            return new TimeSpan(offsetTimes2.Ticks / 2);
        }

        static async Task AwaitWithTimeout(Task task, Task timerTask)
        {
            Task resultTask = await Task.WhenAny(task, timerTask);
            if (resultTask == timerTask)
            {
                throw new TimeoutException("NTP call did not return in time");
            }

            await task;
        }

        static async ValueTask<T> AwaitWithTimeout<T>(Task<T> task, Task timerTask)
        {
            Task resultTask = await Task.WhenAny(task, timerTask);
            if (resultTask == timerTask)
            {
                throw new TimeoutException("NTP call did not return in time");
            }
            
            return await task;
        }        
        
        static DateTime ToDateTime(uint chunk1, uint chunk2)
        {
            ulong secondsInt = SwapEndianness(chunk1);
            double ticks = SwapEndianness(chunk2);
            double secondsFrac = ticks / 0x100000000;
            return EpochTime.AddSeconds(secondsInt).AddSeconds(secondsFrac);
        }

        static uint SwapEndianness(uint x)
        {
            return ((x & 0x000000ff) << 24) +
                   ((x & 0x0000ff00) << 8) +
                   ((x & 0x00ff0000) >> 8) +
                   ((x & 0xff000000) >> 24);
        }
    }
}