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
#include <opencv2/objdetect.hpp>
#include <opencv2/imgproc.hpp>
#include <opencv2/calib3d.hpp>
#include <Eigen/Geometry>

struct Context
{
    cv::Mat image;
    cv::Ptr<cv::aruco::Dictionary> dictionary;
    cv::Ptr<cv::aruco::DetectorParameters> parameters;
    std::vector<int> markerIds;
    std::vector<std::vector<cv::Point2f>> markerCorners;
    std::vector<std::string> markerCodes;
    cv::Mat cameraMatrix;
};

extern "C" {
    static IvizCallback DebugFn = nullptr;
    static IvizCallback InfoFn = nullptr;
    static IvizCallback ErrorFn = nullptr;
    
    static const std::vector<float> distCoeffs = { 0, 0, 0, 0, 0 };
    
    
    void IvizSetupDebug(IvizCallback callback)
    {
        DebugFn = callback;
    }
    
    void IvizSetupInfo(IvizCallback callback)
    {
        InfoFn = callback;
    }
    
    void IvizSetupError(IvizCallback callback)
    {
        ErrorFn = callback;
    }
    
    static void LogDebug(const std::string &str)
    {
        if (DebugFn != nullptr)
        {
            DebugFn(str.c_str());
        }
    }
    
    static void LogInfo(const std::string &str)
    {
        if (InfoFn != nullptr)
        {
            InfoFn(str.c_str());
        }
    }
    
    static void LogError(const std::string &str)
    {
        if (ErrorFn != nullptr)
        {
            ErrorFn(str.c_str());
        }
    }
    
    void* IvizCreateContext(int width, int height)
    {
        Context *ctx = new Context();
        ctx->image = cv::Mat(height, width, CV_8UC3);
        return ctx;
    }
    
    int IvizImageWidth(const void *ctx_base)
    {
        if (ctx_base == nullptr)
        {
            LogError("[OpenCV native] Context cannot be null");
            return -1;
        }
        
        const Context *ctx = (const Context*) ctx_base;
        return ctx->image.cols;
    }
    
    int IvizImageHeight(const void *ctx_base)
    {
        if (ctx_base == nullptr)
        {
            LogError("[OpenCV native] Context cannot be null");
            return -1;
        }
        
        const Context *ctx = (const Context*) ctx_base;
        return ctx->image.rows;
    }
    
    int IvizImageFormat(const void *ctx_base)
    {
        if (ctx_base == nullptr)
        {
            LogError("[OpenCV native] Context cannot be null");
            return -1;
        }
        
        const Context *ctx = (const Context*) ctx_base;
        return ctx->image.type();
    }
    
    int IvizImageSize(const void *ctx_base)
    {
        if (ctx_base == nullptr)
        {
            LogError("[OpenCV native] Context cannot be null");
            return -1;
        }
        
        const Context *ctx = (const Context*) ctx_base;
        return (int) ctx->image.total();
    }
    
    
    bool IvizCopyFrom(void *ctx_base, const uint8_t *ptr, int size)
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
    
    bool IvizCopyTo(const void *ctx_base, uint8_t *ptr, int size)
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
    
    void *IvizGetImagePtr(void *ctx_base)
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

    
    bool IvizSetDictionary(void *ctx_base, int value)
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
    
    bool IvizDetectArucoMarkers(void *ctx_base)
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
            ctx->parameters->cornerRefinementMethod = cv::aruco::CORNER_REFINE_CONTOUR;
        }
        
        cv::aruco::detectMarkers(ctx->image, ctx->dictionary, ctx->markerCorners,
                                 ctx->markerIds, ctx->parameters, rejectedCandidates);
        
        return true;
    }
    
    bool IvizDetectQrMarkers(void *ctx_base)
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
        
        cv::QRCodeDetector qrcode;
        std::vector<cv::Point2f> corners;
        
        ctx->markerCodes.clear();
        qrcode.detectAndDecodeMulti(ctx->image, ctx->markerCodes, corners);
        
        ctx->markerCorners.clear();
        for (int i = 0; i < corners.size(); i += 4)
        {
            std::vector<cv::Point2f> subCorners;
            subCorners.push_back(corners[i]);
            subCorners.push_back(corners[i+1]);
            subCorners.push_back(corners[i+2]);
            subCorners.push_back(corners[i+3]);
            ctx->markerCorners.push_back(subCorners);
        }
        
        if (ctx->markerCorners.size() != ctx->markerCodes.size())
        {
            LogError("[OpenCV native] Inconsistent sizes: " + std::to_string(ctx->markerCorners.size()) +
                     " and " + std::to_string(ctx->markerCodes.size()));
        }
        
        return true;
    }
    
    int IvizGetNumDetectedMarkers(const void *ctx_base)
    {
        if (ctx_base == nullptr)
        {
            LogError("[OpenCV native] Context cannot be null");
            return -1;
        }
        
        const Context *ctx = (const Context*) ctx_base;
        return (int) ctx->markerCorners.size();
    }
    
    bool IvizGetArucoMarkerIds(const void *ctx_base, int *arrayPtr, int arraySize)
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
    
    bool IvizGetQrMarkerCodes(const void *ctx_base, const void **arrayPtr, int *arrayLengths, int arraySize)
    {
        if (ctx_base == nullptr)
        {
            LogError("[OpenCV native] Context cannot be null");
            return false;
        }
        
        const Context *ctx = (const Context*) ctx_base;
        if (arraySize < ctx->markerCodes.size())
        {
            LogError("[OpenCV native] Array size is too small. Given: " + std::to_string(arraySize) +
                     ", needed: " + std::to_string(ctx->markerCodes.size()));
            return false;
        }
        
        for (int i = 0; i < ctx->markerCodes.size(); i++)
        {
            arrayPtr[i] = ctx->markerCodes[i].c_str();
            arrayLengths[i] = (int) ctx->markerCodes[i].length();
        }
        
        return true;
    }
    
    
    bool IvizGetMarkerCorners(const void *ctx_base, float *arrayPtr, int arraySize)
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
    
    bool IvizSetCameraMatrix(void *ctx_base, float *arrayPtr, int arraySize)
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
    
    bool IvizEstimateMarkerPoses(const void *ctx_base, float markerSize, float *rotations, int rotationsSize, float *translations, int translationsSize)
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

        std::vector<cv::Vec3d> rvecs, tvecs;
        cv::aruco::estimatePoseSingleMarkers(ctx->markerCorners, markerSize, ctx->cameraMatrix, distCoeffs, rvecs, tvecs);

        if (rotationsSize < rvecs.size() * 3)
        {
            LogError("[OpenCV native] Rotation array size is too small. Given: " + std::to_string(rotationsSize) +
                     ", needed: " + std::to_string(rvecs.size() * 3));
            return false;
        }

        if (translationsSize < tvecs.size() * 3)
        {
            LogError("[OpenCV native] Translation array size is too small. Given: " + std::to_string(translationsSize) +
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
    
    
    void IvizDisposeContext(void *ctx_base)
    {
        Context *ctx = (Context*) ctx_base;
        delete ctx;
    }

    bool IvizEstimatePnp(const float *inputs, int inputSize, const float *outputs, int outputSize, float *cameraArray, int cameraArraySize, float *result, int resultSize)
    {
        if (inputSize % 2 != 0)
        {
            LogError("[OpenCV native] Invalid inputSize");
            return false;
        }
        
        if (outputSize % 3 != 0)
        {
            LogError("[OpenCV native] Invalid outputSize");
            return false;
        }

        if (outputSize * 2 != inputSize * 3)
        {
            LogError("[OpenCV native] outputSize and inputSize do not match");
            return false;
        }

        
        if (cameraArraySize < 6)
        {
            LogError("[OpenCV native] Invalid cameraArraySize");
            return false;
        }

        if (resultSize < 6)
        {
            LogError("[OpenCV native] Invalid resultSize");
            return false;
        }

        cv::Mat input(inputSize / 2, 2, CV_32F);
        for (int i = 0; i < inputSize / 2; i++)
        {
            input.at<float>(i, 0) = inputs[2 * i];
            input.at<float>(i, 1) = inputs[2 * i + 1];
        }
        
        cv::Mat output(outputSize / 3, 3, CV_32F);
        for (int i = 0; i < outputSize / 3; i++)
        {
            output.at<float>(i, 0) = outputs[3 * i];
            output.at<float>(i, 1) = outputs[3 * i + 1];
            output.at<float>(i, 2) = outputs[3 * i + 2];
        }
        
        cv::Mat cameraMatrix(3, 3, CV_32F);
    
        cameraMatrix.at<float>(0, 0) = cameraArray[0];
        cameraMatrix.at<float>(0, 1) = cameraArray[1];
        cameraMatrix.at<float>(0, 2) = cameraArray[2];
        cameraMatrix.at<float>(1, 0) = cameraArray[3];
        cameraMatrix.at<float>(1, 1) = cameraArray[4];
        cameraMatrix.at<float>(1, 2) = cameraArray[5];
        cameraMatrix.at<float>(2, 0) = 0;
        cameraMatrix.at<float>(2, 1) = 0;
        cameraMatrix.at<float>(2, 2) = 1;

        cv::Mat distCoeffs = cv::Mat::zeros(1, 5, CV_32F);
        
        cv::Mat rvec = cv::Mat(3, 1, CV_64F);
        cv::Mat tvec = cv::Mat(3, 1, CV_64F);
        
        bool success = cv::solvePnP(output, input, cameraMatrix, distCoeffs, rvec, tvec, false, cv::SOLVEPNP_IPPE_SQUARE);
        if (!success)
        {
            LogInfo("[OpenCV native] cv::solvePnp failed");
            return false;
        }
        
        result[0] = (float) rvec.at<double>(0);
        result[1] = (float) rvec.at<double>(1);
        result[2] = (float) rvec.at<double>(2);
        result[3] = (float) tvec.at<double>(0);
        result[4] = (float) tvec.at<double>(1);
        result[5] = (float) tvec.at<double>(2);

        return true;

    }
    
}





