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
    typedef void(*Callback)(const char *);

    void SetupDebug(Callback callback);
    void SetupInfo(Callback callback);
    void SetupError(Callback callback);

    void *CreateContext(int32_t width, int32_t height);
    int ImageWidth(const void *ctx);
    int ImageHeight(const void *ctx);
    int ImageFormat(const void *ctx);
    int ImageSize(const void *ctx);
    bool CopyFrom(void *ctx, const uint8_t *ptr, int size);
    bool CopyTo(const void *ctx, uint8_t *ptr, int size);
    void *GetImagePtr(void *ctx);
    
    
    bool SetDictionary(void *ctx, int value);
    bool DetectArucoMarkers(void *ctx);
    int GetNumDetectedArucoMarkers(const void *ctx);
    bool GetArucoMarkerIds(const void *ctx, int *arrayPtr, int arraySize);
    bool GetArucoMarkerCorners(const void *ctx, float *arrayPtr, int arraySize);
    bool SetCameraMatrix(void *ctx, float *arrayPtr, int arraySize);
    bool EstimateArucoPose(const void *ctx, float markerSize, int* markerIndices, int markerIndicesLength,
                           float *rotations, int rotationsSize, float *translations, int translationsSize);
    
    void DisposeContext(void *ctx);
}


#endif /* OpenCv_hpp */
