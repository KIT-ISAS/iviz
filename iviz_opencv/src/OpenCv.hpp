//
//  OpenCv.hpp
//  Iviz.Opencv
//
//  Created by Antonio Zea on 06.03.21.
//  Copyright © 2021 Antonio Zea. All rights reserved.
//

#ifndef OpenCv_hpp
#define OpenCv_hpp

#include <stdint.h>

extern "C"
{
    typedef void(*IvizCallback)(const char *);

    void IvizSetupDebug(IvizCallback callback);
    void IvizSetupInfo(IvizCallback callback);
    void IvizSetupError(IvizCallback callback);

    void *IvizCreateContext(int32_t width, int32_t height);
    int IvizImageWidth(const void *ctx);
    int IvizImageHeight(const void *ctx);
    int IvizImageFormat(const void *ctx);
    int IvizImageSize(const void *ctx);
    bool IvizCopyFrom(void *ctx, const uint8_t *ptr, int size);
    bool IvizCopyTo(const void *ctx, uint8_t *ptr, int size);
    void *IvizGetImagePtr(void *ctx);
    
    
    bool IvizSetDictionary(void *ctx, int value);
    bool IvizDetectArucoMarkers(void *ctx);
    bool IvizDetectQrMarkers(void *ctx_base);
    int IvizGetNumDetectedMarkers(const void *ctx);
    bool IvizGetArucoMarkerIds(const void *ctx, int *arrayPtr, int arraySize);
    bool IvizGetQrMarkerCodes(const void *ctx_base, const void **arrayPtr, int *arrayLengths, int arraySize);
    bool IvizGetMarkerCorners(const void *ctx, float *arrayPtr, int arraySize);
    bool IvizSetCameraMatrix(void *ctx, float *arrayPtr, int arraySize);
    bool IvizEstimateMarkerPoses(const void *ctx, float markerSize, float *rotations, int rotationsSize, float *translations, int translationsSize);
    
    bool IvizEstimatePnp(const float *inputs, int inputSize, const float *outputs, int outputSize, float *cameraArray, int cameraArraySize, float *result, int resultSize);
    
    void IvizDisposeContext(void *ctx);
}


#endif /* OpenCv_hpp */
