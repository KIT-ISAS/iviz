using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#if false

byte[] src = new byte[128];
ushort[] dst = new ushort[src.Length];

src[0] = 1;
src[1] = 2;
src[2] = 3;
src[3] = 4;

Stopwatch sw = Stopwatch.StartNew();
sw.Reset();
sw.Start();

for (int kk = 0; kk < 10; kk++)
{
    const int n = 64;

    sw.Reset();
    sw.Start();

    unsafe
    {
        fixed (byte* srcPtr = src)
        fixed (ushort* dstPtr = dst)
        {
            for (int i = 0; i < 10000000; i++)
            {
                int j = Random.Shared.Next(n);
                ConvertToCharBase(srcPtr, dstPtr, j);
            }
        }
    }

    Console.WriteLine("Ba : " + sw.ElapsedMilliseconds);


    sw.Reset();
    sw.Start();

    unsafe
    {
        fixed (byte* srcPtr = src)
        fixed (ushort* dstPtr = dst)
        {
            for (int i = 0; i < 10000000; i++)
            {
                int j = Random.Shared.Next(n);
                ConvertToChar(srcPtr, dstPtr, j);
            }
        }
    }

    Console.WriteLine("CC : " + sw.ElapsedMilliseconds);
    //Console.WriteLine(result);
    sw.Reset();
    sw.Start();
    unsafe
    {
        fixed (byte* srcPtr = src)
        fixed (ushort* dstPtr = dst)
        {
            for (int i = 0; i < 10000000; i++)
            {
                int j = Random.Shared.Next(n);
                ConvertToChar2(srcPtr, dstPtr, j);
            }
        }
    }

    Console.WriteLine("C2: " + sw.ElapsedMilliseconds);
    
    sw.Reset();
    sw.Start();
    unsafe
    {
        fixed (byte* srcPtr = src)
        fixed (ushort* dstPtr = dst)
        {
            for (int i = 0; i < 10000000; i++)
            {
                int j = Random.Shared.Next(n);
                ConvertToChar3(srcPtr, dstPtr, j);
            }
        }
    }

    Console.WriteLine("C3: " + sw.ElapsedMilliseconds);

    /*
    sw.Reset();
    sw.Start();
    unsafe
    {
        fixed (byte* ptr = bytes)
        {
            for (int i = 0; i < 10000000; i++)
            {
                int j = Random.Shared.Next(n);
                result |= CheckIfAllAscii2(ptr, j);
            }
        }
    }

    Console.WriteLine("- " + sw.ElapsedMilliseconds);
    */
}

static unsafe void ConvertToCharBase(byte* srcPtr, ushort* strPtr, int size)
{
    for (int i = 0; i < size; i++)
    {
        *strPtr++ = *srcPtr++;
    }
}


static unsafe void ConvertToChar(byte* srcPtr, ushort* strPtr, int size)
{
    ushort* dstPtr = (ushort*)strPtr;
    while (size >= 8)
    {
        *dstPtr++ = *srcPtr++;
        *dstPtr++ = *srcPtr++;
        *dstPtr++ = *srcPtr++;
        *dstPtr++ = *srcPtr++;
        *dstPtr++ = *srcPtr++;
        *dstPtr++ = *srcPtr++;
        *dstPtr++ = *srcPtr++;
        *dstPtr++ = *srcPtr++;
        size -= 8;
    }

    if (size >= 4)
    {
        *dstPtr++ = *srcPtr++;
        *dstPtr++ = *srcPtr++;
        *dstPtr++ = *srcPtr++;
        *dstPtr++ = *srcPtr++;
        size -= 4;
    }

    switch (size)
    {
        case 3:
            *dstPtr++ = *srcPtr++;
            *dstPtr++ = *srcPtr++;
            *dstPtr = *srcPtr;
            return;
        case 2:
            *dstPtr++ = *srcPtr++;
            *dstPtr = *srcPtr;
            return;
        case 1:
            *dstPtr = *srcPtr;
            return;
        case 0:
            return;
    }
}

static unsafe void ConvertToChar2(byte* srcPtr, ushort* strPtr, int size)
{
    uint* dstPtrInt = (uint*)strPtr;

    while (size >= 4)
    {
        uint a0 = *srcPtr++;
        uint a1 = *srcPtr++;
        *dstPtrInt++ = (a0 << 16) | a1;

        a0 = *srcPtr++;
        a1 = *srcPtr++;
        *dstPtrInt++ = (a0 << 16) | a1;

        size -= 4;
    }

    ushort* dstPtr = (ushort*)dstPtrInt;
    switch (size)
    {
        case 3:
            *dstPtr++ = *srcPtr++;
            *dstPtr++ = *srcPtr++;
            *dstPtr = *srcPtr;
            return;
        case 2:
            *dstPtr++ = *srcPtr++;
            *dstPtr = *srcPtr;
            return;
        case 1:
            *dstPtr = *srcPtr;
            return;
        case 0:
            return;
    }
}


static unsafe void ConvertToChar3(byte* srcPtr, ushort* strPtr, int size)
{
    ulong* dstPtrInt = (ulong*)strPtr;

    while (size >= 4)
    {
        ulong a0 = *srcPtr++;
        ulong a1 = *srcPtr++;
        ulong a2 = *srcPtr++;
        ulong a3 = *srcPtr++;

        *dstPtrInt++ = (a3 << 48) | (a2 << 32) | (a1 << 16) | a0;
        size -= 4;
    }

    ushort* dstPtr = (ushort*)dstPtrInt;
    switch (size)
    {
        case 3:
            *dstPtr++ = *srcPtr++;
            *dstPtr++ = *srcPtr++;
            *dstPtr = *srcPtr;
            return;
        case 2:
            *dstPtr++ = *srcPtr++;
            *dstPtr = *srcPtr;
            return;
        case 1:
            *dstPtr = *srcPtr;
            return;
        case 0:
            return;
    }
}

#endif