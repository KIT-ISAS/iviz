#import <ARKit/ARKit.h>

// Taken from https://github.com/Unity-Technologies/arfoundation-samples/issues/615

typedef struct UnityXRNativeSessionPtr
{
    int version;
    void* session;
} UnityXRNativeSessionPtr;

extern "C" struct UnityDepthTextureHandles
{
    void* textureDepth;
    double depthTimestamp;
    int width;
    int height;
};

static double s_DepthTimestamp = 0.0;
static id <MTLTexture> s_CapturedDepthImageTexture = NULL;
static int s_width = 0;
static int s_height = 0;

static id <MTLDevice> _device = NULL;

static CVMetalTextureCacheRef _textureCache;

extern "C"
{
    UnityDepthTextureHandles UnityGetDepthMap(UnityXRNativeSessionPtr* nativeSession)
    {
        if(_device == NULL)
        {
            _device = MTLCreateSystemDefaultDevice();
            CVMetalTextureCacheCreate(NULL, NULL, _device, NULL, &_textureCache);
        }

        ARSession* session = (__bridge ARSession*)nativeSession->session;
        ARFrame* frame = session.currentFrame;

        if (frame.sceneDepth != NULL)
        {
            //double newTimeStamp = frame.capturedDepthDataTimestamp;
            
            //if(newTimeStamp > s_DepthTimestamp)
            {
                id<MTLTexture> textureDepth = nil;
                
                //NSLog(@"depth time is %.2f", s_DepthTimestamp);

                CVPixelBufferRef pixelBuffer = frame.sceneDepth.depthMap;

                size_t depthWidth = CVPixelBufferGetWidth(pixelBuffer);
                size_t depthHeight = CVPixelBufferGetHeight(pixelBuffer);

                if(depthWidth != 0 && depthHeight != 0){
                    
                    MTLPixelFormat pixelFormat = MTLPixelFormatR32Float; // MTLPixelFormatR32Float MTLPixelFormatR16Float;
                    
                    CVMetalTextureRef texture = nullptr;
                    CVReturn status = CVMetalTextureCacheCreateTextureFromImage(nullptr, _textureCache, pixelBuffer, nullptr, pixelFormat, depthWidth, depthHeight, 0, &texture);
                    if(status == kCVReturnSuccess)
                    {
                        textureDepth = CVMetalTextureGetTexture(texture);
                        CFRelease(texture);
                    }
                    
                    if (textureDepth != nil) {
                        s_CapturedDepthImageTexture = textureDepth;
                        s_DepthTimestamp = 0;
                        s_width = (int)depthWidth;
                        s_height = (int)depthHeight;
                    }
                }
            }
        }

        UnityDepthTextureHandles handles;

        handles.textureDepth = (__bridge_retained void*)s_CapturedDepthImageTexture;

        handles.depthTimestamp = s_DepthTimestamp;

        handles.width = s_width;
        handles.height = s_height;

        return handles;
    }

    void ReleaseDepthTextureHandles(UnityDepthTextureHandles handles)
    {
        if (handles.textureDepth != nullptr)
        {
            CFRelease(handles.textureDepth);
            handles.textureDepth = nullptr;
        }
    }

    void UnityUnloadMetalCache()
    {
        if (_textureCache != nullptr) {
            CFRelease(_textureCache);
            _textureCache = nullptr;
        }
        _device = nullptr;
    }
}
