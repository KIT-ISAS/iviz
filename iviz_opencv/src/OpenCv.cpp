//
//  OpenCv.cpp
//  Iviz.Opencv
//
//  Created by Antonio Zea on 06.03.21.
//  Copyright © 2021 Antonio Zea. All rights reserved.
//

#include "OpenCv.hpp"
#include <stdio.h>
#include <opencv2/core/mat.hpp>
#include <opencv2/aruco.hpp>

struct Context
{
    cv::Mat image;
    cv::Ptr<cv::aruco::Dictionary> dictionary;
    cv::Ptr<cv::aruco::DetectorParameters> parameters;
    std::vector<int> markerIds;
    std::vector<std::vector<cv::Point2f>> markerCorners;
    cv::Mat cameraMatrix;
};

extern "C" {
    static Callback DebugFn = nullptr;
    static Callback InfoFn = nullptr;
    static Callback ErrorFn = nullptr;
    
    static const std::vector<float> distCoeffs = { 0, 0, 0, 0, 0 };
    
    
    void SetupDebug(Callback callback)
    {
        DebugFn = callback;
    }
    
    void SetupInfo(Callback callback)
    {
        InfoFn = callback;
    }
    
    void SetupError(Callback callback)
    {
        ErrorFn = callback;
    }
    
    void LogDebug(const std::string &str)
    {
        if (DebugFn != nullptr)
        {
            DebugFn(str.c_str());
        }
    }
    
    void LogInfo(const std::string &str)
    {
        if (InfoFn != nullptr)
        {
            InfoFn(str.c_str());
        }
    }
    
    void LogError(const std::string &str)
    {
        if (ErrorFn != nullptr)
        {
            ErrorFn(str.c_str());
        }
    }
    
    void* CreateContext(int width, int height)
    {
        Context *ctx = new Context();
        ctx->image = cv::Mat(height, width, CV_8UC3);
        return ctx;
    }
    
    int ImageWidth(const void *ctx_base)
    {
        if (ctx_base == nullptr)
        {
            LogError("[OpenCV native] Context cannot be null");
            return -1;
        }
        
        const Context *ctx = (const Context*) ctx_base;
        return ctx->image.cols;
    }
    
    int ImageHeight(const void *ctx_base)
    {
        if (ctx_base == nullptr)
        {
            LogError("[OpenCV native] Context cannot be null");
            return -1;
        }
        
        const Context *ctx = (const Context*) ctx_base;
        return ctx->image.rows;
    }
    
    int ImageFormat(const void *ctx_base)
    {
        if (ctx_base == nullptr)
        {
            LogError("[OpenCV native] Context cannot be null");
            return -1;
        }
        
        const Context *ctx = (const Context*) ctx_base;
        return ctx->image.type();
    }
    
    int ImageSize(const void *ctx_base)
    {
        if (ctx_base == nullptr)
        {
            LogError("[OpenCV native] Context cannot be null");
            return -1;
        }
        
        const Context *ctx = (const Context*) ctx_base;
        return (int) ctx->image.total();
    }
    
    
    bool CopyFrom(void *ctx_base, const uint8_t *ptr, int size)
    {
        if (ctx_base == nullptr)
        {
            LogError("[OpenCV native] Context cannot be null");
            return false;
        }
        
        if (size < 0)
        {
            LogError("[OpenCV native] Invalid array size");
            return false;
        }
        
        Context *ctx = (Context*) ctx_base;
        if (ctx->image.total() > size)
        {
            LogError("[OpenCV native] Image size is too small. Given: " + std::to_string(size) +
                     ", needed: " + std::to_string(ctx->image.total()));
            return false;
        }
        
        memcpy(ctx->image.ptr(), ptr, ctx->image.total());
        return true;
    }
    
    bool CopyTo(const void *ctx_base, uint8_t *ptr, int size)
    {
        if (ctx_base == nullptr)
        {
            LogError("[OpenCV native] Context cannot be null");
            return false;
        }
        
        if (size < 0)
        {
            LogError("[OpenCV native] Invalid array size");
            return false;
        }
        
        const Context *ctx = (const Context*) ctx_base;
        if (ctx->image.total() > size)
        {
            LogError("[OpenCV native] Image size is too small. Given: " + std::to_string(size) +
                     ", needed: " + std::to_string(ctx->image.total()));
            return false;
        }
        
        memcpy(ptr, ctx->image.ptr(), ctx->image.total());
        return true;
    }
    
    void *GetImagePtr(void *ctx_base)
    {
        if (ctx_base == nullptr)
        {
            LogError("[OpenCV native] Context cannot be null");
            return nullptr;
        }
        
        Context *ctx = (Context*) ctx_base;
        if (ctx->image.empty())
        {
            LogError("[OpenCV native] Image is empty");
            return nullptr;
        }

        return ctx->image.ptr();
    }

    
    bool SetDictionary(void *ctx_base, int value)
    {
        if (ctx_base == nullptr)
        {
            LogError("[OpenCV native] Context cannot be null");
            return false;
        }
        
        Context *ctx = (Context*) ctx_base;
        ctx->dictionary = cv::aruco::getPredefinedDictionary(value);
        return true;
    }
    
    bool DetectArucoMarkers(void *ctx_base)
    {
        if (ctx_base == nullptr)
        {
            LogError("[OpenCV native] Context cannot be null");
            return false;
        }
        
        Context *ctx = (Context*) ctx_base;

        ctx->markerCorners.clear();
        ctx->markerIds.clear();

        if (ctx->image.empty())
        {
            LogError("[OpenCV native] Image is empty");
            return false;
        }

        if (ctx->dictionary == nullptr)
        {
            LogError("[OpenCV native] Dictionary is empty");
            return false;
        }
        
        std::vector<std::vector<cv::Point2f>> rejectedCandidates;
        if (ctx->parameters == nullptr)
        {
            ctx->parameters = cv::aruco::DetectorParameters::create();
        }
        
        cv::aruco::detectMarkers(ctx->image, ctx->dictionary, ctx->markerCorners,
                                 ctx->markerIds, ctx->parameters, rejectedCandidates);
        return true;
    }
    
    int GetNumDetectedArucoMarkers(const void *ctx_base)
    {
        if (ctx_base == nullptr)
        {
            LogError("[OpenCV native] Context cannot be null");
            return -1;
        }
        
        const Context *ctx = (const Context*) ctx_base;
        return (int) ctx->markerIds.size();
    }
    
    bool GetArucoMarkerIds(const void *ctx_base, int *arrayPtr, int arraySize)
    {
        if (ctx_base == nullptr)
        {
            LogError("[OpenCV native] Context cannot be null");
            return false;
        }
        
        const Context *ctx = (const Context*) ctx_base;
        if (arraySize < ctx->markerIds.size())
        {
            LogError("[OpenCV native] Array size is too small. Given: " + std::to_string(arraySize) +
                     ", needed: " + std::to_string(ctx->markerIds.size()));
            return false;
        }
        
        for (int i = 0; i < ctx->markerIds.size(); i++)
        {
            arrayPtr[i] = ctx->markerIds[i];
            
        }
        
        return true;
    }
    
    bool GetArucoMarkerCorners(const void *ctx_base, float *arrayPtr, int arraySize)
    {
        if (ctx_base == nullptr)
        {
            LogError("[OpenCV native] Context cannot be null");
            return false;
        }
        
        const Context *ctx = (const Context*) ctx_base;
        if (arraySize < 8 * ctx->markerCorners.size())
        {
            LogError("[OpenCV native] Array size is too small. Given: " + std::to_string(arraySize) +
                     ", needed: " + std::to_string(8 * ctx->markerIds.size()));
            return false;
        }
        
        for (int i = 0; i < ctx->markerCorners.size(); i++)
        {
            const auto &corner = ctx->markerCorners[i];
            arrayPtr[8 * i + 0] = corner[0].x;
            arrayPtr[8 * i + 1] = corner[0].y;
            arrayPtr[8 * i + 2] = corner[1].x;
            arrayPtr[8 * i + 3] = corner[1].y;
            arrayPtr[8 * i + 4] = corner[2].x;
            arrayPtr[8 * i + 5] = corner[2].y;
            arrayPtr[8 * i + 6] = corner[3].x;
            arrayPtr[8 * i + 7] = corner[3].y;
        }
        
        return true;
    }
    
    bool SetCameraMatrix(void *ctx_base, float *arrayPtr, int arraySize)
    {
        if (ctx_base == nullptr)
        {
            LogError("[OpenCV native] Context cannot be null");
            return false;
        }
        
        if (arraySize < 6)
        {
            LogError("[OpenCV native] Array size is too small. Given: " + std::to_string(arraySize) +
                     ", needed: 6");
            return false;
        }

        Context *ctx = (Context*) ctx_base;
        if (ctx->cameraMatrix.empty())
        {
            ctx->cameraMatrix = cv::Mat(3, 3, CV_32F);
        }

        ctx->cameraMatrix.at<float>(0, 0) = arrayPtr[0];
        ctx->cameraMatrix.at<float>(0, 1) = arrayPtr[1];
        ctx->cameraMatrix.at<float>(0, 2) = arrayPtr[2];
        ctx->cameraMatrix.at<float>(1, 0) = arrayPtr[3];
        ctx->cameraMatrix.at<float>(1, 1) = arrayPtr[4];
        ctx->cameraMatrix.at<float>(1, 2) = arrayPtr[5];
        ctx->cameraMatrix.at<float>(2, 0) = 0;
        ctx->cameraMatrix.at<float>(2, 1) = 0;
        ctx->cameraMatrix.at<float>(2, 2) = 1;
        return true;
    }
    
    bool EstimateArucoPose(const void *ctx_base, float markerSize, int* markerIndices, int markerIndicesLength,
                           float *rotations, int rotationsSize, float *translations, int translationsSize)
    {
        if (ctx_base == nullptr)
        {
            LogError("[OpenCV native] Context cannot be null");
            return false;
        }
        
        if (markerSize < 0)
        {
            LogError("[OpenCV native] Invalid marker size");
            return false;
        }

        if (markerSize == 0)
        {
            return true;
        }

        
        const Context *ctx = (const Context*) ctx_base;

        if (ctx->cameraMatrix.empty())
        {
            LogError("[OpenCV native] Camera matrix has not been set");
            return false;
        }

        std::vector<std::vector<cv::Point2f>> markerCorners;
        for (int i = 0; i < markerIndicesLength; i++)
        {
            int markerIndex = markerIndices[i];
            if (markerIndex < 0 || markerIndex >= ctx->markerCorners.size())
            {
                LogError("[OpenCV native] Index at position " + std::to_string(i) + " points to marker " +
                         std::to_string(markerIndex) + " which is out of range");
                return false;
            }
            
            markerCorners.push_back(ctx->markerCorners[markerIndex]);
        }
        
        std::vector<cv::Vec3d> rvecs, tvecs;
        cv::aruco::estimatePoseSingleMarkers(markerCorners, markerSize, ctx->cameraMatrix, distCoeffs, rvecs, tvecs);

        if (rotationsSize < rvecs.size() * 3)
        {
            LogError("[OpenCV native] Rotatino array size is too small. Given: " + std::to_string(rotationsSize) +
                     ", needed: " + std::to_string(rvecs.size() * 3));
            return false;
        }

        if (translationsSize < tvecs.size() * 3)
        {
            LogError("[OpenCV native] Rotation array size is too small. Given: " + std::to_string(translationsSize) +
                     ", needed: " + std::to_string(tvecs.size() * 3));
            return false;
        }
        
        for (int i = 0; i < rvecs.size(); i++)
        {
            rotations[i * 3 + 0] = (float) rvecs[i][0];
            rotations[i * 3 + 1] = (float) rvecs[i][1];
            rotations[i * 3 + 2] = (float) rvecs[i][2];
        }

        for (int i = 0; i < tvecs.size(); i++)
        {
            translations[i * 3 + 0] = (float) tvecs[i][0];
            translations[i * 3 + 1] = (float) tvecs[i][1];
            translations[i * 3 + 2] = (float) tvecs[i][2];
        }
        
        return true;
    }
    
    
    void DisposeContext(void *ctx_base)
    {
        Context *ctx = (Context*) ctx_base;
        delete ctx;
    }
    
}





