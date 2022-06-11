//
//  JpegTurbo.cpp
//  iviz_jpegturbo
//
//  Created by Antonio Zea on 10.06.22.
//

#include "JpegTurbo.hpp"

#include <turbojpeg.h>

extern "C"
{
__attribute__((visibility("default")))
void* IvizInitDecompress() {
    return tjInitDecompress();
}

__attribute__((visibility("default")))
int IvizDestroy(void *handle) {
    return tjDestroy(handle);
}

__attribute__((visibility("default")))
void *IvizGetStr() {
    return tjGetErrorStr();
}

__attribute__((visibility("default")))
int IvizDecompressHeader3(void *handle, const unsigned char *jpegBuf, unsigned long jpegSize,
                          int *width, int *height, int *jpegSubSamp, int *jpegColorSpace)
{
    return tjDecompressHeader3(handle, jpegBuf, jpegSize, width, height, jpegSubSamp, jpegColorSpace);
}

__attribute__((visibility("default")))
int IvizDecompress2(void *handle, const unsigned char *jpegBuf, unsigned long jpegSize,
                    unsigned char *dstBuf, int width, int pitch, int height, int pixelFormat,
                    int flags) {
    return tjDecompress2(handle, jpegBuf, jpegSize, dstBuf, width, pitch, height, pixelFormat, flags);
}

__attribute__((visibility("default")))
void* IvizInitCompress(void)
{
    return tjInitCompress();
}

__attribute__((visibility("default")))
int IvizCompress2(void* handle, const unsigned char *srcBuf,
                  int width, int pitch, int height, int pixelFormat,
                  unsigned char **jpegBuf, unsigned long *jpegSize,
                  int jpegSubsamp, int jpegQual, int flags)
{
    return tjCompress2(handle, srcBuf, width, pitch, height, pixelFormat, jpegBuf, jpegSize, jpegSubsamp, jpegQual, flags);
    
}

__attribute__((visibility("default")))
int IvizCompressFromYUV(void* handle, const unsigned char *srcBuf,
                        int width, int pad, int height, int subsamp,
                        unsigned char **jpegBuf,
                        unsigned long *jpegSize, int jpegQual,
                        int flags)
{
    return tjCompressFromYUV(handle, srcBuf, width, pad, height, subsamp, jpegBuf, jpegSize, jpegQual, flags);
}

__attribute__((visibility("default")))
int IvizCompressFromYUVPlanes(void* handle,
                              const unsigned char **srcPlanes,
                              int width, const int *strides,
                              int height, int subsamp,
                              unsigned char **jpegBuf,
                              unsigned long *jpegSize, int jpegQual,
                              int flags)
{
    return tjCompressFromYUVPlanes(handle, srcPlanes, width, strides, height, subsamp, jpegBuf, jpegSize, jpegQual, flags);
}

__attribute__((visibility("default")))
unsigned long IvizBufSize(int width, int height, int jpegSubsamp)
{
    return tjBufSize(width, height, jpegSubsamp);
}

__attribute__((visibility("default")))
unsigned long IvizBufSizeYUV2(int width, int pad, int height, int subsamp)
{
    return tjBufSizeYUV2(width, pad, height, subsamp);
}

__attribute__((visibility("default")))
unsigned long IvizPlaneSizeYUV(int componentID, int width, int stride,
                               int height, int subsamp)
{
    return tjPlaneSizeYUV(componentID, width, stride, height, subsamp);
}

__attribute__((visibility("default")))
int IvizPlaneWidth(int componentID, int width, int subsamp)
{
    return tjPlaneWidth(componentID, width, subsamp);
}

__attribute__((visibility("default")))
int IvizPlaneHeight(int componentID, int height, int subsamp)
{
    return tjPlaneHeight(componentID, height, subsamp);
}

__attribute__((visibility("default")))
int IvizEncodeYUV3(void* handle, const unsigned char *srcBuf,
                   int width, int pitch, int height, int pixelFormat,
                   unsigned char *dstBuf, int pad, int subsamp,
                   int flags)
{
    return tjEncodeYUV3(handle, srcBuf, width, pitch, height, pixelFormat, dstBuf, pad, subsamp, flags);
}

__attribute__((visibility("default")))
int IvizEncodeYUVPlanes(void* handle, const unsigned char *srcBuf,
                        int width, int pitch, int height,
                        int pixelFormat, unsigned char **dstPlanes,
                        int *strides, int subsamp, int flags)
{
    return tjEncodeYUVPlanes(handle, srcBuf, width, pitch, height, pixelFormat, dstPlanes, strides, subsamp, flags);
}

__attribute__((visibility("default")))
iviz_scalingfactor *IvizGetScalingFactors(int *numscalingfactors)
{
    return (iviz_scalingfactor*) tjGetScalingFactors(numscalingfactors);
}

__attribute__((visibility("default")))
int IvizDecompressToYUV2(void* handle, const unsigned char *jpegBuf,
                         unsigned long jpegSize, unsigned char *dstBuf,
                         int width, int pad, int height, int flags)
{
    return tjDecompressToYUV2(handle, jpegBuf, jpegSize, dstBuf, width, pad, height, flags);
}

__attribute__((visibility("default")))
int IvizDecompressToYUVPlanes(void* handle,
                              const unsigned char *jpegBuf,
                              unsigned long jpegSize,
                              unsigned char **dstPlanes, int width,
                              int *strides, int height, int flags)
{
    return tjDecompressToYUVPlanes(handle, jpegBuf, jpegSize, dstPlanes, width, strides, height, flags);
}

__attribute__((visibility("default")))
int IvizDecodeYUV(void* handle, const unsigned char *srcBuf,
                  int pad, int subsamp, unsigned char *dstBuf,
                  int width, int pitch, int height, int pixelFormat,
                  int flags)
{
    return tjDecodeYUV(handle, srcBuf, pad, subsamp, dstBuf, width, pitch, height, pixelFormat, flags);
}

__attribute__((visibility("default")))
int IvizDecodeYUVPlanes(void* handle,
                        const unsigned char **srcPlanes,
                        const int *strides, int subsamp,
                        unsigned char *dstBuf, int width, int pitch,
                        int height, int pixelFormat, int flags)
{
    return tjDecodeYUVPlanes(handle, srcPlanes, strides, subsamp, dstBuf, width, pitch, height, pixelFormat, flags);
}

__attribute__((visibility("default")))
void* IvizInitTransform(void)
{
    return tjInitTransform();
}

__attribute__((visibility("default")))
int IvizTransform(void* handle, const unsigned char *jpegBuf,
                  unsigned long jpegSize, int n,
                  unsigned char **dstBufs, unsigned long *dstSizes,
                  iviz_transform *transforms, int flags)
{
    return tjTransform(handle, jpegBuf, jpegSize, n, dstBufs, dstSizes, (tjtransform*) transforms, flags);
}

__attribute__((visibility("default")))
unsigned char *IvizAlloc(int bytes)
{
    return tjAlloc(bytes);
}

__attribute__((visibility("default")))
void IvizFree(unsigned char *buffer)
{
    return tjFree(buffer);
}

__attribute__((visibility("default")))
char *IvizGetErrorStr2(void* handle)
{
    return tjGetErrorStr2(handle);
}


}

