//
//  JpegTurbo.hpp
//  iviz_jpegturbo
//
//  Created by Antonio Zea on 10.06.22.
//

#ifndef JpegTurbo_hpp
#define JpegTurbo_hpp

#include <stdio.h>

extern "C"
{
void* IvizInitDecompress();

int IvizDestroy(void *handle);

void *IvizGetErrorStr();

int IvizDecompressHeader3(void *handle, const unsigned char *jpegBuf, unsigned long jpegSize,
                         int *width, int *height, int *jpegSubSamp, int *jpegColorSpace);

int IvizDecompress2(void *handle, const unsigned char *jpegBuf, unsigned long jpegSize,
                    unsigned char *dstBuf, int width, int pitch, int height, int pixelFormat,
                    int flags);

void* IvizInitCompress(void);

int IvizCompress2(void* handle, const unsigned char *srcBuf,
                  int width, int pitch, int height, int pixelFormat,
                  unsigned char **jpegBuf, unsigned long *jpegSize,
                  int jpegSubsamp, int jpegQual, int flags);

int IvizCompressFromYUV(void* handle, const unsigned char *srcBuf,
                        int width, int pad, int height, int subsamp,
                        unsigned char **jpegBuf,
                        unsigned long *jpegSize, int jpegQual,
                        int flags);

int IvizCompressFromYUVPlanes(void* handle,
                              const unsigned char **srcPlanes,
                              int width, const int *strides,
                              int height, int subsamp,
                              unsigned char **jpegBuf,
                              unsigned long *jpegSize, int jpegQual,
                              int flags);

unsigned long IvizBufSize(int width, int height, int jpegSubsamp);

unsigned long IvizBufSizeYUV2(int width, int pad, int height, int subsamp);

unsigned long IvizPlaneSizeYUV(int componentID, int width, int stride,
                               int height, int subsamp);

int IvizPlaneWidth(int componentID, int width, int subsamp);

int IvizPlaneHeight(int componentID, int height, int subsamp);

int IvizEncodeYUV3(void* handle, const unsigned char *srcBuf,
                   int width, int pitch, int height, int pixelFormat,
                   unsigned char *dstBuf, int pad, int subsamp,
                   int flags);

int IvizEncodeYUVPlanes(void* handle, const unsigned char *srcBuf,
                        int width, int pitch, int height,
                        int pixelFormat, unsigned char **dstPlanes,
                        int *strides, int subsamp, int flags);

typedef struct {
    int num;
    int denom;
} iviz_scalingfactor;

iviz_scalingfactor *IvizGetScalingFactors(int *numscalingfactors);

int IvizDecompressToYUV2(void* handle, const unsigned char *jpegBuf,
                         unsigned long jpegSize, unsigned char *dstBuf,
                         int width, int pad, int height, int flags);

int IvizDecompressToYUVPlanes(void* handle,
                              const unsigned char *jpegBuf,
                              unsigned long jpegSize,
                              unsigned char **dstPlanes, int width,
                              int *strides, int height, int flags);

int IvizDecodeYUV(void* handle, const unsigned char *srcBuf,
                  int pad, int subsamp, unsigned char *dstBuf,
                  int width, int pitch, int height, int pixelFormat,
                  int flags);

int IvizDecodeYUVPlanes(void* handle,
                        const unsigned char **srcPlanes,
                        const int *strides, int subsamp,
                        unsigned char *dstBuf, int width, int pitch,
                        int height, int pixelFormat, int flags);

void* IvizInitTransform(void);

typedef struct {
    int x;
    int y;
    int w;
    int h;
} iviz_region;

typedef struct iviz_transform {
    iviz_region r;
    int op;
    int options;
    void *data;
    int (*customFilter) (short *coeffs, iviz_region arrayRegion,
                         iviz_region planeRegion, int componentIndex,
                         int transformIndex, struct iviz_transform *transform);
} iviz_transform;


int IvizTransform(void* handle, const unsigned char *jpegBuf,
                  unsigned long jpegSize, int n,
                  unsigned char **dstBufs, unsigned long *dstSizes,
                  iviz_transform *transforms, int flags);

unsigned char *IvizAlloc(int bytes);

void IvizFree(unsigned char *buffer);

char *IvizGetErrorStr2(void* handle);


}

#endif /* JpegTurbo_hpp */
